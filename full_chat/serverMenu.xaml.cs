using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;

namespace full_chat
{
    public partial class serverMenu : Window
    {

        private Socket socket;
        private List<Socket> clients = new List<Socket>();

        public serverMenu()
        {
            InitializeComponent();
            IPEndPoint ipPont = new IPEndPoint(IPAddress.Any, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPont);
            socket.Listen(1000);
            ListenToClients();
        }

        private async Task ListenToClients()
        {
            while (true)
            {
                Socket client = await socket.AcceptAsync();
                clients.Add(client);
                ReciveMessage(client);
            }
        }

        private async void ReciveMessage(Socket client)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await client.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);

                chat.Items.Add(message);

                foreach (var item in clients)
                {
                    SendMessage(item, message);
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();

            MainWindow.Show();

            this.Close();
        }
        
        private async Task SendMessage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(bytes, SocketFlags.None);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chat.Items.Add($"[Сообщение от сервера]: {text.Text}");

            foreach (var item in clients)
            {

                SendMessage(item, "[Сообщение от сервера]: " + text.Text);
            }
        }
    }
}
