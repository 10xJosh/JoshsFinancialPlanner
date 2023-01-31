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
        // This function is used for situations where SaveFileDialog box shouldnt appear
        public static void ExpressSave(List<PaymentDetails>? paymentEntries)
        {
            if(Path != null)
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PaymentDetails>));

                    using (FileStream fs = File.Create(Path))
                    {
                        xmlSerializer.Serialize(fs, paymentEntries);
                        fs.Close();
                    }
                    isFileSaved = true;
                }
                catch
                {
                    SaveFile(paymentEntries);
                }
            } 
            else
            {
                SaveFile(paymentEntries);
            }
        }

        // This method is called when month selection is changed so that no data is lost when
        // switching between months in the user interface.
        public static void MonthExpressSave(List<PaymentDetails>? paymentEntries, string month) 
        {
            if (Path != null)
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PaymentDetails>));

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
            }
            else
            {
                SaveFile(paymentEntries);
            }
        }
        
        public static List<PaymentDetails> LoadFile()
        {
            string path = "";
            List<PaymentDetails>? paymentEntries = new List<PaymentDetails>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "Josh's Financial Planner (*.jfp)|*.jfp";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                path = openFileDialog.FileName;
            }

            try
            {
                path = openFileDialog.FileName;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PaymentDetails>));

                using(StreamReader selectedFile = new StreamReader(path))
                {
                    paymentEntries = xmlSerializer.Deserialize(selectedFile) as List<PaymentDetails>;
                }

                return paymentEntries;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }
        
    }
}
