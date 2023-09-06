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
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Nghien_cuu_ung_dung.MessageBox;
using Nghien_cuu_ung_dung.Ngoaile;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Bao_cao_tai_chinh.xaml
    /// </summary>
    public partial class Bao_cao_tai_chinh : UserControl
    {
        Control_quan_ly_bctc x=new Control_quan_ly_bctc();
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        public Bao_cao_tai_chinh()
        {
            InitializeComponent();
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(60);
            timer1.Tick += timer_Tick1;
            timer1.Start();
            chart1_Copy.Visibility=Visibility.Hidden;
        }
        void CapNhat()
        {
            DataTable dataTable = x.hien_thi();
            textblock1.Text = bientoancuc.chuanhoa(dataTable.Rows[3][1].ToString());
            textblock2.Text = bientoancuc.chuanhoa(dataTable.Rows[1][1].ToString());
            textblock3.Text = bientoancuc.chuanhoa(dataTable.Rows[0][1].ToString());
            textblock4.Text = bientoancuc.chuanhoa(dataTable.Rows[2][1].ToString());

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

            int q = 0;
            DataTable dt = x.bieu_do(ref q);

            if (q == 0)
            {
                chart1.Visibility = Visibility.Hidden;
                chart1_Copy.Visibility = Visibility.Visible;
                DataContext = null;

                SeriesCollection2 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[0][1].ToString()) },
                    Fill = brush2,
                },
                new ColumnSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[3][1].ToString()) },
                    Fill = brush1,
                },
                new ColumnSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[1][1].ToString()) },
                    Fill = brush3,
                },
                new ColumnSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[2][1].ToString()) },
                    Fill = brush4,
                }
            };
                DateTime dt2 = DateTime.Now;
                Labels2 = new[] { $"{dt2.Month}/{dt2.Year}" };

                Formatter = value => value.ToString("N");

                DataContext = this;
                return;
            }
            else if (q == 1)
            {
                chart1.Visibility = Visibility.Hidden;
                chart1_Copy.Visibility = Visibility.Visible;
                DataContext = null;

                SeriesCollection2 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][3])},
                    Fill = brush2,
                },
                new ColumnSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][3])},
                    Fill = brush1,
                },
                new ColumnSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][3]) },
                    Fill = brush3,
                },
                new ColumnSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][3])},
                    Fill = brush4,
                }
            };
                Labels2 = new[] { $"{dt.Rows[0][1]}/{dt.Rows[0][2]}" };

                Formatter = value => value.ToString("C");

                DataContext = this;
            }
            else if (q == 2)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][3]), Convert.ToDouble(dt.Rows[4][3])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][3]), Convert.ToDouble(dt.Rows[7][3])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][3]), Convert.ToDouble(dt.Rows[5][3])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][3]), Convert.ToDouble(dt.Rows[6][3])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][1]}/{dt.Rows[0][2]}", $"{dt.Rows[4][1]}/{dt.Rows[4][2]}" };

                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            else if (q == 3)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][3]), Convert.ToDouble(dt.Rows[4][3]), Convert.ToDouble(dt.Rows[8][3])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][3]), Convert.ToDouble(dt.Rows[7][3]), Convert.ToDouble(dt.Rows[11][3])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][3]), Convert.ToDouble(dt.Rows[5][3]), Convert.ToDouble(dt.Rows[9][3])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][3]), Convert.ToDouble(dt.Rows[6][3]), Convert.ToDouble(dt.Rows[10][3])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][1]}/{dt.Rows[0][2]}", $"{dt.Rows[4][1]}/{dt.Rows[4][2]}", $"{dt.Rows[8][1]}/{dt.Rows[8][2]}" };

                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            else if (q == 4)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][3]), Convert.ToDouble(dt.Rows[4][3]), Convert.ToDouble(dt.Rows[8][3]),Convert.ToDouble(dt.Rows[12][3])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][3]), Convert.ToDouble(dt.Rows[7][3]), Convert.ToDouble(dt.Rows[11][3]),Convert.ToDouble(dt.Rows[15][3])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][3]), Convert.ToDouble(dt.Rows[5][3]), Convert.ToDouble(dt.Rows[9][3]),Convert.ToDouble(dt.Rows[13][3])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][3]), Convert.ToDouble(dt.Rows[6][3]), Convert.ToDouble(dt.Rows[10][3]),Convert.ToDouble(dt.Rows[14][3])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][1]}/{dt.Rows[0][2]}", $"{dt.Rows[4][1]}/{dt.Rows[4][2]}", $"{dt.Rows[8][1]}/{dt.Rows[8][2]}", $"{dt.Rows[12][1]}/{dt.Rows[12][2]}" };

                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            else if (q == 5)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][3]), Convert.ToDouble(dt.Rows[4][3]), Convert.ToDouble(dt.Rows[8][3]),Convert.ToDouble(dt.Rows[12][3]),Convert.ToDouble(dt.Rows[16][3])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][3]), Convert.ToDouble(dt.Rows[7][3]), Convert.ToDouble(dt.Rows[11][3]),Convert.ToDouble(dt.Rows[15][3]),Convert.ToDouble(dt.Rows[19][3])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][3]), Convert.ToDouble(dt.Rows[5][3]), Convert.ToDouble(dt.Rows[9][3]),Convert.ToDouble(dt.Rows[13][3]),Convert.ToDouble(dt.Rows[17][3])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][3]), Convert.ToDouble(dt.Rows[6][3]), Convert.ToDouble(dt.Rows[10][3]),Convert.ToDouble(dt.Rows[14][3]),Convert.ToDouble(dt.Rows[18][3])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][1]}/{dt.Rows[0][2]}", $"{dt.Rows[4][1]}/{dt.Rows[4][2]}", $"{dt.Rows[8][1]}/{dt.Rows[8][2]}", $"{dt.Rows[12][1]}/{dt.Rows[12][2]}", $"{dt.Rows[16][1]}/{dt.Rows[16][2]}" };

                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            else
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;
                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][3]), Convert.ToDouble(dt.Rows[4][3]), Convert.ToDouble(dt.Rows[8][3]),Convert.ToDouble(dt.Rows[12][3]),Convert.ToDouble(dt.Rows[16][3]),Convert.ToDouble(dt.Rows[20][3])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {

                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][3]), Convert.ToDouble(dt.Rows[7][3]), Convert.ToDouble(dt.Rows[11][3]),Convert.ToDouble(dt.Rows[15][3]),Convert.ToDouble(dt.Rows[19][3]),Convert.ToDouble(dt.Rows[23][3])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][3]), Convert.ToDouble(dt.Rows[5][3]), Convert.ToDouble(dt.Rows[9][3]),Convert.ToDouble(dt.Rows[13][3]),Convert.ToDouble(dt.Rows[17][3]),Convert.ToDouble(dt.Rows[21][3])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,

                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][3]), Convert.ToDouble(dt.Rows[6][3]), Convert.ToDouble(dt.Rows[10][3]),Convert.ToDouble(dt.Rows[14][3]),Convert.ToDouble(dt.Rows[18][3]),Convert.ToDouble(dt.Rows[22][3])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }

            };

                Labels = new[] { $"{dt.Rows[0][1]}/{dt.Rows[0][2]}", $"{dt.Rows[4][1]}/{dt.Rows[4][2]}", $"{dt.Rows[8][1]}/{dt.Rows[8][2]}", $"{dt.Rows[12][1]}/{dt.Rows[12][2]}", $"{dt.Rows[16][1]}/{dt.Rows[16][2]}", $"{dt.Rows[20][1]}/{dt.Rows[20][2]}" };
                YFormatter = value => value.ToString("C");
                DataContext = this;
            }
        }
        void timer_Tick1(object sender, EventArgs e)
        {
            CapNhat();   
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels2 { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            CapNhat();
        }
        void timer_Tick2(object sender, EventArgs e)
        {
            string date1 = $"{Timepicker1.SelectedDate.Value.Month}/{Timepicker1.SelectedDate.Value.Day}/{Timepicker1.SelectedDate.Value.Year}";
            string date2 = $"{Timepicker1.SelectedDate.Value.Month}/{Timepicker1.SelectedDate.Value.Day}/{Timepicker1.SelectedDate.Value.Year}";
            DataTable dataTable = x.hien_thi(date1, date2);
            textblock1.Text = bientoancuc.chuanhoa(dataTable.Rows[3][1].ToString());
            textblock2.Text = bientoancuc.chuanhoa(dataTable.Rows[1][1].ToString());
            textblock3.Text = bientoancuc.chuanhoa(dataTable.Rows[0][1].ToString());
            textblock4.Text = bientoancuc.chuanhoa(dataTable.Rows[2][1].ToString());

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

            int q = 0;
            DataTable dt = x.bieu_do(Timepicker1.SelectedDate.Value, Timepicker2.SelectedDate.Value, ref q);

            if (q == 0)
            {
                chart1.Visibility = Visibility.Hidden;
                chart1_Copy.Visibility = Visibility.Visible;
                DataContext = null;

                SeriesCollection2 = new SeriesCollection{
                    new ColumnSeries
                    {
                        Title = "Thu nhập",
                        Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[0][1].ToString()) },
                        Fill = brush2,
                    },
                    new ColumnSeries
                    {
                        Title = "Công nợ",
                        Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[3][1].ToString()) },
                        Fill = brush1,
                    },
                    new ColumnSeries
                    {
                        Title = "Doanh thu",
                        Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[1][1].ToString()) },
                        Fill = brush3,
                    },
                    new ColumnSeries
                    {
                        Title = "Chi phí",
                        Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[2][1].ToString()) },
                        Fill = brush4,
                    }
                };
                Labels2 = new[] { $"{Timepicker1.Text}" };

                Formatter = value => value.ToString("N");

                DataContext = this;
                return;
            }
            if (q == 1)
            {
                chart1.Visibility = Visibility.Hidden;
                chart1_Copy.Visibility = Visibility.Visible;
                DataContext = null;

                SeriesCollection2 = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Thu nhập",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7])},
                        Fill = brush2,
                    },
                    new ColumnSeries
                    {
                        Title = "Công nợ",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7])},
                        Fill = brush1,
                    },
                    new ColumnSeries
                    {
                        Title = "Doanh thu",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]) },
                        Fill = brush3,
                    },
                    new ColumnSeries
                    {
                        Title = "Chi phí",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7])},
                        Fill = brush4,
                    }
                };
                Labels2 = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}" };

                Formatter = value => value.ToString("N");

                DataContext = this;
            }
            if (q == 2)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Thu nhập",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7])},
                        PointGeometry = null,
                        PointForeground = brush2,
                    },
                    new LineSeries
                    {
                        Title = "Công nợ",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7])},
                        PointForeground = brush1,
                    },
                    new LineSeries
                    {
                        Title = "Doanh thu",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7])},
                        PointGeometry = DefaultGeometries.Square,
                        PointGeometrySize = 15,
                        PointForeground = brush3,
                    },
                    new LineSeries
                    {
                        Title = "Chi phí",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7])},
                        LineSmoothness = 0,
                        PointGeometry = DefaultGeometries.Circle,
                        PointGeometrySize = 15,
                        PointForeground = brush4,
                    }
                };
                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}", $"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}" };
                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            if (q == 3)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7]), Convert.ToDouble(dt.Rows[8][7])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7]), Convert.ToDouble(dt.Rows[11][7]) },
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7]), Convert.ToDouble(dt.Rows[9][7]) },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7]), Convert.ToDouble(dt.Rows[10][7])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}", $"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}", $"{dt.Rows[8][2]}/{dt.Rows[8][1]}/{dt.Rows[8][3]}" };
                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            if (q == 4)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7]), Convert.ToDouble(dt.Rows[8][7]),Convert.ToDouble(dt.Rows[12][7])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7]), Convert.ToDouble(dt.Rows[11][7]),Convert.ToDouble(dt.Rows[15][7])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7]), Convert.ToDouble(dt.Rows[9][7]),Convert.ToDouble(dt.Rows[13][7])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7]), Convert.ToDouble(dt.Rows[10][7]),Convert.ToDouble(dt.Rows[14][7])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}",
                $"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}",
                $"{dt.Rows[8][2]}/{dt.Rows[8][1]}/{dt.Rows[8][3]}",
                $"{dt.Rows[12][2]}/{dt.Rows[12][1]}/{dt.Rows[12][3]}"};
                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            if (q == 5)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7]), Convert.ToDouble(dt.Rows[8][7]),Convert.ToDouble(dt.Rows[12][7]),Convert.ToDouble(dt.Rows[16][7])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7]), Convert.ToDouble(dt.Rows[11][7]),Convert.ToDouble(dt.Rows[15][7]),Convert.ToDouble(dt.Rows[19][7])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7]), Convert.ToDouble(dt.Rows[9][7]),Convert.ToDouble(dt.Rows[13][7]),Convert.ToDouble(dt.Rows[17][7])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7]), Convert.ToDouble(dt.Rows[10][7]),Convert.ToDouble(dt.Rows[14][7]),Convert.ToDouble(dt.Rows[18][7])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}",
                $"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}",
                $"{dt.Rows[8][2]}/{dt.Rows[8][1]}/{dt.Rows[8][3]}",
                $"{dt.Rows[12][2]}/{dt.Rows[12][1]}/{dt.Rows[12][3]}",
                $"{dt.Rows[16][2]}/{dt.Rows[16][1]}/{dt.Rows[16][3]}"};
                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            if (q >= 6)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;
                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7]), Convert.ToDouble(dt.Rows[8][7]),Convert.ToDouble(dt.Rows[12][7]),Convert.ToDouble(dt.Rows[16][7]),Convert.ToDouble(dt.Rows[20][7])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {

                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7]), Convert.ToDouble(dt.Rows[11][7]),Convert.ToDouble(dt.Rows[15][7]),Convert.ToDouble(dt.Rows[19][7]),Convert.ToDouble(dt.Rows[23][7])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7]), Convert.ToDouble(dt.Rows[9][7]),Convert.ToDouble(dt.Rows[13][7]),Convert.ToDouble(dt.Rows[17][7]),Convert.ToDouble(dt.Rows[21][7])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,

                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7]), Convert.ToDouble(dt.Rows[10][7]),Convert.ToDouble(dt.Rows[14][7]),Convert.ToDouble(dt.Rows[18][7]),Convert.ToDouble(dt.Rows[22][7])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }

            };

                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}-{dt.Rows[0][5]}/{dt.Rows[0][4]}/{dt.Rows[0][6]}",
                $"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}-{dt.Rows[4][5]}/{dt.Rows[4][4]}/{dt.Rows[4][6]}",
                $"{dt.Rows[8][2]}/{dt.Rows[8][1]}/{dt.Rows[8][3]}-{dt.Rows[8][5]}/{dt.Rows[8][4]}/{dt.Rows[8][6]}",
                $"{dt.Rows[12][2]}/{dt.Rows[12][1]}/{dt.Rows[12][3]}-{dt.Rows[12][5]}/{dt.Rows[12][4]}/{dt.Rows[12][6]}",
                $"{dt.Rows[16][2]}/{dt.Rows[16][1]}/{dt.Rows[16][3]}-{dt.Rows[16][5]}/{dt.Rows[16][4]}/{dt.Rows[16][6]}",
                $"{dt.Rows[20][2]}/{dt.Rows[20][1]}/{dt.Rows[20][3]}-{dt.Rows[20][5]}/{dt.Rows[20][4]}/{dt.Rows[20][6]}" };
                YFormatter = value => value.ToString("C");
                DataContext = this;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Timepicker1.Text) && string.IsNullOrEmpty(Timepicker2.Text))
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
            if ((string.IsNullOrEmpty(Timepicker1.Text)&& !string.IsNullOrEmpty(Timepicker2.Text))
                || (string.IsNullOrEmpty(Timepicker2.Text) && !string.IsNullOrEmpty(Timepicker1.Text)))
            {
                MS4 ms4 = new MS4();
                ms4.Show();
                return;
            }
            DateTime k;
            if(!DateTime.TryParse(Timepicker1.Text, out k)
                || !DateTime.TryParse(Timepicker2.Text, out k))
            {
                MS5 ms5 = new MS5();
                ms5.Show(); return;
            }    
            if(Convert.ToDateTime(Timepicker1.Text) > Convert.ToDateTime(Timepicker2.Text))
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

            string date1 = $"{Timepicker1.SelectedDate.Value.Month}/{Timepicker1.SelectedDate.Value.Day}/{Timepicker1.SelectedDate.Value.Year}";
            string date2 = $"{Timepicker2.SelectedDate.Value.Month}/{Timepicker2.SelectedDate.Value.Day}/{Timepicker2.SelectedDate.Value.Year}";
            DataTable dataTable = x.hien_thi(date1, date2);
            textblock1.Text = bientoancuc.chuanhoa(dataTable.Rows[3][1].ToString());
            textblock2.Text = bientoancuc.chuanhoa(dataTable.Rows[1][1].ToString());
            textblock3.Text = bientoancuc.chuanhoa(dataTable.Rows[0][1].ToString());
            textblock4.Text = bientoancuc.chuanhoa(dataTable.Rows[2][1].ToString());

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

            int q = 0;
            DataTable dt = x.bieu_do(Timepicker1.SelectedDate.Value, Timepicker2.SelectedDate.Value,ref q);

            if (q == 0)
            {
                chart1.Visibility = Visibility.Hidden;
                chart1_Copy.Visibility = Visibility.Visible;
                DataContext = null;

                SeriesCollection2 = new SeriesCollection{
                    new ColumnSeries
                    {
                        Title = "Thu nhập",
                        Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[0][1].ToString()) },
                        Fill = brush2,
                    },
                    new ColumnSeries
                    {
                        Title = "Công nợ",
                        Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[3][1].ToString()) },
                        Fill = brush1,
                    },
                    new ColumnSeries
                    {
                        Title = "Doanh thu",
                        Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[1][1].ToString()) },
                        Fill = brush3,
                    },
                    new ColumnSeries
                    {
                        Title = "Chi phí",
                        Values = new ChartValues<double> {Convert.ToDouble(dataTable.Rows[2][1].ToString()) },
                        Fill = brush4,
                    }
                };
                Labels2 = new[] { $"{Timepicker1.Text}" };

                Formatter = value => value.ToString("N");

                DataContext = this;
                return;
            }
            if (q == 1)
            {
                chart1.Visibility = Visibility.Hidden;
                chart1_Copy.Visibility = Visibility.Visible;
                DataContext = null;

                SeriesCollection2 = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Thu nhập",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7])},
                        Fill = brush2,
                    },
                    new ColumnSeries
                    {
                        Title = "Công nợ",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7])},
                        Fill = brush1,
                    },
                    new ColumnSeries
                    {
                        Title = "Doanh thu",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]) },
                        Fill = brush3,
                    },
                    new ColumnSeries
                    {
                        Title = "Chi phí",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7])},
                        Fill = brush4,
                    }
                };
                Labels2 = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}" };

                Formatter = value => value.ToString("N");

                DataContext = this;
            }
            if (q == 2)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Thu nhập",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7])},
                        PointGeometry = null,
                        PointForeground = brush2,
                    },
                    new LineSeries
                    {
                        Title = "Công nợ",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7])},
                        PointForeground = brush1,
                    },
                    new LineSeries
                    {
                        Title = "Doanh thu",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7])},
                        PointGeometry = DefaultGeometries.Square,
                        PointGeometrySize = 15,
                        PointForeground = brush3,
                    },
                    new LineSeries
                    {
                        Title = "Chi phí",
                        Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7])},
                        LineSmoothness = 0,
                        PointGeometry = DefaultGeometries.Circle,
                        PointGeometrySize = 15,
                        PointForeground = brush4,
                    }
                };
                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}",$"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}"};
                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            if (q == 3)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7]), Convert.ToDouble(dt.Rows[8][7])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7]), Convert.ToDouble(dt.Rows[11][7]) },
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7]), Convert.ToDouble(dt.Rows[9][7]) },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7]), Convert.ToDouble(dt.Rows[10][7])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}",$"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}",$"{dt.Rows[8][2]}/{dt.Rows[8][1]}/{dt.Rows[8][3]}"};
                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            if (q == 4)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7]), Convert.ToDouble(dt.Rows[8][7]),Convert.ToDouble(dt.Rows[12][7])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7]), Convert.ToDouble(dt.Rows[11][7]),Convert.ToDouble(dt.Rows[15][7])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7]), Convert.ToDouble(dt.Rows[9][7]),Convert.ToDouble(dt.Rows[13][7])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7]), Convert.ToDouble(dt.Rows[10][7]),Convert.ToDouble(dt.Rows[14][7])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}",
                $"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}",
                $"{dt.Rows[8][2]}/{dt.Rows[8][1]}/{dt.Rows[8][3]}",
                $"{dt.Rows[12][2]}/{dt.Rows[12][1]}/{dt.Rows[12][3]}"};
                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            if (q == 5)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;

                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7]), Convert.ToDouble(dt.Rows[8][7]),Convert.ToDouble(dt.Rows[12][7]),Convert.ToDouble(dt.Rows[16][7])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {
                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7]), Convert.ToDouble(dt.Rows[11][7]),Convert.ToDouble(dt.Rows[15][7]),Convert.ToDouble(dt.Rows[19][7])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7]), Convert.ToDouble(dt.Rows[9][7]),Convert.ToDouble(dt.Rows[13][7]),Convert.ToDouble(dt.Rows[17][7])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,
                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7]), Convert.ToDouble(dt.Rows[10][7]),Convert.ToDouble(dt.Rows[14][7]),Convert.ToDouble(dt.Rows[18][7])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }
            };
                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}",
                $"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}",
                $"{dt.Rows[8][2]}/{dt.Rows[8][1]}/{dt.Rows[8][3]}",
                $"{dt.Rows[12][2]}/{dt.Rows[12][1]}/{dt.Rows[12][3]}",
                $"{dt.Rows[16][2]}/{dt.Rows[16][1]}/{dt.Rows[16][3]}"};
                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
            if (q >=6)
            {
                chart1.Visibility = Visibility.Visible;
                chart1_Copy.Visibility = Visibility.Hidden;
                DataContext = null;
                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu nhập",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[0][7]), Convert.ToDouble(dt.Rows[4][7]), Convert.ToDouble(dt.Rows[8][7]),Convert.ToDouble(dt.Rows[12][7]),Convert.ToDouble(dt.Rows[16][7]),Convert.ToDouble(dt.Rows[20][7])},
                    PointGeometry = null,
                    PointForeground = brush2,
                },
                new LineSeries
                {

                    Title = "Công nợ",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[3][7]), Convert.ToDouble(dt.Rows[7][7]), Convert.ToDouble(dt.Rows[11][7]),Convert.ToDouble(dt.Rows[15][7]),Convert.ToDouble(dt.Rows[19][7]),Convert.ToDouble(dt.Rows[23][7])},
                    PointForeground = brush1,
                },
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[1][7]), Convert.ToDouble(dt.Rows[5][7]), Convert.ToDouble(dt.Rows[9][7]),Convert.ToDouble(dt.Rows[13][7]),Convert.ToDouble(dt.Rows[17][7]),Convert.ToDouble(dt.Rows[21][7])},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    PointForeground = brush3,

                },
                new LineSeries
                {
                    Title = "Chi phí",
                    Values = new ChartValues<double> {Convert.ToDouble(dt.Rows[2][7]), Convert.ToDouble(dt.Rows[6][7]), Convert.ToDouble(dt.Rows[10][7]),Convert.ToDouble(dt.Rows[14][7]),Convert.ToDouble(dt.Rows[18][7]),Convert.ToDouble(dt.Rows[22][7])},
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    PointForeground = brush4,
                }

            };

                Labels = new[] { $"{dt.Rows[0][2]}/{dt.Rows[0][1]}/{dt.Rows[0][3]}-{dt.Rows[0][5]}/{dt.Rows[0][4]}/{dt.Rows[0][6]}",
                $"{dt.Rows[4][2]}/{dt.Rows[4][1]}/{dt.Rows[4][3]}-{dt.Rows[4][5]}/{dt.Rows[4][4]}/{dt.Rows[4][6]}",
                $"{dt.Rows[8][2]}/{dt.Rows[8][1]}/{dt.Rows[8][3]}-{dt.Rows[8][5]}/{dt.Rows[8][4]}/{dt.Rows[8][6]}",
                $"{dt.Rows[12][2]}/{dt.Rows[12][1]}/{dt.Rows[12][3]}-{dt.Rows[12][5]}/{dt.Rows[12][4]}/{dt.Rows[12][6]}",
                $"{dt.Rows[16][2]}/{dt.Rows[16][1]}/{dt.Rows[16][3]}-{dt.Rows[16][5]}/{dt.Rows[16][4]}/{dt.Rows[16][6]}",
                $"{dt.Rows[20][2]}/{dt.Rows[20][1]}/{dt.Rows[20][3]}-{dt.Rows[20][5]}/{dt.Rows[20][4]}/{dt.Rows[20][6]}" };
                YFormatter = value => value.ToString("C");
                DataContext = this;
            }
        }

        private void TextBlock_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
