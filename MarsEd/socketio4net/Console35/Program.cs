using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quobject.SocketIoClientDotNet.Client;

namespace Console35
{
    class Program
    {
        static void Main(string[] args)
        {
            var socket = IO.Socket("http://localhost");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                socket.Emit("hi");

            });

            socket.On("hi", (data) =>
            {
                Console.WriteLine(data);
                socket.Disconnect();
            });
            Console.ReadLine();
        }
    }
}
