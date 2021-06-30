using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
namespace Ping_Lab4
{
    class Program
    {
        private const double LOSS_RATE = 0.3;
        private const double AVG_DELAY = 100; //milliseconds
        private const string host = "127.0.0.1";
        private const int port = 1055;
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Required arguments: port");
                return;
            }
            int port = Convert.ToInt32(args[0]);
            IPEndPoint localpt = new IPEndPoint(IPAddress.Any, port);
            // Create random number generator for use in simulating
            // packet loss and network delay.
            Random random = new Random();
            // Create a datagram socket for receiving and sending UDP packets
            // through the port specified on the command line.
            UdpClient socket = new UdpClient(host, port);
            socket.Client.SetSocketOption(SocketOptionLevel.Socket,
           SocketOptionName.ReuseAddress, true);
            socket.Client.Bind(localpt);
            System.Net.IPEndPoint ep = null;
            socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 1000);

            // Processing loop.
            for(int i =0; i<10; i++)
            {
                // Block until the host receives a UDP packet.
                byte[] msg = Encoding.UTF8.GetBytes("PING "+i+" "+DateTime.Now.ToString("h:mm:ss tt"));
                socket.Send(null, 0, ep);
                // Print the recieved data.
            }
        }
    }
}