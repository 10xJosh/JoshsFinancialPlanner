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

        public SaveLoadFunctions()
        {

        }

        public static void SaveFile()
        {
            string path = "";
            SaveFileDialog saveFileDialog= new SaveFileDialog();

            saveFileDialog.Title = "Select Location to Save file";
            saveFileDialog.Filter = "Josh's Financial Planner (*.jfp)|*.jfp";
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.ShowDialog();

            // TODO: Loop through entries entered and save them to the file
            // TODO: Use "using" instead of file.Close();
            // TODO: impliment isolated storage

            if (saveFileDialog.FileName != "")
            {
                path = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                
                AppSettings overview = new AppSettings();
                XmlSerializer writer = new XmlSerializer(typeof(AppSettings));

                FileStream file = File.Create(path);

                writer.Serialize(file, overview);
                file.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            isFileSaved = true;
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
                XmlSerializer reader = new XmlSerializer(typeof(AppSettings));
                StreamReader file = new StreamReader((Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/SerializationOverview.jfp"));
                AppSettings overview = (AppSettings)reader.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



    }
}
