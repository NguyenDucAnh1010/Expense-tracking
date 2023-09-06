using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nghien_cuu_ung_dung.Entity;
using Nghien_cuu_ung_dung.Ngoaile;

namespace Nghien_cuu_ung_dung.Control
{
    internal class Control_quan_ly_bctc
    {
        Entity_baocaotaichinh x = new Entity_baocaotaichinh();
        public DataTable hien_thi()
        {
            return x.hien_thi();
        }
        public DataTable hien_thi(string date1, string date2)
        {
            return x.hien_thi(date1, date2);
        }
        public DataTable bieu_do(ref int q)
        {
            return x.bieu_do(ref q);
        }
        public DataTable bieu_do(DateTime date1, DateTime date2,ref int q)
        {
            return x.bieu_do(date1,date2,ref q);
        }
    }
}
