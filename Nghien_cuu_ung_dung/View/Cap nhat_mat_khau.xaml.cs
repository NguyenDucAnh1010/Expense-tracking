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
using Nghien_cuu_ung_dung.Control;
using Nghien_cuu_ung_dung.MessageBox;
using Nghien_cuu_ung_dung.Ngoaile;
using static Nghien_cuu_ung_dung.View.Information_Account;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Cap_nhat_mat_khau.xaml
    /// </summary>
    public partial class Cap_nhat_mat_khau : Window
    {
        public Cap_nhat_mat_khau()
        {
            InitializeComponent();
        }
        string mk,sdt;
        public SendMessage1 send;
        Control_dang_ky x=new Control_dang_ky();

        SerialPort port = new SerialPort();
        clsSMS objclsSMS = new clsSMS();
        ShortMessageCollection objShortMessageCollection = new ShortMessageCollection();
        bool check1= false;
        public bool isModemConnect = false;
        public Cap_nhat_mat_khau(string mk,string sdt, SendMessage1 sender)
        {
            InitializeComponent();
            this.mk = mk;
            this.sdt = sdt;
            Textbox1.Text = mk;
            this.send = sender;
            Label3_Copy2.Visibility= Visibility.Hidden;
            Label3_Copy1.Visibility= Visibility.Hidden;
            Label3_Copy.Visibility= Visibility.Hidden;
            Label3_Copy3.Visibility= Visibility.Hidden;
        }
        private void SetValue1()
        {
            this.check1 = true;
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
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Textbox2.Text))
            {
                Label3_Copy1.Visibility = Visibility.Visible;
                Label3_Copy1.Text = "Bạn chưa nhập mật khảu mới!!!";
            }
            else if (Textbox2.Text == Textbox1.Text)
            {
                Label3_Copy1.Visibility = Visibility.Visible;
                Label3_Copy1.Text = "Mật khảu mới trùng mật khẩu cũ!!!";
            }
            if (string.IsNullOrEmpty(Textbox3.Text))
            {
                Label3_Copy2.Visibility = Visibility.Visible;
                Label3_Copy2.Text = "Bạn chưa nhập xác thực mật khảu mới!!!";
            }
            else if (Textbox2.Text != Textbox3.Text)
            {
                Label3_Copy2.Visibility = Visibility.Visible;
                Label3_Copy2.Text = "Xác nhận mật khảu mới chưa chính xác!!!";
            }
            else if (Textbox2.Text == Textbox3.Text)
            {
                Label3_Copy2.Visibility = Visibility.Visible;
                Label3_Copy2.Text = "Xác nhận mật khảu mới thành công!!!";
            }
            if (mk==Textbox1.Text && Textbox2.Text==Textbox3.Text && Textbox2.Text != Textbox1.Text)
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
                objclsSMS.sendMsg(this.port, sdt.TrimEnd(), a, 5000);
                MessageBox_OPT wd1 = new MessageBox_OPT(a, SetValue1);
                wd1.ShowDialog();
                if (check1)
                {
                    send(Textbox2.Text);
                    x.capnhap_mk(bientoancuc.tk, Textbox2.Text);
                    this.Close();
                }
                else
                {
                    Label3_Copy3.Visibility= Visibility.Visible;
                    Label3_Copy3.Text = "Bạn chưa xác thực OTP!!!";
                }
            }
        }

        private void Textbox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Textbox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Textbox2.Text))
            {
                Label3_Copy1.Visibility = Visibility.Visible;
                Label3_Copy1.Text = "Bạn chưa nhập mật khảu mới!!!";
            }else if (Textbox2.Text == Textbox1.Text)
            {
                Label3_Copy1.Visibility = Visibility.Visible;
                Label3_Copy1.Text = "Mật khảu mới trùng mật khẩu cũ!!!";
            }
            else
            {
                Label3_Copy1.Visibility = Visibility.Hidden;
            }
        }

        private void Textbox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Textbox3.Text))
            {
                Label3_Copy2.Visibility = Visibility.Visible;
                Label3_Copy2.Text = "Bạn chưa nhập xác thực mật khảu mới!!!";
            }else if (Textbox2.Text!=Textbox3.Text)
            {
                Label3_Copy2.Visibility = Visibility.Visible;
                Label3_Copy2.Text = "Xác nhận mật khảu mới chưa chính xác!!!";
            }else if(Textbox2.Text == Textbox3.Text)
            {
                Label3_Copy2.Visibility = Visibility.Visible;
                Label3_Copy2.Text = "Xác nhận mật khảu mới thành công!!!";
            }
            else
            {
                Label3_Copy2.Visibility = Visibility.Hidden;
            }
        }
    }
}
