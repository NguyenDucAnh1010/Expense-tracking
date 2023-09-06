using Nghien_cuu_ung_dung.Control;
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
using System.Windows.Shapes;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            button1.IsChecked = true;
        }

        public void ControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void btnClode_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
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

      

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Login wd = new Login();
            this.Close();
            wd.Show();
        }
        Control_dang_nhap x = new Control_dang_nhap();
        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = x.hien_thi(bientoancuc.tk);
            DataRow dr = dataTable.Rows[0];
            Textblock1.Text= $"{dr[1].ToString()} loggin";
        }
    }
}
