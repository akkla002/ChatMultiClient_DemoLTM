﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class fMain : Form
    {
        
        public fMain()
        {
            CheckForIllegalCrossThreadCalls = false;
            ServerSite.TheServer.Instance.ShowChat = AddContentToRtbx;
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txbContent.Text == "")
                return;
            ChatContent temp = new ChatContent();
            temp.Content = txbContent.Text;
            ServerSite.TheServer.Instance.SendChatContent(temp);
            AddContentToRtbx(temp);
            txbContent.Clear();
        }
        private void AddContentToRtbx(ChatContent data)
        {
            //string s = data.Sender + ": ";
            //s += data.Content;
            rtxbAllContent.Text += data + System.Environment.NewLine;
        }
    }
}
