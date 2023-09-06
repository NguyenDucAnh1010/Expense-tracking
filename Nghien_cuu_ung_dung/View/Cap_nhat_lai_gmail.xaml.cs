using Nghien_cuu_ung_dung.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using Nghien_cuu_ung_dung.Control;
using Nghien_cuu_ung_dung.Ngoaile;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Cap_nhat_lai_gmail.xaml
    /// </summary>
    public partial class Cap_nhat_lai_gmail : Window
    {
        string gmail;
        public SendMessage1 send;
        Control_dang_ky x=new Control_dang_ky();
        public Cap_nhat_lai_gmail(string gmail, SendMessage1 sender)
        {
            InitializeComponent();
            this.gmail = gmail;
            Textbox1.Text = gmail;
            LabelError7.Visibility= System.Windows.Visibility.Hidden;
            this.send=sender;
        }
        bool check2 = false;
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
        private void SetValue2()
        {
            this.check2 = true;
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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(Textbox2.Text.Trim());
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

            if (check2&&string.IsNullOrEmpty(Textbox2.Text)==false&&Textbox2.Text!=Textbox1.Text)
            {

                x.capnhap_gmail(bientoancuc.tk, Textbox2.Text);
                send(Textbox2.Text);
                this.Close();
            }
            if (check2 == false)
            {
                LabelError7.Visibility= Visibility.Visible;
                LabelError7.Text = "Bạn chưa xác thực gmail mới!!!";
            }else if (string.IsNullOrEmpty(Textbox2.Text))
            {
                LabelError7.Visibility = Visibility.Visible;
                LabelError7.Text = "Bạn nhập thiếu thông tin!!!";
            }else if (Textbox2.Text == Textbox1.Text)
            {
                LabelError7.Visibility = Visibility.Visible;
                LabelError7.Text = "Gmail mới trùng với gmail cũ!!!";
            }
        }
    }
}
