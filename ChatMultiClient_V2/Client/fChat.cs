using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server;

namespace Client
{

    public partial class fChat : Form
    {
        //private Server.UserAccount destination;

        public fChat()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //SetDataBinding();
        }
        List<UserAccount> listUserOnline = new List<UserAccount>();
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txbContent.Text == "")
                return;
            ClientSite.Instance.SendChatContent(txbContent.Text,txbReceiver.Text);
            txbContent.Clear();
        }

        public void ShowChatContent(ChatContent data)
        {
            string s = "";
            if(data.Sender == "" && data.Receiver == "")
            {
                s += "Server to " + ClientSite.Instance.User.UserName + ": " + data.Content;
            }
            else
            {
                if (data.Sender == "")
                    s += "Server to " + data.Receiver + ": " + data.Content;
                else if (data.Receiver == "")
                    s += data.Sender + " to Server: " + data.Content;
                else s = data.ToString();
            }
            rtxbAllContent.Text += s + System.Environment.NewLine;
        }
        public void SetDataBinding()
        {
            Binding dataBindingCbx = new Binding("DataSource", ClientSite.Instance, "ListUserOnline",true,DataSourceUpdateMode.OnPropertyChanged);
            cbxUserOnline.DataBindings.Add(dataBindingCbx);
            cbxUserOnline.DisplayMember = "NickName";
        }
    }
}
