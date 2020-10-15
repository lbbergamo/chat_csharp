namespace ChatCliente
{
    partial class frmCliente
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 12);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(510, 353);
            this.txtLog.TabIndex = 4;
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(12, 373);
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(406, 20);
            this.txtMensagem.TabIndex = 6;
            this.txtMensagem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMensagem_KeyPress);
            // 
            // btnEnviar
            // 
            this.btnEnviar.FlatAppearance.BorderSize = 3;
            this.btnEnviar.Location = new System.Drawing.Point(424, 370);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(98, 23);
            this.btnEnviar.TabIndex = 7;
            this.btnEnviar.Text = "&Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(534, 414);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtMensagem);
            this.Controls.Add(this.txtLog);
            this.Name = "frmCliente";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Chat - Cliente";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCliente_FormClosed);
            this.Load += new System.EventHandler(this.frmCliente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Button btnEnviar;
    }
}

