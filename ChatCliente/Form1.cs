using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System;

namespace ChatCliente
{
    public partial class open_chat : Form
    {
        public open_chat()
        {
            InitializeComponent();
        }

        private void open_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(tb_user.Text.Length != 0)
            {
                frmCliente chat = new frmCliente( tb_user.Text, "127.0.0.1");
                chat.Show();
                Hide();
            }
            else{
                MessageBox.Show("Preencha os dados necessarios!");
            }
        }
    }
}
