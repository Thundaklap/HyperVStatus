using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace HyperVStatus.HyperV.Wmi
{
    public class WmiHyperVService : IHyperVService
    {
        public List<IVirtualMachine> GetVMs()
        {
            // TODO add support for systems which aren't localhost
            // TODO add support for authentication, at the moment this program must be run by admin to work properly.
            ManagementScope manScope = new ManagementScope(@"\\.\root\virtualization\v2");
            ObjectQuery queryObj = new ObjectQuery("SELECT * FROM Msvm_ComputerSystem WHERE Caption = \"Virtual Machine\"");
            ManagementObjectSearcher vmSearcher = new ManagementObjectSearcher(manScope, queryObj);
            ManagementObjectCollection vmCollection = vmSearcher.Get();

            List<IVirtualMachine> ret = new List<IVirtualMachine>();
            foreach(var obj in vmCollection)
            {
                ret.Add(new ConcreteVirtualMachine(obj as ManagementObject));
            }
            return ret;
        }
    }
}
