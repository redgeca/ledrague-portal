using System;
using System.Collections.Generic;
using System.Text;

namespace LeDragueCoreObjects.cia
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApplicationPrefix { get; set; }

        public List<ApplicationRight> ApplicationRights { get; set; }
    }
}
