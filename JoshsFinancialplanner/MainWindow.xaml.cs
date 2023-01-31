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
    public partial class MainWindow : Window
    {
        List<PaymentDetails>? paymentEntryData = new List<PaymentDetails>();

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
                    dataGridPaymentDisplay.Items.Clear();
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
            try
            {
                var entries = SaveLoadFunctions.LoadFile();
                paymentEntryData = entries;
                var result = from entry in paymentEntryData
                             where entry.Month == cmboMonths.Text
                             select entry;

                foreach (var entry in result)
                {
                    dataGridPaymentDisplay.Items.Add(entry);
                }
            }catch(Exception)
            {
                MessageBox.Show("There was an error loading the file. Make sure that it is not an empty file.");
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
                SaveLoadFunctions.isFileSaved = false;
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

        private void cmboMonths_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: add this function into a seperate function to be called when the selection has been changed.

            if (paymentEntryData != null)
            {
                var filterByMonth = from entry in paymentEntryData
                                    where entry.Month == cmboMonths.Text
                                    select entry;


                dataGridPaymentDisplay.Items.Clear();

                foreach (var entry in filterByMonth)
                {
                    dataGridPaymentDisplay.Items.Add(entry);
                }
            }
            else
            {
                MessageBox.Show("payment data is null");
                return;
            }
        }

        private void NewPaymentEntry(PaymentDetails paymentDetails)
        {
            //cmboMonths is inaccessible outside MainWindow so the data is added here
            paymentDetails.Month = cmboMonths.Text;
            paymentEntryData.Add(paymentDetails);
            dataGridPaymentDisplay.Items.Add(paymentDetails);
        }

        private void ChangedPaymentEntry(PaymentDetails paymentDetails)
        {
            //This is here to make sure that the edited data is updated within memory
            if (paymentEntryData.Contains(dataGridPaymentDisplay.SelectedItem as PaymentDetails))
            {
                paymentEntryData.Remove(dataGridPaymentDisplay.SelectedItem as PaymentDetails);
                paymentEntryData.Add(paymentDetails);
            }
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
