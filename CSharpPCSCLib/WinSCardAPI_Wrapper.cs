
using System;
using System.Runtime.InteropServices;

namespace GS.SCard
{
    //some of the WinScard API being implemented

    partial class WinSCardAPIWrapper
    {
        //entering an unmanaged code block
        #region Native WinScard API Wrapper

       
        [DllImport("winscard.dll")]
        public static extern int SCardEstablishContext(  uint dwScope, 
                                                         IntPtr pvReserved1, 
                                                         IntPtr pvReserved2,
                                                         out IntPtr phContext );

       // prototype in C# to let us use the 
        //native api call
        [DllImport("WinScard.dll")]
        public static extern int SCardReleaseContext( IntPtr hContext );


        //out symbol
        [DllImport("WinScard.dll")]
        public static extern int SCardConnect( IntPtr hContext,
                                               string cReaderName,
                                               uint dwShareMode,
                                               uint dwPrefProtocol,
                                               out IntPtr phCard,
                                               out uint pdwActiveProtocol );

       
        [DllImport("WinScard.dll")]
        public static extern int SCardDisconnect( IntPtr hCard, uint dwDisposition );


        
        [DllImport("WinScard.dll")]
        public static extern int SCardReconnect( IntPtr hCard,
                                                 uint dwShareMode,
                                                 uint dwPrefProtocol,
                                                 uint dwInitialization,
                                                 out uint pdwActiveProtocol );

        
        [DllImport( "WinScard.dll" )]
        public static extern int SCardListReaders( IntPtr hContext,
                                                   string mszGroups,
                                                   [MarshalAs( UnmanagedType.LPArray,
                                                   ArraySubType = UnmanagedType.LPWStr )] 
                                                   byte[] mszReaders,
                                                   ref int pcchReaders );

        
      
        [DllImport( "WinScard.DLL" )]
        public static extern int SCardGetStatusChange( IntPtr hContext,
                                                       uint dwTimeout,
                                                       [In, Out] SCARD_READERSTATE[] rgReaderStates,
                                                       uint cReaders );


        
        [DllImport( "WinScard.dll" )]
        public static extern int SCardTransmit( IntPtr hCard,
                                                ref SCARD_IO_REQUEST pioSendPci,
                                                byte[] pbSendBuffer,
                                                int cbSendLength,
                                                IntPtr pioRecvPci,
                                                byte[] pbRecvBuffer,
                                                ref int pcbRecvLength );






       
        [DllImport( "WinScard.dll" )]
        public static extern int SCardControl( IntPtr hCard,
                                               uint dwControlCode,
                                               byte[] lpInBuffer,
                                               int nInBufferSize,
                                               byte[] lpOutBuffer,
                                               int nOutBufferSize,
                                               ref int lpBytesReturned );




       
        [DllImport("WinScard.dll")]
        public static extern int SCardFreeMemory( IntPtr hContext, IntPtr pvMem );


        
        [DllImport("winscard.dll", SetLastError = true)]
        public static extern int SCardGetAttrib( IntPtr hContext,
                                                 uint dwAttrId,
                                                 [Out] byte[] resultBuffer,
                                                 ref int resultLength);



        #endregion
        

    }
}