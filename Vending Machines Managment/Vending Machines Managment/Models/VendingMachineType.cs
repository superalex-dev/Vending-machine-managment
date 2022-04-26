using System;
using System.Collections.Generic;

namespace Vending_Machines_Managment.Models
{
    public partial class VendingMachineType
    {
        public VendingMachineType()
        {
            VendingMachines = new HashSet<VendingMachine>();
        }

        public int TypeId { get; set; }
        public string TypeInfo { get; set; } = null!;

        public virtual ICollection<VendingMachine> VendingMachines { get; set; }
    }
}
