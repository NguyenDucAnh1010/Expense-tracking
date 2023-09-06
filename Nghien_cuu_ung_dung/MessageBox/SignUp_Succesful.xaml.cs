using Nghien_cuu_ung_dung.View;
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

namespace Nghien_cuu_ung_dung.MessageBox
{
    /// <summary>
    /// Interaction logic for SignUp_Succesful.xaml
    /// </summary>
    public partial class SignUp_Succesful : Window
    {
        public SignUp_Succesful()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login wd = new Login();
            Sign_up wd1 = new Sign_up();
            this.Close();
            wd1.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
