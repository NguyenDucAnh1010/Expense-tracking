using Nghien_cuu_ung_dung.View;
using Nghien_cuu_ung_dung.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Nghien_cuu_ung_dung
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            Login view = new Login();
            view.Show();
        }   
    }
}
