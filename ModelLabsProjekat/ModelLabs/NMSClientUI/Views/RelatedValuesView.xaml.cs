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
    /// Interaction logic for RelatedValuesView.xaml
    /// </summary>
    public partial class RelatedValuesView : UserControl
    {
        GDAClient gdaClient;

        public RelatedValuesView()
        {
            InitializeComponent();
            gdaClient = new GDAClient();
            GIDSelectionCombobox.ItemsSource = gdaClient.GetAllGID();
            ConcreteSelectionCombobox.ItemsSource = gdaClient.GetConcreteModelCodes();
            PropertiesSelection.ItemsSource = gdaClient.GetAllProperties();
        }

        private void GIDSelectionCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            long selectedGID = (long)GIDSelectionCombobox.SelectedItem;
            if (selectedGID == 0) { return; }

            ReferenceSelectionCombobox.ItemsSource = gdaClient.GetRefrenceModelCodes(selectedGID);
            ReferenceSelectionCombobox.SelectedIndex = 0;
        }

        private void ConcreteSelectionCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PropertiesSelection.ItemsSource = gdaClient.GetPropertiesModelCodes((ModelCode)ConcreteSelectionCombobox.SelectedItem);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            DataTextBlock.Text = string.Empty;

            long selectedGID = (long)GIDSelectionCombobox.SelectedItem;
            if (selectedGID == 0) { return; }

            List<ModelCode> properties = PropertiesSelection.SelectedItems.Cast<ModelCode>().ToList();

            Association association;

            if (ConcreteSelectionCombobox.SelectedItem == null)
            {
                association = new Association((ModelCode)ReferenceSelectionCombobox.SelectedItem);
            }
            else
            {
                association = new Association((ModelCode)ReferenceSelectionCombobox.SelectedItem, (ModelCode)ConcreteSelectionCombobox.SelectedItem);
            }

            DataTextBlock.Text = gdaClient.GetRelatedValues(selectedGID, properties, association);
        }

    }
}
