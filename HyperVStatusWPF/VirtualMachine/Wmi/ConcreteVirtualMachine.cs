using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace HyperVStatus.VirtualMachine.Wmi
{
    internal class ConcreteVirtualMachine : IVirtualMachine
    {
        private ManagementObject _internalVmObject;

        internal ConcreteVirtualMachine(ManagementObject vmObject)
        {
            Console.WriteLine(vmObject["CreationClassName"]);
            if (vmObject?["CreationClassName"]?.Equals("Msvm_ComputerSystem") ?? false)
            {
                _internalVmObject = vmObject;
            }
            else
            {
                throw new Exception("Expected ManagementObject of type Msvm_ComputerSystem");
            }
        }

        public string Name
        {
            get
            {
                return _internalVmObject["ElementName"] as string;
            }
        }

        public EnabledState State
        {
            get
            {
                return (EnabledState)_internalVmObject["EnabledState"];
            }
        }

        public TimeSpan Uptime
        {
            get
            {
                // remove milliseconds
                var span = TimeSpan.FromMilliseconds((ulong)_internalVmObject["OnTimeInMilliseconds"]);
                return span - TimeSpan.FromMilliseconds(span.Milliseconds);
            }
        }

        public void RequestStateChange(StateChange requestedState)
        {
            uint retval = (uint)_internalVmObject.InvokeMethod("RequestStateChange",
                new object[] { (ushort)requestedState, null, null });

            // 0: Completed with no error.
            // 4096: The transition is asynchronous (parameters checked, should be OK?).
            // TODO: check async status and make sure everything is *actually* OK.
            if(retval != 0 && retval != 4096)
            {
                throw new Exception("State change failed with error code " + retval);
            }
        }
    }
}
