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
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        UdpClient udp = new UdpClient();
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = rsa.ToXmlString(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] a = Encoding.ASCII.GetBytes(textBox2.Text);
            label4.Text = Convert.ToBase64String(rsa.SignData(a, "SHA1"));
            String c = textBox2.Text + ":" + label4.Text;
            byte[] b = Encoding.ASCII.GetBytes(c);
            udp.Send(b, b.Length, ep);
        }
    }
}
