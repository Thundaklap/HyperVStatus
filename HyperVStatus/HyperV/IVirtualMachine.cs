using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperVStatus.HyperV
{
    /// <summary>
    /// Useful information about a Hyper-V virtual machine.
    /// </summary>
    public interface IVirtualMachine
    {
        string Name { get; }

        EnabledState State { get; }

        /// <summary>
        /// Request a state change for the virtual machine.
        /// </summary>
        /// <param name="machine">The virtual machine </param>
        /// <param name="requestedState">The state requested. An exception may be thrown if the state change is invalid.</param>
        void RequestStateChange(EnabledState requestedState);
    }

    /// <summary>
    /// See https://msdn.microsoft.com/en-us/library/cc136822(v=vs.85).aspx
    /// </summary>
    public enum EnabledState : ushort
    {
        /// <summary>
        /// The state of the VM could not be determined.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The VM is running.
        /// </summary>
        Enabled = 2,

        /// <summary>
        /// The VM is turned off.
        /// </summary>
        Disabled = 3,

        /// <summary>
        /// The VM is paused.
        /// </summary>
        Paused = 32768,

        /// <summary>
        /// The VM is in a saved state.
        /// </summary>
        Suspended = 32769,

        /// <summary>
        /// The VM is starting.
        /// </summary>
        Starting = 32770,

        /// <summary>
        /// Not supported.
        /// </summary>
        [Obsolete]
        Snapshotting = 32771,

        /// <summary>
        /// VM is saving its state.
        /// </summary>
        Saving = 32773,

        /// <summary>
        /// VM is turning off.
        /// </summary>
        Stopping = 32774,

        /// <summary>
        /// VM is pausing.
        /// </summary>
        Pausing = 32776,

        /// <summary>
        /// VM is resuming.
        /// </summary>
        Resuming = 32777
    }
}
