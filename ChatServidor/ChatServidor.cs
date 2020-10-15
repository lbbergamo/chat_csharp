using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;

namespace ChatServidor
{ 
    public class StatusChangedEventArgs : EventArgs
    {
        private string EventMsg;

        public string EventMessage
        {
            get { return EventMsg;}
            set { EventMsg = value;}
        }

        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMsg = strEventMsg;
        }
    }

    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    class ChatServidor
    {
        public static Hashtable htUsuarios = new Hashtable(30); // 30 usuarios é o limite definido
        public static Hashtable htConexoes = new Hashtable(30); // 30 usuários é o limite definido
        private IPAddress enderecoIP;
        private TcpClient tcpCliente;
      
        public static event StatusChangedEventHandler StatusChanged;
        private static StatusChangedEventArgs e;

        public ChatServidor(IPAddress endereco)
        {
            enderecoIP = endereco;
        }

        private Thread thrListener;

        private TcpListener tlsCliente;

        bool ServRodando = false;

        public static void IncluiUsuario(TcpClient tcpUsuario, string strUsername)
        {
            ChatServidor.htUsuarios.Add(strUsername, tcpUsuario);
            ChatServidor.htConexoes.Add(tcpUsuario, strUsername);

            EnviaMensagemAdmin(htConexoes[tcpUsuario] + " entrou..");
        }

        public static void RemoveUsuario(TcpClient tcpUsuario)
        {
            if (htConexoes[tcpUsuario] != null)
            {
                EnviaMensagemAdmin(htConexoes[tcpUsuario] + " saiu...");

                ChatServidor.htUsuarios.Remove(ChatServidor.htConexoes[tcpUsuario]);
                ChatServidor.htConexoes.Remove(tcpUsuario);
            }
        }

        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                statusHandler(null, e);
            }
        }

        public static void EnviaMensagemAdmin(string Mensagem)
        {
            StreamWriter swSenderSender;

            e = new StatusChangedEventArgs("Administrador: " + Mensagem);
            OnStatusChanged(e);

            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);
            for (int i = 0; i < tcpClientes.Length; i++)
            {
                try
                {
                    if (Mensagem.Trim() == "" || tcpClientes[i] == null)
                    {
                        continue;
                    }
                    swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                    swSenderSender.WriteLine("Administrador: " + Mensagem);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch 
                {
                    RemoveUsuario(tcpClientes[i]);
                }
            }
        }

        public static void EnviaMensagem(string Origem, string Mensagem)
        {
            StreamWriter swSenderSender;

            e = new StatusChangedEventArgs(Origem + " disse : " + Mensagem);
            OnStatusChanged(e);

            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);
            for (int i = 0; i < tcpClientes.Length; i++)
            {
                try
                {
                    if (Mensagem.Trim() == "" || tcpClientes[i] == null)
                    {
                        continue;
                    }
                    swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                    swSenderSender.WriteLine(Origem + " disse: " + Mensagem);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch 
                {
                    RemoveUsuario(tcpClientes[i]);
                }
            }
        }

        public void IniciaAtendimento()
        {
            try
            {
                IPAddress ipaLocal = enderecoIP;
                tlsCliente = new TcpListener(ipaLocal, 2502);
                tlsCliente.Start();
                ServRodando = true;
                thrListener = new Thread(MantemAtendimento);
                thrListener.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MantemAtendimento()
        {
            while (ServRodando == true)
            {
                tcpCliente = tlsCliente.AcceptTcpClient();
                Conexao newConnection = new Conexao(tcpCliente);
            }
        }
    }

    class Conexao
    {
        TcpClient tcpCliente;
        private Thread thrSender;
        private StreamReader srReceptor;
        private StreamWriter swEnviador;
        private string usuarioAtual;
        private string strResposta;

        public Conexao(TcpClient tcpCon)
        {
            tcpCliente = tcpCon;
            thrSender = new Thread(AceitaCliente);
            thrSender.Start();
        }

        private void FechaConexao()
        {
            tcpCliente.Close();
            srReceptor.Close();
            swEnviador.Close();
        }

        private void AceitaCliente()
        {
            srReceptor = new System.IO.StreamReader(tcpCliente.GetStream());
            swEnviador = new System.IO.StreamWriter(tcpCliente.GetStream());

            usuarioAtual = srReceptor.ReadLine();

            if (usuarioAtual != "")
            {
                if (ChatServidor.htUsuarios.Contains(usuarioAtual) == true)
                {
                    // 0 => significa não conectado
                    swEnviador.WriteLine("0|Este nome de usuário já existe.");
                    swEnviador.Flush();
                    FechaConexao();
                    return;
                }
                else if (usuarioAtual == "Administrator")
                {
                    // 0 => não conectado
                    swEnviador.WriteLine("0|Este nome de usuário é reservado.");
                    swEnviador.Flush();
                    FechaConexao();
                    return;
                }
                else
                {
                    swEnviador.WriteLine("1");
                    swEnviador.Flush();
                    ChatServidor.IncluiUsuario(tcpCliente, usuarioAtual);
                }
            }
            else
            {
                FechaConexao();
                return;
            }
            //
            try
            {
                while ((strResposta = srReceptor.ReadLine()) != "")
                {
                    if (strResposta == null)
                    {
                        ChatServidor.RemoveUsuario(tcpCliente);
                    }
                    else
                    {
                        ChatServidor.EnviaMensagem(usuarioAtual, strResposta);
                    }
                }
            }
            catch
            {
                ChatServidor.RemoveUsuario(tcpCliente);
            }
        }
    }
}
