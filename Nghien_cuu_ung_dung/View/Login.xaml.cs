using Nghien_cuu_ung_dung.Control;
using Nghien_cuu_ung_dung.MessageBox;
using Nghien_cuu_ung_dung.Ngoaile;
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

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            textBox3.Visibility = Visibility.Hidden;
            textBox2.Visibility = Visibility.Visible;
        }

        Control_dang_nhap x = new Control_dang_nhap();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sign_up wd = new Sign_up();
            this.Hide();
            wd.Show();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Password))
                return;
            int i=x.KiemTraDangNhap(textBox1.Text,textBox2.Password);
            if (i == 1)
            {
                Label3.Visibility= Visibility.Visible;
                Label3.Text = "Tài khoản không tồn tại";
            }
            else if (i==2)
            {
                Label3.Visibility = Visibility.Visible;
                Label3.Text = "Mật khẩu không chính xác";
            }
            if (x.dang_nhap(textBox1.Text, textBox2.Password))
            {
                bientoancuc.tk = textBox1.Text;
                MS1 wd = new MS1();
                wd.ShowDialog();
                MainMenu wd1 = new MainMenu();
                this.Hide();
                wd1.Show();
                this.Close();
            }
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Label3.Visibility = Visibility.Hidden;
        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox1.IsChecked == true)
            {
                textBox3.Visibility = Visibility.Visible;
                textBox2.Visibility = Visibility.Hidden;
            }
            else if (checkBox1.IsChecked == false)
            {
                textBox3.Visibility = Visibility.Hidden;
                textBox2.Visibility = Visibility.Visible;
            }
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textBox3.Visibility== Visibility.Visible)
            {
                textBox2.Password = textBox3.Text;
            }
        }

        private void textBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(textBox2.Visibility== Visibility.Visible)
            {
                textBox3.Text = textBox2.Password;
            }
        }
    }
}