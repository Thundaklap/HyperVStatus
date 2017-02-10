using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperVStatus.VirtualMachine
{
    /// <summary>
    /// Possible state updates which can be requested.
    /// </summary>
    public enum StateChange : ushort
    {
        /// <summary>
        /// Turns the VM on.
        /// </summary>
        Enabled = 2,

        /// <summary>
        /// Turns the VM off.
        /// </summary>
        Disabled = 3,

        /// <summary>
        /// A hard rest of the VM.
        /// </summary>
        Reboot = 10,

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        Reset = 11,

        // 13-32767 reserved

        /// <summary>
        /// Pauses the VM.
        /// </summary>
        Paused = 32768,

        /// <summary>
        /// Saves the state of the VM.
        /// </summary>
        Suspended = 32768,

        // -> 65535 reserved
    }
}
