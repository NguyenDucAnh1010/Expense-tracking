using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Nghien_cuu_ung_dung.Ngoaile;
using Microsoft.Xaml.Behaviors.Media;
using System.Globalization;
using System.Windows.Forms;

namespace Nghien_cuu_ung_dung.Entity
{
    internal class Entity_so_quy
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
        public decimal thu_nhap(string tk)
        {
            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienVanChuyen*10) from HoaDon where TaiKhoan = @a and LoaiHD like N'%Loại 2%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@a", SqlDbType.NChar).Value = tk;
            DbDataReader reader = cmd.ExecuteReader();
            decimal x = 0;
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(SP_HD.SLXuat*SP.DonGia) from HoaDon HD,SanPham_HoaDon SP_HD,SanPham SP " +
                $"where HD.TaiKhoan='{tk}' and HD.SoHD=SP_HD.SoHD and SP_HD.MaSP=SP.MaSP and HD.LoaiHD like N'%Loại 1%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x -= reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 2

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and NguoiNhanTien like N'%Cửa hàng%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(SP_HD.SLXuat*SP.DonGia) from HoaDon HD,SanPham_HoaDon SP_HD,SanPham SP " +
                $"where HD.TaiKhoan='{tk}' and HD.SoHD=SP_HD.SoHD and SP_HD.MaSP=SP.MaSP and HD.LoaiHD like N'%Loại 2%' and HD.NguoiNhanTien like N'%Cửa hàng%';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x -= reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 5

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%';" +
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

        public decimal tien_van_chuyen(string tk)
        {
            //Loai 2 TH1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienVanChuyen) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' and NguoiNhanTien=N'Cửa hàng';" +
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
                $"and CHARINDEX(NVC.MaNVC,HD.LoaiHD)>0;" +
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
        public decimal tien_mat_bang(string tk)
        {

            //Loai 11 TH1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                    $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                    $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and CHARINDEX(N'Tiền nhà',LoaiHD)>0;" +
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

            return x;
        }
        public decimal tien_sinh_hoat(string tk)
        {

            //Loai 11 TH2

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and CHARINDEX(N'Tiền nhà',LoaiHD)<=0;" +
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

            return x;
        }
        public decimal doanh_thu(string tk)
        {
            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienVanChuyen*10) from HoaDon where TaiKhoan='{tk}' and LoaiHD = N'Loại 1%';" +
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
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' and NguoiNhanTien=N'Cửa hàng';" +
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
                $"insert into @Tien(Tien) select sum(HD.TienHang*(NVC.COD+1)) from HoaDon HD, CongNoNVC NVC " +
                $"where HD.TaiKhoan='{tk}' and HD.LoaiHD like N'%Loại 2%' and HD.DichVuShipCOD='Yes' and HD.NguoiNhanTien=N'Cửa hàng' and CHARINDEX(NVC.MaNVC,HD.LoaiHD)>0;" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%';" +
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
        public decimal tong_chi(string tk)
        {

            //Loai 4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 6%';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 9%';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%';" +
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
        public decimal tk_thu_nhap(string tk, string date1, string date2)
        {
            
            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienVanChuyen*10) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD = N'Loại 1' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(SP_HD.SLXuat*SP.DonGia) from HoaDon HD,SanPham_HoaDon SP_HD,SanPham SP " +
                $"where HD.TaiKhoan='{tk}' and HD.SoHD=SP_HD.SoHD and SP_HD.MaSP=SP.MaSP " +
                $"and HD.LoaiHD = N'Loại 1' and HD.ThoiGian>='{date1}' and HD.ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x -= reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 2

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienHang) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and NguoiNhanTien like N'%Cửa hàng%' " +
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

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(SP_HD.SLXuat*SP.DonGia) from HoaDon HD,SanPham_HoaDon SP_HD,SanPham SP " +
                $"where HD.TaiKhoan='{tk}' and HD.SoHD=SP_HD.SoHD and SP_HD.MaSP=SP.MaSP and HD.LoaiHD like N'%Loại 2%' and HD.NguoiNhanTien like N'%Cửa hàng%' " +
                $"and HD.ThoiGian>='{date1}' and HD.ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x -= reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 5

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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

        public decimal tk_tien_van_chuyen(string tk, string date1, string date2)
        {
            //Loai 2 TH1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienVanChuyen) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' " +
                $"and NguoiNhanTien=N'Cửa hàng' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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
                $"and CHARINDEX(NVC.MaNVC,HD.LoaiHD)>0 and HD.ThoiGian>='{date1}' and HD.ThoiGian<='{date2}';" +
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
        public decimal tk_tien_mat_bang(string tk, string date1, string date2)
        {
            open();
            //Loai 11 TH1
            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and CHARINDEX(N'Tiền nhà',LoaiHD)>0 and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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

            return x;
        }
        public decimal tk_tien_sinh_hoat(string tk, string date1, string date2)
        {
            //Loai 11 TH2

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and CHARINDEX(N'Tiền nhà',LoaiHD)<=0 " +
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

            return x;
        }
        public decimal tk_doanh_thu(string tk, string date1, string date2)
        {
            //Loai 1

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienVanChuyen*10) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD = N'Loại 1' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' and NguoiNhanTien=N'Cửa hàng' " +
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

            //Loai 2 TH3

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(HD.TienHang*(NVC.COD+1)) from HoaDon HD, CongNoNVC NVC " +
                $"where HD.TaiKhoan='{tk}' and HD.LoaiHD like N'%Loại 2%' and HD.DichVuShipCOD='Yes' and HD.NguoiNhanTien=N'Cửa hàng' " +
                $"and CHARINDEX(NVC.MaNVC,HD.LoaiHD)>0 and HD.ThoiGian>='{date1}' and HD.ThoiGian<='{date2}';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 5%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 10%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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
        public decimal tk_tong_chi(string tk, string date1, string date2)
        {
           //Loai 4

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 4%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 6%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 9%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
                $"\r\n\tupdate @Tien set Tien=0 where Tien is null;\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                x += reader.GetDecimal(0);
            }
            reader.Close();

            //Loai 6

            sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tTien money\r\n\t);\r\n\t" +
                $"insert into @Tien(Tien) select sum(TienThanhToan) from HoaDon " +
                $"where TaiKhoan='{tk}' and LoaiHD like N'%Loại 11%' and ThoiGian>='{date1}' and ThoiGian<='{date2}';" +
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

            string sql = $"select * from SoQuy where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                check = true;
            }
            reader.Close();

