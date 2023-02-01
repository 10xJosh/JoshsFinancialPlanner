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
        List<PaymentDetails> paymentEntryData = new List<PaymentDetails>();
        //TotalAmount is used for the lblAmount label 
        private int TotalAmount { get; set; } = 0;
        // PreviousValue is used in case the user edits an entry so that the previous
        // value can be subtracted or added to the total value.
        private int PreviousValue { get; set; } = 0;

        public MainWindow()
        {
            InitializeComponent();

            FrmAddPayment.NewPaymentEntry += NewPaymentEntry;
            FrmEditPayment.ChangedPaymentEntry += ChangedPaymentEntry;   
        }

        

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            if (SaveLoadFunctions.isFileSaved == false)
            {
                var dialogresult = MessageBox.Show("Would you like to save your changes?",
                    "Changes Unsaved", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogresult == MessageBoxResult.Yes)
                {
                    SaveLoadFunctions.SaveFile(paymentEntryData);
                    MessageBox.Show("Saved");

                    dataGridPaymentDisplay.Items.Clear();
                    TotalAmount = 0;
                    lblAmount.Content = $"${TotalAmount}";
                }
                else if (dialogresult == MessageBoxResult.No)
                {
                    dataGridPaymentDisplay.Items.Clear();
                    TotalAmount = 0;
                    lblAmount.Content = $"${TotalAmount}";
                }
            }
            else
            {
                dataGridPaymentDisplay.Items.Clear();
                TotalAmount = 0;
                lblAmount.Content = $"${TotalAmount}";
            }
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadFunctions.ExpressSave(paymentEntryData);
        }

        private void MenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadFunctions.SaveFile(paymentEntryData);
        }

        private void MenuLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var entries = SaveLoadFunctions.LoadFile();

                foreach (var entry in entries)
                {
                    dataGridPaymentDisplay.Items.Add(entry);
                }

                GetPaymentTotal(entries);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            PreviousValue = Int32.Parse(paymentEntry.Amount.ToString().Replace("$",""));
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

        private void NewPaymentEntry(PaymentDetails paymentDetails)
        {
            TotalAmount += Int32.Parse(paymentDetails.Amount.Replace("$",""));
            lblAmount.Content = $"${TotalAmount}";

            paymentEntryData.Add(paymentDetails);
            dataGridPaymentDisplay.Items.Add(paymentDetails);
        }

        private void ChangedPaymentEntry(PaymentDetails paymentDetails)
        {
            GetChangedDifference(PreviousValue, 
                Int32.Parse(paymentDetails.Amount.ToString().Replace("$","")));

            lblAmount.Content = $"${TotalAmount}";
            dataGridPaymentDisplay.Items.Remove(dataGridPaymentDisplay.SelectedItem);
            dataGridPaymentDisplay.Items.Add(paymentDetails);
        }

        private void GetChangedDifference(int previousValue, int currentValue)
        {
            int difference; 
            if (previousValue < currentValue)
            {
                difference = currentValue - previousValue;
                TotalAmount += difference;
            }
            else if (previousValue > currentValue)
            {
                difference = previousValue - currentValue;
                TotalAmount -= difference;
            }
        }

        private void GetPaymentTotal(List<PaymentDetails> paymentEntries)
        {
            List<string> Amounts = new List<string>();
            

            foreach (var entry in paymentEntries)
            {
                Amounts.Add(entry.Amount.Replace("$", ""));
            }

            foreach (var entry in Amounts)
            {
                TotalAmount += Int32.Parse(entry);
            }

            if(TotalAmount < 0)
            {
                lblAmount.Content = $"$0";
            }
            else
            {
                lblAmount.Content = $"${TotalAmount.ToString()}";
            }
        }
    }
}
