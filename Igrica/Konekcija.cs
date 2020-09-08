using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igrica
{
    class Konekcija
    {
        private static string Server = "FUJITSUP900-PC\\SQLEXPRESS";
        private static string Datab = "Igrica";
        //private static string User = "sa";
        //private static string pass = "ikt";
        public static string GetCon()
        {
            return string.Format("Server={0};Database={1};Integrated security=true", Server, Datab);
        }
    }
}
