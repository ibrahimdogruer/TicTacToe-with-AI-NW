using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class ConnectionToNetwork : Form
    {
        public ConnectionToNetwork(Form1 f)
        {
            InitializeComponent();
            this.form1 = f;
        }

        Form1 form1;

        public static string txtStaticLocalPort;
        public static string txtStaticFriendsPort;
        public static string txtStaticFriendsIP;


        private void ConnectionToNetwork_Load(object sender, EventArgs e)
        {
            txtLocalIP.Text = Form1.firstPlayerIP;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmbLocalPort.Text == "" || cmbFriendsPort.Text == "" || txtFriendsIP.Text == "")
                MessageBox.Show("Lütfen Port Numaralarını Seçiniz!");
            else
            {
                txtStaticLocalPort = cmbLocalPort.Text;
                txtStaticFriendsPort = cmbFriendsPort.Text;
                txtStaticFriendsIP = txtFriendsIP.Text;

                this.Close();
                form1.Show();
            }
        } //Connect
    }
}
