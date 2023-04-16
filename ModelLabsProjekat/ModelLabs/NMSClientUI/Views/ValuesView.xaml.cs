using FTN.Common;
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

namespace NMSClientUI.Views
{
    /// <summary>
    /// Interaction logic for ValuesView.xaml
    /// </summary>
    public partial class ValuesView : UserControl
    {
        GDAClient gdaClient;

        public ValuesView()
        {
            InitializeComponent();
            gdaClient = new GDAClient();
            GIDSelectionCombobox.ItemsSource = gdaClient.GetAllGID();
        }

        private void GIDSelectionCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            long selectedGID = (long)GIDSelectionCombobox.SelectedItem;
            if (selectedGID == 0) { return; }

            PropertiesSelection.ItemsSource = gdaClient.GetPropertiesModelCodes(selectedGID);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            DataTextBlock.Text = string.Empty;

            long selectedGID = (long)GIDSelectionCombobox.SelectedItem;
            if (selectedGID == 0) { return; }

            List<ModelCode> properties = PropertiesSelection.SelectedItems.Cast<ModelCode>().ToList();

            DataTextBlock.Text = gdaClient.GetValues(selectedGID, properties);
        }
    }
}
