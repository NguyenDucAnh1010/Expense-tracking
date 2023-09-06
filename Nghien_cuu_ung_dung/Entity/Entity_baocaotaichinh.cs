using MaterialDesignThemes.Wpf;
using Nghien_cuu_ung_dung.Ngoaile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nghien_cuu_ung_dung.Entity
{
    internal class Entity_baocaotaichinh
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
        Entity_so_quy x = new Entity_so_quy();
        Entity_CN_NVC y = new Entity_CN_NVC();
        Entity_CN_NCC z = new Entity_CN_NCC();
        Entity_CN_STMDT m = new Entity_CN_STMDT();

        public DataTable hien_thi()
        {
            open();

            x.hien_thi();
            y.hien_thi();
            z.hien_thi();
            m.hien_thi();

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tten int,\r\n\t\tTien money\r\n\t);\r\n\tdeclare @Congno TABLE(\r\n\t\tTien money\r\n\t);" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 1,ThuNhap from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 2,DoanhThu from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 3,TongChi from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoNVC where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiTra) from CongNoNCC where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoSTMDT where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 4,sum(Tien) from @Congno\r\n\tselect * from @Tien;\r\nend;";
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

            x.hien_thi(date1, date2);
            y.hien_thi(date1, date2);
            z.hien_thi(date1, date2);
            m.hien_thi(date1, date2);

            string sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tten int,\r\n\t\tTien money\r\n\t);\r\n\tdeclare @Congno TABLE(\r\n\t\tTien money\r\n\t);" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 1,ThuNhap from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 2,DoanhThu from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 3,TongChi from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoNVC where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiTra) from CongNoNCC where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoSTMDT where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 4,sum(Tien) from @Congno\r\n\tselect * from @Tien;\r\nend;";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            close();
            return dt;
        }
        int check(string thang,string nam)
        {
            if (thang == "1" || thang == "3" || thang == "5" || thang == "7" || thang == "8" || thang == "10" || thang == "12")
            {
                return 31;
            }
            else if (thang == "2")
            {
                if (Convert.ToInt32(nam) % 400 == 0)
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }
            else if (thang == "4" || thang == "6" || thang == "9" || thang == "11")
            {
                return 30;
            }   
            return 1;
        }
        public DataTable bieu_do(ref int q)
        {
            open();

            int thang = 5;
            int nam = 2023;
            string sql;
            DataTable dt = null;
            int j = 1,qk=nam;
            if (thang < 6)
            {
                qk--;
            }
            int thangmoi = thang - 6;
            if (thangmoi <= 0)
            {
                thangmoi += 12;
            }
            int day = check(thangmoi.ToString(), qk.ToString());

            //Thoi Gian

            sql = $"begin declare @ThoiGian TABLE(thoigian date);" +
                $"\r\n\t\t\t\tinsert into @ThoiGian(thoigian) select distinct ThoiGian from HoaDon;" +
                $"\r\n\t\t\t\tselect distinct * from @ThoiGian where thoigian> @a;\r\nend;";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@a", new DateTime(qk, thangmoi, day));
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            sql = $"delete from ThoiGian";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            reader.Close();

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                sql = $"insert into ThoiGian values({Convert.ToDateTime(dt.Rows[i][0]).Day},{Convert.ToDateTime(dt.Rows[i][0]).Month},{Convert.ToDateTime(dt.Rows[i][0]).Year})";
                cmd = new SqlCommand(sql, con);
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            sql = $"select distinct thang,nam from ThoiGian order by nam asc";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            while (dt.Rows.Count < 6)
            {
                thang--;
                if (thang == 0)
                {
                    thang = 12;
                    nam--;
                }
                thangmoi = thang - 6;
                if (thangmoi <= 0)
                {
                    thangmoi += 12;
                }
                day = check(thangmoi.ToString(), qk.ToString());

                sql = $"begin declare @ThoiGian TABLE(thoigian date);" +
                $"\r\n\t\t\t\tinsert into @ThoiGian(thoigian) select distinct ThoiGian from HoaDon;" +
                $"\r\n\t\t\t\tselect distinct * from @ThoiGian where thoigian> @a;\r\nend;";
                cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@a", new DateTime(qk, thangmoi, day));
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                sql = $"delete from ThoiGian";
                cmd = new SqlCommand(sql, con);
                reader = cmd.ExecuteReader();
                reader.Close();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = $"insert into ThoiGian values({Convert.ToDateTime(dt.Rows[i][0]).Day},{Convert.ToDateTime(dt.Rows[i][0]).Month},{Convert.ToDateTime(dt.Rows[i][0]).Year})";
                    cmd = new SqlCommand(sql, con);
                    reader = cmd.ExecuteReader();
                    reader.Close();
                }

                sql = $"select distinct thang,nam from ThoiGian order by nam asc";
                cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }

            sql = $"delete from bctc";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                day = check(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                
                x.hien_thi($"{dt.Rows[i][0]}/1/{dt.Rows[i][1]}", $"{dt.Rows[i][0]}/{day}/{dt.Rows[i][1]}");
                y.hien_thi($"{dt.Rows[i][0]}/1/{dt.Rows[i][1]}", $"{dt.Rows[i][0]}/{day}/{dt.Rows[i][1]}");
                z.hien_thi($"{dt.Rows[i][0]}/1/{dt.Rows[i][1]}", $"{dt.Rows[i][0]}/{day}/{dt.Rows[i][1]}");
                m.hien_thi($"{dt.Rows[i][0]}/1/{dt.Rows[i][1]}", $"{dt.Rows[i][0]}/{day}/{dt.Rows[i][1]}");

                sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tten int,\r\n\t\tTien money\r\n\t);\r\n\tdeclare @Congno TABLE(\r\n\t\tTien money\r\n\t);" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 1,ThuNhap from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 2,DoanhThu from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 3,TongChi from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoNVC where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiTra) from CongNoNCC where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoSTMDT where TaiKhoan='{bientoancuc.tk}';" +
                $"\r\n\tinsert into @Tien(ten,Tien) select 4,sum(Tien) from @Congno\r\n\tselect * from @Tien;\r\nend;";
                cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);

                sql = $"INSERT INTO bctc VALUES ({j},{dt.Rows[i][0].ToString()},{dt.Rows[i][1].ToString()},@a)," +
                    $"({j+1},{dt.Rows[i][0].ToString()},{dt.Rows[i][1].ToString()},@b)," +
                    $"({j+2},{dt.Rows[i][0].ToString()},{dt.Rows[i][1].ToString()},@c)," +
                    $"({j+3},{dt.Rows[i][0].ToString()},{dt.Rows[i][1].ToString()},@d)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[0][1].ToString());
                cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[1][1].ToString());
                cmd.Parameters.Add("@c", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[2][1].ToString());
                cmd.Parameters.Add("@d", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[3][1].ToString());
                reader = cmd.ExecuteReader();
                reader.Close();
                j+=4;
            }

            sql = $"select * from bctc order by ma asc";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            q = dt.Rows.Count/4;

            close();
            return dt;
        }
        public DataTable bieu_do(DateTime Date1, DateTime Date2,ref int q)
        {
            open();
            int j = 1;
            int day = 0;
            TimeSpan span = Date2.Subtract(Date1);
            string date1 = $"{Date1.Month}/{Date1.Day}/{Date1.Year}";
            string date2 = $"{Date2.Month} / {Date2.Day} / {Date2.Year}";

            //Thoi Gian

            string sql = $"begin declare @ThoiGian TABLE(thoigian date);" +
            $"\r\n\t\t\t\tinsert into @ThoiGian(thoigian) select distinct ThoiGian from HoaDon;" +
                $"\r\n\t\t\t\tselect distinct * from @ThoiGian where thoigian>='{date1}' and thoigian<='{date2}';\r\nend;";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            sql = $"delete from ThoiGian";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            reader.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sql = $"insert into ThoiGian values({Convert.ToDateTime(dt.Rows[i][0]).Day},{Convert.ToDateTime(dt.Rows[i][0]).Month},{Convert.ToDateTime(dt.Rows[i][0]).Year})";
                cmd = new SqlCommand(sql, con);
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            sql = $"select * from ThoiGian";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            int n = dt.Rows.Count / 6;
            if (n == 0) n = 1;
            int a = 0;
            if (dt.Rows.Count / 6>=1)
            {
                a = 6;
            }
            else if(dt.Rows.Count / 6<1)
            {
                a = dt.Rows.Count;
            }

            sql = $"delete from bctc2";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Close();

            for (int i = 0; i < dt.Rows.Count-n+1; i+=n)
            {
                if (j == 4*a -3)
                {
                    x.hien_thi($"{dt.Rows[i][1]}/{dt.Rows[i][0]}/{dt.Rows[i][2]}", $"{dt.Rows[dt.Rows.Count - 1][1]}/{dt.Rows[dt.Rows.Count - 1][0]}/{dt.Rows[dt.Rows.Count - 1][2]}");
                    y.hien_thi($"{dt.Rows[i][1]}/{dt.Rows[i][0]}/{dt.Rows[i][2]}", $"{dt.Rows[dt.Rows.Count - 1][1]}/{dt.Rows[dt.Rows.Count - 1][0]}/{dt.Rows[dt.Rows.Count - 1][2]}");
                    z.hien_thi($"{dt.Rows[i][1]}/{dt.Rows[i][0]}/{dt.Rows[i][2]}", $"{dt.Rows[dt.Rows.Count - 1][1]}/{dt.Rows[dt.Rows.Count - 1][0]}/{dt.Rows[dt.Rows.Count - 1][2]}");
                    m.hien_thi($"{dt.Rows[i][1]}/{dt.Rows[i][0]}/{dt.Rows[i][2]}", $"{dt.Rows[dt.Rows.Count - 1][1]}/{dt.Rows[dt.Rows.Count - 1][0]}/{dt.Rows[dt.Rows.Count - 1][2]}");

                    sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tten int,\r\n\t\tTien money\r\n\t);\r\n\tdeclare @Congno TABLE(\r\n\t\tTien money\r\n\t);" +
                        $"\r\n\tinsert into @Tien(ten,Tien) select 1,ThuNhap from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Tien(ten,Tien) select 2,DoanhThu from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Tien(ten,Tien) select 3,TongChi from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoNVC where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiTra) from CongNoNCC where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoSTMDT where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Tien(ten,Tien) select 4,sum(Tien) from @Congno\r\n\tselect * from @Tien;\r\nend;";
                    cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);

                    sql = $"INSERT INTO bctc2 VALUES ({j},{dt.Rows[i][0]},{dt.Rows[i][1]},{dt.Rows[i][2]},{dt.Rows[dt.Rows.Count - 1][0]},{dt.Rows[dt.Rows.Count - 1][1]},{dt.Rows[dt.Rows.Count - 1][2]},@a)," +
                    $"({j + 1},{dt.Rows[i][0]},{dt.Rows[i][1]},{dt.Rows[i][2]},{dt.Rows[dt.Rows.Count - 1][0]},{dt.Rows[dt.Rows.Count - 1][1]},{dt.Rows[dt.Rows.Count - 1][2]},@b)," +
                    $"({j + 2},{dt.Rows[i][0]},{dt.Rows[i][1]},{dt.Rows[i][2]},{dt.Rows[dt.Rows.Count - 1][0]},{dt.Rows[dt.Rows.Count - 1][1]},{dt.Rows[dt.Rows.Count - 1][2]},@c)," +
                    $"({j + 3},{dt.Rows[i][0]},{dt.Rows[i][1]},{dt.Rows[i][2]},{dt.Rows[dt.Rows.Count - 1][0]},{dt.Rows[dt.Rows.Count - 1][1]},{dt.Rows[dt.Rows.Count - 1][2]},@d)";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[0][1].ToString());
                    cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[1][1].ToString());
                    cmd.Parameters.Add("@c", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[2][1].ToString());
                    cmd.Parameters.Add("@d", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[3][1].ToString());
                    reader = cmd.ExecuteReader();
                    reader.Close();
                    j+=4;
                }
                else
                {
                    x.hien_thi($"{dt.Rows[i][1]}/{dt.Rows[i][0]}/{dt.Rows[i][2]}", $"{dt.Rows[i + n - 1][1]}/{dt.Rows[i + n - 1][0]}/{dt.Rows[i + n - 1][2]}");
                    y.hien_thi($"{dt.Rows[i][1]}/{dt.Rows[i][0]}/{dt.Rows[i][2]}", $"{dt.Rows[i + n - 1][1]}/{dt.Rows[i + n - 1][0]}/{dt.Rows[i + n - 1][2]}");
                    z.hien_thi($"{dt.Rows[i][1]}/{dt.Rows[i][0]}/{dt.Rows[i][2]}", $"{dt.Rows[i + n - 1][1]}/{dt.Rows[i + n - 1][0]}/{dt.Rows[i + n - 1][2]}");
                    m.hien_thi($"{dt.Rows[i][1]}/{dt.Rows[i][0]}/{dt.Rows[i][2]}", $"{dt.Rows[i + n - 1][1]}/{dt.Rows[i + n - 1][0]}/{dt.Rows[i + n - 1][2]}");

                    sql = $"begin \r\n\tdeclare @Tien TABLE(\r\n\t\tten int,\r\n\t\tTien money\r\n\t);\r\n\tdeclare @Congno TABLE(\r\n\t\tTien money\r\n\t);" +
                        $"\r\n\tinsert into @Tien(ten,Tien) select 1,ThuNhap from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Tien(ten,Tien) select 2,DoanhThu from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Tien(ten,Tien) select 3,TongChi from SoQuy where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoNVC where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiTra) from CongNoNCC where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Congno(Tien) select sum(CongNoPhaiThu+CongNoPhaiTra) from CongNoSTMDT where TaiKhoan='{bientoancuc.tk}';" +
                        $"\r\n\tinsert into @Tien(ten,Tien) select 4,sum(Tien) from @Congno\r\n\tselect * from @Tien;\r\nend;";
                    cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);

                    sql = $"INSERT INTO bctc2 VALUES " +
                        $"({j},{dt.Rows[i][0].ToString()},{dt.Rows[i][1].ToString()},{dt.Rows[i][2]},{dt.Rows[i + n - 1][0]},{dt.Rows[i + n - 1][1]},{dt.Rows[i + n - 1][2]},@a)," +
                    $"({j + 1},{dt.Rows[i][0].ToString()},{dt.Rows[i][1].ToString()},{dt.Rows[i][2]},{dt.Rows[i + n - 1][0]},{dt.Rows[i + n - 1][1]},{dt.Rows[i + n - 1][2]},@b)," +
                    $"({j + 2},{dt.Rows[i][0].ToString()},{dt.Rows[i][1].ToString()},{dt.Rows[i][2]},{dt.Rows[i + n - 1][0]},{dt.Rows[i + n - 1][1]},{dt.Rows[i + n - 1][2]},@c)," +
                    $"({j + 3},{dt.Rows[i][0].ToString()},{dt.Rows[i][1].ToString()},{dt.Rows[i][2]},{dt.Rows[i + n - 1][0]},{dt.Rows[i + n - 1][1]},{dt.Rows[i + n - 1][2]},@d)";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[0][1].ToString());
                    cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[1][1].ToString());
                    cmd.Parameters.Add("@c", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[2][1].ToString());
                    cmd.Parameters.Add("@d", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[3][1].ToString());
                    reader = cmd.ExecuteReader();
                    reader.Close();
                    j+=4;
                }
            }

            sql = $"select * from bctc2 order by ma asc";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            q = dt.Rows.Count/4;

            close();
            return dt;
        }
    }
}
