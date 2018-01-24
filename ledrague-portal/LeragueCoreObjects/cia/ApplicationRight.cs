using System;
using System.Collections.Generic;
using System.Text;

namespace LeDragueCoreObjects.cia
{
    public class ApplicationRight
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
