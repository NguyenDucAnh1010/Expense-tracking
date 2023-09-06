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
using Nghien_cuu_ung_dung.Ngoaile;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Information_Account.xaml
    /// </summary>
    public partial class Information_Account : UserControl
    {
        DispatcherTimer timer1 = new DispatcherTimer();

        public Information_Account()
        {
            InitializeComponent();
            timer1.Interval = TimeSpan.FromSeconds(10);
            timer1.Tick += timer_Tick1;
            timer1.Start();
        }
        void timer_Tick1(object sender, EventArgs e)
        {
            DataTable dataTable = x.hien_thi(bientoancuc.tk);
            DataRow dr = dataTable.Rows[0];
            Label1.Text = dr[1].ToString();
            Label2.Text = dr[2].ToString();
            Textbox1.Text = dr[0].ToString();
            TextBox2.Text = dr[2].ToString();
            TextBox3.Text = dr[1].ToString();
            TextBox4.Text = dr[3].ToString();
            TextBox5.Text = dr[4].ToString();
            mk = dr[5].ToString();
        }
        Control_dang_nhap x=new Control_dang_nhap();
        public delegate void SendMessage1(string mk);

        private void SetValue1(string mk)
        {
            this.mk = mk;
        }
        private void SetValue2(string gmail)
        {
            this.TextBox4.Text = gmail;
        }
        private void SetValue3(string sdt)
        {
            this.TextBox5.Text = sdt;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        string mk;
        private void UpdatePSButton_Click(object sender, RoutedEventArgs e)
        {
            Cap_nhat_mat_khau wd = new Cap_nhat_mat_khau(mk,TextBox5.Text,SetValue1);
            wd.ShowDialog();
        }

        private void UpdateEmailButton_Click(object sender, RoutedEventArgs e)
        {
            Cap_nhat_lai_gmail wd = new Cap_nhat_lai_gmail(TextBox4.Text,SetValue2);
            wd.ShowDialog();
        }

        private void UpdateSDTButton_Click(object sender, RoutedEventArgs e)
        {
            Cap_nhat_lai_so_dien_thoai wd = new Cap_nhat_lai_so_dien_thoai(TextBox5.Text,SetValue3);
            wd.ShowDialog();
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = x.hien_thi(bientoancuc.tk);
            DataRow dr = dataTable.Rows[0];
            Label1.Text= dr[1].ToString();
            Label2.Text= dr[2].ToString();
            Textbox1.Text = dr[0].ToString();
            TextBox2.Text =  dr[2].ToString();
            TextBox3.Text = dr[1].ToString();
            TextBox4.Text = dr[3].ToString();
            TextBox5.Text = dr[4].ToString();
            mk = dr[5].ToString();
        }
    }
}
