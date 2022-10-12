using System;
using System.Collections.Generic;

namespace consoleApp.Data.EfCore
{
    public partial class InventoryTransactionType
    {
        public InventoryTransactionType()
        {
            InventoryTransactions = new HashSet<InventoryTransaction>();
        }

        public sbyte Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; }
    }
}
