using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeDragueCoreObjects.Contracts
{
    public class Contract
    {
        public Contract()
        {
            this.Token_Value = new HashSet<Token_Value>();
        }

        [Key]
        public int Id { get; set; }
        public string ContractNumber{ get; set; }
        public System.DateTime ContractDate{ get; set; }
        public decimal ContractAmount { get; set; }
        public Artist Artist { get; set; }
        public Template Template{ get; set; }
        public Contract_Content ContractContent { get; set; }

        public virtual ICollection<Token_Value> Token_Value { get; set; }
    }
}
