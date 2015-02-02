
using System;
using System.Diagnostics;
using GS.Apdu;
using GS.SCard.Const;
using GS.Util.Hex;

namespace GS.SCard  
{
    public partial class WinSCard 
    {
      
        //handler  for resource manager context
        //needs to be passed to other functions attempting to work with in this context
        private IntPtr phContext;

    
        // A pointer that identifies the connection to the smart card.
        private IntPtr phCARD;    

        //this is the list of readers connected to Windows box
        string[] readerStrings;


      
        // The connected Reader Name
        string connectedReaderName;

        //the protocol we are using
        // Indicates the established active protocol.
        uint activeSCardProtocol;


       //debug
        // Indicates if the Trace is enabled or not.
        private bool scardTrace = true;


       //constructor for our object which
        //represents the smart card and the context 
        //in which it operates
        public WinSCard()
		{
            this.phContext = (IntPtr)0;
            this.phCARD = (IntPtr)0;
		}

     
        // The EstablishContext function establishes the smart card resourete manager context.
        // The context is a user context, and any database operations are performed within the
        // domain of the user.
       
        //default everything is local
        //we set the WinSCard instance to work with SCARD_SCOPE.User
        public void EstablishContext()
        {
            EstablishContext(SCARD_SCOPE.User);
        }

       
        // The EstablishContext function establishes the smart card resourete manager context.
        // Scope of the resource manager context.
        //you can also change the Context such that it operates outside of the local context
        public void EstablishContext(SCARD_SCOPE dwScope)
        {
            //returns zero if successful according to 
            int ret = WinSCardAPIWrapper.SCardEstablishContext( (uint)dwScope, IntPtr.Zero, IntPtr.Zero, out phContext );
            
            if(ret == 0)
            {
                
                Trace.WriteLineIf(scardTrace, string.Format("    SCard.EstablishContext({0})", (SCARD_SCOPE)dwScope)); 
            }
            else
            {
                throw new WinSCardException( scardTrace, "SCard.EstablishContext", ret );
            }
        }

       
       
        
    
        // The names of available readers.
        //invokes api method to return a list of readers
        public string[] ListReaders()
        {
            int pcchReaders = 0; 
            readerStrings = null;

            // Get first the mszReaders buffer length
            int ret = WinSCardAPIWrapper.SCardListReaders( phContext, null, null, ref pcchReaders );
           
            if (ret == 0)
            {
                byte[] mszReaders = new byte[pcchReaders];

                // Get list of readers
                ret = WinSCardAPIWrapper.SCardListReaders( phContext, null, mszReaders, ref pcchReaders );
                
                if (ret == 0)
                {
                    Trace.WriteLineIf( scardTrace, "    SCard.ListReaders..." );

                    if (pcchReaders > 2)
                    {
                        readerStrings = System.Text.Encoding.ASCII.GetString( mszReaders ).Substring( 0, pcchReaders - 2 ).Split( '\0' );

                        for (int i = 0; i < readerStrings.Length; i++)
                        {
                            Trace.WriteLineIf( scardTrace, string.Format( "        Reader {0:#0}: {1}", i, readerStrings[i] ) );
                        }
                    }
                }
                return readerStrings;
            }
            else
            {
                throw new WinSCardException( scardTrace, "SCard.ListReaders", ret );
            }
        }

        //function blocks until card is present 
        public void WaitForCardPresent()
        {
            if (string.IsNullOrEmpty(this.ReaderName) == false)
            {
                this.WaitForCardPresent(this.connectedReaderName);
            }
        }

      
        // blocks until someone scans their card
        public void WaitForCardPresent(string szReader)
        {
            int ret;

            SCARD_READERSTATE[] readerStates = new SCARD_READERSTATE[1];
            readerStates[0].m_szReader = szReader;
            readerStates[0].m_dwEventState = (uint)SCARD_CARD_STATE.UNAWARE;
            readerStates[0].m_dwCurrentState = (uint)SCARD_CARD_STATE.UNAWARE;

            ret = WinSCardAPIWrapper.SCardGetStatusChange( phContext, (uint)10, readerStates, (uint)1 );
            if (ret != 0)   throw new WinSCardException( scardTrace, "SCard.Connect", ret );

            if ((readerStates[0].m_dwEventState & (uint)SCARD_CARD_STATE.PRESENT) == (uint)SCARD_CARD_STATE.PRESENT)
            {
                return;
            }
            Trace.WriteLineIf(this.TraceSCard, "    Wait for card present...");
            
            do
            {
                ret = WinSCardAPIWrapper.SCardGetStatusChange(phContext, (uint)10, readerStates, (uint)1);

                if (ret != 0)  throw new WinSCardException( scardTrace, "SCard.Connect", ret );

                //this is our event loop to simulate what would be an action listener
            } while ((readerStates[0].m_dwEventState & (uint)SCARD_CARD_STATE.PRESENT) != (uint)SCARD_CARD_STATE.PRESENT);
        }

        
        // The WaitForCardRemoval function blocks execution until there is no card         
        // present in the previous selected the selected reader.
      

