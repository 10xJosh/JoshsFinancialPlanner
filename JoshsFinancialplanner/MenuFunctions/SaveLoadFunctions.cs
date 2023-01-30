using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.DataContracts;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Windows;
using static JoshsFinancialplanner.ButtonFunctions.FrmAddPayment;

namespace JoshsFinancialplanner.MenuFunctions
{
    internal class SaveLoadFunctions
    {
        public static event OnNewPaymentEntry NewPaymentEntry;
        public static bool isFileSaved { get; set; }
        public static string? Path { get; set; }

        public SaveLoadFunctions()
        {

        }

        public static void GetGridData(PaymentDetails[] paymentDetails)
        {

        }

        public static void SaveFile(List<PaymentDetails>? paymentEntries)
        {
            if(paymentEntries == null)
            {
                return;
            }

            SaveFileDialog saveFileDialog= new SaveFileDialog();

            saveFileDialog.Title = "Select Location to Save file";
            saveFileDialog.Filter = "Josh's Financial Planner (*.jfp)|*.jfp";
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.ShowDialog();

            // TODO: Loop through entries entered and save them to the file
            // TODO: Use "using" instead of file.Close();
            // TODO: impliment isolated storage

            if (saveFileDialog.FileName != "")
            {
                Path = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(List<PaymentDetails>));

                using (FileStream fs = File.Create(Path))
                {
                    writer.Serialize(fs, paymentEntries);
                    fs.Close();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            isFileSaved = true;
        }
        // This function is used so that the SaveFileDialog box doesn't appear
        public static void ExpressSave(List<PaymentDetails>? paymentEntries)
        {
            if(Path != null)
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(PaymentDetails));

                    using (FileStream fs = File.Create(Path))
                    {
                        xmlSerializer.Serialize(fs, paymentEntries);
                        fs.Close();
                    }
                }
                catch
                {
                    SaveFile(paymentEntries);
                }
            } else
            {
                SaveFile(paymentEntries);
            }
        }
        
        public static void LoadFile()
        {
            string path = "";
            List<PaymentDetails>? paymentEntries = new List<PaymentDetails>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "Josh's Financial Planner (*.jfp)|*.jfp";

            // TODO: Loop through entries entered and load them to the Program
            // TODO: Use "using" instead of file.Close();
            // TODO: impliment isolated storage

            if (openFileDialog.FileName != "")
            {
                path = openFileDialog.FileName;
            }

            try
            {
                path = openFileDialog.FileName;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(PaymentDetails));
                PaymentDetails storage = new PaymentDetails();

                using(StreamReader selectedFile = new StreamReader(path))
                {
                    storage = xmlSerializer.Deserialize(selectedFile) as PaymentDetails;
                }

                foreach (var item in storage)
                {
                    MessageBox.Show(item.pay)
                }
                /*
                foreach (var entry in storage)
                {
                    paymentEntries.Add(entry);
                }*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return;
        }
        
    }
}
