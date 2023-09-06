using Nghien_cuu_ung_dung.MessageBox;
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
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;
using System.IO.Ports;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using Nghien_cuu_ung_dung.Control;
using Nghien_cuu_ung_dung.Ngoaile;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Sign_up.xaml
    /// </summary>
    public partial class Sign_up : Window
    {
        public Sign_up()
        {
            InitializeComponent();
        }
        public delegate void SendMessage();

        private void SetValue1()
        {
            this.check1 = true;
        }
        private void SetValue2()
        {
            this.check2 = true;
        }

        Control_dang_ky x = new Control_dang_ky();
        bool check1 = false, check2 = false;

        SerialPort port = new SerialPort();
        clsSMS objclsSMS = new clsSMS();
        ShortMessageCollection objShortMessageCollection = new ShortMessageCollection();
        public bool isModemConnect = false;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Login wd= new Login();
            wd.Show();
            this.Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                LabelError3.Visibility = Visibility.Visible;
                LabelError3.Text = "Bạn chưa nhập Số điện thoại!!!";
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                LabelError4.Visibility = Visibility.Visible;
                LabelError4.Text = "Bạn chưa nhập Gmail!!!";
            }
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                LabelError2.Visibility = Visibility.Visible;
                LabelError2.Text = "Bạn chưa nhập Tên cửa hàng!!!";
            }
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                LabelError6.Visibility = Visibility.Visible;
                LabelError6.Text = "Bạn chưa nhập Tên quản lý!!!";
            }
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                LabelError1.Visibility = Visibility.Visible;
                LabelError1.Text = "Bạn chưa nhập Tài khoản!!!";
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                LabelError5.Visibility = Visibility.Visible;
                LabelError5.Text = "Bạn chưa nhập Mật khẩu!!!";
            }
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                LabelError7.Visibility = Visibility.Visible;
                LabelError7.Text = "Bạn chưa Xác thực mật khẩu";
            }

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox7.Text)
                || string.IsNullOrEmpty(textBox8.Text) || checkBox1.IsChecked == false || textBox3.Text != textBox4.Text || check1 == false || check2 == false ||
                string.IsNullOrEmpty(textBox3.Text)|| string.IsNullOrEmpty(textBox4.Text))
            {
                LabelError8.Visibility = Visibility.Visible;
                LabelError8.Text = "Bạn chưa xác nhận!!!";
                return;
            }
                
            if (x.dang_ky(textBox8.Text, textBox5.Text, textBox7.Text, textBox2.Text, textBox1.Text, textBox3.Text))
            {
                SignUp_Succesful wd = new SignUp_Succesful();
                wd.ShowDialog();
            }
            else
            {
                LabelError8.Visibility= Visibility.Visible;
                LabelError8.Text = "Tài khoản đã tồn tại!!!";
            }
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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            objclsSMS.sendMsg(this.port, textBox1.Text, a, 5000);
            MessageBox_OPT wd1 = new MessageBox_OPT(a, SetValue1);
            wd1.ShowDialog();

            if (check1)
            {
                XT1Button_Copy.Visibility = Visibility.Visible;
                XT1Button.Visibility = Visibility.Hidden;
                XT1Button_Copy.Content = "Xác thực thành công";
            }
        }

        private void textBox8_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                LabelError1.Visibility= Visibility.Visible;
                LabelError1.Text = "Bạn chưa nhập Tài khoản!!!";
            }
            else
            {
                LabelError1.Visibility = Visibility.Hidden;
            }
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            LabelError1.Visibility = Visibility.Hidden;
            LabelError2.Visibility = Visibility.Hidden;
            LabelError3.Visibility = Visibility.Hidden;
            LabelError4.Visibility = Visibility.Hidden;
            LabelError5.Visibility = Visibility.Hidden;
            LabelError6.Visibility = Visibility.Hidden;
            LabelError7.Visibility = Visibility.Hidden;
            LabelError8.Visibility = Visibility.Hidden;
            XT1Button1_Copy.Visibility = Visibility.Hidden;
            XT1Button_Copy.Visibility = Visibility.Hidden;
        }

        private void textBox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                LabelError2.Visibility = Visibility.Visible;
                LabelError2.Text = "Bạn chưa nhập Tên cửa hàng!!!";
            }
            else
            {
                LabelError2.Visibility = Visibility.Hidden;
            }
        }

        private void textBox7_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                LabelError6.Visibility = Visibility.Visible;
                LabelError6.Text = "Bạn chưa nhập Tên quản lý!!!";
            }
            else
            {
                LabelError6.Visibility = Visibility.Hidden;
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                LabelError3.Visibility = Visibility.Visible;
                LabelError3.Text = "Bạn chưa nhập Số điện thoại!!!";
            }
            else
            {
                LabelError3.Visibility = Visibility.Hidden;
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                LabelError4.Visibility = Visibility.Visible;
                LabelError4.Text = "Bạn chưa nhập Gmail!!!";
            }
            else
            {
                LabelError4.Visibility = Visibility.Hidden;
            }
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                LabelError5.Visibility = Visibility.Visible;
                LabelError5.Text = "Bạn chưa nhập Mật khẩu!!!";
            }
            else
            {
                LabelError5.Visibility = Visibility.Hidden;
            }
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                LabelError7.Visibility = Visibility.Visible;
                LabelError7.Text = "Bạn chưa Xác thực mật khẩu";
            }
            else if(textBox3.Text!=textBox4.Text)
            {
                LabelError7.Visibility = Visibility.Visible;
                LabelError7.Text = "Xác thực mật khẩu không trùng khớp!!!";
            }
            else
            {
                LabelError7.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(textBox2.Text.Trim());
            mail.From = new MailAddress("nguyenducanh10102003@gmail.com");
            mail.Subject = "Ma OTP xac thuc gmail";
            string a = random();
            mail.Body = $"Ma OTP cua ban la: {a}";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("nguyenducanh10102003@gmail.com", "xcruuqivjvmpbjrf");
            smtp.Send(mail);

            MessageBox_OPT wd1 = new MessageBox_OPT(a, SetValue2);
            wd1.ShowDialog();

            if (check2)
            {
                XT1Button1_Copy.Visibility = Visibility.Visible;
                XT1Button1.Visibility = Visibility.Hidden;
                XT1Button1_Copy.Content = "Xác thực thành công";
                //Button4.FontFamily = ;
            }
        }
    }
}
