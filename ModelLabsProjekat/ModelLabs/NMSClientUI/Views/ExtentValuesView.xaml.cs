using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace NMSClientUI.Views
{
    /// <summary>
    /// Interaction logic for ExtentValuesView.xaml
    /// </summary>
    public partial class ExtentValuesView : UserControl
    {
        GDAClient gdaClient;
        
        public ExtentValuesView()
        {
            InitializeComponent();
            gdaClient = new GDAClient();
            ConcreteSelectionCombobox.ItemsSource = gdaClient.GetConcreteModelCodes();
        }

        private void ConcreteSelectionCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModelCode selectedConcrete = (ModelCode)ConcreteSelectionCombobox.SelectedItem;
            if (selectedConcrete == 0) { return; }

            PropertiesSelection.ItemsSource = gdaClient.GetModelCodes(selectedConcrete);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            ModelCode selectedConcrete = (ModelCode)ConcreteSelectionCombobox.SelectedItem;
            if (selectedConcrete == 0) { return; }

            List<ModelCode> properties = PropertiesSelection.SelectedItems.Cast<ModelCode>().ToList();

            DataTextBlock.Text = gdaClient.GetExtentValues(selectedConcrete, properties);
        }
    }
}
