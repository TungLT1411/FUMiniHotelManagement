using BLL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

namespace LeThanhTungWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly CustomerObject customerObject;
        private readonly IConfiguration configuration;
        public Login()
        {
            InitializeComponent();
            customerObject = new CustomerObject();

            configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void btnLogin(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Password;
            if (email == "")
            {
                MessageBox.Show("UserName is not blank");
            }
            else if (password == "")
            {
                MessageBox.Show("Password is not Blank");
            }
            else
            {
                var defaultEmail = configuration["AdminAccount:Email"];
                var defaultPassword = configuration["AdminAccount:Password"]; 
                var checkAccount = await customerObject.Login(email, password);

                if (checkAccount != null)
                {
                   WindowMainCustomer mainWindowCustomer = new WindowMainCustomer(checkAccount);
                   mainWindowCustomer.Show();
                   this.Close();
                }
                else if (email == defaultEmail && password == defaultPassword)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login failed, email or password incorrext!", "Warning");
                }
            }
        }
    }
}
