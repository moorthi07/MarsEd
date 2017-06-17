﻿using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace uwpSocketio
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var socket = IO.Socket("http://adamt.marsed.ngrok.io");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Debug.WriteLine("Emitting hi");
                socket.Emit("hi");
                Debug.WriteLine("Emitted hi");
            });

            socket.On("hi", (data) =>
            {
                Debug.WriteLine("Received");
                Debug.WriteLine(data);
                socket.Disconnect();
            });
        }
    }
}
