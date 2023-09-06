using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nghien_cuu_ung_dung.View
{
    /// <summary>
    /// Interaction logic for Ho_tro.xaml
    /// </summary>
    public partial class Ho_tro : UserControl
    {
        public Ho_tro()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Answer_1 answer = new Answer_1();
            answer.Show();
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Answer_2 answer = new Answer_2();
            answer.Show();
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Answer_3 answer = new Answer_3();
            answer.Show();
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Answer_4 answer = new Answer_4();
            answer.Show();
        }
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            Answer_5 answer = new Answer_5();
            answer.Show();
        }

        private void Butto5_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
