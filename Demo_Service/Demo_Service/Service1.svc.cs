using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace Demo_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            log();
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "-meh";
            }

           // IsolatedStorageFileStream logfile = new IsolatedStorageFileStream("log", System.IO.FileMode.OpenOrCreate);
            //logfile.

            log();
            
            return composite;
            
        }

        private static void log()
        {
            System.IO.StreamWriter writer = new StreamWriter("log.text");
            writer.WriteLine(DateTime.Now.ToString());
            writer.Close();
        }

        public string getajoke()
        {
            log();
            return "Tropical Hair Party !";
        }
    }
}
