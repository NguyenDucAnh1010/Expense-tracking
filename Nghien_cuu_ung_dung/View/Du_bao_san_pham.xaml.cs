using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Nghien_cuu_ung_dung.Control;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Du_bao_san_pham.xaml
    /// </summary>
    public class DubaoSP
    {
        private string maSP;
        public string MaSP
        {
            get { return maSP; }
            set { maSP = value; }
        }

        private string tenSP;
        public string TenSP
        {
            get { return tenSP; }
            set { tenSP = value; }
        }
        private string soluong;
        public string SoLuong
        {
            get { return soluong; }
            set { soluong = value; }
        }
    }
    public partial class Du_bao_san_pham : UserControl
    {
        DispatcherTimer timer1 = new DispatcherTimer();
        public Du_bao_san_pham()
        {
            InitializeComponent();
            dataTable = x.Du_bao_hang_hoa_ban_chay();
            int n = 0;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                if (row[2].ToString().TrimEnd() == "nam")
                {
                    n++;
                    if (n == 1)
                    {
                        label1.Text = dataTable.Rows[i][1].ToString();
                    }else if (n == 2)
                    {
                        label2.Text = dataTable.Rows[i][1].ToString();
                    }else if (n == 3)
                    {
                        label3.Text = dataTable.Rows[i][1].ToString();
                    }
                }
            }
            n = 0;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                if (row[2].ToString().TrimEnd() == "nu")
                {
                    n++;
                    if (n == 1)
                    {
                        label4.Text = dataTable.Rows[i][1].ToString();
                    }
                    else if (n == 2)
                    {
                        label5.Text = dataTable.Rows[i][1].ToString();
                    }
                    else if (n == 3)
                    {
                        label6.Text = dataTable.Rows[i][1].ToString();
                    }
                }
            }
            DateTime dateTime = DateTime.Now;
            int thang = dateTime.Month + 1;
            int ngay = dateTime.Day;
            if (ngay <= 20)
            {
                thang = thang - 1;
                if (thang == 0)
                {
                    thang = 12;
                }
            }
            if (thang == 13)
            {
                thang = 1;
            }
            if (thang >= 3 && thang <= 5)
            {
                label7.Text = $"Dự báo số lượng bán tháng {thang} trong mùa xuân";
            }
            else if (thang >= 6 && thang <= 8)
            {
                label7.Text = $"Dự báo số lượng bán tháng {thang} trong mùa hè";
            }
            else if (thang >= 9 && thang <= 11)
            {
                label7.Text = $"Dự báo số lượng bán tháng {thang} trong mùa thu";
            }
            else if (thang == 2 || thang == 12 || thang == 1)
            {
                label7.Text = $"Dự báo số lượng bán tháng {thang} trong mùa đông";
            }
            timer1.Interval = TimeSpan.FromSeconds(60);
            timer1.Tick += timer_Tick1;
            timer1.Start();
        }
        Control_du_bao x=new Control_du_bao();
        DataTable dataTable = new DataTable();
        List<DubaoSP> dubao1 = new List<DubaoSP>();
        List<DubaoSP> dubao2 = new List<DubaoSP>();
        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        void timer_Tick1(object sender, EventArgs e)
        {
            dataTable = x.Du_bao_hang_hoa_ban_chay();
            int n = 0;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                if (row[2].ToString().TrimEnd() == "nam")
                {
                    n++;
                    if (n == 1)
                    {
                        label1.Text = dataTable.Rows[i][1].ToString();
                    }
                    else if (n == 2)
                    {
                        label2.Text = dataTable.Rows[i][1].ToString();
                    }
                    else if (n == 3)
                    {
                        label3.Text = dataTable.Rows[i][1].ToString();
                    }
                }
            }
            n = 0;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                if (row[2].ToString().TrimEnd() == "nu")
                {
                    n++;
                    if (n == 1)
                    {
                        label4.Text = dataTable.Rows[i][1].ToString();
                    }
                    else if (n == 2)
                    {
                        label5.Text = dataTable.Rows[i][1].ToString();
                    }
                    else if (n == 3)
                    {
                        label6.Text = dataTable.Rows[i][1].ToString();
                    }
                }
            }
            DateTime dateTime = DateTime.Now;
            int thang = dateTime.Month + 1;
            int ngay = dateTime.Day;
            if (ngay <= 20)
            {
                thang = thang - 1;
                if (thang == 0)
                {
                    thang = 12;
                }
            }
            if (thang == 13)
            {
                thang = 1;
            }
            if (thang >= 3 && thang <= 5)
            {
                label7.Text = $"Dự báo số lượng bán tháng {thang} trong mùa xuân";
            }
            else if (thang >= 6 && thang <= 8)
            {
                label7.Text = $"Dự báo số lượng bán tháng {thang} trong mùa hè";
            }
            else if (thang >= 9 && thang <= 11)
            {
                label7.Text = $"Dự báo số lượng bán tháng {thang} trong mùa thu";
            }
            else if (thang == 2 || thang == 12 || thang == 1)
            {
                label7.Text = $"Dự báo số lượng bán tháng {thang} trong mùa đông";
            }

            dubao1.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                if (row[2].ToString().TrimEnd() == "nam")
                {
                    dubao1.Add(new DubaoSP()
                    {
                        MaSP = row[0].ToString(),
                        TenSP = row[1].ToString(),
                        SoLuong = row[3].ToString()
                    });
                }
            }
            ListView1.ItemsSource = null;
            ListView1.ItemsSource = dubao1;

            dubao2.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                if (row[2].ToString().TrimEnd() == "nu")
                {
                    dubao2.Add(new DubaoSP()
                    {
                        MaSP = row[0].ToString(),
                        TenSP = row[1].ToString(),
                        SoLuong = row[3].ToString()
                    });
                }
            }
            ListView2.ItemsSource = null;
            ListView2.ItemsSource = dubao2;
        }
        private void ListView1_Loaded(object sender, RoutedEventArgs e)
        {
            dubao1.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                if (row[2].ToString().TrimEnd() == "nam")
                {
                    dubao1.Add(new DubaoSP()
                    {
                        MaSP = row[0].ToString(),
                        TenSP = row[1].ToString(),
                        SoLuong = row[3].ToString()
                    });
                }
            }
            ListView1.ItemsSource = null;
            ListView1.ItemsSource = dubao1;
        }

        private void ListView2_Loaded(object sender, RoutedEventArgs e)
        {
            dubao2.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                if (row[2].ToString().TrimEnd() == "nu")
                {
                    dubao2.Add(new DubaoSP()
                    {
                        MaSP = row[0].ToString(),
                        TenSP = row[1].ToString(),
                        SoLuong = row[3].ToString()
                    });
                }
            }
            ListView2.ItemsSource = null;
            ListView2.ItemsSource = dubao2;
        }
    }
}
