namespace Server
{
    partial class fMain
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.rtxbAllContent = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.txbContent = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.rtxbAllContent);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(13, 14);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(470, 278);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // rtxbAllContent
            // 
            this.rtxbAllContent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rtxbAllContent.Location = new System.Drawing.Point(3, 3);
            this.rtxbAllContent.Name = "rtxbAllContent";
            this.rtxbAllContent.Size = new System.Drawing.Size(457, 263);
            this.rtxbAllContent.TabIndex = 0;
            this.rtxbAllContent.ReadOnly = true;
            this.rtxbAllContent.Text = "";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(491, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 370);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Controls.Add(this.txbContent);
            this.panel2.Location = new System.Drawing.Point(16, 301);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(467, 83);
            this.panel2.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(358, 13);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(99, 55);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Gửi";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txbContent
            // 
            this.txbContent.Location = new System.Drawing.Point(13, 13);
            this.txbContent.Multiline = true;
            this.txbContent.Name = "txbContent";
            this.txbContent.Size = new System.Drawing.Size(327, 55);
            this.txbContent.TabIndex = 0;
            // 
            // fMain
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 396);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fMain";
            this.Text = "Server";
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtxbAllContent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txbContent;
    }
}

