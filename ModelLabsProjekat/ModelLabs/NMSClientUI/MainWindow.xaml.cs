using NMSClientUI.Views;
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

namespace NMSClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentView.Content = new ValuesView();
            
        }

        private void GetValuesView(object sender, RoutedEventArgs e)
        {
            ContentView.Content = new ValuesView();
        }

        private void GetExtentValuesView(object sender, RoutedEventArgs e)
        {
            ContentView.Content = new ExtentValuesView();
        }

        private void GetRelatedValuesView(object sender, RoutedEventArgs e)
        {
            ContentView.Content = new RelatedValuesView();
        }
    }
}
