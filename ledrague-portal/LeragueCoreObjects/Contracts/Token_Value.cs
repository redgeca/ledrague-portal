using System;
using System.Collections.Generic;
using System.Text;

namespace LeDragueCoreObjects.Contracts
{
    public class Token_Value
    {
        public int Id { get; set; }
        public Contract Contract { get; set; }
        public string Token { get; set; }
        public string Value { get; set; }

    }
}
