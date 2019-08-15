using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Enums
{
    public enum CallStatus
    {
        Undefined = 0,
        Dialing,
        Completed,
        Missed,
        Mailbox,
        Busy,
        InCall,
        ErrorInredirect
    }
}
