using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace PingClient
{
    class Program
    {
        private const string host = "127.0.0.1";
        private const int port = 1055;
        static void Main(string[] args)
        {

            Stopwatch stopWatch = new Stopwatch();
            //int port = Convert.ToInt32(args[0]);
            IPEndPoint localpt = new IPEndPoint(IPAddress.Any, port);
            // Create random number generator for use in simulating
            // packet loss and network delay.
            Random random = new Random();
            // Create a datagram socket for receiving and sending UDP packets
            // through the port specified on the command line.
            UdpClient socket = new UdpClient(host, port);
            socket.Client.SetSocketOption(SocketOptionLevel.Socket,
           SocketOptionName.ReuseAddress, true);
            System.Net.IPEndPoint ep = null;
            socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 1000);
            socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);

            // Processing loop.
            for (int i = 0; i < 10; i++)
            {   
                // Block until the host receives a UDP packet.
                byte[] msg = Encoding.UTF8.GetBytes("PING " + i + " " + DateTime.Now.ToString("h:mm:ss tt")+"\r\n");

                stopWatch.Start();
                socket.Send(msg, msg.Length);
                Console.WriteLine("Sending ping\r\n");
                // Print the recieved data.
                try
                {
                    byte[] rdata = socket.Receive(ref ep);
                    stopWatch.Stop();
                    Console.WriteLine("Ping request was answered with RTT: "+ stopWatch.Elapsed + "\r\n");
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Ping was not answered within 1 second. The packet has been dropped.");
                }

                stopWatch.Reset();
            }
        }
    }
}