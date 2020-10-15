using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System;

namespace ChatCliente
{
    public partial class frmCliente : Form
    {
        // Trata o nome do usuário
        private string NomeUsuario = "Desconhecido";
        private StreamWriter stwEnviador;
        private StreamReader strReceptor;
        private TcpClient tcpServidor;

        // Necessário para atualizar o formulário com mensagens da outra thread
        private delegate void AtualizaLogCallBack(string strMensagem);

        // Necessário para definir o formulário para o estado "disconnected" de outra thread
        private delegate void FechaConexaoCallBack(string strMotivo);
        private Thread mensagemThread;
        private IPAddress enderecoIP;
        private bool Conectado;

        public frmCliente(string nomeDoUsuario, string ip = "127.0.0.1")
        {
           // Na saida da aplicação : desconectar
           Application.ApplicationExit += new EventHandler(OnApplicationExit);
           InitializeComponent();
            this.enderecoIP = IPAddress.Parse(ip);
            this.NomeUsuario = nomeDoUsuario ?? "teste";
            Conectado = false;
            if (Conectado == false)
            {
                // Inicializa a conexão
                InicializaConexao();
            }
            else // Se esta conectado entao desconecta
            {
                FechaConexao("Desconectado a pedido do usuário.");
            }
        }

        private void InicializaConexao()
        {
            try
            {
                // Trata o endereço IP informado em um objeto IPAdress
                //enderecoIP = IPAddress.Parse("127.0.0.1");
                // Inicia uma nova conexão TCP com o servidor chat
                tcpServidor = new TcpClient();
                tcpServidor.Connect(this.enderecoIP, 2502);

                // AJuda a verificar se estamos conectados ou não
                Conectado = true;
                txtMensagem.Enabled = true;
                btnEnviar.Enabled = true;

                //btnConectar.Text = "Desconectado";

                // Envia o nome do usuário ao servidor
                stwEnviador = new StreamWriter(tcpServidor.GetStream());
                stwEnviador.WriteLine(this.NomeUsuario);
                stwEnviador.Flush();

                //Inicia a thread para receber mensagens e nova comunicação
                mensagemThread = new Thread(new ThreadStart(RecebeMensagens));
                mensagemThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message, "Erro na conexão com servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RecebeMensagens()
        {
            strReceptor = new StreamReader(tcpServidor.GetStream());
            string ConResposta = strReceptor.ReadLine();
            if (ConResposta[0] == '1')
            {
                this.Invoke(new AtualizaLogCallBack(this.AtualizaLog), new object[] { "Conectado com sucesso!" });
            }
            else 
            {
                string Motivo = "Não Conectado: ";
                Motivo += ConResposta.Substring(2, ConResposta.Length - 2);
                this.Invoke(new FechaConexaoCallBack(this.FechaConexao), new object[] { Motivo });
               return;
            }

            while (Conectado)
            {
                try
                {
                    this.Invoke(new AtualizaLogCallBack(this.AtualizaLog), new object[] { strReceptor.ReadLine() });
                }
                catch (Exception)
                {
                }
            }
        }

        private void AtualizaLog(string strMensagem)
        {
            txtLog.AppendText(strMensagem + "\r\n");
        }

        private void btnEnviar_Click(object sender, System.EventArgs e)
        {
            EnviaMensagem();
        }

        private void txtMensagem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                EnviaMensagem();
            }
        }

        private void EnviaMensagem()
        {
            if (txtMensagem.Lines.Length >= 1)
            {
                stwEnviador.WriteLine(txtMensagem.Text);
                stwEnviador.Flush();
                txtMensagem.Lines = null;
            }
            txtMensagem.Text = "";
        }

        private void FechaConexao(string Motivo)
        {
            txtLog.AppendText(Motivo + "\r\n");

            Conectado = false;
            stwEnviador.Close();
            strReceptor.Close();
            tcpServidor.Close();
        }

        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Conectado == true)
            {
                Conectado = false;
                stwEnviador.Close();
                strReceptor.Close();
                tcpServidor.Close();
                MessageBox.Show("Test");
                Close();
                Dispose();
            }
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {

        }

        private void x_Click(object sender, EventArgs e)
        {

        }

        private void frmCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Conectado == true)
            {
                Conectado = false;
                stwEnviador.Close();
                strReceptor.Close();
                tcpServidor.Close();
                Close();
                Dispose();
                Application.Exit();
            }
        }
    }
}
