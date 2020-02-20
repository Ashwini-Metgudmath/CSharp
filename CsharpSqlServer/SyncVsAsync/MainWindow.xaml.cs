using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SyncVsAsync
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

        private void ExecuteSync_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            RunDownloadSync();

            watch.Stop();

            var elapseMs = watch.ElapsedMilliseconds;
            resultWindow.Text += $"Total executaion time: {elapseMs}";

        }

        private async void ExecuteAsync_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            await RunDownloadParallelAsync();

            watch.Stop();

            var elapseMs = watch.ElapsedMilliseconds;
            resultWindow.Text += $"Total executaion time: {elapseMs}";
        }

        private async Task RunDownloadParallelAsync()
        {
            List<string> webSites = PrepData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
            foreach (string site in webSites)
            {
                tasks.Add(Task.Run(() => DownloadWebsite(site)));
                
            }

            var results = await Task.WhenAll(tasks);

            foreach(var item in results)
            {
                ReportWebsiteInfo(item);
            }
        }

        private async Task RunDownloadAsync()
        {
            List<string> webSites = PrepData();
            foreach (string site in webSites)
            {
                WebsiteDataModel results = await Task.Run(() => DownloadWebsite(site));
                ReportWebsiteInfo(results);
            }
        }

        private void RunDownloadSync()
        {
            List<string> webSites = PrepData();
            foreach(string site in webSites)
            {
                WebsiteDataModel results = DownloadWebsite(site);
                ReportWebsiteInfo(results);
            }
        }

        private List<string> PrepData()
        {
            resultWindow.Text = "";

            List<string> output = new List<string>();

            output.Add("https://www.yahoo.com");
            output.Add("https://www.google.com");
            output.Add("https://www.microsoft.com");
            output.Add("https://www.codeproject.com");
            output.Add("https://stackoverflow.com");

            return output;
        }

        private WebsiteDataModel DownloadWebsite(string site)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();
            output.WebsiteURL = site;
            output.WebsiteData = client.DownloadString(site);

            return output;
        }

        private void ReportWebsiteInfo(WebsiteDataModel data)
        {
            resultWindow.Text += $"{data.WebsiteURL} downloaded: {data.WebsiteData.Length} charecters long. {Environment.NewLine}";
        }
    }
}
