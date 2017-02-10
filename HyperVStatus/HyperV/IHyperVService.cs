using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperVStatus.HyperV
{
    /// <summary>
    /// Super-lightweight service for Hyper-V. Provides access to list of virtual machines and their data,
    /// and also allows for VMs to be turned on and off.
    /// </summary>
    public interface IHyperVService
    {
        /// <summary>
        /// The virtual machines on the given Hyper-V service.
        /// </summary>
        /// <returns>All virtual machines.</returns>
        List<IVirtualMachine> GetVMs();
    }
}
