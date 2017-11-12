namespace MyEvernote.WinForm
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.ChBoxSignUp = new System.Windows.Forms.CheckBox();
            this.tBoxNameUser = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.toolTipShowInfo = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnSignIn
            // 
            this.btnSignIn.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnSignIn.Location = new System.Drawing.Point(110, 118);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(75, 23);
            this.btnSignIn.TabIndex = 1;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // ChBoxSignUp
            // 
            this.ChBoxSignUp.AutoSize = true;
            this.ChBoxSignUp.Location = new System.Drawing.Point(203, 95);
            this.ChBoxSignUp.Name = "ChBoxSignUp";
            this.ChBoxSignUp.Size = new System.Drawing.Size(64, 17);
            this.ChBoxSignUp.TabIndex = 3;
            this.ChBoxSignUp.Text = "Sign Up";
            this.ChBoxSignUp.UseVisualStyleBackColor = true;
            // 
            // tBoxNameUser
            // 
            this.tBoxNameUser.Location = new System.Drawing.Point(97, 92);
            this.tBoxNameUser.Name = "tBoxNameUser";
            this.tBoxNameUser.Size = new System.Drawing.Size(100, 20);
            this.tBoxNameUser.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(94, 76);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(103, 13);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Имя пользователя";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.tBoxNameUser);
            this.Controls.Add(this.ChBoxSignUp);
            this.Controls.Add(this.btnSignIn);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Evernote";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSignIn;
        public System.Windows.Forms.CheckBox ChBoxSignUp;
        private System.Windows.Forms.TextBox tBoxNameUser;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ToolTip toolTipShowInfo;
    }
}

