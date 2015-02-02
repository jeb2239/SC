
namespace GS.SCard.Const
{
    /// <summary>
    /// Defines for the scope input parameter of SCardEstablishContext.  
    /// Generally, only User should be used.
    /// </summary>
    public enum SCARD_SCOPE : uint
    {
        /// <summary>
        /// The context is a user context, and any database operations are performed within the
        /// domain of the user.
        /// </summary>
        User = 0,

        /// <summary>
        /// The context is that of the current terminal, and any database operations are performed
        /// within the domain of that terminal.  (The calling application must have appropriate
        /// access permissions for any database actions.)
        /// </summary>
        Terminal = 1,

        /// <summary>
        /// The context is the system context, and any database operations are performed within the
        /// domain of the system.  (The calling application must have appropriate access
        /// permissions for any database actions.)
        /// </summary>
        System = 2
    }

    /// <summary>
    /// Defines for the scope input parameter of SCardEstablishContext.  
    /// Generally, only User should be used.
    /// </summary>
    public enum SCARD_SHARE_MODE : uint
    {
        /// <summary>
        /// This application is willing to share the card with other applications.
        /// </summary>
        Exclusive = 1,

        /// <summary>
        /// This application is not willing to share the card with other applications.
        /// </summary>
        Shared = 2,

        /// <summary>
        /// This application is allocating the reader for its private use, and will be controlling it directly.
        /// No other applications are allowed access to it.
        /// </summary>
        Direct = 3
    }

    /// <summary>
    /// The Smart Card Protocol.
    /// </summary>
    public enum SCARD_PROTOCOL : uint
    {
        /// <summary>
        /// There is no active protocol.
        /// </summary>
        Undefined = 0x00000000,

        /// <summary>
        /// T=0 is the active protocol.
        /// </summary>
        T0 = 0x00000001,

        /// <summary>
        /// T=1 is the active protocol.
        /// </summary>
        T1 = 0x00000002,

        /// <summary>
        /// Raw is the active protocol.
        /// </summary>
        Raw = 0x00010000,


        /// <summary>
        /// Use implicit Protocol Type Selection (PST)
        /// </summary>
        Default = 0x80000000,

        /// <summary>
        /// T=1 or T=0 can be the active protocol
        /// </summary>
        Tx = T0 | T1
    }

    /// <summary>
    /// Defines the action to take on the card in the connected reader on close.
    /// </summary>
    public enum SCARD_DISCONNECT : uint
    {
        /// <summary>
        /// Don't do anything special on close
        /// </summary>
        Leave = 0,

        /// <summary>
        /// Reset the card on close
        /// </summary>
        Reset = 1,

        /// <summary>
        /// Power down the card on close
        /// </summary>
        Unpower = 2,

        /// <summary>
        /// Eject the card on close
        /// </summary>
        Eject = 3
    }


    /// <summary>
    /// Current state of the reader, as seen by the application. 
    /// This field can take on any of the following values, in combination, as a bitmask. 
    /// </summary>
    public enum SCARD_CARD_STATE : uint
    {
        /// <summary>
        /// The application is unaware of the current state, and would like to know. 
        /// The use of this value results in an immediate return from state transition 
        /// monitoring services. This is represented by all bits set to zero.
        /// </summary>
        UNAWARE = 0x00000000,

        /// <summary>
        /// The application requested that this reader be ignored.  No other
        /// bits will be set.
        /// </summary>
        IGNORE = 0x00000001,

        /// <summary>
        /// This implies that there is a difference between the state believed by 
        /// the application, and the state known by the Service Manager.  When this 
        /// bit is set, the application may assume a significant state change has
        /// occurred on this reader. 
        /// </summary>
        CHANGED = 0x00000002,

        /// <summary>
        /// This implies that the given reader name is not recognized by the Service Manager.
        /// If this bit is set, then SCARD_STATE_CHANGED and SCARD_STATE_IGNORE will also be set.
        /// </summary>
        UNKNOWN = 0x00000004,

        /// <summary>
        /// This implies that the actual state of this reader is not available.  
        /// If this bit is set, then all the following bits are clear.
        /// </summary>
        UNAVAILABLE = 0x00000008,

        /// <summary>
        /// This implies that there is not card in the reader.  If this bit is set, all the 
        /// following bits will be clear.
        /// </summary>
        EMPTY = 0x00000010,

        /// <summary>
        /// // This implies that there is a card in the reader.
        /// </summary>
        PRESENT = 0x00000020,

        /// <summary>
        /// This implies that there is a card in the reader with an ATR matching one of 
        /// the target cards. If this bit is set, SCARD_STATE_PRESENT will also be set.
        /// This bit is only returned on the SCardLocateCard() service.
        /// </summary>
        ATRMATCH = 0x00000040,

        /// <summary>
        /// // This implies that the card in the reader is allocated for exclusive use by 
        /// another application. If this bit is set, SCARD_STATE_PRESENT will also be set.
        /// </summary>
        EXCLUSIVE = 0x00000080,

