using LiveCharts;
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
using LiveCharts.Wpf;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization;
using System.Data;
using Nghien_cuu_ung_dung.Control;
using Nghien_cuu_ung_dung.Ngoaile;
using LiveCharts.Definitions.Charts;
using static IronPython.Modules.PythonWeakRef;
using System.Windows.Threading;
using Nghien_cuu_ung_dung.MessageBox;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Kiem_Tra_Giao_Dich.xaml
    /// </summary>
    public partial class Kiem_Tra_Giao_Dich : UserControl
    {
        Control_quan_ly_giao_dich x = new Control_quan_ly_giao_dich();
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        public Kiem_Tra_Giao_Dich()
        {
            InitializeComponent();
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(60);
            timer1.Tick += timer_Tick1;
            timer1.Start();
        }
        void CapNhat()
        {
            DataTable dataTable = x.hien_thi();
            DataRow dr = dataTable.Rows[0];
            TextBlock1_Tien_mat1.Text = bientoancuc.chuanhoa(dr[1].ToString());
            TextBlock1_Tien_chuyen_khoan1.Text = bientoancuc.chuanhoa(dr[2].ToString());
            TextBlock1_Tien_the1.Text = bientoancuc.chuanhoa(dr[3].ToString());
            TextBlock1_Tien_mat2.Text = bientoancuc.chuanhoa(dr[4].ToString());
            TextBlock1_Tien_chuyen_khoan2.Text = bientoancuc.chuanhoa(dr[5].ToString());
            TextBlock1_Tien_the2.Text = bientoancuc.chuanhoa(dr[6].ToString());

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

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

            PieChart1.Series = new SeriesCollection
        {
            new PieSeries
            {

                Title = "Tiền mặt",
                Values = new ChartValues<double> {Convert.ToDouble(dr[1].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush1
            },
            new PieSeries
            {
                Title = "Tiền chuyển khoản",
                Values = new ChartValues<double> {Convert.ToDouble(dr[2].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush3,
            },
            new PieSeries
            {
                Title = "Tiền thẻ",
                Values = new ChartValues<double> {Convert.ToDouble(dr[3].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush2,
            },

        };

            PieChart2.Series = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Tiền mặt",
                Values = new ChartValues<double> {Convert.ToDouble(dr[4].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush1,
            },
            new PieSeries
            {
                Title = "Tiền chuyển khoản",
                Values = new ChartValues<double> {Convert.ToDouble(dr[5].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush3,
            },
            new PieSeries
            {
                Title = "Tiền thẻ",
                Values = new ChartValues<double> {Convert.ToDouble(dr[6].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush2,
            },
        };

            DataContext = this;
        }
        void timer_Tick1(object sender, EventArgs e)
        {
            CapNhat();
        }
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }


        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            CapNhat();
        }
        void timer_Tick2(object sender, EventArgs e)
        {
            string date1 = $"{TimePikcer1.SelectedDate.Value.Month}/{TimePikcer1.SelectedDate.Value.Day}/{TimePikcer1.SelectedDate.Value.Year}";
            string date2 = $"{TimePikcer2.SelectedDate.Value.Month}/{TimePikcer2.SelectedDate.Value.Day}/{TimePikcer2.SelectedDate.Value.Year}";
            DataTable dataTable = x.hien_thi(date1, date2);
            DataRow dr = dataTable.Rows[0];
            TextBlock1_Tien_mat1.Text = bientoancuc.chuanhoa(dr[1].ToString());
            TextBlock1_Tien_chuyen_khoan1.Text = bientoancuc.chuanhoa(dr[2].ToString());
            TextBlock1_Tien_the1.Text = bientoancuc.chuanhoa(dr[3].ToString());
            TextBlock1_Tien_mat2.Text = bientoancuc.chuanhoa(dr[4].ToString());
            TextBlock1_Tien_chuyen_khoan2.Text = bientoancuc.chuanhoa(dr[5].ToString());
            TextBlock1_Tien_the2.Text = bientoancuc.chuanhoa(dr[6].ToString());

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

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

            PieChart1.Series = new SeriesCollection
        {
            new PieSeries
            {

                Title = "Tiền mặt",
                Values = new ChartValues<double> {Convert.ToDouble(dr[1].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush1
            },
            new PieSeries
            {
                Title = "Tiền chuyển khoản",
                Values = new ChartValues<double> {Convert.ToDouble(dr[2].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush3,
            },
            new PieSeries
            {
                Title = "Tiền thẻ",
                Values = new ChartValues<double> {Convert.ToDouble(dr[3].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush2,
            },

        };

            PieChart2.Series = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Tiền mặt",
                Values = new ChartValues<double> {Convert.ToDouble(dr[4].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush1,
            },
            new PieSeries
            {
                Title = "Tiền chuyển khoản",
                Values = new ChartValues<double> {Convert.ToDouble(dr[5].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush3,
            },
            new PieSeries
            {
                Title = "Tiền thẻ",
                Values = new ChartValues<double> {Convert.ToDouble(dr[6].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush2,
            },
        };
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TimePikcer1.Text) && string.IsNullOrEmpty(TimePikcer2.Text))
            {
                CapNhat();
                timer2.Stop();
                timer1.Stop();
                timer1 = new DispatcherTimer();
                timer1.Interval = TimeSpan.FromSeconds(60);
                timer1.Tick += timer_Tick1;
                timer1.Start();
                return;
            }
            if ((string.IsNullOrEmpty(TimePikcer1.Text) && !string.IsNullOrEmpty(TimePikcer2.Text))
                || (string.IsNullOrEmpty(TimePikcer2.Text) && !string.IsNullOrEmpty(TimePikcer1.Text)))
            {
                MS4 ms4 = new MS4();
                ms4.Show();
                return;
            }
            DateTime k;
            if (!DateTime.TryParse(TimePikcer1.Text, out k)
                || !DateTime.TryParse(TimePikcer2.Text, out k))
            {
                MS5 ms5 = new MS5();
                ms5.Show(); return;
            }
            if (Convert.ToDateTime(TimePikcer1.Text) > Convert.ToDateTime(TimePikcer2.Text))
            {
                MS6 ms6 = new MS6();
                ms6.Show(); return;
            }

            timer1.Stop();
            timer2.Stop();
            timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(60);
            timer2.Tick += timer_Tick2;
            timer2.Start();

            string date1 = $"{TimePikcer1.SelectedDate.Value.Month}/{TimePikcer1.SelectedDate.Value.Day}/{TimePikcer1.SelectedDate.Value.Year}";
            string date2 = $"{TimePikcer2.SelectedDate.Value.Month}/{TimePikcer2.SelectedDate.Value.Day}/{TimePikcer2.SelectedDate.Value.Year}";
            DataTable dataTable = x.hien_thi(date1, date2);
            DataRow dr = dataTable.Rows[0];
            TextBlock1_Tien_mat1.Text = bientoancuc.chuanhoa(dr[1].ToString());
            TextBlock1_Tien_chuyen_khoan1.Text = bientoancuc.chuanhoa(dr[2].ToString());
            TextBlock1_Tien_the1.Text = bientoancuc.chuanhoa(dr[3].ToString());
            TextBlock1_Tien_mat2.Text = bientoancuc.chuanhoa(dr[4].ToString());
            TextBlock1_Tien_chuyen_khoan2.Text = bientoancuc.chuanhoa(dr[5].ToString());
            TextBlock1_Tien_the2.Text = bientoancuc.chuanhoa(dr[6].ToString());

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

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

            PieChart1.Series = new SeriesCollection
        {
            new PieSeries
            {

                Title = "Tiền mặt",
                Values = new ChartValues<double> {Convert.ToDouble(dr[1].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush1
            },
            new PieSeries
            {
                Title = "Tiền chuyển khoản",
                Values = new ChartValues<double> {Convert.ToDouble(dr[2].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush3,
            },
            new PieSeries
            {
                Title = "Tiền thẻ",
                Values = new ChartValues<double> {Convert.ToDouble(dr[3].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush2,
            },

        };

            PieChart2.Series = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Tiền mặt",
                Values = new ChartValues<double> {Convert.ToDouble(dr[4].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush1,
            },
            new PieSeries
            {
                Title = "Tiền chuyển khoản",
                Values = new ChartValues<double> {Convert.ToDouble(dr[5].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush3,
            },
            new PieSeries
            {
                Title = "Tiền thẻ",
                Values = new ChartValues<double> {Convert.ToDouble(dr[6].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush2,
            },
        };
        }
    }
      
}

