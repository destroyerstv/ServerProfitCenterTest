using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ServerProfitCenterTest
{
    class Program
    {
        static void GetXmlParam(string filename, out string multicast, out double startRange, out double endRange)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(XmlData));

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                XmlData newXmlData = (XmlData)formatter.Deserialize(fs);
                multicast = newXmlData.Multicast;
                startRange = newXmlData.StartRange;
                endRange = newXmlData.EndRange;
            }
        }

        static async void AsyncStart(UdpEngine srv, string multicast, double startRange,double endRange)
        {
            await Task.Run(() => srv.Start(startRange, endRange));
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter xml config argument.");
                Console.WriteLine("Usage: ServerProfitCenter <filename>");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File {0} not exist", args[0]);
                return;
            }

            string multicast;
            double startRange, endRange;

            try
            {
                GetXmlParam(args[0], out multicast, out startRange, out endRange);
                UdpEngine srv = new UdpEngine(multicast);
                AsyncStart(srv, multicast, startRange, endRange);
                Console.WriteLine("Start");
                Console.WriteLine("Press any key to Stop server");
                Console.ReadKey();
                srv.Stop();
                Console.WriteLine("End");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}