        /// <summary>
        /// This implies that the card in the reader is in use by one or more other applications, 
        /// but may be connected to in shared mode.  If this bit is set, SCARD_STATE_PRESENT will 
        /// also be set.
        /// </summary>
        INUSE = 0x00000100,

        /// <summary>
        /// This implies that the card in the reader is unresponsive or not supported by 
        /// the reader or software.
        /// </summary>
        MUTE = 0x00000200,

        /// <summary>
        /// This implies that the card in the reader has not been powered up.
        /// </summary>
        UNPOWERED = 0x00000400
    }

    enum SCARD_CLASS : uint
    {
        /// <summary>
        /// Vendor information definitions       
        /// </summary>
        VENDOR_INFO = 1,
        /// <summary>
        /// Communication definitions            
        /// </summary>
        COMMUNICATIONS = 2,
        /// <summary>
        /// Protocol definitions                 
        /// </summary>
        PROTOCOL = 3,
        /// <summary>
        /// Power Management definitions         
        /// </summary>
        POWER_MGMT = 4,
        /// <summary>
        /// Security Assurance definitions       
        /// </summary>
        SECURITY = 5,
        /// <summary>
        /// Mechanical characteristic definitions
        /// </summary>
        MECHANICAL = 6,
        /// <summary>
        /// Vendor specific definitions          
        /// </summary>
        VENDOR_DEFINED = 7,
        /// <summary>
        /// Interface Device Protocol options    
        /// </summary>
        IFD_PROTOCOL = 8,
        /// <summary>
        /// ICC State specific definitions       
        /// </summary>
        ICC_STATE = 9,

        /// <summary>
        /// // performace counters
        /// </summary>
        PERF =       0x7ffe,   

        /// <summary>
        /// // System-specific definitions
        /// </summary>
        SYSTEM =     0x7fff,   

    }

    /// <summary>
    /// 
    /// </summary>              
    public enum SCARD_ATTR : uint 
    {
        /// <summary>
        /// Vendor name.
        /// </summary>
        VENDOR_NAME = (uint)(SCARD_CLASS.VENDOR_INFO << 16) | 0x0100,
        
        /// <summary>
        /// Vendor-supplied interface device type (model designation of reader).
        /// </summary>
        VENDOR_IFD_TYPE = (uint)(SCARD_CLASS.VENDOR_INFO << 16) | 0x0101,
        
        /// <summary>
        /// Vendor-supplied interface device version (DWORD in the form 0xMMmmbbbb where 
        /// MM = major version, mm = minor version, and bbbb = build number).
        /// </summary>
        VENDOR_IFD_VERSION = (uint)(SCARD_CLASS.VENDOR_INFO << 16) | 0x0102,
        
        /// <summary>
        /// Vendor-supplied interface device serial number.
        /// </summary>
        VENDOR_IFD_SERIAL_NO = (uint)(SCARD_CLASS.VENDOR_INFO << 16) | 0x0103,
        
        /// <summary>
        /// WORD encoded as 0xDDDDCCCC, where DDDD = data channel type and CCCC = channel number: 
        ///The following encodings are defined for DDDD: 
        ///0x01 serial I/O; CCCC is a port number. 
        ///0x02 parallel I/O; CCCC is a port number. 
        ///0x04 PS/2 keyboard port; CCCC is zero. 
        ///0x08 SCSI; CCCC is SCSI ID number. 
        ///0x10 IDE; CCCC is device number. 
        ///0x20 USB; CCCC is device number. 
        ///0xFy vendor-defined interface with y in the range zero through 15; CCCC is vendor defined.
        /// </summary>
        CHANNEL_ID = (uint)(SCARD_CLASS.COMMUNICATIONS << 16) | 0x0110,
        
        /// <summary>
        /// DWORD encoded as 0x0rrrpppp where rrr is RFU and should be 0x000. 
        /// pppp encodes the supported protocol types. A '1' in a given bit position 
        /// indicates support for the associated ISO protocol, so if bits zero and one are set,
        /// both T=0 and T=1 protocols are supported.
        /// </summary>
        PROTOCOL_TYPES = (uint)(SCARD_CLASS.PROTOCOL << 16) | 0x0120,

        /// <summary>
        /// Default clock rate, in kHz.
        /// </summary>
        DEFAULT_CLK = (uint)(SCARD_CLASS.PROTOCOL << 16) | 0x0121,
        
        /// <summary>
        /// Maximum clock rate, in kHz.
        /// </summary>
        MAX_CLK = (uint)(SCARD_CLASS.PROTOCOL << 16) | 0x0122,
        
        /// <summary>
        /// Default data rate, in bps.
        /// </summary>
        DEFAULT_DATA_RATE  = (uint)(SCARD_CLASS.PROTOCOL << 16) | 0x0123,
        
        /// <summary>
        /// Maximum data rate, in bps.
        /// </summary>
        MAX_DATA_RATE = (uint)(SCARD_CLASS.PROTOCOL << 16) | 0x0124,
        
        /// <summary>
        /// Maximum bytes for information file size device.
        /// </summary>
        MAX_IFSD = (uint)(SCARD_CLASS.PROTOCOL << 16) | 0x0125,

