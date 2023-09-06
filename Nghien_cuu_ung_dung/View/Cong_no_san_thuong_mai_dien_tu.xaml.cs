using LiveCharts;
using LiveCharts.Wpf;
using Nghien_cuu_ung_dung.Control;
using Nghien_cuu_ung_dung.MessageBox;
using Nghien_cuu_ung_dung.Ngoaile;
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

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Cong_no_san_thuong_mai_dien_tu.xaml
    /// </summary>
    public class congnoSTMDT
    {
        private string maSTMDT;
        public string MaSTMDT
        {
            get { return maSTMDT; }
            set { maSTMDT = value; }
        }

        private string tenSTMDT;
        public string TenSTMDT
        {
            get { return tenSTMDT; }
            set { tenSTMDT = value; }
        }
        private string phiThanhToan;
        public string PhiThanhToan
        {
            get { return phiThanhToan; }
            set { phiThanhToan = value; }
        }
        private string phiCoDinh;
        public string PhiCoDinh
        {
            get { return phiCoDinh; }
            set { phiCoDinh = value; }
        }
        private string phiDichVu;
        public string PhiDichVu
        {
            get { return phiDichVu; }
            set { phiDichVu = value; }
        }
        private string cuocPhiLenhVanChuyen;
        public string CuocPhiLenhVanChuyen
        {
            get { return cuocPhiLenhVanChuyen; }
            set { cuocPhiLenhVanChuyen = value; }
        }
        private string thuNhapCaNhan;
        public string ThuNhapCaNhan
        {
            get { return thuNhapCaNhan; }
            set { thuNhapCaNhan = value; }
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
    public partial class Cong_no_san_thuong_mai_dien_tu : UserControl
    {
        Control_quan_ly_CNSTMDT x = new Control_quan_ly_CNSTMDT();
        List<congnoSTMDT> listcongno = new List<congnoSTMDT>();
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        public Cong_no_san_thuong_mai_dien_tu()
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
                listcongno.Add(new congnoSTMDT()
                {
                    MaSTMDT = row[0].ToString(),
                    TenSTMDT = row[2].ToString(),
                    PhiThanhToan = row[3].ToString(),
                    PhiCoDinh = row[4].ToString()
                    ,
                    PhiDichVu = row[5].ToString(),
                    CuocPhiLenhVanChuyen = row[6].ToString(),
                    ThuNhapCaNhan = row[7].ToString(),
                    CongNoPhaiThu = bientoancuc.chuanhoa(row[8].ToString())
                    ,
                    CongNoPhaiTra = bientoancuc.chuanhoa(row[9].ToString())
                });
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
                    Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][8].ToString()), Convert.ToDouble(dataTable.Rows[1][8].ToString()), Convert.ToDouble(dataTable.Rows[2][8].ToString()), Convert.ToDouble(dataTable.Rows[3][8].ToString()) },
                    Fill = brush2,
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Công nợ phải trả",
                Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][9].ToString()), Convert.ToDouble(dataTable.Rows[1][9].ToString()), Convert.ToDouble(dataTable.Rows[2][9].ToString()), Convert.ToDouble(dataTable.Rows[3][9].ToString()) },
                Fill = brush3,
            });

            //also adding values updates and animates the chart automatically
            Labels = new[] { dataTable.Rows[0][0].ToString(), dataTable.Rows[1][0].ToString(), dataTable.Rows[2][0].ToString(), dataTable.Rows[3][0].ToString() };

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

        private void ListView_Loaded(object sender, RoutedEventArgs e)
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
                listcongno.Add(new congnoSTMDT()
                {
                    MaSTMDT = row[0].ToString(),
                    TenSTMDT = row[2].ToString(),
                    PhiThanhToan = row[3].ToString(),
                    PhiCoDinh = row[4].ToString(),
                    PhiDichVu = row[5].ToString(),
                    CuocPhiLenhVanChuyen = row[6].ToString(),
                    ThuNhapCaNhan = row[7].ToString(),
                    CongNoPhaiThu = bientoancuc.chuanhoa(row[8].ToString()),
                    CongNoPhaiTra = bientoancuc.chuanhoa(row[9].ToString())
                });
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
                    Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][8].ToString()), Convert.ToDouble(dataTable.Rows[1][8].ToString()), Convert.ToDouble(dataTable.Rows[2][8].ToString()), Convert.ToDouble(dataTable.Rows[3][8].ToString())},
                    Fill = brush2,
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Công nợ phải trả",
                Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][9].ToString()), Convert.ToDouble(dataTable.Rows[1][9].ToString()), Convert.ToDouble(dataTable.Rows[2][9].ToString()), Convert.ToDouble(dataTable.Rows[3][9].ToString()) },
                Fill = brush3,
            });

            //also adding values updates and animates the chart automatically
            Labels = new[] { dataTable.Rows[0][0].ToString(), dataTable.Rows[1][0].ToString(), dataTable.Rows[2][0].ToString(), dataTable.Rows[3][0].ToString() };

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
                listcongno.Add(new congnoSTMDT()
                {
                    MaSTMDT = row[0].ToString(),
                    TenSTMDT = row[2].ToString(),
                    PhiThanhToan = row[3].ToString(),
                    PhiCoDinh = row[4].ToString(),
                    PhiDichVu = row[5].ToString(),
                    CuocPhiLenhVanChuyen = row[6].ToString(),
                    ThuNhapCaNhan = row[7].ToString(),
                    CongNoPhaiThu = bientoancuc.chuanhoa(row[8].ToString()),
                    CongNoPhaiTra = bientoancuc.chuanhoa(row[9].ToString())
                });
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
                    Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][8].ToString()), Convert.ToDouble(dataTable.Rows[1][8].ToString()), Convert.ToDouble(dataTable.Rows[2][8].ToString()), Convert.ToDouble(dataTable.Rows[3][8].ToString())},
                    Fill = brush2,
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Công nợ phải trả",
                Values = new ChartValues<double> { Convert.ToDouble(dataTable.Rows[0][9].ToString()), Convert.ToDouble(dataTable.Rows[1][9].ToString()), Convert.ToDouble(dataTable.Rows[2][9].ToString()), Convert.ToDouble(dataTable.Rows[3][9].ToString()) },
                Fill = brush3,
            });

            //also adding values updates and animates the chart automatically
            Labels = new[] { dataTable.Rows[0][0].ToString(), dataTable.Rows[1][0].ToString(), dataTable.Rows[2][0].ToString(), dataTable.Rows[3][0].ToString() };

            Formatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
