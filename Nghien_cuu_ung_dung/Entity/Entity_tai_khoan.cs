using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Windows.Media.Media3D;
using IronPython.Modules;
using static IronPython.Modules._ast;
using Nghien_cuu_ung_dung.Ngoaile;

namespace Nghien_cuu_ung_dung.Entity
{
    internal class Entity_tai_khoan
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
        public bool dang_ky(string id, string tenql, string tench, string gmail, string sdt, string mk)
        {
            open();
            string sql = $"select * from QuanLiCuaHang where TaiKhoan='{id}'";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                return false;
            }
            else
            {
                reader.Close();
                sql = $"insert into QuanLiCuaHang values ('{id}',N'{tench}',N'{tenql}','{gmail}','{sdt}','{mk}')";
                cmd.Connection = con;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                reader.Close();
                return true;
            }
            close();
        }

        public bool dang_nhap(string un, string pw)
        {
            open();
            string sql = $"select * from QuanLiCuaHang where TaiKhoan='{un}' and MatKhau='{pw}'";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            close();
        }
        public DataTable hien_thi(string tk)
        {
            open();

            string sql = $"select * from QuanLiCuaHang where TaiKhoan='{tk}'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            close();
            return dt;
        }
        public int KiemTraDangNhap(string un, string pw)
        {
            open();
            string sql = $"select * from QuanLiCuaHang where TaiKhoan='{un}'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            sql = $"select MatKhau from QuanLiCuaHang where TaiKhoan='{un}'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.Rows[0];
            if (pw != dr[0].ToString().TrimEnd())
            {
                return 2;
            }
            return 0;
        }
        public void capnhap_mk(string tk, string mk)
        {
            open();
            string sql = $"UPDATE QuanLiCuaHang SET MatKhau=N'{mk}' where TaiKhoan={tk}";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            reader.Close();
            close();
        }
        public void capnhap_gmail(string tk, string gmail)
        {
            open();
            string sql = $"UPDATE QuanLiCuaHang SET Gmail='{gmail}' where TaiKhoan={tk}";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            reader.Close();
            close();
        }
        public void capnhap_sdt(string tk, string sdt)
        {
            open();
            string sql = $"UPDATE QuanLiCuaHang SET SoDienThoai='{sdt}' where TaiKhoan={tk}";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            reader.Close();
            close();
        }
    }
}
