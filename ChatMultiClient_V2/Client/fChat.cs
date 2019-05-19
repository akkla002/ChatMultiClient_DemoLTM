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
            lsbUserOnline.DisplayMember = "NickName";
            //SetDataBinding();
        }

        #region Methods
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
        public void AddOrRemoveOnlineUser(UserAccount user)
        {
            for (int i = 0; i < lsbUserOnline.Items.Count ; i++)
            {
                UserAccount tempAccount = lsbUserOnline.Items[i] as UserAccount;
                if (tempAccount.UserName == user.UserName)
                {
                    lsbUserOnline.Items.RemoveAt(i);
                    lsbUserOnline.Refresh();
                    return;
                }
            }
            lsbUserOnline.Items.Add(user);
        }

        #endregion



        #region event

        private void lsbUserOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbUserOnline.SelectedItem == null)
                return;
            txbReceiver.Text = (lsbUserOnline.SelectedItem as UserAccount).UserName;
            //txbReceiver.Name = (lsbUserOnline.SelectedItem as UserAccount).NickName;
        }

        private void fChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientSite.Instance.Finish();
            ClientSite.Instance.ListFormShow.Remove(this);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txbContent.Text == "")
                return;
            ClientSite.Instance.SendChatContent(txbContent.Text, txbReceiver.Text);
            txbContent.Clear();
        }
        #endregion
    }
}
