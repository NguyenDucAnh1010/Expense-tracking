using Nghien_cuu_ung_dung.Entity;
using Nghien_cuu_ung_dung.Ngoaile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nghien_cuu_ung_dung.Control
{
    internal class Control_du_bao
    {
        Entity_du_bao x = new Entity_du_bao();
        public DataTable Du_bao_hang_hoa_ban_chay()
        {
            return x.Du_bao_hang_hoa_ban_chay();
        }
        public DataTable Du_bao_doanh_thu()
        {
            return x.Du_bao_doanh_thu();
        }
        public DataTable hien_thi_doanh_thu()
        {
            return x.hien_thi_doanh_thu();
        }
    }
}
