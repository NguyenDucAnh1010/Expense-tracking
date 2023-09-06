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
using System.Windows.Threading;
using static Nghien_cuu_ung_dung.View.Sign_up;

namespace Nghien_cuu_ung_dung.MessageBox
{
    /// <summary>
    /// Interaction logic for MessageBox_OPT.xaml
    /// </summary>
    public partial class MessageBox_OPT : Window
    {
        public MessageBox_OPT()
        {
            InitializeComponent();
        }
        string otp;
        public SendMessage send;
        public MessageBox_OPT(string otp, SendMessage sender)
        {
            InitializeComponent();
            this.otp = otp;
            this.send = sender;
            LabelError1.Visibility= Visibility.Hidden;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Textbox1.Text == otp)
            {
                send();
                this.Close();
            }
            else
            {
                LabelError1.Visibility= Visibility.Visible;
                LabelError1.Text = "Bạn nhập sai mã OTP!!!";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private int decrement = 60;
        void timer_Tick(object sender, EventArgs e)
        {
            decrement--;
            lblTime.Content = decrement.ToString();
            if(decrement == 0) {
                this.Close();
            }
        }
    }
}
