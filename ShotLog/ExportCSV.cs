using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using CsvHelper;

namespace ShotLog
{
    class ExportCSV
    {
        public static void ExportVideo(List<VideoItem> source)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "CSV File|*.csv",
                Title = "Export Video Log"
            };
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {

                using (var writer = new StreamWriter(saveFileDialog1.FileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(source);
                }

            }
            
                
        }


        public static void ExportStills(List<StillItem> source)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "CSV File|*.csv",
                Title = "Export Stills Log"
            };
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {

                using (var writer = new StreamWriter(saveFileDialog1.FileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(source);
                }

            }


        }
    }
}
