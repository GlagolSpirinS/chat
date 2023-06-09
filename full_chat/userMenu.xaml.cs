﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace full_chat
{
    public partial class userMenu : Window
    {
        private Socket server;

        public userMenu(string ipText)
        {
            InitializeComponent();

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.ConnectAsync(ipText, 8888);
            SendMessage(MainWindow.nameTexBox + " подключился");
            RecieveMessage();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();

            SendMessage(MainWindow.nameTexBox + " отключился");

            MainWindow.Show();

            this.Close();

        }

        private async Task RecieveMessage()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await server.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);

                if (message.Contains(" подключился"))
                {
                    string[] strings = message.Split(' ');
                    user_list.Items.Add(strings[0]);
                }
                if (message.Contains(" отключился"))
                {
                    string[] strings = message.Split(' ');
                    user_list.Items.Remove(strings[0]);
                }

                chat.Items.Add(message);
            }
        }



        private async Task SendMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await server.SendAsync(bytes, SocketFlags.None);
            
            if(message.Contains("/disconet"))
            {
                MainWindow MainWindow = new MainWindow();

                MainWindow.Show();

                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(MainWindow.nameTexBox + ": " + text.Text);
        }
    }
}
