
using System;
using System.Diagnostics;
using GS.SCard.ReturnCodes;

namespace GS.SCard
{
    
    /// The exception that is thrown when a WinScard function error occurs.
   
    public class WinSCardException : ApplicationException
    {
       // Fields
        private string message;
        private int status;
        private string functionName;

        
        public WinSCardException( bool enableTrace, string winSCardfunctionName, int status )
        {
            this.functionName = winSCardfunctionName;
            
            this.status = status;
         
            if ( (SCARD_ERROR)this.status == SCARD_ERROR.SCARD_F_INTERNAL_ERROR )
            {
                this.message += "An internal consistency check failed!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_CANCELLED)
            {
                this.message += "The action was cancelled by an SCardCancel request!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_INVALID_HANDLE)
            {
                this.message += "The supplied handle was invalid!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_INVALID_PARAMETER)
            {
                this.message += "One or more of the supplied parameters could not be properly interpreted!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_INVALID_TARGET)
            {
                this.message += "Registry startup information is missing or invalid!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NO_MEMORY)
            {
                this.message += "Not enough memory available to complete this command!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_F_WAITED_TOO_LONG)
            {
                this.message += "An internal consistency timer has expired!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_INSUFFICIENT_BUFFER)
            {
                this.message += "The data buffer to receive returned data is too small for the returned data!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_UNKNOWN_READER)
            {
                this.message += "The specified reader name is not recognized!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_TIMEOUT)
            {
                this.message += "The user-specified timeout value has expired!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_SHARING_VIOLATION)
            {
                this.message += "The smart card cannot be accessed because of other connections outstanding!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NO_SMARTCARD)
            {
                this.message += "The operation requires a Smart Card, but no Smart Card is currently in the device!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_UNKNOWN_CARD)
            {
                this.message += "The specified smart card name is not recognized!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_CANT_DISPOSE)
            {
                this.message += "The system could not dispose of the media in the requested manner!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_PROTO_MISMATCH)
            {
                this.message += "The requested protocols are incompatible with the protocol currently in use with the smart card!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NOT_READY)
            {
                this.message += "The reader or smart card is not ready to accept commands!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_INVALID_VALUE)
            {
                this.message += "One or more of the supplied parameters values could not be properly interpreted!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_SYSTEM_CANCELLED)
            {
                this.message += "The action was cancelled by the system, presumably to log off or shut down!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_F_COMM_ERROR)
            {
                this.message += "An internal communications error has been detected!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_F_UNKNOWN_ERROR)
            {
                this.message += "An internal error has been detected, but the source is unknown!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_INVALID_ATR)
            {
                this.message += "An ATR obtained from the registry is not a valid ATR string!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NOT_TRANSACTED)
            {
                this.message += "An attempt was made to end a non-existent transaction!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_READER_UNAVAILABLE)
            {
                this.message += "The specified reader is not currently available for use!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_P_SHUTDOWN)
            {
                this.message += "The operation has been aborted to allow the server application to exit!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_PCI_TOO_SMALL)
            {
                this.message += "The PCI Receive buffer was too small!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_READER_UNSUPPORTED)
            {
                this.message += "The reader driver does not meet minimal requirements for support!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_DUPLICATE_READER)
            {
                this.message += "The reader driver did not produce a unique reader name!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_CARD_UNSUPPORTED)
            {
                this.message += "The smart card does not meet minimal requirements for support!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NO_SERVICE)
            {
                this.message += "The Smart card resource manager is not running!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_SERVICE_STOPPED)
            {
                this.message += "The Smart card resource manager has shut down!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_UNEXPECTED)
            {
                this.message += "An unexpected card error has occurred!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_ICC_INSTALLATION)
            {
                this.message += "No Primary Provider can be found for the smart card!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_ICC_CREATEORDER)
            {
                this.message += "The requested order of object creation is not supported!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_UNSUPPORTED_FEATURE)
            {
                this.message += "This smart card does not support the requested feature!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_DIR_NOT_FOUND)
            {
                this.message += "The identified directory does not exist in the smart card!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_FILE_NOT_FOUND)
            {
                this.message += "The identified file does not exist in the smart card!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NO_DIR)
            {
                this.message += "The supplied path does not represent a smart card directory!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NO_FILE)
            {
                this.message += "The supplied path does not represent a smart card file!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NO_ACCESS)
            {
                this.message += "Access is denied to this file!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_WRITE_TOO_MANY)
            {
                this.message += "The smartcard does not have enough memory to store the information!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_BAD_SEEK)
            {
                this.message += "There was an error trying to set the smart card file object pointer!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_INVALID_CHV)
            {
                this.message += "The supplied PIN is incorrect!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_UNKNOWN_RES_MNG)
            {
                this.message += "An unrecognized error code was returned from a layered component!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NO_SUCH_CERTIFICATE)
            {
                this.message += "The requested certificate does not exist!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_CERTIFICATE_UNAVAILABLE)
            {
                this.message += "The requested certificate could not be obtained!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NO_READERS_AVAILABLE)
            {
                this.message += "Cannot find a smart card reader!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_COMM_DATA_LOST)
            {
                this.message += "A communications error with the smart card has been detected. Retry the operation!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_NO_KEY_CONTAINER)
            {
                this.message += "The requested key container does not exist on the smart card!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_E_SERVER_TOO_BUSY)
            {
                this.message += "The Smart card resource manager is too busy to complete this operation!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_UNSUPPORTED_CARD)
            {
                this.message += "The reader cannot communicate with the smart card, due to ATR configuration conflicts!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_UNRESPONSIVE_CARD)
            {
                this.message += "The smart card is not responding to a reset!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_UNPOWERED_CARD)
            {
                this.message += "Power has been removed from the smart card, so that further communication is not possible!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_RESET_CARD)
            {
                this.message += "The smart card has been reset, so any shared state information is invalid!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_REMOVED_CARD)
            {
                this.message += "The smart card has been removed, so that further communication is not possible!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_SECURITY_VIOLATION)
            {
                this.message += "Access was denied because of a security violation!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_WRONG_CHV)
            {
                this.message += "The card cannot be accessed because the wrong PIN was presented!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_CHV_BLOCKED)
            {
                this.message += "The card cannot be accessed because the maximum number of PIN entry attempts has been reached!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_EOF)
            {
                this.message += "The end of the smart card file has been reached!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_CANCELLED_BY_USER)
            {
                this.message += "The action was cancelled by the user!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_CARD_NOT_AUTHENTICATED)
            {
                this.message += "No PIN was presented to the smart card!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_CACHE_ITEM_NOT_FOUND)
            {
                this.message += "The requested item could not be found in the cache!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_CACHE_ITEM_STALE)
            {
                this.message += "The requested cache item is too old and was deleted from the cache!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_CACHE_ITEM_STALE)
            {
                this.message += "The requested cache item is too old and was deleted from the cache!";
            }
            else if ((SCARD_ERROR)this.status == SCARD_ERROR.SCARD_W_CACHE_ITEM_TOO_BIG)
            {
                this.message += "The new cache item exceeds the maximum per-item size defined for the cache!";
            }
            else
            {
                this.message += "";
                //this.message += "Unknow error code!";
            }
            Trace.WriteLineIf(enableTrace, winSCardfunctionName + " Error 0x" + status.ToString("X08") + ": " + this.message);    
        }


      
        // Gets a message that describes the current exception.
       //message attribute --- when an error occures this is accessed 
        //and the message value will one of the above
        //should have used a switch statment ....
        public override string Message
        {
            get {return this.message;}
        }

        //status properties
        public int Status
        {
            get {return this.status;}
        }

        // Gets the name of the PC/SC API function name.
       
        public string WinSCardFunctionName
        {
            get { return functionName; }
        }



    }
}
