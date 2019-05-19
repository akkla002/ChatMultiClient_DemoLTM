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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
            //ClientSite client = new ClientSite();
            Server.Logger.Write("Da vao duoc!");
        }

        #region Methods
        public bool Login()
        {
            UserAccount user = new UserAccount();
            user.UserName = txbUserName.Text;
            user.Password = txbPassWord.Text;
            ClientSite.Instance.User = user;
            if (ClientSite.Instance.Connect())
            {
                return ClientSite.Instance.Login();
            }
            else
            {
                MessageBox.Show("Lỗi kết nối!");
                return false;
            }
        }

        private void ClearInput()
        {
            txbUserName.Clear();
            txbPassWord.Clear();
            txbUserName.Focus();
        }
        #endregion


        #region event
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(Login()==true)
            {
                
                Server.Logger.Write("Dang nhap thanh cong");
                //Server.Logger.Write(ClientSite.Instance.User.UserName);
                this.Hide();
                this.ClearInput();
                fChat f = new fChat();
                ClientSite.Instance.ListFormShow.Add(f);
                f.AddOrRemoveOnlineUser(ClientSite.Instance.ListUserOnline[0]);
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!");
                Server.Logger.Write("Can't connect!");
            }
            ClientSite.Instance.Finish();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
                //ClientSite.Instance.Finish();
                //System.Environment.Exit(0);
            }
            else
                e.Cancel = true;
        }
        #endregion
    }
}
