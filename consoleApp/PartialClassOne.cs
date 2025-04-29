using System.Reflection;

namespace PartialClassDemo;

    public partial class PartialEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public double Salary { get; set; }

         partial void AssemblyNameFlags();
    }
