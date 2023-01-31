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
        // This string is here to get the month selection without having to pass it as a argument 
        // in the save functions.
        public string Month { get; set; }
        List<PaymentDetails>? paymentEntryData = new List<PaymentDetails>();

        PaymentDetails entry2 = new PaymentDetails
        {
            Month = "January",
            Amount = "999.99",
            DueDate = "23rd",
            Category = "Living",
            PaymentName = "Lamborghini"
        };

        public MainWindow()
        {
            InitializeComponent();
            PaymentDetails entry = new PaymentDetails { 
                Month = "January", Amount = "1",
                DueDate = "25rd", Category="Living",
                PaymentName="Yeeahw" };

       

            dataGridPaymentDisplay.Items.Add(entry);
            
            ComboBoxInitializtion();

            FrmAddPayment.NewPaymentEntry += NewPaymentEntry;
            FrmEditPayment.ChangedPaymentEntry += ChangedPaymentEntry;
        }

        

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            if(SaveLoadFunctions.isFileSaved == false)
            {
                var dialogresult = MessageBox.Show("Would you like to save your changes?",
                    "Changes Unsaved",MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(dialogresult == MessageBoxResult.Yes)
                {
                    GetPaymentEntryData();
                    SaveLoadFunctions.ExpressSave(paymentEntryData);
                    MessageBox.Show("Saved");
                }
                else if (dialogresult == MessageBoxResult.No)  
                {
                    //TODO: Implement new grid file data
                }
            }
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            GetPaymentEntryData();
            SaveLoadFunctions.ExpressSave(paymentEntryData);
        }

        private void MenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            GetPaymentEntryData();
            SaveLoadFunctions.SaveFile(paymentEntryData);
        }

        private void MenuLoad_Click(object sender, RoutedEventArgs e)
        {
            var entries = SaveLoadFunctions.LoadFile();
            entries = paymentEntryData;
            var result = from entry in paymentEntryData
                         where entry.Month == cmboMonths.SelectedItem.ToString()
                         select entry;

            foreach (var entry in result)
            {
                dataGridPaymentDisplay.Items.Add(entry);
            }
            
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            if(SaveLoadFunctions.isFileSaved == false)
            {
                var dialogResult = MessageBox.Show("Would you like to save changes before exiting?",
                    "Information", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                if(dialogResult == MessageBoxResult.Yes)
                {
                    GetPaymentEntryData();
                    SaveLoadFunctions.ExpressSave(paymentEntryData);
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
            var paymentEntry = dataGridPaymentDisplay.SelectedItem as PaymentDetails;
            FrmEditPayment frmEditPayment = new FrmEditPayment(paymentEntry);
            frmEditPayment.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure you want to delete this entry?",
                                "Confirm Deletion",MessageBoxButton.YesNo,MessageBoxImage.Information);
            
            if(dialogResult == MessageBoxResult.Yes)
            {
                dataGridPaymentDisplay.Items.Remove(dataGridPaymentDisplay.SelectedItem);
            }
            else
            {
                return;
            }
            
        }


        private void dataGridPaymentDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGridPaymentDisplay.SelectedItem != null)
            {
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
            
        }

        private void cmboMonths_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (paymentEntryData != null)
            {
                var filterByMonth = from entry in paymentEntryData
                                    where entry.Month == cmboMonths.SelectedItem.ToString()
                                    select entry;

                SaveLoadFunctions.ExpressSave(paymentEntryData);

                dataGridPaymentDisplay.Items.Clear();

                foreach (var entry in filterByMonth)
                {
                    dataGridPaymentDisplay.Items.Add(entry);
                }
            }
            else
            {
                return;
            }
        }

        private void NewPaymentEntry(PaymentDetails paymentDetails)
        {
            dataGridPaymentDisplay.Items.Add(paymentDetails);
        }

        private void ChangedPaymentEntry(PaymentDetails paymentDetails)
        {
            dataGridPaymentDisplay.Items.Remove(dataGridPaymentDisplay.SelectedItem);
            dataGridPaymentDisplay.Items.Add(paymentDetails);
        }

        private void ComboBoxInitializtion()
        {
            cmboMonths.Items.Add("January");
            cmboMonths.Items.Add("Feburary");
            cmboMonths.Items.Add("March");
            cmboMonths.Items.Add("April");
            cmboMonths.Items.Add("May");
            cmboMonths.Items.Add("June");
            cmboMonths.Items.Add("July");
            cmboMonths.Items.Add("August");
            cmboMonths.Items.Add("September");
            cmboMonths.Items.Add("October");
            cmboMonths.Items.Add("November");
            cmboMonths.Items.Add("December");
            cmboMonths.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void GetPaymentEntryData()
        {
            //List<PaymentDetails> _paymentDetails = new List<PaymentDetails>();

            if (dataGridPaymentDisplay.Items != null)
            {
                foreach (var item in dataGridPaymentDisplay.Items)
                {
                    paymentEntryData.Add(item as PaymentDetails);
                }
                foreach (var entry in paymentEntryData)
                {
                    entry.Month = cmboMonths.SelectedItem.ToString();
                }
            }
            else
            {
                return;
            }
        }


    }
}
