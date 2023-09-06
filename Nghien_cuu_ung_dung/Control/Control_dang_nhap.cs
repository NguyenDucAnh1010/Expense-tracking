using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nghien_cuu_ung_dung.Entity;

namespace Nghien_cuu_ung_dung.Control
{
    internal class Control_dang_nhap
    {
        Entity_tai_khoan x = new Entity_tai_khoan();
        public bool dang_nhap(string un, string pw)
        {
            return x.dang_nhap(un, pw);
        }
        public DataTable hien_thi(string tk)
        {
            return x.hien_thi(tk);
        }
        public int KiemTraDangNhap(string un, string pw)
        {
            return x.KiemTraDangNhap(un, pw);
        }
    }
}
