
using System;
using GS.SCard.Const;
using GS.Util.Hex;

namespace GS.SCard
{
    
    // Smart Card and Reader Access Functions
    
    public partial class WinSCard 
    {
       
        public bool TraceSCard
        {
            get { return scardTrace; }
            set { scardTrace = value; }
        }

        private SCARD_PROTOCOL SCardProtocol
        {
            get { return (SCARD_PROTOCOL)activeSCardProtocol; }
        }

       
        public bool IsRMContextEstablished
        {
            get
            {
                if (phContext == (IntPtr)0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

       
        public bool IsCardContextEstablished
        {
            get
            {
                if (phCARD == (IntPtr)0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

     
        public bool IsCardPresent
        {
            get
            {
                if (string.IsNullOrEmpty(this.ReaderName) == false)
                {
                    return this.GetCardPresentState(this.connectedReaderName);
                }
                else
                {
                    return false;
                }
            }
        }


        
        public byte[] Atr
        {
            get
            {
                if (phCARD != (IntPtr)0)
                {
                    byte[] result = new byte[256];
                    int resultLength = result.Length;

                    int ret = WinSCardAPIWrapper.SCardGetAttrib( phCARD,
                                                 (uint)SCARD_ATTR.ATR_STRING,
                                                 result,
                                                 ref resultLength);

                    if (resultLength > 0)
                    {
                        byte[] baATR = new byte[resultLength];
                        Array.Copy(result, baATR, resultLength);
                        return baATR;
                    }
                }
                return null;
            }
        }


       //answer to reset string
        public string AtrString
        {
            get
            {
                byte[] baATR = Atr;

                if (baATR != null)
                {
                    return HexFormatting.ToHexString(baATR, true);
                }
                else
                {
                    return null;
                }
            }
        }

     

        //abstracting the native api call which returns a list of readers
        public string[] ReaderNames
        {
            get
            {
                return readerStrings;
            }
        }

        //the currently selected reader
        public string ReaderName
        {
            get
            {
                return this.connectedReaderName;
            }
        }

        
  
    }
}
