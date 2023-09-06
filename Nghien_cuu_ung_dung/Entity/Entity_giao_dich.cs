using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Nghien_cuu_ung_dung.Ngoaile;
using System.Reflection.Metadata;
using System.Data.SqlTypes;

namespace Nghien_cuu_ung_dung.Entity
{
    internal class Entity_giao_dich
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
        public decimal tien_mat_thu(string tk)
        {
            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon where TaiKhoan='{tk}' and LoaiHD = N'Loại 1' and PTGD like N'Tiền mặt';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0.000m;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 5

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' and PTGD like N'Tiền mặt';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 10

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%' and PTGD like N'Tiền mặt';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tien_chuyen_khoan_thu(string tk)
        {

            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon where TaiKhoan='{tk}' and LoaiHD = N'Loại 1' and PTGD like N'%chuyển khoản%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0.000m;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 2 TH1

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienVanChuyen+TienHang) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' and NguoiNhanTien=N'Cửa hàng' and PTGD like N'%chuyển khoản%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 2 TH3

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='Yes' and NguoiNhanTien=N'Cửa hàng' and PTGD like N'%chuyển khoản%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 5

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' and PTGD like N'%chuyển khoản%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 10

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%' and PTGD like N'%chuyển khoản%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tien_the_thu(string tk)
        {

            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon where TaiKhoan='{tk}' and LoaiHD = N'Loại 1' and PTGD like N'Tiền thẻ';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0.000m;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 5

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' and PTGD like N'Tiền thẻ';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 10

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%' and PTGD like N'Tiền thẻ';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tien_mat_chi(string tk)
        {
            //Loai 4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%' and PTGD like N'Tiền mặt';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0.000m;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 6

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 6%' and PTGD like N'Tiền mặt';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 9

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 9%' and PTGD like N'Tiền mặt';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 11

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and PTGD like N'Tiền mặt';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tien_chuyen_khoan_chi(string tk)
        {

            //Loai 4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%' and PTGD like N'%chuyển khoản%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0.000m;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 6

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 6%' and PTGD like N'%chuyển khoản%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 9

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 9%' and PTGD like N'%chuyển khoản%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 11

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and PTGD like N'%chuyển khoản%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tien_the_chi(string tk)
        {
            //Loai 4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%' and PTGD like N'Tiền thẻ';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0.000m;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 6

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 6%' and PTGD like N'Tiền thẻ';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 9

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 9%' and PTGD like N'Tiền thẻ';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 11

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and PTGD like N'Tiền thẻ';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tk_tien_mat_thu(string tk, string date1, string date2)
        {
            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon where TaiKhoan='{tk}' and LoaiHD = N'Loại 1' and PTGD like N'Tiền mặt' " +
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

            //Loai 5

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' and PTGD like N'Tiền mặt' " +
                $"and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 10

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%' and PTGD like N'Tiền mặt' and " +
                $"ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tk_tien_chuyen_khoan_thu(string tk, string date1, string date2)
        {
            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon where TaiKhoan='{tk}' and LoaiHD = N'Loại 1' and PTGD like N'%chuyển khoản%' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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

            //Loai 2 TH1

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienVanChuyen+TienHang) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' and NguoiNhanTien=N'Cửa hàng' and PTGD like N'%chuyển khoản%' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 2 TH3

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='Yes' and NguoiNhanTien=N'Cửa hàng' and PTGD like N'%chuyển khoản%' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 5

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' and PTGD like N'%chuyển khoản%' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 10

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%' and PTGD like N'%chuyển khoản%' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tk_tien_the_thu(string tk, string date1, string date2)
        {

            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon where TaiKhoan='{tk}' and LoaiHD = N'Loại 1' and PTGD like N'Tiền thẻ' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' and PTGD like N'Tiền thẻ' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 10

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%' and PTGD like N'Tiền thẻ' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tk_tien_mat_chi(string tk, string date1, string date2)
        {

            //Loai 4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%' and PTGD like N'Tiền mặt' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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

            //Loai 6

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 6%' and PTGD like N'Tiền mặt' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 9

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 9%' and PTGD like N'Tiền mặt' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 11

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and PTGD like N'Tiền mặt' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tk_tien_chuyen_khoan_chi(string tk, string date1, string date2)
        {

            //Loai 4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%' and PTGD like N'%chuyển khoản%' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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

            //Loai 6

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 6%' and PTGD like N'%chuyển khoản%' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 9

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 9%' and PTGD like N'%chuyển khoản%' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 11

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and PTGD like N'%chuyển khoản%' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public decimal tk_tien_the_chi(string tk, string date1, string date2)
        {
            //Loai 4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%' and PTGD like N'Tiền thẻ' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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

            //Loai 6

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 6%' and PTGD like N'Tiền thẻ' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 9

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 9%' and PTGD like N'Tiền thẻ' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 11

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and PTGD like N'Tiền thẻ' " +
                $" and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            return x;
        }
        public DataTable hien_thi()
        {
            open();

            bool check = false;

            string sql = $"select * from GiaoDich where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                check = true;
            }
            reader.Close();

            if (check)
            {
                string a = tien_mat_thu(bientoancuc.tk).ToString();
                string b = tien_chuyen_khoan_thu(bientoancuc.tk).ToString();
                string c = tien_the_thu(bientoancuc.tk).ToString();
                string d = tien_mat_chi(bientoancuc.tk).ToString();
                string e = tien_chuyen_khoan_chi(bientoancuc.tk).ToString();
                string f = tien_the_chi(bientoancuc.tk).ToString();
                sql = $"UPDATE GiaoDich SET TienMatThu = @a,TienChuyenKhoanThu = @b,TienTheThu = @c," +
                    $"TienMatChi = @d,TienChuyenKhoanChi = @e,TienTheChi = @f where TaiKhoan = '{bientoancuc.tk}'";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a",SqlDbType.Decimal).Value = decimal.Parse(a);
                cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse(b);
                cmd.Parameters.Add("@c", SqlDbType.Decimal).Value = decimal.Parse(c);
                cmd.Parameters.Add("@d", SqlDbType.Decimal).Value = decimal.Parse(d);
                cmd.Parameters.Add("@e", SqlDbType.Decimal).Value = decimal.Parse(e);
                cmd.Parameters.Add("@f", SqlDbType.Decimal).Value = decimal.Parse(f);
                reader = cmd.ExecuteReader();
                reader.Close();
            }
            else
            {
                sql = $"insert into GiaoDich values('{bientoancuc.tk}',0,0,0,0,0,0)";
                cmd = new SqlCommand(sql, con);
                reader = cmd.ExecuteReader();
                reader.Close();

                string a = tien_mat_thu(bientoancuc.tk).ToString();
                string b = tien_chuyen_khoan_thu(bientoancuc.tk).ToString();
                string c = tien_the_thu(bientoancuc.tk).ToString();
                string d = tien_mat_chi(bientoancuc.tk).ToString();
                string e = tien_chuyen_khoan_chi(bientoancuc.tk).ToString();
                string f = tien_the_chi(bientoancuc.tk).ToString();
                sql = $"UPDATE GiaoDich SET TienMatThu = @a,TienChuyenKhoanThu = @b,TienTheThu = @c," +
                    $"TienMatChi = @d,TienChuyenKhoanChi = @e,TienTheChi = @f where TaiKhoan = '{bientoancuc.tk}'";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(a);
                cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse(b);
                cmd.Parameters.Add("@c", SqlDbType.Decimal).Value = decimal.Parse(c);
                cmd.Parameters.Add("@d", SqlDbType.Decimal).Value = decimal.Parse(d);
                cmd.Parameters.Add("@e", SqlDbType.Decimal).Value = decimal.Parse(e);
                cmd.Parameters.Add("@f", SqlDbType.Decimal).Value = decimal.Parse(f);
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            sql = $"select * from GiaoDich where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            close();
            return dt;
        }
        public DataTable hien_thi(string date1, string date2)
        {
            open();

            bool check = false;

            string sql = $"select * from GiaoDich where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                check = true;
            }
            reader.Close();

            if (check)
            {
                string a = tk_tien_mat_thu(bientoancuc.tk,date1, date2).ToString();
                string b = tk_tien_chuyen_khoan_thu(bientoancuc.tk, date1, date2).ToString();
                string c = tk_tien_the_thu(bientoancuc.tk, date1, date2).ToString();
                string d = tk_tien_mat_chi(bientoancuc.tk, date1, date2).ToString();
                string e = tk_tien_chuyen_khoan_chi(bientoancuc.tk, date1, date2).ToString();
                string f = tk_tien_the_chi(bientoancuc.tk, date1, date2).ToString();
                sql = $"UPDATE GiaoDich SET TienMatThu = @a,TienChuyenKhoanThu = @b,TienTheThu = @c," +
                    $"TienMatChi = @d,TienChuyenKhoanChi = @e,TienTheChi = @f where TaiKhoan = '{bientoancuc.tk}'";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(a);
                cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse(b);
                cmd.Parameters.Add("@c", SqlDbType.Decimal).Value = decimal.Parse(c);
                cmd.Parameters.Add("@d", SqlDbType.Decimal).Value = decimal.Parse(d);
                cmd.Parameters.Add("@e", SqlDbType.Decimal).Value = decimal.Parse(e);
                cmd.Parameters.Add("@f", SqlDbType.Decimal).Value = decimal.Parse(f);
                reader = cmd.ExecuteReader();
                reader.Close();
            }
            else
            {
                sql = $"insert into GiaoDich values('{bientoancuc.tk}',0,0,0,0,0,0)";
                cmd = new SqlCommand(sql, con);
                reader = cmd.ExecuteReader();
                reader.Close();

                string a = tk_tien_mat_thu(bientoancuc.tk, date1, date2).ToString();
                string b = tk_tien_chuyen_khoan_thu(bientoancuc.tk, date1, date2).ToString();
                string c = tk_tien_the_thu(bientoancuc.tk, date1, date2).ToString();
                string d = tk_tien_mat_chi(bientoancuc.tk, date1, date2).ToString();
                string e = tk_tien_chuyen_khoan_chi(bientoancuc.tk, date1, date2).ToString();
                string f = tk_tien_the_chi(bientoancuc.tk, date1, date2).ToString();
                sql = $"UPDATE GiaoDich SET TienMatThu = @a,TienChuyenKhoanThu = @b,TienTheThu = @c," +
                    $"TienMatChi = @d,TienChuyenKhoanChi = @e,TienTheChi = @f where TaiKhoan = '{bientoancuc.tk}'";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(a);
                cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse(b);
                cmd.Parameters.Add("@c", SqlDbType.Decimal).Value = decimal.Parse(c);
                cmd.Parameters.Add("@d", SqlDbType.Decimal).Value = decimal.Parse(d);
                cmd.Parameters.Add("@e", SqlDbType.Decimal).Value = decimal.Parse(e);
                cmd.Parameters.Add("@f", SqlDbType.Decimal).Value = decimal.Parse(f);
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            sql = $"select * from GiaoDich where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            close();
            return dt;
        }
    }
}
