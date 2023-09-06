using Microsoft.ML.Data;
using Microsoft.ML;
using Nghien_cuu_ung_dung.Ngoaile;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ML.DataOperationsCatalog;
using static IronPython.Modules.PythonCsvModule;
using System.Windows.Controls;
using System.IO.Packaging;

namespace Nghien_cuu_ung_dung.Entity
{
    internal class Entity_du_bao
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
        public DataTable Du_bao_hang_hoa_ban_chay()
        {
            open();

            DateTime dateTime = DateTime.Now;
            int thang = dateTime.Month + 1;
            if (thang == 13)
            {
                thang = 1;
            }

            string sql = $"select SP.MaSP,SP.TenSP,SP_HD.SLXuat,HD.ThoiGian from HoaDon HD,SanPham SP,SanPham_HoaDon SP_HD where HD.TaiKhoan='{bientoancuc.tk}' and SP_HD.SoHD=HD.SoHD and SP_HD.MaSP=SP.MaSP";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                reader.Close();
                return null;
            }
            reader.Close();

            sql = $"select SP.MaSP,SP.TenSP,SP_HD.SLXuat,HD.ThoiGian from HoaDon HD,SanPham SP,SanPham_HoaDon SP_HD where HD.TaiKhoan='{bientoancuc.tk}' and SP_HD.SoHD=HD.SoHD and SP_HD.MaSP=SP.MaSP";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            sql = $"DELETE FROM ThongKe";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Close();

            bool check1 = false;

