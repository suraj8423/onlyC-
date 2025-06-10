# Both intersect and except will be same conceptwise

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
namespace LINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> dataSource1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            List<int> dataSource2 = new List<int>() { 1, 3, 5, 8, 9, 10 };

            //Method Syntax
            var MS = dataSource1.Except(dataSource2).ToList();

            //Query Syntax
            var QS = (from num in dataSource1
                      select num)
                      .Except(dataSource2).ToList();

            foreach (var item in QS)
            {
                Console.Write(item + " ");
            }

            Console.ReadKey();
        }
    }
}

Run the application, and you will see the output as expected, i.e., 2 4 6. 

What Happens if any of the Sequences is Null?

using System;
using System.Collections.Generic;
using System.Linq;
namespace LINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> dataSource1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            List<int> dataSource2 = null;

            //Method Syntax
            var MS = dataSource1.Except(dataSource2).ToList();
            
            foreach (var item in MS)
            {
                Console.Write(item + " ");
            }

            Console.ReadKey();
        }
    }
}

LINQ Except() Method with Complex Type in C#:

namespace LINQDemo
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}


using System.Collections.Generic;
using System;
using System.Linq;
namespace LINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> AllStudents = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 103, Name = "Hina"},
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
                new Student {ID = 106, Name = "Santosh"},
            };

            List<Student> Class6Students = new List<Student>()
            {
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
            };

            //Method Syntax
            var MS = AllStudents.Select(x => x.Name).Except(Class6Students.Select(y => y.Name)).ToList();

            //Query Syntax
            var QS = (from std in AllStudents
                      select std.Name).Except(Class6Students.Select(y => y.Name)).ToList();

            foreach (var name in MS)
            {
                Console.WriteLine(name);
            }
            
            Console.ReadKey();
        }
    }
}

// result will be Preety, Hina, Santosh

using System.Collections.Generic;
using System;
using System.Linq;
namespace LINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> AllStudents = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 103, Name = "Hina"},
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
                new Student {ID = 106, Name = "Santosh"},
            };

            List<Student> Class6Students = new List<Student>()
            {
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
            };
            
            //Method Syntax
            var MS = AllStudents.Except(Class6Students).ToList();

            //Query Syntax
            var QS = (from std in AllStudents
                      select std).Except(Class6Students).ToList();
            
            foreach (var student in MS)
            {
                Console.WriteLine($" ID : {student.ID} Name : {student.Name}");
            }
            
            Console.ReadKey();
        }
    }
}

// result we will get with ID and Name both

Using Anonymous Type with Except Method in C#:

using System.Collections.Generic;
using System;
using System.Linq;
namespace LINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> AllStudents = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 103, Name = "Hina"},
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
                new Student {ID = 106, Name = "Santosh"},
            };

            List<Student> Class6Students = new List<Student>()
            {
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
            };
            
            //Method Syntax
            var MS = AllStudents.Select(x => new {x.ID, x.Name })
                    .Except(Class6Students.Select(x => new { x.ID, x.Name })).ToList();

            //Query Syntax
            var QS = (from std in AllStudents
                      select new { std.ID, std.Name})
                      .Except(Class6Students.Select(x => new { x.ID, x.Name })).ToList();
            
            foreach (var student in QS)
            {
                Console.WriteLine($" ID : {student.ID} Name : {student.Name}");
            }
            
            Console.ReadKey();
        }
    }
}
```