using JoshsFinancialplanner.MenuFunctions;
using JoshsFinancialplanner.ButtonFunctions.HelperFunctions;
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
        
        public delegate void OnChangedPaymentEntry(PaymentDetails paymentDetails);
        public static event OnChangedPaymentEntry ChangedPaymentEntry;
        InputChecks inputChecks = new InputChecks();
        PaymentDetails paymentDetails;

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
            txtAmount.Text = paymentDetails.Amount;
            txtDueDate.Text = paymentDetails.DueDate;
            cmboCategory.Text = paymentDetails.Category;
        }
        
        //MAKE SURE ANY MAJOR CHANGES ARE REFLECTED ON FrmAddPayment
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            decimal _txtAmount;

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
                if(inputChecks.isPaymentNameShort(txtPaymentName.Text))
                {
                    MessageBox.Show("Payment name is too long. Please shorten the","Error",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else if (inputChecks.isDueDateInputShort(txtDueDate.Text))
                {
                    MessageBox.Show("Due Date entry is too long. Please reenter it in order to continue",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                try
                {
                    if (txtAmount.Text.Contains("$"))
                    {
                        txtAmount.Text = txtAmount.Text.Replace("$", "");
                        _txtAmount = Convert.ToDecimal(txtAmount.Text);
                        Math.Round(_txtAmount);
                    }

                }
                catch
                {
                    MessageBox.Show("Error: Please reenter the Amount.");
                    return;
                }

                if (decimal.TryParse(txtAmount.Text, out decimal parsedAmount))
                {
                    paymentDetails = new PaymentDetails
                    {
                        PaymentName = txtPaymentName.Text,
                        DueDate = txtDueDate.Text,
                        Amount = $"${Math.Round(parsedAmount)}",
                        Category = cmboCategory.Text
                    };

                    ChangedPaymentEntry(paymentDetails);
                }
                else
                {
                    MessageBox.Show("Error: Please reenter the amount. ", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
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
