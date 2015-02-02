
using System;
using System.Runtime.InteropServices;

namespace GS.SCard
{
   
  


    //holds info about the request made by the reader
    //32 bit boundary
    //api expects this kind of layout 
    [StructLayout(LayoutKind.Sequential)]
    public struct SCARD_IO_REQUEST
    {
        //exact name of C++ struct
        
        public UInt32 dwProtocol;

        
        public UInt32 cbPciLength;
    }

   //sequential , this time we a string member
    
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SCARD_READERSTATE
    {
        //char* to the name of the scanner being monitered
        //string is marshalled as mutiple byte char*
        public string m_szReader;

        //user data
        public IntPtr m_pvUserData;

        
       
       //current state of the reader as bitmask
        public UInt32 m_dwCurrentState;

        //state of reader according to smart card resource manager
        public UInt32 m_dwEventState;

        
        
        //size in bytes of return ATR
        public UInt32 m_cbAtr;

      
        // ATR of the inserted card, with extra alignment bytes.
        //
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_rgbAtr;
    }

}
