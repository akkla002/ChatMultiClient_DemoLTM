using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Server.ServerSite
{
    internal class TheServer
    {
        private static TheServer instance = null;
        Socket sv;
        IPEndPoint serverEP;
        ListClientOnline listClient;
        Stack<ChatContent> stackInput;
        List<UserAccount> userList;
        public delegate void SetDataControl(string Data);
        public SetDataControl SetDataFunction = null;

        public delegate void ShowChatContent(ChatContent Data);
        public ShowChatContent ShowChat;

        Thread readData;

        internal static TheServer Instance
        {
            get
            {
                if (instance == null)
                    instance = new TheServer();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        internal ListClientOnline ListClient
        {
            get
            {
                return listClient;
            }
            private set
            {
                listClient = value;
            }
        }

        private TheServer()
        {
            serverEP = new IPEndPoint(IPAddress.Any, MyConstant.PORT_SERVER);
            sv = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sv.Bind(serverEP);
            ListClient = new ListClientOnline(sv);
            stackInput = new Stack<ChatContent>();
            ListClient.SendMyObject = PushInStack;
            readData = new Thread(new ThreadStart(ShowMessage));
        }
        public void Start()
        {
            ListClient.Start();
            readData.Start();
        }
        private void PushInStack(ChatContent obj)
        {
            try
            {
                stackInput.Push(obj);
            }
            catch (Exception)
            {
                string error = "Error function PushInStack in Class " + ToString();
                Logger.Write(error);
            }
            if (readData.ThreadState == System.Threading.ThreadState.Suspended)
                readData.Resume();

        }
        private void ShowMessageInconsole()
        {
            stackInput.Clear();
            while (true)
            {
                if (stackInput.Count < 1)
                    readData.Suspend();
                else
                {
                    Console.WriteLine(stackInput.Pop().ToString());
                }
            }
        }
        private void ShowMessage()
        {
            stackInput.Clear();
            while (true)
            {
                if (stackInput.Count < 1)
                    readData.Suspend();
                else
                {
                    ChatContent temp = stackInput.Pop();
                    if (temp.Receiver == "" || temp.Receiver == null)
                    {
                        ShowChat(temp);
                    }
                    else
                    {
                        if (!temp.Save())
                        {
                            //throw new Exception("Save error");
                            MessageBox.Show("");
                        }
                    }
                    SendChatContent(temp);
                }
            }
        }
        public void SendChatContent(ChatContent data)
        {

            ListClient.SendChatContent(data);
        }
        public void Stop()
        {
            ListClient.Stop();
        }
    }
}
