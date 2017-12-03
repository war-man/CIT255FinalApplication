using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;


namespace Data
{
    /// <summary>
    /// this class sets up the xml file
    /// </summary>
    public class InitializeDataFileXML
    {
        /// <summary>
        /// creates a web client and uses it to download data from the xml file
        /// </summary>
        public static void PullDataApi()
        {
            
            //
            // try accessing the Wunderground API and save to file
            //
            try
            {
                using (WebClient client = new WebClient())
                {
                    string value = client.DownloadString(DataSettings.dataFilePathAPI);
                    //File.AppendAllText(Data.DataSettings.dataFilePathLocal, value);
                    File.WriteAllText(DataSettings.dataFilePathLocal, value);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Whoops! We could not save the data from the Wunderground API for some reason");

                Console.WriteLine("Press any key to continue.");

                Console.ReadKey();
            }
        }
    }
}
