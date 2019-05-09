using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Server;
using System.Windows.Forms;

namespace Client
{
    public class ClientSite
    {
        private static ClientSite instance = null;
        public static ClientSite Instance
        {
            get
            {
                if (instance == null)
                    instance = new ClientSite();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }


        public UserAccount User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public List<Form> ListFormShow
        {
            get
            {
                return listFormShow;
            }

            set
            {
                listFormShow = value;
            }
        }

        private List<UserAccount> listUser;
        IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Server.MyConstant.PORT_SERVER);
        Socket clientSocket;
        DataTransmission dataTrans;
        UserAccount user;
        List<Form> listFormShow;

        public delegate void AddOrRemoveOnlineUser(UserAccount user);
        public AddOrRemoveOnlineUser AddOrRemove = null;

        Thread threadListen;

        private ClientSite()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            dataTrans = new DataTransmission(clientSocket);
            User = new UserAccount();
            listUser = new List<UserAccount>();
            threadListen = new Thread(StartReceiveData);

            ListFormShow = new List<Form>();
        }
        public bool Connect()
        {
            try
            {
                clientSocket.Connect(ipepServer);
            }
            catch (SocketException)
            {
                Server.Logger.Write("Ket noi that bai!");
                return false;
            }
            //threadListen.Start();
            return true;
        }
        public bool Login()
        {
            dataTrans.SendData(new MyTransportObject(this.User));
            MyTransportObject receiveData = dataTrans.ReceiveData();
            UserAccount receiveUser = receiveData.Obj as UserAccount;
            if (receiveUser.UserName == "")
                return false;
            this.User = receiveUser;
            threadListen.Start();
            return true;
        }
        public void Finish()
        {
            threadListen.Abort();
            Server.Logger.Write("Da ngat ket noi!");
            Instance = null;
        }
        public void StartReceiveData()
        {
            Thread.Sleep(100);
            Server.MyTransportObject buffIn;
            while (true)
            {
                buffIn = dataTrans.ReceiveData();
                if (buffIn == null)
                {
                    MessageBox.Show("Ngắt kết nối đến server!", "Thông báo!", MessageBoxButtons.OK);
                    Form temp = this.ListFormShow[0];
                    temp.Close();
                    this.ListFormShow.Remove(temp);
                    clientSocket.Close();
                    Finish();
                    //Application.Exit();
                    break;
                }
                ProcessObject(buffIn.Obj);
            }
        }
        public void SendChatContent(string data, string destination)
        {
            ChatContent chatContent = new ChatContent(User.UserName, destination, data);
            dataTrans.SendData(new MyTransportObject(chatContent));
        }
        public void ProcessObject(Server.MyObject obj)
        {
            //if(obj is Server.Database.ChatContent)
            if (obj is Server.ChatContent)
            {
                ChatContent temp = obj as ChatContent;
                fChat item = ListFormShow[0] as fChat;
                if ("" == temp.Receiver && temp.Sender == "")
                    temp.Receiver = User.UserName;
                item.ShowChatContent(temp);
            }
            else if (obj is UserAccount)
            {
                //this bug
                //(this.ListFormShow[0] as fChat).AddOrRemoveUser(obj as UserAccount);
                this.AddOrRemove(obj as UserAccount);
            }
        }
    }
}