        //waits until there is no more card in the specified reader
        public void WaitForCardRemoval(string szReader)
        {
            int ret;

            SCARD_READERSTATE[] readerStates = new SCARD_READERSTATE[1];
            readerStates[0].m_szReader = szReader;
            readerStates[0].m_dwEventState = (uint)SCARD_CARD_STATE.UNAWARE;
            readerStates[0].m_dwCurrentState = (uint)SCARD_CARD_STATE.UNAWARE;

            ret = WinSCardAPIWrapper.SCardGetStatusChange(phContext, (uint)10, readerStates, (uint)1);
            if (ret != 0) throw new WinSCardException(scardTrace, "SCard.Connect", ret);

            if ((readerStates[0].m_dwEventState & (uint)SCARD_CARD_STATE.EMPTY) == (uint)SCARD_CARD_STATE.EMPTY)
            {
                return;
            }
            Trace.WriteLineIf(this.TraceSCard, "    Wait for card removal...");

            do
            {
                ret = WinSCardAPIWrapper.SCardGetStatusChange(phContext, (uint)10, readerStates, (uint)1);
                if (ret != 0) throw new WinSCardException(scardTrace, "SCard.Connect", ret);
                //this is our event loop to simulate what would be an action listener
            } while ((readerStates[0].m_dwEventState & (uint)SCARD_CARD_STATE.EMPTY) != (uint)SCARD_CARD_STATE.EMPTY);
        }