            if (check)
            {
                string a = thu_nhap(bientoancuc.tk).ToString();
                string b = tien_van_chuyen(bientoancuc.tk).ToString();
                string c = tien_mat_bang(bientoancuc.tk).ToString();
                string d = tien_sinh_hoat(bientoancuc.tk).ToString();
                string e = doanh_thu(bientoancuc.tk).ToString();
                string f = tong_chi(bientoancuc.tk).ToString();
                sql = $"UPDATE SoQuy SET ThuNhap = @a,TienVanChuyen=@b,TienMatBang=@c," +
                    $"TienSinhHoat =@d,DoanhThu =@e,TongChi=@f where TaiKhoan='{bientoancuc.tk}'";
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
                sql = $"insert into SoQuy values('{bientoancuc.tk}',0,0,0,0,0,0)";
                cmd = new SqlCommand(sql, con);
                reader = cmd.ExecuteReader();
                reader.Close();

                string a = thu_nhap(bientoancuc.tk).ToString();
                string b = tien_van_chuyen(bientoancuc.tk).ToString();
                string c = tien_mat_bang(bientoancuc.tk).ToString();
                string d = tien_sinh_hoat(bientoancuc.tk).ToString();
                string e = doanh_thu(bientoancuc.tk).ToString();
                string f = tong_chi(bientoancuc.tk).ToString();
                sql = $"UPDATE SoQuy SET ThuNhap = @a,TienVanChuyen=@b,TienMatBang=@c," +
                    $"TienSinhHoat =@d,DoanhThu =@e,TongChi=@f where TaiKhoan='{bientoancuc.tk}'";
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

            sql = $"select * from SoQuy where TaiKhoan='{bientoancuc.tk}'";
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

            string sql = $"select * from SoQuy where TaiKhoan='{bientoancuc.tk}'";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                check = true;
            }
            reader.Close();

            if (check)
            {
                string a = tk_thu_nhap(bientoancuc.tk, date1, date2).ToString();
                string b = tk_tien_van_chuyen(bientoancuc.tk, date1, date2).ToString();
                string c = tk_tien_mat_bang(bientoancuc.tk, date1, date2).ToString();
                string d = tk_tien_sinh_hoat(bientoancuc.tk, date1, date2).ToString();
                string e = tk_doanh_thu(bientoancuc.tk, date1, date2).ToString();
                string f = tk_tong_chi(bientoancuc.tk, date1, date2).ToString();
                sql = $"UPDATE SoQuy SET ThuNhap = @a,TienVanChuyen=@b,TienMatBang=@c," +
                    $"TienSinhHoat =@d,DoanhThu =@e,TongChi=@f where TaiKhoan='{bientoancuc.tk}'";
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
                sql = $"insert into SoQuy values('{bientoancuc.tk}',0,0,0,0,0,0)";
                cmd = new SqlCommand(sql, con);
                reader = cmd.ExecuteReader();
                reader.Close();

                string a = tk_thu_nhap(bientoancuc.tk, date1, date2).ToString();
                string b = tk_tien_van_chuyen(bientoancuc.tk, date1, date2).ToString();
                string c = tk_tien_mat_bang(bientoancuc.tk, date1, date2).ToString();
                string d = tk_tien_sinh_hoat(bientoancuc.tk, date1, date2).ToString();
                string e = tk_doanh_thu(bientoancuc.tk, date1, date2).ToString();
                string f = tk_tong_chi(bientoancuc.tk, date1, date2).ToString();
                sql = $"UPDATE SoQuy SET ThuNhap = @a,TienVanChuyen=@b,TienMatBang=@c," +
                    $"TienSinhHoat =@d,DoanhThu =@e,TongChi=@f where TaiKhoan='{bientoancuc.tk}'";
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

            sql = $"select * from SoQuy where TaiKhoan='{bientoancuc.tk}'";
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