            if (thang >= 3 && thang <= 5)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataRow = dt.Rows[i];
                    if (Convert.ToDateTime(dataRow[3]).Month >= 3 && Convert.ToDateTime(dataRow[3]).Month <= 5)
                    {
                        check1 = true;
                        sql = $"INSERT INTO ThongKe VALUES ({dataRow[0].ToString()},N'{dataRow[1].ToString().TrimEnd()}',{dataRow[2].ToString()},{Convert.ToDateTime(dataRow[3]).Month},{Convert.ToDateTime(dataRow[3]).Year})";
                        cmd = new SqlCommand(sql, con);
                        reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                }
            }
            else if (thang >= 4 && thang <= 6)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataRow = dt.Rows[i];
                    if (Convert.ToDateTime(dataRow[3]).Month >= 4 && Convert.ToDateTime(dataRow[3]).Month <= 6)
                    {
                        check1 = true;
                        sql = $"INSERT INTO ThongKe VALUES ({dataRow[0].ToString()},N'{dataRow[1].ToString().TrimEnd()}',{dataRow[2].ToString()},{Convert.ToDateTime(dataRow[3]).Month},{Convert.ToDateTime(dataRow[3]).Year})";
                        cmd = new SqlCommand(sql, con);
                        reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                }
            }
            else if (thang >= 9 && thang <= 11)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataRow = dt.Rows[i];
                    if (Convert.ToDateTime(dataRow[3]).Month >= 9 && Convert.ToDateTime(dataRow[3]).Month <= 11)
                    {
                        check1 = true;
                        sql = $"INSERT INTO ThongKe VALUES ({dataRow[0].ToString()},N'{dataRow[1].ToString().TrimEnd()}',{dataRow[2].ToString()},{Convert.ToDateTime(dataRow[3]).Month},{Convert.ToDateTime(dataRow[3]).Year})";
                        cmd = new SqlCommand(sql, con);
                        reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                }
            }
            else if (thang == 1 || thang == 12 || thang == 2)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataRow = dt.Rows[i];
                    if (Convert.ToDateTime(dataRow[3]).Month == 1 || Convert.ToDateTime(dataRow[3]).Month == 12 || Convert.ToDateTime(dataRow[3]).Month == 2)
                    {
                        check1 = true;
                        sql = $"INSERT INTO ThongKe VALUES ({dataRow[0].ToString()},N'{dataRow[1].ToString().TrimEnd()}',{dataRow[2].ToString()},{Convert.ToDateTime(dataRow[3].ToString).Month},{Convert.ToDateTime(dataRow[3]).Year})";
                        cmd = new SqlCommand(sql, con);
                        reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                }
            }

            if (check1 = false)
            {
                return null;
            }

            sql = $"begin \r\n\tdeclare @Dubao TABLE(\r\n\t\tma int,\r\n\t\tten nvarchar(100),\r\n\t\tsl int,\r\n\t\tgt char(5),\r\n\t\tthang int\r\n\t);" +
                $"\r\n\tinsert into @Dubao(ma,ten,sl,gt,thang) select ma,ten,sum(sl),'nam',thang from ThongKe where ten like N'%nam%' group by ma,ten,thang,nam;" +
                $"\r\n\tinsert into @Dubao(ma,ten,sl,gt,thang) select ma,ten,sum(sl),'nu',thang from ThongKe where ten like N'%nữ%' group by ma,ten,thang,nam;" +
                $"\r\n\t\r\n\tselect * from @Dubao;\r\nend;";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            sql = $"select distinct ma from ThongKe where thang={thang}";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);

            sql = $"DELETE FROM hangbanchay";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Close();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                bool check = false;

                DataTable dataTable = new DataTable("DOANH THU");
                DataColumn thoigian = new DataColumn("thoigian");
                thoigian.DataType = typeof(int);
                thoigian.AllowDBNull = false;
                dataTable.Columns.Add(thoigian);
                DataColumn thanhtien = new DataColumn("doanhthu");
                thanhtien.DataType = typeof(int);
                thanhtien.AllowDBNull = false;
                dataTable.Columns.Add(thanhtien);
                DataRow dr1 = dt2.Rows[i];
                string ten = "", gt = "";

                int m = 1;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow dr2 = dt.Rows[j];
                    if (dr1[0].ToString() == dr2[0].ToString())
                    {
                        m++;
                        check = true;
                        ten = dr2[1].ToString().TrimEnd();
                        gt = dr2[3].ToString().TrimEnd();
                        dataTable.Rows.Add(Convert.ToInt32(dr2[4]), Convert.ToInt32(dr2[2]));
                    }
                }
                if (check)
                {
                    MLContext mlContext = new MLContext();
                    Doanh_thu[] doanhthu = new Doanh_thu[m - 1];
                    for (int j = 0; j < m - 1; j++)
                    {
                        DataRow dataRow = dataTable.Rows[j];
                        doanhthu[j] = new Doanh_thu() { Size = Convert.ToInt32(dataRow[0]), Price = Convert.ToSingle(dataRow[1]) };
                    }

                    IDataView alldata = mlContext.Data.LoadFromEnumerable(doanhthu);
                    TrainTestData splitDataView = mlContext.Data.TrainTestSplit(alldata, testFraction: 0.2);

                    var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
                    .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

                    var model = pipeline.Fit(splitDataView.TrainSet);

                    RegressionMetrics metrics = mlContext.Regression.Evaluate(splitDataView.TestSet, labelColumnName: "Size", scoreColumnName: "Price");

                    mlContext.Model.Save(model, splitDataView.TrainSet.Schema, $"dubaomodel{dr1[0].ToString()}.zip");

                    DataViewSchema modelSchema;
                    var modelDaLuu = mlContext.Model.Load($"dubaomodel{dr1[0].ToString()}.zip", out modelSchema);

                    var input = new Doanh_thu() { Size = thang };
                    var output1 = mlContext.Model.CreatePredictionEngine<Doanh_thu, Du_bao>(modelDaLuu).Predict(input);

                    sql = $"insert into hangbanchay values({dr1[0].ToString()},N'{ten}','{gt}',@a)";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(output1.Price.ToString());
                    reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }

            sql = $"select * from hangbanchay order by sl desc";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            close();
            return dt;
        }
        public DataTable Du_bao_doanh_thu()
        {
            open();

            //tong chi

            string sql = $"select ThoiGian,TienThanhToan from HoaDon " +
                $"where TaiKhoan='{bientoancuc.tk}' and LoaiHD like N'%Loại 4%' or LoaiHD like N'%Loại 6%' or LoaiHD like N'%Loại 9%' or LoaiHD like N'%Loại 11%'";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            sql = $"DELETE FROM TongChi";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            reader.Close();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataRow dataRow = dt.Rows[j];
                sql = $"INSERT INTO TongChi VALUES ({Convert.ToDateTime(dataRow[0]).Month},{Convert.ToDateTime(dataRow[0]).Year},@a)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(dataRow[1].ToString());
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            DataTable dataTable = new DataTable("DOANH THU");
            DataColumn thoigian = new DataColumn("thoigian");
            thoigian.DataType = typeof(int);
            thoigian.AllowDBNull = false;
            dataTable.Columns.Add(thoigian);
            DataColumn thanhtien = new DataColumn("doanhthu");
            thanhtien.DataType = typeof(decimal);
            thanhtien.AllowDBNull = false;
            dataTable.Columns.Add(thanhtien);

            sql = $"select thang,Sum(tien) from TongChi group by thang,nam";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            int i = 1;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataTable.Rows.Add(reader.GetInt32(0), reader.GetDecimal(1) / 1000);
                    i++;
                }
            }
            reader.Close();

            MLContext mlContext = new MLContext();
            Doanh_thu[] doanhthu = new Doanh_thu[i - 1];
            for (int j = 0; j < i - 1; j++)
            {
                DataRow dataRow = dataTable.Rows[j];
                doanhthu[j] = new Doanh_thu() { Size = Convert.ToInt32(dataRow[0]), Price = Convert.ToSingle(dataRow[1]) };
            }

            IDataView alldata = mlContext.Data.LoadFromEnumerable(doanhthu);
            TrainTestData splitDataView = mlContext.Data.TrainTestSplit(alldata, testFraction: 0.2);

            var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
            .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

            var model = pipeline.Fit(splitDataView.TrainSet);

            RegressionMetrics metrics = mlContext.Regression.Evaluate(splitDataView.TestSet, labelColumnName: "Size", scoreColumnName: "Price");

            mlContext.Model.Save(model, splitDataView.TrainSet.Schema, "dubaomodel1.zip");

            //tong thu

            sql = $"begin \r\n\tdeclare @Dubao TABLE(\r\n\t\tthoigian date,\r\n\t\ttien money\r\n\t);\r\n\t" +
                $"insert into @Dubao(thoigian,tien) select ThoiGian,TienHang from HoaDon where TaiKhoan='{bientoancuc.tk}' and LoaiHD = N'Loại 1';\r\n\t" +
                $"insert into @Dubao(thoigian,tien) select ThoiGian,(TienVanChuyen+TienHang) from HoaDon \r\n\t" +
                $"where TaiKhoan='{bientoancuc.tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' and NguoiNhanTien=N'Cửa hàng';\r\n\t" +
                $"insert into @Dubao(thoigian,tien) select ThoiGian,(HD.TienHang*(NVC.COD+1)) from HoaDon HD, CongNoNVC NVC \r\n\t" +
                $"where HD.TaiKhoan='{bientoancuc.tk}' and HD.LoaiHD like N'%Loại 2%' and HD.DichVuShipCOD='Yes' and HD.NguoiNhanTien=N'Cửa hàng' and CHARINDEX(NVC.MaNVC,HD.LoaiHD)>0;\r\n\t" +
                $"insert into @Dubao(thoigian,tien) select ThoiGian,TienThanhToan from HoaDon where TaiKhoan='{bientoancuc.tk}' and LoaiHD like N'%Loại 5%';\r\n\t" +
                $"insert into @Dubao(thoigian,tien) select ThoiGian,TienThanhToan from HoaDon where TaiKhoan='{bientoancuc.tk}' and LoaiHD like N'%Loại 10%';\r\n\t" +
                $"select * from @Dubao;\r\nend;";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            sql = $"DELETE FROM TongThu";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Close();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataRow dataRow = dt.Rows[j];
                sql = $"INSERT INTO TongThu VALUES ({Convert.ToDateTime(dataRow[0]).Month},{Convert.ToDateTime(dataRow[0]).Year},@a)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(dataRow[1].ToString());
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            dataTable = new DataTable("DOANH THU");
            thoigian = new DataColumn("thoigian");
            thoigian.DataType = typeof(int);
            thoigian.AllowDBNull = false;
            dataTable.Columns.Add(thoigian);
            thanhtien = new DataColumn("doanhthu");
            thanhtien.DataType = typeof(decimal);
            thanhtien.AllowDBNull = false;
            dataTable.Columns.Add(thanhtien);

            sql = $"select thang,Sum(tien) from TongThu group by thang,nam";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            i = 1;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataTable.Rows.Add(reader.GetInt32(0), reader.GetDecimal(1) / 1000);
                    i++;
                }
            }
            reader.Close();

            mlContext = new MLContext();
            doanhthu = new Doanh_thu[i - 1];
            for (int j = 0; j < i - 1; j++)
            {
                DataRow dataRow = dataTable.Rows[j];
                doanhthu[j] = new Doanh_thu() { Size = Convert.ToInt32(dataRow[0]), Price = Convert.ToSingle(dataRow[1]) };
            }

            alldata = mlContext.Data.LoadFromEnumerable(doanhthu);
            splitDataView = mlContext.Data.TrainTestSplit(alldata, testFraction: 0.2);

            pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
            .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

            model = pipeline.Fit(splitDataView.TrainSet);

            metrics = mlContext.Regression.Evaluate(splitDataView.TestSet, labelColumnName: "Size", scoreColumnName: "Price");

            mlContext.Model.Save(model, splitDataView.TrainSet.Schema, "dubaomodel2.zip");

            //cong no thu

            sql = $"begin \r\n\tdeclare @Dubao TABLE(\r\n\t\tthoigian date,\r\n\t\ttien money\r\n\t);" +
                $"\r\n\tinsert into @Dubao(thoigian,tien) select ThoiGian,TienHang from HoaDon where TaiKhoan='{bientoancuc.tk}' and LoaiHD like N'%Loại 2%' and NguoiNhanTien=N'Shipper';" +
                $"\r\n\tinsert into @Dubao(thoigian,tien) select ThoiGian,(HD.TienHang-STMDT.CuocLechPhiVanChuyen-HD.TienHang*(STMDT.PhiThanhToan+STMDT.PhiCoDinh+STMDT.PhiDichVu)) " +
                $"from HoaDon HD, CongNoSTMDT STMDT\r\n" +
                $"where HD.TaiKhoan='{bientoancuc.tk}' and HD.LoaiHD like N'%Loại 7%' and CHARINDEX(STMDT.MaSTMDT,HD.LoaiHD)>0;" +
                $"\r\n\tselect * from @Dubao;\r\nend;";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            sql = $"DELETE FROM CongNoThu";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Close();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataRow dataRow = dt.Rows[j];
                sql = $"INSERT INTO CongNoThu VALUES ({Convert.ToDateTime(dataRow[0]).Month},{Convert.ToDateTime(dataRow[0]).Year},@a)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(dataRow[1].ToString());
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            dataTable = new DataTable("DOANH THU");
            thoigian = new DataColumn("thoigian");
            thoigian.DataType = typeof(int);
            thoigian.AllowDBNull = false;
            dataTable.Columns.Add(thoigian);
            thanhtien = new DataColumn("doanhthu");
            thanhtien.DataType = typeof(decimal);
            thanhtien.AllowDBNull = false;
            dataTable.Columns.Add(thanhtien);

            sql = $"select thang,Sum(tien) from CongNoThu group by thang,nam";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            i = 1;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataTable.Rows.Add(reader.GetInt32(0), reader.GetDecimal(1) / 1000);
                    i++;
                }
            }
            reader.Close();

            mlContext = new MLContext();
            doanhthu = new Doanh_thu[i - 1];
            for (int j = 0; j < i - 1; j++)
            {
                DataRow dataRow = dataTable.Rows[j];
                doanhthu[j] = new Doanh_thu() { Size = Convert.ToInt32(dataRow[0]), Price = Convert.ToSingle(dataRow[1]) };
            }

            alldata = mlContext.Data.LoadFromEnumerable(doanhthu);
            splitDataView = mlContext.Data.TrainTestSplit(alldata, testFraction: 0.2);

            pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
            .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

            model = pipeline.Fit(splitDataView.TrainSet);

            metrics = mlContext.Regression.Evaluate(splitDataView.TestSet, labelColumnName: "Size", scoreColumnName: "Price");

            mlContext.Model.Save(model, splitDataView.TrainSet.Schema, "dubaomodel3.zip");

            //cong no tra

            sql = $"begin \r\n\tdeclare @Dubao TABLE(\r\n\t\tthoigian date,\r\n\t\ttien money\r\n\t);\r\n\tinsert into @Dubao(thoigian,tien) select ThoiGian,TienVanchuyen from HoaDon " +
                $"\r\n\twhere TaiKhoan='{bientoancuc.tk}' and LoaiHD like N'%Loại 2%' and DichVuShipCOD='No' and NguoiNhanTien=N'Cửa hàng';" +
                $"\r\n\tinsert into @Dubao(thoigian,tien) select ThoiGian,HD.TienHang*NVC.COD from HoaDon HD, CongNoNVC NVC " +
                $"\r\n\twhere HD.TaiKhoan='{bientoancuc.tk}' and HD.LoaiHD like N'%Loại 2%' and HD.DichVuShipCOD='Yes' and HD.NguoiNhanTien=N'Cửa hàng' and CHARINDEX(NVC.MaNVC,HD.LoaiHD)>0;" +
                $"\r\n\tinsert into @Dubao(thoigian,tien) select ThoiGian,TienVanChuyen+TienHang from HoaDon where TaiKhoan='{bientoancuc.tk}' and LoaiHD like N'%Loại 3%';" +
                $"\r\n\tinsert into @Dubao(thoigian,tien) select ThoiGian,HD.TienHang*STMDT.VAT_ThuNhapCaNhan from HoaDon HD, CongNoSTMDT STMDT " +
                $"\r\n\twhere HD.TaiKhoan='{bientoancuc.tk}' and HD.LoaiHD like N'%Loại 8%' and CHARINDEX(STMDT.MaSTMDT,HD.LoaiHD)>0;" +
                $"\r\n\tselect * from @Dubao;\r\nend;";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            sql = $"DELETE FROM CongNoChi";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Close();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataRow dataRow = dt.Rows[j];
                sql = $"INSERT INTO CongNoChi VALUES ({Convert.ToDateTime(dataRow[0]).Month},{Convert.ToDateTime(dataRow[0]).Year},@a)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(dataRow[1].ToString());
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            dataTable = new DataTable("DOANH THU");
            thoigian = new DataColumn("thoigian");
            thoigian.DataType = typeof(int);
            thoigian.AllowDBNull = false;
            dataTable.Columns.Add(thoigian);
            thanhtien = new DataColumn("doanhthu");
            thanhtien.DataType = typeof(decimal);
            thanhtien.AllowDBNull = false;
            dataTable.Columns.Add(thanhtien);

            sql = $"select thang,Sum(tien) from CongNoChi group by thang,nam";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            i = 1;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataTable.Rows.Add(reader.GetInt32(0), reader.GetDecimal(1) / 1000);
                    i++;
                }
            }
            reader.Close();

            mlContext = new MLContext();
            doanhthu = new Doanh_thu[i - 1];
            for (int j = 0; j < i - 1; j++)
            {
                DataRow dataRow = dataTable.Rows[j];
                doanhthu[j] = new Doanh_thu() { Size = Convert.ToInt32(dataRow[0]), Price = Convert.ToSingle(dataRow[1]) };
            }

            alldata = mlContext.Data.LoadFromEnumerable(doanhthu);
            splitDataView = mlContext.Data.TrainTestSplit(alldata, testFraction: 0.2);

            pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
            .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

            model = pipeline.Fit(splitDataView.TrainSet);

            metrics = mlContext.Regression.Evaluate(splitDataView.TestSet, labelColumnName: "Size", scoreColumnName: "Price");

            mlContext.Model.Save(model, splitDataView.TrainSet.Schema, "dubaomodel4.zip");

            //du bao

            DataViewSchema modelSchema;
            var modelDaLuu = mlContext.Model.Load("dubaomodel1.zip", out modelSchema);

            DateTime dateTime = DateTime.Now;
            int thang = dateTime.Month + 1;
            int ngay = dateTime.Day;
            if (thang == 13)
            {
                thang = 1;
            }

            if (ngay <= 20)
            {
                thang = thang - 1;
                if (thang == 0)
                {
                    thang = 12;
                }
            }
            var input = new Doanh_thu() { Size = thang };
            var output1 = mlContext.Model.CreatePredictionEngine<Doanh_thu, Du_bao>(modelDaLuu).Predict(input);
            //output1.Price;

            modelDaLuu = mlContext.Model.Load("dubaomodel2.zip", out modelSchema);

            var output2 = mlContext.Model.CreatePredictionEngine<Doanh_thu, Du_bao>(modelDaLuu).Predict(input);
            //output2.Price;

            modelDaLuu = mlContext.Model.Load("dubaomodel3.zip", out modelSchema);

            var output3 = mlContext.Model.CreatePredictionEngine<Doanh_thu, Du_bao>(modelDaLuu).Predict(input);
            //output3.Price;

            modelDaLuu = mlContext.Model.Load("dubaomodel4.zip", out modelSchema);

            var output4 = mlContext.Model.CreatePredictionEngine<Doanh_thu, Du_bao>(modelDaLuu).Predict(input);
            //output4.Price;

            sql = $"DELETE FROM DuBao";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Close();

            string e = x.tk_tien_mat_bang(bientoancuc.tk, $"{dateTime.Month}/1/{dateTime.Year - 1}", $"{dateTime.Month}/1/{dateTime.Year - 1}").ToString();
            sql = $"INSERT INTO DuBao VALUES (1,@e),(2,@a),(3,@b),(4,@c),(5,@d)";
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@e", SqlDbType.Decimal).Value = decimal.Parse(e);
            cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse((output1.Price * 1000).ToString());
            cmd.Parameters.Add("@b", SqlDbType.Decimal).Value = decimal.Parse((output2.Price * 1000).ToString());
            cmd.Parameters.Add("@c", SqlDbType.Decimal).Value = decimal.Parse((output3.Price * 1000).ToString());
            cmd.Parameters.Add("@d", SqlDbType.Decimal).Value = decimal.Parse((output4.Price * 1000).ToString());
            reader = cmd.ExecuteReader();
            reader.Close();

            sql = $"select * from DuBao";
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            close();
            return dt;
        }
        public DataTable hien_thi_doanh_thu()
        {
            open();

            DateTime dateTime = DateTime.Now;
            int thang = dateTime.Month;
            int nam = dateTime.Year;
            string sql;
            DataTable dt = null;

            sql = $"begin declare @thoigian TABLE(thang int,nam int);\r\n\t\t\t\tinsert into @thoigian(thang,nam) select distinct thang,nam from TongThu;" +
                $"\r\n\t\t\t\tinsert into @thoigian(thang,nam) select distinct thang,nam from TongChi;" +
                $"\r\n\t\t\t\tinsert into @thoigian(thang,nam) select distinct thang,nam from CongNoChi;" +
                $"\r\n\t\t\t\tinsert into @thoigian(thang,nam) select distinct thang,nam from CongNoThu;" +
                $"\r\n\t\t\t\tselect distinct * from @thoigian where (nam={nam - 1} and thang>{12 + thang - 6}) or nam={nam} order by nam desc;\r\nend;";
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

                sql = $"begin declare @thoigian TABLE(thang int,nam int);\r\n\t\t\t\tinsert into @thoigian(thang,nam) select distinct thang,nam from TongThu;" +
                $"\r\n\t\t\t\tinsert into @thoigian(thang,nam) select distinct thang,nam from TongChi;" +
                $"\r\n\t\t\t\tinsert into @thoigian(thang,nam) select distinct thang,nam from CongNoChi;" +
                $"\r\n\t\t\t\tinsert into @thoigian(thang,nam) select distinct thang,nam from CongNoThu;" +
                $"\r\n\t\t\t\tselect distinct * from @thoigian where (nam={nam - 1} and thang>{12 + thang - 6}) or nam={nam} order by nam desc;\r\nend;";
                cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            sql = $"delete from doanhthu";
            cmd = new SqlCommand(sql, con);
            DbDataReader reader = cmd.ExecuteReader();
            reader.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sql = $"begin \r\n\tdeclare @DoanhThu TABLE(\r\n\t\ttien money\r\n\t);" +
                $"\r\n\tinsert into @DoanhThu(tien) select sum(tien) from TongThu where thang={dt.Rows[i][0].ToString()} and nam={dt.Rows[i][1].ToString()} group by thang,nam;" +
                $"\r\n\tinsert into @DoanhThu(tien) select sum(tien)*-1 from TongChi where thang={dt.Rows[i][0].ToString()} and nam={dt.Rows[i][1].ToString()} group by thang,nam;" +
                $"\r\n\tinsert into @DoanhThu(tien) select sum(tien)*-1 from CongNoChi where thang={dt.Rows[i][0].ToString()} and nam={dt.Rows[i][1].ToString()} group by thang,nam;" +
                $"\r\n\tinsert into @DoanhThu(tien) select sum(tien) from CongNoThu where thang={dt.Rows[i][0].ToString()} and nam={dt.Rows[i][1].ToString()} group by thang,nam;" +
                $"\r\n\tinsert into @DoanhThu(tien) select sum(TienThanhToan)*-1 from HoaDon " +
                $"where TaiKhoan='{bientoancuc.tk}' and LoaiHD like N'%Loại 11%' and CHARINDEX(N'Tiền nhà',LoaiHD)>0 and ThoiGian>='{thang}/1/{nam - 1}' and ThoiGian<='{thang - 1}/1/{nam - 1}';" +
                $"\r\n\tselect sum(tien) from @DoanhThu;" +
                $"\r\nend;";
                cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);

                sql = $"INSERT INTO doanhthu VALUES ({dt.Rows[i][0].ToString()},{dt.Rows[i][1].ToString()},@a)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@a", SqlDbType.Decimal).Value = decimal.Parse(dt1.Rows[0][0].ToString().ToString());
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            sql = $"select * from doanhthu order by nam asc";
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