        /// <summary>
        /// Zero if device does not support power down while smart card is inserted. Nonzero otherwise.
        /// </summary>
        POWER_MGMT_SUPPORT = (uint)(SCARD_CLASS.POWER_MGMT << 16) | 0x0131,
        
        /// <summary>
        /// 
        /// </summary>
        USER_TO_CARD_AUTH_DEVICE = (uint)(SCARD_CLASS.SECURITY << 16) | 0x0140,
        
        /// <summary>
        /// 
        /// </summary>
        USER_AUTH_INPUT_DEVICE = (uint)(SCARD_CLASS.SECURITY << 16) | 0x0142,
        
        /// <summary>
        /// 
        /// </summary>
        CHARACTERISTICS = (uint)(SCARD_CLASS.MECHANICAL << 16) | 0x0150,

        /// <summary>
        /// 
        /// </summary>
        CURRENT_PROTOCOL_TYPE = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x0201,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_CLK = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x0202,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_F = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x0203,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_D = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x0204,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_N = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x0205,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_W = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x0206,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_IFSC = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x0207,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_IFSD = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x0208,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_BWT = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x0209,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_CWT = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x020a,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_EBC_ENCODING = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x020b,
        
        /// <summary>
        /// 
        /// </summary>
        EXTENDED_BWT = (uint)(SCARD_CLASS.IFD_PROTOCOL << 16) | 0x020c,

        /// <summary>
        /// Single byte indicating smart card presence: 
        /// 0 = not present
        /// 1 = card present but not swallowed (applies only if reader supports smart card swallowing)
        /// 2 = card present (and swallowed if reader supports smart card swallowing)
        /// 4 = card confiscated.
        /// </summary>
        ICC_PRESENCE = (uint)(SCARD_CLASS.ICC_STATE << 16) | 0x0300,
        
        /// <summary>
        /// 
        /// </summary>
        ICC_INTERFACE_STATUS = (uint)(SCARD_CLASS.ICC_STATE << 16) | 0x0301,
        
        /// <summary>
        /// 
        /// </summary>
        CURRENT_IO_STATE = (uint)(SCARD_CLASS.ICC_STATE << 16) | 0x0302,

        /// <summary>
        /// Answer to reset (ATR) string.
        /// </summary>
        ATR_STRING = (uint)(SCARD_CLASS.ICC_STATE << 16) | 0x0303,
        
        /// <summary>
        /// 
        /// </summary>
        ICC_TYPE_PER_ATR = (uint)(SCARD_CLASS.ICC_STATE << 16) | 0x0304,

        /// <summary>
        /// 
        /// </summary>
        ESC_RESET = (uint)(SCARD_CLASS.VENDOR_DEFINED << 16) | 0xA000,
        
        /// <summary>
        /// 
        /// </summary>
        ESC_CANCEL = (uint)(SCARD_CLASS.VENDOR_DEFINED << 16) | 0xA003,
        
        /// <summary>
        /// 
        /// </summary>
        ESC_AUTHREQUEST = (uint)(SCARD_CLASS.VENDOR_DEFINED << 16) | 0xA005,
        
        /// <summary>
        /// 
        /// </summary>
        MAXINPUT = (uint)(SCARD_CLASS.VENDOR_DEFINED << 16) | 0xA007,

        /// <summary>
        /// 
        /// </summary>
        DEVICE_UNIT = (uint)(SCARD_CLASS.SYSTEM << 16) | 0x0001,
        
        /// <summary>
        /// 
        /// </summary>
        DEVICE_IN_USE = (uint)(SCARD_CLASS.SYSTEM << 16) | 0x0002,
        
        /// <summary>
        /// 
        /// </summary>
        DEVICE_FRIENDLY_NAME_A = (uint)(SCARD_CLASS.SYSTEM << 16) | 0x0003,
        
        /// <summary>
        /// 
        /// </summary>
        DEVICE_SYSTEM_NAME_A = (uint)(SCARD_CLASS.SYSTEM << 16) | 0x0004,
        
        /// <summary>
        /// 
        /// </summary>
        DEVICE_FRIENDLY_NAME_W = (uint)(SCARD_CLASS.SYSTEM << 16) | 0x0005,
        
        /// <summary>
        /// 
        /// </summary>
        DEVICE_SYSTEM_NAME_W = (uint)(SCARD_CLASS.SYSTEM << 16) | 0x0006,
        
        /// <summary>
        /// 
        /// </summary>
        SUPRESS_T1_IFS_REQUEST = (uint)(SCARD_CLASS.SYSTEM << 16) | 0x0007,

        /// <summary>
        /// 
        /// </summary>
        PERF_NUM_TRANSMISSIONS = (uint)(SCARD_CLASS.PERF << 16) | 0x0001,
        
        /// <summary>
        /// 
        /// </summary>
        PERF_BYTES_TRANSMITTED = (uint)(SCARD_CLASS.PERF << 16) | 0x0002,
        
        /// <summary>
        /// 
        /// </summary>
        PERF_TRANSMISSION_TIME = (uint)(SCARD_CLASS.PERF << 16) | 0x0003           
    }

}
