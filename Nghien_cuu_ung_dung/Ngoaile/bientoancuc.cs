using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nghien_cuu_ung_dung.Ngoaile
{
    public class bientoancuc
    {
        public static string tk;
        public static string dc = @"Data Source=DESKTOP-TTBV302\MSSQLSERVER02;Initial Catalog=NewQLTC_8_5;Integrated Security=True";
        public static string chuanhoa(string x)
        {
            int n = 0;
            string s = "0";
            if (Convert.ToDecimal(x) <= 0)
            {
                n = (x.Length - 6) / 3;
                if ((x.Length - 6) % 3 == 0)
                {
                    n--;
                }
                if (n > 0)
                {
                    s = "-"+x.Substring(1, x.Length - 6 - 3 * n);
                }
                for (int i = 0; i < n; i++)
                {
                    s += $".{x.Substring(x.Length - 6 - 3 * (n - i), 3)}";
                }
                
            }
            else
            {
                n = (x.Length - 5) / 3;
                if ((x.Length - 5) % 3 == 0)
                {
                    n--;
                }
                if (n > 0)
                {
                    s = x.Substring(0, x.Length - 5 - 3 * n);
                }
                for (int i = 0; i < n; i++)
                {
                    s += $".{x.Substring(x.Length - 5 - 3 * (n - i), 3)}";
                }
            }
            s += ",0000";
            return s;
        }
    }
}
