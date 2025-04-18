using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;


namespace LIMSConnect
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtIP.Text))
            {
                MessageBox.Show("Enter Host IP", "Warning");
                return;
            }
            else if (string.IsNullOrEmpty(txtPort.Text))
            {
                MessageBox.Show("Enter TCP/IP Port","Warning");
                return;
            }
            else if (string.IsNullOrEmpty(txtmessage.Text))
            {
                MessageBox.Show("Enter Message to Communicate", "Warning");
                return;
            }

            string serverIp = txtIP.Text; // Replace with the lab machine's IP address
            int port = Convert.ToInt32(txtPort.Text); // 
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(serverIp, port);
                if(client.Connected)
                {
                    MessageBox.Show("Connected Successfully");
                }
                //NetworkStream stream = client.GetStream();

                //string command = "GET CBC DATA";
                //byte[] data = Encoding.ASCII.GetBytes(command);
                //stream.Write(data, 0, data.Length);
                //MessageBox.Show("Command sent.");

                //byte[] buffer = new byte[1024];
                //int bytesRead = stream.Read(buffer, 0, buffer.Length);
                //string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //MessageBox.Show("Received: " + response);

                client.Close();


                //using (TcpClient client = new TcpClient(serverIp, port))
                //using (NetworkStream stream = client.GetStream())
                //{
                //    // Send data to the lab machine
                //    string message =txtmessage.Text ;
                //    byte[] data = Encoding.ASCII.GetBytes(message);
                //    stream.Write(data, 0, data.Length);
                //    MessageBox.Show(message, "Message Sent");

                //    client.ReceiveTimeout = 1200000;

                //    // Receive response from the lab machine
                //    byte[] buffer = new byte[256];
                //    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                //    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //    MessageBox.Show(response,"Message Received");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Client_Load(object sender, EventArgs e)
        {
            txtPort.Text = "5000";
            //TcpListener server = null;
            //try
            //{
            //    int port = 5600;
            //    server = new TcpListener(IPAddress.Any, port);
            //    server.Start();
            //    //lblIP.Text = IPAddress.Any.ToString();
            //    Console.WriteLine("Server is listening on port " + port);

            //    // Accept client connection
            //    TcpClient client = server.AcceptTcpClient();
            //    Console.WriteLine("Client connected.");

            //    NetworkStream stream = client.GetStream();

            //    // Read incoming message
            //    byte[] buffer = new byte[256];
            //    int bytesRead = stream.Read(buffer, 0, buffer.Length);
            //    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //    Console.WriteLine("Received: " + message);
            //    txtmessage.Text = message;

            //    // Optionally respond
            //    string response = "Message received!";
            //    byte[] responseData = Encoding.UTF8.GetBytes(response);
            //    stream.Write(responseData, 0, responseData.Length);

            //    // Clean up
            //    stream.Close();
            //    client.Close();
            //    server.Stop();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //}
        }

        
    }
}
