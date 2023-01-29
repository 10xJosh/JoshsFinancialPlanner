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

namespace JoshsFinancialplanner.MenuFunctions
{
    internal class SaveLoadFunctions
    {
        public static bool isFileSaved { get; set; }
        public static string Path { get; set; }

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
                    XmlSerializer writer = new XmlSerializer(typeof(List<PaymentDetails>));

                    using (FileStream fs = File.Create(Path))
                    {
                        writer.Serialize(fs, paymentEntries);
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
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "Josh's Financial Planner (*.jfp)|*.jfp";

            // TODO: Loop through entries entered and load them to the Program
            // TODO: Use "using" instead of file.Close();
            // TODO: impliment isolated storage

            if (openFileDialog.FileName != "")
            {
                path = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                path = openFileDialog.FileName;
                XmlSerializer reader = new XmlSerializer(typeof(PaymentDetails));
                StreamReader file = new StreamReader((Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/SerializationOverview.jfp"));
                PaymentDetails overview = (PaymentDetails)reader.Deserialize(file);
                file.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



    }
}
