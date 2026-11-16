using FlottaKliensWPF.DTOs;
using FlottaKliensWPF.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlottaKliensWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        List<User> userlist = new List<User>();
        private HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
        public MainWindow()
        {
            InitializeComponent();
            dgrUsers.ItemsSource = userlist;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginDTO logindto = new LoginDTO()
            {
                LoginName = tbxLoginName.Text,
                Password = tbxPassword.Text
            };
            string dtoString = JsonSerializer.Serialize(logindto, JsonSerializerOptions.Default);
            var body = new StringContent(dtoString, Encoding.UTF8,"application/json");
            var valasz = await client.PostAsync("api/Login", body);
            tbxResult.Text = valasz.StatusCode.ToString();

        }

        private async void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            string url = "aoi/User/GetAllUser";
            var users = await client.GetFromJsonAsync<List<User>>(url);
            dgrUsers.ItemsSource = users;
        }
    }
}