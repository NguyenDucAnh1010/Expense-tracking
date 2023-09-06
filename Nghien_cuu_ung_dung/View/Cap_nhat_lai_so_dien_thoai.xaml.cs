using Nghien_cuu_ung_dung.Control;
using Nghien_cuu_ung_dung.MessageBox;
using Nghien_cuu_ung_dung.Ngoaile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
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
using System.Windows.Shapes;
using static Nghien_cuu_ung_dung.View.Information_Account;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Cap_nhat_lai_so_dien_thoai.xaml
    /// </summary>
    public partial class Cap_nhat_lai_so_dien_thoai : Window
    {
        string sdt;
        public SendMessage1 send;
        Control_dang_ky x=new Control_dang_ky();
        public Cap_nhat_lai_so_dien_thoai(string sdt, SendMessage1 sender)
        {
            InitializeComponent();
            this.sdt = sdt;
            Textbox1.Text= sdt;
            LabelError7.Visibility= Visibility.Hidden;
            this.send = sender;
        }
        bool check1 = false;
        string random()
        {
            string a = "";
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                a += Convert.ToString(random.Next(9 - (0 - 1)) + 0);
            }
            return a;
        }
        public delegate void SendMessage();
        private void SetValue1()
        {
            this.check1 = true;
        }
        SerialPort port = new SerialPort();
        clsSMS objclsSMS = new clsSMS();
        ShortMessageCollection objShortMessageCollection = new ShortMessageCollection();
        public bool isModemConnect = false;
        public string FindPortAndConnect()
        {
            string result = null;
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                this.port = objclsSMS.OpenPort(port, Convert.ToInt32(115200), Convert.ToInt32(8), Convert.ToInt32(100), Convert.ToInt32(100));
                bool isConnect = objclsSMS.CheckPortSendSMS(this.port);
                if (isConnect)
                {
                    return port;

                }

            }
            return result;
        }
        public void ControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void btnClode_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        public void btnMiniimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button1_Click_1(object sender, RoutedEventArgs e)
        {
            string a = random();
            if (isModemConnect)
            {
                try
                {
                    objclsSMS.ClosePort(this.port);

                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
                isModemConnect = false;
            }
            else
            {
                var str_Port_Connect = FindPortAndConnect();
                if (string.IsNullOrEmpty(str_Port_Connect))
                {
                    isModemConnect = false;

                }
                else
                {
                    isModemConnect = true;
                }
            }
            objclsSMS.sendMsg(this.port, Textbox2.Text, a, 5000);

            MessageBox_OPT wd1 = new MessageBox_OPT(a, SetValue1);
            wd1.ShowDialog();

            if (check1 && string.IsNullOrEmpty(Textbox2.Text) == false && Textbox2.Text != Textbox1.Text)
            {
                x.capnhap_sdt(bientoancuc.tk, Textbox2.Text);
                send(Textbox2.Text);
                this.Close();
            }
            if (check1 == false)
            {
                LabelError7.Visibility = Visibility.Visible;
                LabelError7.Text = "Bạn chưa xác thực gmail mới!!!";
            }
            else if (string.IsNullOrEmpty(Textbox2.Text))
            {
                LabelError7.Visibility = Visibility.Visible;
                LabelError7.Text = "Bạn nhập thiếu thông tin!!!";
            }
            else if (Textbox2.Text == Textbox1.Text)
            {
                LabelError7.Visibility = Visibility.Visible;
                LabelError7.Text = "Gmail mới trùng với gmail cũ!!!";
            }
        }
    }
}
