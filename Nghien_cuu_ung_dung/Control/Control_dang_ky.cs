using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nghien_cuu_ung_dung.Entity;

namespace Nghien_cuu_ung_dung.Control
{
    internal class Control_dang_ky
    {
        Entity_tai_khoan x = new Entity_tai_khoan();
        public bool dang_ky(string id, string tenql, string tench, string gmail, string sdt, string mk)
        {
            return x.dang_ky(id, tenql, tench, gmail, sdt, mk);
        }
        public void capnhap_mk(string tk, string mk)
        {
            x.capnhap_mk(tk, mk);
        }
        public void capnhap_gmail(string tk, string gmail)
        {
            x.capnhap_gmail(tk, gmail);
        }
        public void capnhap_sdt(string tk, string sdt)
        {
            x.capnhap_sdt(tk, sdt);
        }
    }
}
