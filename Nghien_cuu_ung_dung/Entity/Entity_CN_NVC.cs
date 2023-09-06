using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nghien_cuu_ung_dung.Ngoaile;

namespace Nghien_cuu_ung_dung.Entity
{
    internal class Entity_CN_NVC
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter da;

        void open()
        {
            string cn = bientoancuc.dc;
            con = new SqlConnection(cn);
            con.Open();
        }

        void close()
        {
            con.Close();
            con.Dispose();
            con = null;
        }
        public decimal cn_thu(string tk, string manvc)
        {
            //Loai 2 TH2,TH4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and NguoiNhanTien=N'Shipper' and LoaiHD like N'%{manvc}%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 5

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' and LoaiHD like N'%{manvc}%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x -= reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal cn_tra(string tk, string manvc)
        {
            //Loai 2 TH1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienVanChuyen) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' and NguoiNhanTien=N'Cửa hàng' and LoaiHD like N'%{manvc}%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 2 TH3

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(HD.TienHang*NVC.COD) from HoaDon HD, CongNoNVC NVC " +
                $"where HD.TaiKhoan='{tk}' and HD.LoaiHD like N'%Loại 2%' and HD.DichVuShipCOD='Yes' and HD.NguoiNhanTien=N'Cửa hàng' " +
                $"and CHARINDEX(NVC.MaNVC,HD.LoaiHD)>0 and LoaiHD like N'%{manvc}%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 4

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%' and LoaiHD like N'%{manvc}%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x -= reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tk_cn_thu(string tk, string manvc, string date1, string date2)
        {
            //Loai 2 TH2,TH4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and NguoiNhanTien=N'Shipper' and LoaiHD like N'%{manvc}%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 5

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' " +
                $"and LoaiHD like N'%{manvc}%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x -= reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tk_cn_tra(string tk, string manvc, string date1, string date2)
        {
            //Loai 2 TH1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' and NguoiNhanTien=N'Shipper' and LoaiHD like N'%{manvc}%' " +
                $"and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 2 TH3

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(HD.TienHang*NVC.COD) from HoaDon HD, CongNoNVC NVC " +
                $"where HD.TaiKhoan='{tk}' and HD.LoaiHD like N'%Loại 2%' and HD.DichVuShipCOD='Yes' and HD.NguoiNhanTien=N'Cửa hàng' " +
                $"and CHARINDEX(NVC.MaNVC,HD.LoaiHD)>0 and LoaiHD like N'%{manvc}%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 4

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%' and LoaiHD like N'%{manvc}%' " +
                $"and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x -= reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public DataTable hien_thi()
        {
            open();

            string sql = $"select distinct MaNVC from CongNoNVC where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string a = cn_thu(bientoancuc.tk, dr[0].ToString().TrimEnd()).ToString();
                string b = cn_tra(bientoancuc.tk, dr[0].ToString().TrimEnd()).ToString();
                sql = $"UPDATE CongNoNVC SET CongNoPhaiThu = @a,CongNoPhaiTra =@b where TaiKhoan='{bientoancuc.tk}' and MaNVC='{dr[0].ToString().TrimEnd()}'";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(a);
                cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse(b);
                DbDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }

            sql = $"select * from CongNoNVC where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            close();
            return dt;
        }
        public DataTable hien_thi(string date1, string date2)
        {
            open();

            string sql = $"select distinct MaNVC from CongNoNVC where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string a = tk_cn_thu(bientoancuc.tk, dr[0].ToString().TrimEnd(),date1, date2).ToString();
                string b = tk_cn_tra(bientoancuc.tk, dr[0].ToString().TrimEnd(),date1, date2).ToString();
                sql = $"UPDATE CongNoNVC SET CongNoPhaiThu = @a,CongNoPhaiTra =@b where TaiKhoan='{bientoancuc.tk}' and MaNVC='{dr[0].ToString().TrimEnd()}'";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(a);
                cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse(b);
                DbDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }

            sql = $"select * from CongNoNVC where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            close();
            return dt;
        }
    }
}
