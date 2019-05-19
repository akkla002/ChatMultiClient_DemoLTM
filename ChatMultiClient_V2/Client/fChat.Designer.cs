namespace Client
{
    partial class fChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtxbAllContent = new System.Windows.Forms.RichTextBox();
            this.txbContent = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txbReceiver = new System.Windows.Forms.TextBox();
            this.pnUserOnline = new System.Windows.Forms.Panel();
            this.lsbUserOnline = new System.Windows.Forms.ListBox();
            this.lbUserOnline = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnUserOnline.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxbAllContent
            // 
            this.rtxbAllContent.Location = new System.Drawing.Point(13, 13);
            this.rtxbAllContent.Name = "rtxbAllContent";
            this.rtxbAllContent.ReadOnly = true;
            this.rtxbAllContent.Size = new System.Drawing.Size(527, 311);
            this.rtxbAllContent.TabIndex = 0;
            this.rtxbAllContent.Text = "";
            // 
            // txbContent
            // 
            this.txbContent.Location = new System.Drawing.Point(13, 330);
            this.txbContent.Multiline = true;
            this.txbContent.Name = "txbContent";
            this.txbContent.Size = new System.Drawing.Size(417, 56);
            this.txbContent.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(437, 331);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(103, 55);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Gửi";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txbReceiver
            // 
            this.txbReceiver.Location = new System.Drawing.Point(351, 429);
            this.txbReceiver.Name = "txbReceiver";
            this.txbReceiver.Size = new System.Drawing.Size(189, 26);
            this.txbReceiver.TabIndex = 3;
            // 
            // pnUserOnline
            // 
            this.pnUserOnline.AutoScroll = true;
            this.pnUserOnline.Controls.Add(this.lsbUserOnline);
            this.pnUserOnline.Controls.Add(this.lbUserOnline);
            this.pnUserOnline.Location = new System.Drawing.Point(547, 13);
            this.pnUserOnline.Name = "pnUserOnline";
            this.pnUserOnline.Size = new System.Drawing.Size(147, 442);
            this.pnUserOnline.TabIndex = 4;
            // 
            // lsbUserOnline
            // 
            this.lsbUserOnline.FormattingEnabled = true;
            this.lsbUserOnline.ItemHeight = 20;
            this.lsbUserOnline.Location = new System.Drawing.Point(9, 18);
            this.lsbUserOnline.Name = "lsbUserOnline";
            this.lsbUserOnline.Size = new System.Drawing.Size(135, 404);
            this.lsbUserOnline.TabIndex = 2;
            this.lsbUserOnline.SelectedIndexChanged += new System.EventHandler(this.lsbUserOnline_SelectedIndexChanged);
            // 
            // lbUserOnline
            // 
            this.lbUserOnline.AutoSize = true;
            this.lbUserOnline.Location = new System.Drawing.Point(3, 3);
            this.lbUserOnline.MaximumSize = new System.Drawing.Size(147, 442);
            this.lbUserOnline.Name = "lbUserOnline";
            this.lbUserOnline.Size = new System.Drawing.Size(0, 20);
            this.lbUserOnline.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(235, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Người nhận:";
            // 
            // fChat
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 467);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnUserOnline);
            this.Controls.Add(this.txbReceiver);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txbContent);
            this.Controls.Add(this.rtxbAllContent);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fChat";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fChat_FormClosing);
            this.pnUserOnline.ResumeLayout(false);
            this.pnUserOnline.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxbAllContent;
        private System.Windows.Forms.TextBox txbContent;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txbReceiver;
        private System.Windows.Forms.Panel pnUserOnline;
        private System.Windows.Forms.Label lbUserOnline;
        private System.Windows.Forms.ListBox lsbUserOnline;
        private System.Windows.Forms.Label label1;
    }
}

