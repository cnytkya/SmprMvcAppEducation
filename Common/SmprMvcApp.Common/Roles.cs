using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmprMvcApp.Common
{
    public class Roles
    {
        //const anahtar sözcüğü, C# dilinde sabit (constant) bir değeri tanımlamak için kullanılır. const ile tanımlanan bir değişkenin değeri, derleme (compile) zamanı sırasında belirlenir ve daha sonra değiştirilemez.
        public const string Role_Customer = "Customer";

        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
        public const string Role_Company = "Company";
    }
}