using System;
using System.Collections.Generic;
using System.Text;

namespace LeDragueCoreObjects.Contracts
{
    public class Contract_Content
    {
        public Contract_Content()
        {
            this.Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string HashCode { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