       //gets the present state of the card reader specifyed
        public bool GetCardPresentState(string szReader)
        {
            int ret;

            SCARD_READERSTATE[] readerStates = new SCARD_READERSTATE[1];
            readerStates[0].m_szReader = szReader;
            readerStates[0].m_dwEventState = (uint)SCARD_CARD_STATE.UNAWARE;
            readerStates[0].m_dwCurrentState = (uint)SCARD_CARD_STATE.UNAWARE;

            ret = WinSCardAPIWrapper.SCardGetStatusChange(phContext, (uint)100, readerStates, (uint)1);
            if (ret != 0) throw new WinSCardException(scardTrace, "SCard.Connect", ret);

            if ((readerStates[0].m_dwEventState & (uint)SCARD_CARD_STATE.PRESENT) == (uint)SCARD_CARD_STATE.PRESENT)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



      
        // The SCardConnect function establishes a connection (using a specific resource manager context) 
        // between the calling application and a smart card contained by a specific reader. 
        // If no card exists in the specified reader, an error is returned.
        // The name of the reader that contains the target card.
     
        public void Connect(string szReader)
        {
            Connect( szReader, SCARD_SHARE_MODE.Shared, SCARD_PROTOCOL.Tx );
        }

       
        // The SCardConnect function establishes a connection (using a specific resource manager context) 
        // between the calling application and a smart card contained by a specific reader. 
        // If no card exists in the specified reader, an error is returned.
   
      
        // The name of the reader that contains the target card.
   
        // <param name="dwShareMode">
        // A flag that indicates whether other applications may form connections to the card.
       
     
        // A bitmask of acceptable protocols for the connection. Possible values may be combined with the OR operation.
    
        public void Connect(string szReader, SCARD_SHARE_MODE dwShareMode, SCARD_PROTOCOL dwPrefProtocol)
        {
            
            int ret = WinSCardAPIWrapper.SCardConnect( phContext, szReader, (uint)dwShareMode, (uint)dwPrefProtocol, out phCARD, out activeSCardProtocol );
            if (ret == 0)
            {   
                connectedReaderName = szReader;
                Trace.WriteLineIf(scardTrace, String.Format("    SCard.Connect({0}, SHARE_MODE.{1}, SCARD_PROTOCOL.{2})",
                                                                  szReader, dwShareMode, (SCARD_PROTOCOL)dwPrefProtocol));
                Trace.WriteLineIf(scardTrace, String.Format("        Active Protocol: SCARD_PROTOCOL.{0} ",(SCARD_PROTOCOL)activeSCardProtocol));
                Trace.WriteLineIf(this.TraceSCard, HexFormatting.Dump("        ATR: 0x", this.Atr, this.Atr.Length, 24));    
            }
            else
            {
                connectedReaderName = null;
                throw new WinSCardException( scardTrace, "SCard.Connect", ret ); 
                //Trace.WriteLineIf(pcscTrace, String.Format("    Error: SCardConnect failed with 0x{0:X8}.", ret));
            }
        }

     
        public void Transmit( byte[] sendBuffer, int sendLength, byte[] responseBuffer, ref int responseLength )
        {
            SCARD_IO_REQUEST SCARD_PCI;
            SCARD_PCI.dwProtocol = (uint)activeSCardProtocol;
            SCARD_PCI.cbPciLength = 8;

            Trace.WriteLineIf(this.scardTrace, HexFormatting.Dump("--> C-APDU: 0x", sendBuffer, sendLength, 16, ValueFormat.HexASCII));

            int ret = WinSCardAPIWrapper.SCardTransmit( phCARD,ref SCARD_PCI, sendBuffer,
                                                     sendLength,
                                                     IntPtr.Zero,
                                                     responseBuffer,
                                                     ref responseLength);

            if (ret == 0)
            {
                RespApdu respApdu = new RespApdu( responseBuffer, responseLength );
                Trace.WriteLineIf(scardTrace, respApdu.ToString());
            }
            else
            {
                throw new WinSCardException( scardTrace, "SCard.Transmit", ret ); 
            }
        }

    
        public void GetAttrib(SCARD_ATTR AttrId)
        {
            byte[] respBuffer = new byte[512];
            int respLength = respBuffer.Length;
            GetAttrib(AttrId, respBuffer, ref respLength);
        }

     
        public void GetAttrib(uint AttrId)
        {
            byte[] respBuffer = new byte[512];
            int respLength = respBuffer.Length;
            GetAttrib(AttrId, respBuffer, ref respLength);
        }

      
        public void GetAttrib(SCARD_ATTR attrId, byte[] responseBuffer, ref int responseLength)
        {
            GetAttrib((uint)attrId, responseBuffer, ref responseLength);
        }

       
        public void GetAttrib(uint attrId, byte[] responseBuffer, ref int responseLength)
        {

            Trace.WriteLineIf(this.scardTrace, string.Format("    SCard.SCardGetAttrib(AttrId: {0})", (SCARD_ATTR)attrId));

            int ret = WinSCardAPIWrapper.SCardGetAttrib(phCARD, (uint)attrId, responseBuffer, ref responseLength);

            if (ret == 0)
            {
                Trace.WriteLineIf(this.scardTrace, HexFormatting.Dump("        Attr. Value: 0x", responseBuffer, responseLength, 16, ValueFormat.HexASCII));
            }
            else
            {
                throw new WinSCardException(scardTrace, "SCard.Control", ret);
            }
        }


        public void Control(uint dwControlCode, byte[] inBuffer, int inBufferLength, byte[] outBuffer, int outBufferSize, ref int bytesReturned)
        {
            Trace.WriteLineIf( this.scardTrace, string.Format( "    SCard.Control (Cntrl Code: 0x{0:X}",dwControlCode ));
            //Trace.WriteLineIf( this.scardTrace, HexFormatting.Dump(string.Format( "--> SCard.Control (Cntrl Code: 0x{0:X} ): 0x", 
            //                                                    dwControlCode ), inBuffer, inBufferLength, 16, ValueFormat.HexASCII ) );

            int ret = WinSCardAPIWrapper.SCardControl( phCARD,
                                                    dwControlCode,
                                                    inBuffer,
                                                    inBufferLength,
                                                    outBuffer,
                                                    outBufferSize,  
                                                    ref  bytesReturned);
            if (ret == 0)
            {
                Trace.WriteLineIf( this.scardTrace, HexFormatting.Dump( "        Value: 0x", outBuffer, bytesReturned, 16, ValueFormat.HexASCII ) );  
            }
            else
            {
                throw new WinSCardException( scardTrace, "SCard.Control", ret ); 
            }
        }

       
        public void Disconnect()
        {
            Disconnect( SCARD_DISCONNECT.Unpower );
        }


     
        public void Disconnect(SCARD_DISCONNECT disposition)
        {
            connectedReaderName = null;

            if (this.phCARD != (IntPtr)0)
            {
                int ret = WinSCardAPIWrapper.SCardDisconnect(phCARD, (uint)disposition);
                this.phCARD = (IntPtr)0;

                this.readerStrings = null;
                if (ret == 0)
                {
                    Trace.WriteLineIf(scardTrace, String.Format("    SCard.Disconnect(SCARD_DISCONNECT.{0})...", disposition));
                }
                else
                {
                    throw new WinSCardException( scardTrace, "SCard.Disconnect", ret );
                }
            }
        }

    
        // The SCardReconnect function reestablishes an existing connection between the calling application and 
        // a smart card. This function moves a card handle from direct access to general access, or acknowledges 
        // and clears an error condition that is preventing further access to the card.
   
        public void Reconnect()
        {
            Reconnect( SCARD_SHARE_MODE.Exclusive, SCARD_PROTOCOL.Tx, SCARD_DISCONNECT.Unpower );
        }

      
        public void Reconnect(SCARD_DISCONNECT disconnectAction)
        {
            Reconnect(SCARD_SHARE_MODE.Exclusive, SCARD_PROTOCOL.Tx, disconnectAction);
        }

       
        public void Reconnect(SCARD_SHARE_MODE dwShareMode, SCARD_PROTOCOL dwPrefProtocol, SCARD_DISCONNECT disconnectAction)
        {


            int ret = WinSCardAPIWrapper.SCardReconnect( phCARD, (uint)dwShareMode, (uint)dwPrefProtocol,
                                                       (uint)disconnectAction, out activeSCardProtocol);
            if (ret == 0)
            {
                Trace.WriteLineIf(scardTrace, String.Format("    SCard.Reconnect(SHARE_MODE.{0}, SCARD_PROTOCOL.{1}, SCARD_DISCONNECT.{2} )",
                                                                 dwShareMode, (SCARD_PROTOCOL)dwPrefProtocol, (SCARD_DISCONNECT)disconnectAction));
                Trace.WriteLineIf(scardTrace, String.Format("        Active Protocol: SCARD_PROTOCOL.{0} ", (SCARD_PROTOCOL)activeSCardProtocol));
                Trace.WriteLineIf(this.TraceSCard, HexFormatting.Dump("        ATR: 0x",this.Atr,this.Atr.Length,24));                
                
            }
            else
            {
                throw new WinSCardException(scardTrace, "SCard.Reconnect", ret);
            }
        }

       
        public void ReleaseContext()
        {
            connectedReaderName = null;
            

            if (this.phContext != (IntPtr)0)
            {
                int ret = WinSCardAPIWrapper.SCardReleaseContext( phContext );
                this.phContext = (IntPtr)0;

                if (ret == 0)
                {
                    Trace.WriteLineIf( scardTrace, "    SCard.ReleaseContext..." );
                }
                else
                {
                    throw new WinSCardException( scardTrace, "SCard.ReleaseContext", ret );
                }
            }
        }

        
        /// This function implements the functionality of the SCARD_CTL_CODE Macro (WinSmCrd.h). 
    
      
    
        // The WinSCardControl dwControlCode.
   
        public int GetSCardCtlCode(int code)
        {
            const int FILE_DEVICE_SMARTCARD = 0x00000031;
            const int METHOD_BUFFERED = 0;
            const int FILE_ANY_ACCESS = 0;

            return ((FILE_DEVICE_SMARTCARD) << 16) | ((FILE_ANY_ACCESS) << 14) | ((code) << 2) | (METHOD_BUFFERED);
        }
    }
}