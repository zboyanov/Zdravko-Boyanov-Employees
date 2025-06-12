using BuddiesOnTheSameJob.Models;
using BuddiesOnTheSameJob.Services;
using Microsoft.Win32;
using System.Collections.ObjectModel;
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

namespace BuddiesOnTheSameJob
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FileParser parser;
        private readonly BuddyAnalyzer analyzer;

        public MainWindow()
        {
            parser = new FileParser();
            analyzer = new BuddyAnalyzer();
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Избери CSV файл"
            };

            if (dialog.ShowDialog() == true)
            {
                List<WorkLog> logs = parser.Parse(dialog.FileName);
                List<BuddyPair> pairs = analyzer.GetLongestWorkingPairs(logs);
                EmployeeGrid.ItemsSource = pairs;
            }
        }
    }
}