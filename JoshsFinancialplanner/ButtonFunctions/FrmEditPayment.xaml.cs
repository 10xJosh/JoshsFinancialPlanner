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
    /// Edit Payment Form
    /// </summary>
    public partial class FrmEditPayment : Window
    {
        PaymentDetails paymentDetails;
        public delegate void OnChangedPaymentEntry(PaymentDetails paymentDetails);
        public static event OnChangedPaymentEntry ChangedPaymentEntry;

        public FrmEditPayment()
        {
            InitializeComponent();
            ComboBoxSettings();
        }

        public FrmEditPayment(PaymentDetails paymentDetails)
        {
            InitializeComponent();
            ComboBoxSettings();

            txtPaymentName.Text = paymentDetails.PaymentName;
            txtAmount.Text = paymentDetails.Amount.ToString();
            txtDueDate.Text = paymentDetails.DueDate;
            cmboCategory.Text = paymentDetails.Category;
        }
        
        //THIS CLASS WAS COPIED OVER, MAKE SURE ANY MAJOR CHANGES ARE REFLECTED ON FrmAddPayment
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cmboCategory.SelectedItem == null)
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

                if (decimal.TryParse(txtAmount.Text, out decimal parsedAmount))
                {
                    paymentDetails = new PaymentDetails
                    {
                        PaymentName = txtPaymentName.Text,
                        DueDate = txtDueDate.Text,
                        Amount = parsedAmount,
                        Category = cmboCategory.Text
                    };

                    ChangedPaymentEntry(paymentDetails);
                }
                else
                {
                    return;
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
