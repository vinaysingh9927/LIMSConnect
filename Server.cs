using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIMSConnect
{
    public partial class Server : Form
    {
        TcpListener server = null;
        bool isRunning = false;

        public Server()
        {
            InitializeComponent();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRunning)
                {
                    isRunning = false;
                    server.Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting server: " + ex.Message);
            }
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int port = 5600;
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                isRunning = true;

                lblIP.Text = GetLocalIPAddress(); // Show actual IP on UI
                txtReceived.Text = "Server started and listening on port " + port;

                await Task.Run(() => AcceptClientOnce());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting server: " + ex.Message);
            }
        }

        private void AcceptClientOnce()
        {
            try
            {
                // Accept a single client
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Update UI from the main thread
                Invoke(new Action(() =>
                {
                    txtReceived.Text = "Received: " + message;
                }));

                // Send response
                string response = "ACK";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);

                // Cleanup
                stream.Close();
                client.Close();
                server.Stop();
                isRunning = false;
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("Error handling client: " + ex.Message);
                }));
            }
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        private void Server_Load(object sender, EventArgs e)
        {
            txtReceived.Text = "Server not started.";
        }
    }
}
