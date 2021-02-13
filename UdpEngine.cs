using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerProfitCenterTest
{
    class UdpEngine
    {
        private IPAddress Multicast; // хост для отправки данных
        private const int remotePort = 8001; // порт для отправки данных
        private bool ExitFlag = false;
        private bool StopOK = false;

        private double RandomGenerator(double startRange, double endRange)
        {
            Random random = new Random();
            double d = startRange + random.NextDouble() * (endRange - startRange);
            return d;
        }

        public UdpEngine(string multicast)
        {
            Multicast = IPAddress.Parse(multicast);
        }

        public void Start(double startRange, double endRange)
        {
            UInt64 numPacket = 0;
            Packet packet;
            UdpClient sender = new UdpClient(); // создаем UdpClient для отправки
            IPEndPoint endPoint = new IPEndPoint(Multicast, remotePort);
            try
            {
                while (true)
                {
                    double quotation = RandomGenerator(startRange, endRange);
                    packet.NumPacket = numPacket;
                    packet.Quotation = quotation;
                    packet.StartRange = startRange;
                    packet.EndRange = endRange;

                    //Console.WriteLine(numPacket);
                    //Console.WriteLine(quotation);
                    //Console.WriteLine(startRange);
                    //Console.WriteLine(endRange);

                    if (numPacket == UInt64.MaxValue)
                        numPacket = 0;
                    else
                        numPacket++;

                    byte[] data = packet.GetBytes();
                    int bytes = sender.Send(data, data.Length, endPoint); // отправка

                    if (ExitFlag)
                        break;

                    //Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
                Console.WriteLine("Stop OK");
                StopOK = true;
            }
            
        }

        public void Stop()
        {
            ExitFlag = true;
            while (!StopOK) ;

        }
    }
}
