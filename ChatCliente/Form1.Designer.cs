namespace ChatCliente
{
    partial class open_chat
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
            this.tb_user = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_servidor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_user
            // 
            this.tb_user.Location = new System.Drawing.Point(95, 83);
            this.tb_user.Name = "tb_user";
            this.tb_user.Size = new System.Drawing.Size(169, 20);
            this.tb_user.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Usuário";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "&Conectar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Servidor IP";
            // 
            // tb_servidor
            // 
            this.tb_servidor.Location = new System.Drawing.Point(95, 57);
            this.tb_servidor.Name = "tb_servidor";
            this.tb_servidor.Size = new System.Drawing.Size(169, 20);
            this.tb_servidor.TabIndex = 0;
            // 
            // open_chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 184);
            this.Controls.Add(this.tb_user);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_servidor);
            this.Controls.Add(this.label2);
            this.Name = "open_chat";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.open_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_user;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_servidor;
    }
}