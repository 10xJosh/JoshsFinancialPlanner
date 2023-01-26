using JoshsFinancialplanner.MenuFunctions;
using JoshsFinancialplanner.ButtonFunctions;
using Microsoft.Win32;
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

namespace JoshsFinancialplanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SaveLoadFunctions saveLoadFunctions = new SaveLoadFunctions();

        public MainWindow()
        {
            InitializeComponent();
            PaymentDetails entry = new PaymentDetails { Month = "January", Amount = 999.99m,
                DueDate = "23", Category="Living",PaymentName="Lamborghini" };

            dataGridPaymentDisplay.Items.Add(entry);
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadFunctions.SaveFile();
        }

        private void MenuLoad_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadFunctions.LoadFile();

        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            if(SaveLoadFunctions.isFileSaved == false)
            {
                var dialogResult = MessageBox.Show("Would you like to save changes before exiting?",
                    "Information", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                if(dialogResult == MessageBoxResult.Yes)
                {
                    SaveLoadFunctions.SaveFile();
                    this.Close();
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrmAddPayment frmAddPaymen = new FrmAddPayment();
            frmAddPaymen.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridPaymentDisplay.SelectedItem != null)
            {
                FrmAddPayment frmAddPaymen = new FrmAddPayment();
                frmAddPaymen.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //dataGridPaymentDisplay;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGridPaymentDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEdit.IsEnabled= true;
            btnDelete.IsEnabled= true;
        }
    }
}
