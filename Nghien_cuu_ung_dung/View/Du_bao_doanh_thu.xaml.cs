using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
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
using Nghien_cuu_ung_dung.Control;
using System.Data;
using System.Windows.Threading;
using Nghien_cuu_ung_dung.Ngoaile;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Du_bao_doanh_thu.xaml
    /// </summary>
    public partial class Du_bao_doanh_thu : UserControl
    {
        Control_du_bao x=new Control_du_bao();
        DispatcherTimer timer1 = new DispatcherTimer();
        public Du_bao_doanh_thu()
        {
            InitializeComponent();
            timer1.Interval = TimeSpan.FromSeconds(100);
            timer1.Tick += timer_Tick1;
            timer1.Start();
        }
        void timer_Tick1(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            int thang = dateTime.Month + 1;
            int year = dateTime.Year;
            if (thang == 13)
            {
                thang = 1;
                year += 1;
            }
            int ngay = dateTime.Day;
            if (ngay <= 20)
            {
                thang = thang - 1;
                if (thang == 0)
                {
                    thang = 12;
                }
            }

            DataTable dt = x.Du_bao_doanh_thu();
            textblock1.Text = bientoancuc.chuanhoa(dt.Rows[2][1].ToString());
            textblock2.Text = bientoancuc.chuanhoa(dt.Rows[3][1].ToString());
            textblock3.Text = bientoancuc.chuanhoa(dt.Rows[1][1].ToString());
            textblock5.Text = bientoancuc.chuanhoa(dt.Rows[0][1].ToString());
            textblock5_Copy.Text = bientoancuc.chuanhoa(dt.Rows[4][1].ToString());
            textblock6.Text = bientoancuc.chuanhoa((Convert.ToDecimal(dt.Rows[2][1].ToString()) 
                + Convert.ToDecimal(dt.Rows[3][1].ToString()) 
                - Convert.ToDecimal(dt.Rows[1][1].ToString()) 
                - Convert.ToDecimal(dt.Rows[0][1].ToString()) 
                - Convert.ToDecimal(dt.Rows[4][1].ToString())).ToString());

            var converter1 = new System.Windows.Media.BrushConverter();
            var brush1 = (Brush)converter1.ConvertFromString("#E285DE");

            var converter2 = new System.Windows.Media.BrushConverter();
            var brush2 = (Brush)converter2.ConvertFromString("#4ADAEC");

            var converter3 = new System.Windows.Media.BrushConverter();
            var brush3 = (Brush)converter3.ConvertFromString("#FB529B");

            var converter4 = new System.Windows.Media.BrushConverter();
            var brush4 = (Brush)converter4.ConvertFromString("#7E82FC");

            var converter5 = new System.Windows.Media.BrushConverter();
            var brush5 = (Brush)converter5.ConvertFromString("#f22613");

            var converter6 = new System.Windows.Media.BrushConverter();
            var brush6 = (Brush)converter6.ConvertFromString("#00aa00");

            dt = x.hien_thi_doanh_thu();
            DataContext = null;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu Nhập",
                    Values = new ChartValues<double> { Convert.ToDouble(dt.Rows[0][2]),
                        Convert.ToDouble(dt.Rows[1][2]),
                        Convert.ToDouble(dt.Rows[2][2]),
                        Convert.ToDouble(dt.Rows[3][2]),
                        Convert.ToDouble(dt.Rows[4][2]),
                        Convert.ToDouble(dt.Rows[5][2]),
                        Convert.ToDouble(Convert.ToDecimal(dt.Rows[2][1].ToString())+ Convert.ToDecimal(dt.Rows[3][1].ToString())
                - Convert.ToDecimal(dt.Rows[1][1].ToString())
                - Convert.ToDecimal(dt.Rows[0][1].ToString())
                - Convert.ToDecimal(dt.Rows[4][1].ToString()))},
                    PointForeground = brush2,
                },
            };

            Labels = new[] { $"{dt.Rows[0][0]}/{dt.Rows[0][1]}", $"{dt.Rows[1][0]}/{dt.Rows[1][1]}", $"{dt.Rows[2][0]}/{dt.Rows[2][1]}", $"{dt.Rows[3][0]}/{dt.Rows[3][1]}", $"{dt.Rows[4][0]}/{dt.Rows[4][1]}", $"{dt.Rows[5][0]}/{dt.Rows[5][1]}", $"{thang}/{year}" };
            YFormatter = value => value.ToString("C");
            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            int thang = dateTime.Month+1;
            int year = dateTime.Year;
            if (thang == 13)
            {
                thang = 1;
                year += 1;
            }
            int ngay = dateTime.Day;
            if (ngay <= 20)
            {
                thang = thang - 1;
                if (thang == 0)
                {
                    thang = 12;
                }
            }

            DataTable dt = x.Du_bao_doanh_thu();
            textblock1.Text = bientoancuc.chuanhoa(dt.Rows[2][1].ToString());
            textblock2.Text = bientoancuc.chuanhoa(dt.Rows[3][1].ToString());
            textblock3.Text = bientoancuc.chuanhoa(dt.Rows[1][1].ToString());
            textblock5.Text = bientoancuc.chuanhoa(dt.Rows[0][1].ToString());
            textblock5_Copy.Text = bientoancuc.chuanhoa(dt.Rows[4][1].ToString());
            textblock6.Text = bientoancuc.chuanhoa((Convert.ToDecimal(dt.Rows[2][1].ToString())
                + Convert.ToDecimal(dt.Rows[3][1].ToString())
                - Convert.ToDecimal(dt.Rows[1][1].ToString())
                - Convert.ToDecimal(dt.Rows[0][1].ToString())
                - Convert.ToDecimal(dt.Rows[4][1].ToString())).ToString());

            var converter1 = new System.Windows.Media.BrushConverter();
            var brush1 = (Brush)converter1.ConvertFromString("#E285DE");

            var converter2 = new System.Windows.Media.BrushConverter();
            var brush2 = (Brush)converter2.ConvertFromString("#4ADAEC");

            var converter3 = new System.Windows.Media.BrushConverter();
            var brush3 = (Brush)converter3.ConvertFromString("#FB529B");

            var converter4 = new System.Windows.Media.BrushConverter();
            var brush4 = (Brush)converter4.ConvertFromString("#7E82FC");

            var converter5 = new System.Windows.Media.BrushConverter();
            var brush5 = (Brush)converter5.ConvertFromString("#f22613");

            var converter6 = new System.Windows.Media.BrushConverter();
            var brush6 = (Brush)converter6.ConvertFromString("#00aa00");

            dt = x.hien_thi_doanh_thu();
            if (dt == null)
            {
                DataContext = null;
                return;
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu Nhập",
                    Values = new ChartValues<double> { Convert.ToDouble(dt.Rows[0][2]), 
                        Convert.ToDouble(dt.Rows[1][2]), 
                        Convert.ToDouble(dt.Rows[2][2]),
                        Convert.ToDouble(dt.Rows[3][2]),
                        Convert.ToDouble(dt.Rows[4][2]),
                        Convert.ToDouble(dt.Rows[5][2]),
                        Convert.ToDouble(Convert.ToDecimal(dt.Rows[2][1].ToString())+ Convert.ToDecimal(dt.Rows[3][1].ToString())
                - Convert.ToDecimal(dt.Rows[1][1].ToString())
                - Convert.ToDecimal(dt.Rows[0][1].ToString())
                - Convert.ToDecimal(dt.Rows[4][1].ToString()))},
                    PointForeground = brush2,
                },

            };

            Labels = new[] { $"{dt.Rows[0][0]}/{dt.Rows[0][1]}", $"{dt.Rows[1][0]}/{dt.Rows[1][1]}", $"{dt.Rows[2][0]}/{dt.Rows[2][1]}", $"{dt.Rows[3][0]}/{dt.Rows[3][1]}", $"{dt.Rows[4][0]}/{dt.Rows[4][1]}" , $"{dt.Rows[5][0]}/{dt.Rows[5][1]}", $"{thang}/{year}" };
            YFormatter = value => value.ToString("C");
            DataContext = this;
        }

        private void textblock7_Loaded_1(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            int ngay = dateTime.Day;
            int thang = dateTime.Month + 1;
            if (thang == 13)
            {
                thang = 1;
            }

            if (ngay >20)
            {
                textblock7.Text = (thang).ToString();
            }
            else
            {
                textblock7.Text = (dateTime.Month).ToString();
            }
        }
    }
}
