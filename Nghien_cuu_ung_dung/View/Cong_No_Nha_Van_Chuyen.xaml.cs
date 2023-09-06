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

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Cong_No_Nha_Van_Chuyen.xaml
    /// </summary>
    public class congno
    {
        private string maNVC;
        public string MaNVC
        {
            get { return maNVC; }
            set { maNVC = value; }
        }

        private string tenNVC;
        public string TenNVC
        {
            get { return tenNVC; }
            set { tenNVC = value; }
        }
        private string cod;
        public string COD
        {
            get { return cod; }
            set { cod = value; }
        }
        private string phiShipToiThieu;
        public string PhiShipToiThieu
        {
            get { return phiShipToiThieu; }
            set { phiShipToiThieu = value; }
        }
        private string congNoPhaiThu;
        public string CongNoPhaiThu
        {
            get { return congNoPhaiThu; }
            set { congNoPhaiThu = value; }
        }
        private string congNoPhaiTra;
        public string CongNoPhaiTra
        {
            get { return congNoPhaiTra; }
            set { congNoPhaiTra = value; }
        }
    }
    public partial class Cong_No_Nha_Van_Chuyen : UserControl
    {
        Control_quan_ly_CNNVC x=new Control_quan_ly_CNNVC();
        List<congno> listcongno= new List<congno>();
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        public Cong_No_Nha_Van_Chuyen()
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
            listcongno.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                listcongno.Add(new congno() { MaNVC = row[0].ToString(), TenNVC = row[2].ToString(), COD = row[3].ToString(), PhiShipToiThieu = row[4].ToString(), CongNoPhaiThu = bientoancuc.chuanhoa(row[5].ToString()), CongNoPhaiTra = bientoancuc.chuanhoa(row[6].ToString()) });
            }
            ListView1.ItemsSource = null;
            ListView1.ItemsSource = listcongno;

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

            DataContext = null;
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Công nợ phải thu",
                    Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][5].ToString()), Convert.ToDouble(dataTable.Rows[1][5].ToString()), Convert.ToDouble(dataTable.Rows[2][5].ToString()), Convert.ToDouble(dataTable.Rows[3][5].ToString()), Convert.ToDouble(dataTable.Rows[4][5].ToString()), Convert.ToDouble(dataTable.Rows[5][5].ToString()) },
                    Fill = brush2,
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Công nợ phải trả",
                Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][6].ToString()), Convert.ToDouble(dataTable.Rows[1][6].ToString()), Convert.ToDouble(dataTable.Rows[2][6].ToString()), Convert.ToDouble(dataTable.Rows[3][6].ToString()), Convert.ToDouble(dataTable.Rows[4][6].ToString()), Convert.ToDouble(dataTable.Rows[5][6].ToString()) },
                Fill = brush3,
            });

            //also adding values updates and animates the chart automatically
            Labels = new[] { dataTable.Rows[0][0].ToString(), dataTable.Rows[1][0].ToString(), dataTable.Rows[2][0].ToString(), dataTable.Rows[3][0].ToString(), dataTable.Rows[4][0].ToString(), dataTable.Rows[5][0].ToString() };

            Formatter = value => value.ToString("N");

            DataContext = this;
        }
        void timer_Tick1(object sender, EventArgs e)
        {
            CapNhat();

        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void ListView1_Loaded(object sender, RoutedEventArgs e)
        {
            CapNhat();
        }
        void timer_Tick2(object sender, EventArgs e)
        {
            string date1 = $"{DatePicker1.SelectedDate.Value.Month}/{DatePicker1.SelectedDate.Value.Day}/{DatePicker1.SelectedDate.Value.Year}";
            string date2 = $"{DatePicker2.SelectedDate.Value.Month}/{DatePicker2.SelectedDate.Value.Day}/{DatePicker2.SelectedDate.Value.Year}";
            DataTable dataTable = x.hien_thi(date1, date2);
            listcongno.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                listcongno.Add(new congno() { MaNVC = row[0].ToString(), TenNVC = row[2].ToString(), PhiShipToiThieu = row[4].ToString(), CongNoPhaiThu = bientoancuc.chuanhoa(row[5].ToString()), CongNoPhaiTra = bientoancuc.chuanhoa(row[6].ToString()) });
            }
            ListView1.ItemsSource = null;
            ListView1.ItemsSource = listcongno;

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

            DataContext = null;

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Công nợ phải thu",
                    Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][5].ToString()), Convert.ToDouble(dataTable.Rows[1][5].ToString()), Convert.ToDouble(dataTable.Rows[2][5].ToString()), Convert.ToDouble(dataTable.Rows[3][5].ToString()), Convert.ToDouble(dataTable.Rows[4][5].ToString()), Convert.ToDouble(dataTable.Rows[5][5].ToString()) },
                    Fill = brush2,
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Công nợ phải trả",
                Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][6].ToString()), Convert.ToDouble(dataTable.Rows[1][6].ToString()), Convert.ToDouble(dataTable.Rows[2][6].ToString()), Convert.ToDouble(dataTable.Rows[3][6].ToString()), Convert.ToDouble(dataTable.Rows[4][6].ToString()), Convert.ToDouble(dataTable.Rows[5][6].ToString()) },
                Fill = brush3,
            });

            //also adding values updates and animates the chart automatically
            Labels = new[] { dataTable.Rows[0][0].ToString(), dataTable.Rows[1][0].ToString(), dataTable.Rows[2][0].ToString(), dataTable.Rows[3][0].ToString(), dataTable.Rows[4][0].ToString(), dataTable.Rows[5][0].ToString() };

            Formatter = value => value.ToString("N");

            DataContext = this;
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DatePicker1.Text) && string.IsNullOrEmpty(DatePicker2.Text))
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
            timer2.Interval = TimeSpan.FromSeconds(60);
            timer2.Tick += timer_Tick2;
            timer2.Start();

            string date1 = $"{DatePicker1.SelectedDate.Value.Month}/{DatePicker1.SelectedDate.Value.Day}/{DatePicker1.SelectedDate.Value.Year}";
            string date2 = $"{DatePicker2.SelectedDate.Value.Month}/{DatePicker2.SelectedDate.Value.Day}/{DatePicker2.SelectedDate.Value.Year}";
            DataTable dataTable = x.hien_thi(date1, date2);
            listcongno.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                listcongno.Add(new congno() { MaNVC = row[0].ToString(), TenNVC = row[2].ToString(), PhiShipToiThieu = row[4].ToString(), CongNoPhaiThu = bientoancuc.chuanhoa(row[5].ToString()), CongNoPhaiTra = bientoancuc.chuanhoa(row[6].ToString()) });
            }
            ListView1.ItemsSource = null;
            ListView1.ItemsSource = listcongno;

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

            DataContext = null;

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Công nợ phải thu",
                    Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][5].ToString()), Convert.ToDouble(dataTable.Rows[1][5].ToString()), Convert.ToDouble(dataTable.Rows[2][5].ToString()), Convert.ToDouble(dataTable.Rows[3][5].ToString()), Convert.ToDouble(dataTable.Rows[4][5].ToString()), Convert.ToDouble(dataTable.Rows[5][5].ToString()) },
                    Fill = brush2,
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Công nợ phải trả",
                Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][6].ToString()), Convert.ToDouble(dataTable.Rows[1][6].ToString()), Convert.ToDouble(dataTable.Rows[2][6].ToString()), Convert.ToDouble(dataTable.Rows[3][6].ToString()), Convert.ToDouble(dataTable.Rows[4][6].ToString()), Convert.ToDouble(dataTable.Rows[5][6].ToString()) },
                Fill = brush3,
            });

            //also adding values updates and animates the chart automatically
            Labels = new[] { dataTable.Rows[0][0].ToString(), dataTable.Rows[1][0].ToString(), dataTable.Rows[2][0].ToString(), dataTable.Rows[3][0].ToString(), dataTable.Rows[4][0].ToString(), dataTable.Rows[5][0].ToString() };

            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        private void ListView1_SelectionChanged()
        {

        }
    }
}
