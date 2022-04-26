using System;
using System.Collections.Generic;

namespace Vending_Machines_Managment.Models
{
    public partial class VendingMachine
    {
        public int Id { get; set; }
        public string Location { get; set; } = null!;
        public DateTime InstalledOn { get; set; }
        public int TypeId { get; set; }

        public virtual VendingMachineType Type { get; set; } = null!;
    }
}
