using JoshsFinancialplanner.MenuFunctions;
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
using System.Windows.Shapes;

namespace JoshsFinancialplanner.ButtonFunctions
{
    /// <summary>
    /// Interaction logic for Frm.xaml
    /// </summary>
    public partial class FrmAddPayment : Window
    {
        public PaymentDetails paymentDetails;
        public delegate void OnNewPaymentEntry(PaymentDetails paymentDetails);
        public static event OnNewPaymentEntry NewPaymentEntry;

        public FrmAddPayment()
        {
            InitializeComponent();
            ComboBoxSettings();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(cmboCategory.SelectedItem == null) 
            {
                cmboCategory.SelectedItem = "Uncategorized";
            }

            if (txtPaymentName.Text == "" || txtDueDate.Text == "" || txtAmount.Text == "")
            {
                MessageBox.Show("Entries are missing. Double check everything is filled out " +
                    "in order to add entry.", "Error", MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
            else if (txtPaymentName.Text != "" || txtDueDate.Text != "" || txtAmount.Text != "")
            {
                try
                {
                    if (txtAmount.Text.Contains("$"))
                    {
                        txtAmount.Text = txtAmount.Text.Replace("$", "");
                        Convert.ToDecimal(txtAmount.Text);
                    }
                    
                }
                catch
                {
                    MessageBox.Show("Error: Please reenter Amount ");
                    return;
                }

                if(decimal.TryParse(txtAmount.Text, out decimal parsedAmount))
                {
                    paymentDetails = new PaymentDetails
                    {
                        PaymentName = txtPaymentName.Text,
                        DueDate = txtDueDate.Text,
                        Amount = parsedAmount,
                        Category = cmboCategory.Text
                    };

                    NewPaymentEntry(paymentDetails);
                }
                else
                {
                    
                }


                SaveLoadFunctions.isFileSaved = false;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBoxSettings()
        {
            cmboCategory.Items.Add("Home & Utilities");
            cmboCategory.Items.Add("Transportation");
            cmboCategory.Items.Add("Groceries");
            cmboCategory.Items.Add("Personal & Family Care");
            cmboCategory.Items.Add("Health");
            cmboCategory.Items.Add("Insurance");
            cmboCategory.Items.Add("Resturants & Dining");
            cmboCategory.Items.Add("Shopping & Entertainment");
            cmboCategory.Items.Add("Cash, Checks & misc");
            cmboCategory.Items.Add("Giving");
            cmboCategory.Items.Add("Business Expenses");
            cmboCategory.Items.Add("Education");
            cmboCategory.Items.Add("Finance");
            cmboCategory.Items.Add("Uncategorized");
        }
    }
}
