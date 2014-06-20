using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoursoKama;

namespace BoursokamasStats
{
    class Program
    {
        static String path = "kamas.csv";

        static void Main(string[] args)
        {
            BoursoKamaReader bkr = new BoursoKamaReader();

            bkr.Login("eregrith", "nDKLPSZ7");
            bkr.SelectServer(BoursoKama.BoursoKamaReader.Servers.Djaul);
            while (true)
            {
                UpdateKamas(bkr);
                Thread.Sleep(1000*60*10);
            }
        }

        private static void UpdateKamas(BoursoKamaReader bkr)
        {
            BigInteger kamas = bkr.GetKamas();
            DateTime n = DateTime.Now;
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(n + ";" + kamas.ToString());
            }
            Console.WriteLine("{0} : {1}k", n, kamas);
        }
    }
}
