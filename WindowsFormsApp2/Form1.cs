using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ci = new ConnectionInfo("192.168.137.99","pi",new PasswordAuthenticationMethod("pi","!betterwhy123"));
            var cli = new SshClient(ci);
            cli.Connect();

            var output = cli.CreateCommand("ls -al").Execute();

            Console.WriteLine(output);

            using (var sftp = new SftpClient(ci))
            {
                sftp.Connect();

                using (var infile = File.Open("test_gunho.txt",FileMode.Open))
                {
                    sftp.UploadFile(infile, "./test_gunho2.txt");
                }
                sftp.Disconnect();
            }

            cli.CreateCommand("sudo reboot").Execute();
            cli.Disconnect();
        }
    }
}
