using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentalsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public List<Movie> GetMovies()
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync("https://localhost:7271/Movie").Result;
            var movies = JsonConvert.DeserializeObject<List<Movie>>(response)?? new List<Movie>();
            return movies;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new RentalsViewModel() { Movies = new System.Collections.ObjectModel.ObservableCollection<Movie>(GetMovies())};
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            InsertMovie insertMovie = new InsertMovie();
            if(insertMovie.ShowDialog() == true)
            {
                var movie = insertMovie.DataContext as Movie;
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(movie);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("https://localhost:7271/Movie", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Movie inserted successfully");
                    this.DataContext = new RentalsViewModel() { Movies = new System.Collections.ObjectModel.ObservableCollection<Movie>(GetMovies()) };
                }
                else
                {
                    MessageBox.Show("Error inserting movie");
                }
            }
        }
    }
}