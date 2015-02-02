
using System;
using GS.SCard;
using GS.SCard.Const;

namespace GS.PCSC
{
    public partial class PCSCReader
    {
       
        // Stores the actual smartcard reader name.
        // this should be omnikey when I run it
        private string readerName;

        
        // WinScard Functions.
        //scard
        public GS.SCard.WinSCard SCard;


        /// <summary>
        /// Initializes a new instance of the <see cref="PCSCReader"/> class.
        /// </summary>
        public PCSCReader()
        {
            this.SCard = new GS.SCard.WinSCard();
            readerName = null;
        }

        /// <summary>
        /// Establishes the smart card resourete manager context and provides the list of readers.
        /// </summary>
        public void Connect()
        {
            Connect( null );
        }

        /// <summary>
        /// Establishes the smart card resourete manager context and provides the list of readers.
        /// </summary>
        /// <param name="dwScope">Scope of the resource manager context.</param>
        public void Connect(SCARD_SCOPE dwScope)
        {
            Connect(null, dwScope);
        }

        /// <summary>
        /// Establishes the smart card resourete manager context and selectes the specified reader.
        /// </summary>
        /// <param name="szReader">
        /// The name of the reader that contains the target card.
        /// </param>
        public void Connect( string szReader )
        {
            Connect( szReader, SCARD_SCOPE.System );
        }

        /// <summary>
        /// Establishes the smart card resourete manager context and selectes the specified reader.
        /// </summary>
        /// <param name="szReader">
        /// The name of the reader that contains the target card.
        /// </param>
        /// <param name="dwScope">Scope of the resource manager context.</param>
        public void Connect(string szReader, SCARD_SCOPE dwScope)
        {
            try
            {
                this.SCard.EstablishContext( dwScope );

                if (!string.IsNullOrEmpty( szReader ))
                {
                    this.readerName = szReader;
                    return;
                }

                this.SCard.ListReaders();

                if (this.SCard.ReaderNames.Length == 1)
                {
                    this.readerName = this.SCard.ReaderNames[0];
                    return;
                }

                Console.WriteLine( "------------------------------------------------------" );
                Console.WriteLine( "Available PC/SC Readers:");
                Console.WriteLine( "------------------------------------------------------" );

                for (int i = 0; i < this.SCard.ReaderNames.Length; i++)
                {
                    Console.WriteLine( string.Format( "Reader {0:#0}: {1}", i, this.SCard.ReaderNames[i] ) );
                }
                Console.WriteLine( "" );
                Console.Write( "Please select a reader (0..n):  " );
                this.readerName = this.SCard.ReaderNames[int.Parse( Console.ReadLine() )] ;
            }
            catch (WinSCardException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Activates the card.
        /// </summary>
        public void ActivateCard()
        {
            ActivateCard(SCARD_SHARE_MODE.Exclusive, SCARD_PROTOCOL.Tx); 
        }

        /// <summary>
        /// Activates the card.
        /// </summary>
        /// <param name="dwPrefProtocol">
        /// A bitmask of acceptable protocols for the connection. Possible values may be combined with the OR operation.
        /// </param>
        public void ActivateCard(SCARD_PROTOCOL dwPrefProtocol)
        {
            ActivateCard( SCARD_SHARE_MODE.Exclusive, dwPrefProtocol );
        }

        /// <summary>
        /// Activates the card.
        /// </summary>
        /// <param name="dwShareMode">
        /// A flag that indicates whether other applications may form connections to the card.
        /// </param>
        /// <param name="dwPrefProtocol">
        /// A bitmask of acceptable protocols for the connection. Possible values may be combined with the OR operation.
        /// </param>
        public void ActivateCard(SCARD_SHARE_MODE dwShareMode, SCARD_PROTOCOL dwPrefProtocol)
        {
            try
            {
                this.SCard.WaitForCardPresent(this.readerName);
                                
                this.SCard.Connect( this.readerName, dwShareMode, dwPrefProtocol );
            }
            catch (WinSCardException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Disconnects the an established connection to a smart card and closes 
        /// an established resource manager context, freeing any resources allocated 
        /// under that context.
        /// </summary>
        public void Disconnect()
        {
            Disconnect(SCARD_DISCONNECT.Unpower);
        }

        /// <summary>
        /// Disconnects the an established connection to a smart card and closes 
        /// an established resource manager context, freeing any resources allocated 
        /// under that context.
        /// </summary>
        /// <param name="disposition">Action to take on the card in the connected reader on close.</param>
        public void Disconnect(SCARD_DISCONNECT disposition)
        {
            try
            {
                this.SCard.Disconnect(disposition);
                this.SCard.ReleaseContext();
                this.readerName = null;
            }
            catch (WinSCardException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
