using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
using System.Data;
using Nghien_cuu_ung_dung.Control;
using Nghien_cuu_ung_dung.Ngoaile;
using System.Windows.Threading;
using Nghien_cuu_ung_dung.MessageBox;
using MaterialDesignThemes.Wpf;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for So_quy.xaml
    /// </summary>
    public partial class So_quy : UserControl
    {
        Control_quan_ly_so_quy x = new Control_quan_ly_so_quy();

        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        public So_quy()
        {
            InitializeComponent();
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(10);
            timer1.Tick += timer_Tick1;
            timer1.Start();
            PointLabel = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
        }
        void CapNhat()
        {
            DataTable dataTable = x.hien_thi();
            DataRow dr = dataTable.Rows[0];
            TextBlock1_Doanh_thu.Text = bientoancuc.chuanhoa(dr[1].ToString());
            TextBlock2_Tien_van_chuyen.Text = bientoancuc.chuanhoa(dr[2].ToString());
            TextBlock3_Tien_mat_bang.Text = bientoancuc.chuanhoa(dr[3].ToString());
            TextBlock4_Tien_sinh_hoat.Text = bientoancuc.chuanhoa(dr[4].ToString());
            TextBlock5_Tong_thu.Text = bientoancuc.chuanhoa(dr[5].ToString());
            TextBlock5_Tong_chi.Text = bientoancuc.chuanhoa(dr[6].ToString());

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            //Day la cai dat cho mau
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


            PieChart2.Series = new SeriesCollection
        {
            new PieSeries
            {

                Title = "Thu nhập",
                Values = new ChartValues<double> {Convert.ToDouble(dr[1].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush1,
            },
            new PieSeries
            {
                Title = "Tiền vận chuyển",
                Values = new ChartValues<double> {Convert.ToDouble(dr[2].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush2,
            },
            new PieSeries
            {
                Title = "Tiền mặt bằng",
                Values = new ChartValues<double> {Convert.ToDouble(dr[3].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush3,

            },
             new PieSeries
            {
                Title = "Tiền sinh hoạt",
                Values = new ChartValues<double> {Convert.ToDouble(dr[4].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush4,
            },

        };

            PieChart1.Series = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Doanh thu",
                Values = new ChartValues<double> {Convert.ToDouble(dr[5].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush5,
            },
            new PieSeries
            {
                Title = "Tiền chi",
                Values = new ChartValues<double> {Convert.ToDouble(dr[6].ToString())},
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush6,
            },
        };
        }
        void timer_Tick1(object sender, EventArgs e)
        {
            CapNhat();
            timer2.Stop();
            timer1.Stop();
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(10);
            timer1.Tick += timer_Tick1;
            timer1.Start();
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
            string date1 = $"{DatePicker1.SelectedDate.Value.Month}/{DatePicker1.SelectedDate.Value.Day}/{DatePicker1.SelectedDate.Value.Year}";
            string date2 = $"{DatePicker2.SelectedDate.Value.Month}/{DatePicker2.SelectedDate.Value.Day}/{DatePicker2.SelectedDate.Value.Year}";
            DataTable dataTable = x.hien_thi(date1,date2);
            DataRow dr = dataTable.Rows[0];
            TextBlock1_Doanh_thu.Text = bientoancuc.chuanhoa(dr[1].ToString());
            TextBlock2_Tien_van_chuyen.Text = bientoancuc.chuanhoa(dr[2].ToString());
            TextBlock3_Tien_mat_bang.Text = bientoancuc.chuanhoa(dr[3].ToString());
            TextBlock4_Tien_sinh_hoat.Text = bientoancuc.chuanhoa(dr[4].ToString());
            TextBlock5_Tong_thu.Text = bientoancuc.chuanhoa(dr[5].ToString());
            TextBlock5_Tong_chi.Text = bientoancuc.chuanhoa(dr[6].ToString());
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

            PieChart2.Series = new SeriesCollection
        {
            new PieSeries
            {

                Title = "Thu nhập",
                Values = new ChartValues<double> {Convert.ToDouble(dr[1].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush1,
            },
            new PieSeries
            {
                Title = "Tiền vận chuyển",
                Values = new ChartValues<double> {Convert.ToDouble(dr[2].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush2,
            },
            new PieSeries
            {
                Title = "Tiền mặt bằng",
                Values = new ChartValues<double> {Convert.ToDouble(dr[3].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush3,

            },
             new PieSeries
            {
                Title = "Tiền sinh hoạt",
                Values = new ChartValues<double> {Convert.ToDouble(dr[4].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush4,
            },

        };

            PieChart1.Series = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Doanh thu",
                Values = new ChartValues<double> {Convert.ToDouble(dr[5].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush5,
            },
            new PieSeries
            {
                Title = "Tiền chi",
                Values = new ChartValues<double> {Convert.ToDouble(dr[6].ToString())},
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush6,
            },
        };
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DatePicker1.Text) && string.IsNullOrEmpty(DatePicker2.Text))
            {
                CapNhat();
                timer2.Stop();
                timer1.Stop();
                timer1 = new DispatcherTimer();
                timer1.Interval = TimeSpan.FromSeconds(10);
                timer1.Tick += timer_Tick1;
                timer1.Start();
                return;
            }
            if ((string.IsNullOrEmpty(DatePicker1.Text) && !string.IsNullOrEmpty(DatePicker2.Text))
                || (string.IsNullOrEmpty(DatePicker2.Text) && !string.IsNullOrEmpty(DatePicker1.Text)))
            {
                MS4 ms4 = new MS4();
                ms4.Show();
                return;
            }
            DateTime k;
            if (!DateTime.TryParse(DatePicker1.Text, out k)
                || !DateTime.TryParse(DatePicker2.Text, out k))
            {
                MS5 ms5 = new MS5();
                ms5.Show(); return;
            }
            if (Convert.ToDateTime(DatePicker1.Text) > Convert.ToDateTime(DatePicker2.Text))
            {
                MS6 ms6 = new MS6();
                ms6.Show(); return;
            }

            timer1.Stop();
            timer2.Stop();
            timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(10);
            timer2.Tick += timer_Tick2;
            timer2.Start();

            string date1 = $"{DatePicker1.SelectedDate.Value.Month}/{DatePicker1.SelectedDate.Value.Day}/{DatePicker1.SelectedDate.Value.Year}";
            string date2 = $"{DatePicker2.SelectedDate.Value.Month}/{DatePicker2.SelectedDate.Value.Day}/{DatePicker2.SelectedDate.Value.Year}";
            DataTable dataTable = x.hien_thi(date1, date2);
            DataRow dr = dataTable.Rows[0];
            TextBlock1_Doanh_thu.Text = bientoancuc.chuanhoa(dr[1].ToString());
            TextBlock2_Tien_van_chuyen.Text = bientoancuc.chuanhoa(dr[2].ToString());
            TextBlock3_Tien_mat_bang.Text = bientoancuc.chuanhoa(dr[3].ToString());
            TextBlock4_Tien_sinh_hoat.Text = bientoancuc.chuanhoa(dr[4].ToString());
            TextBlock5_Tong_thu.Text = bientoancuc.chuanhoa(dr[5].ToString());
            TextBlock5_Tong_chi.Text = bientoancuc.chuanhoa(dr[6].ToString());
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

            PieChart2.Series = new SeriesCollection
        {
            new PieSeries
            {

                Title = "Thu nhập",
                Values = new ChartValues<double> {Convert.ToDouble(dr[1].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush1,
            },
            new PieSeries
            {
                Title = "Tiền vận chuyển",
                Values = new ChartValues<double> {Convert.ToDouble(dr[2].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush2,
            },
            new PieSeries
            {
                Title = "Tiền mặt bằng",
                Values = new ChartValues<double> {Convert.ToDouble(dr[3].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush3,

            },
             new PieSeries
            {
                Title = "Tiền sinh hoạt",
                Values = new ChartValues<double> {Convert.ToDouble(dr[4].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush4,
            },

        };

            PieChart1.Series = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Doanh thu",
                Values = new ChartValues<double> {Convert.ToDouble(dr[5].ToString()) },
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush5,
            },
            new PieSeries
            {
                Title = "Tiền chi",
                Values = new ChartValues<double> {Convert.ToDouble(dr[6].ToString())},
                DataLabels = true,
                LabelPoint = labelPoint,
                Fill = brush6,
            },
        };
        }
    }
}
