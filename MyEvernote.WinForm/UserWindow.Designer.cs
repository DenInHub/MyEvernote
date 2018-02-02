namespace MyEvernote.WinForm
{
    partial class UserWindow
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
            this.lblNoteСhoice = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnViewInfo = new System.Windows.Forms.Button();
            this.tBoxNoteText = new System.Windows.Forms.RichTextBox();
            this.btnCreateNote = new System.Windows.Forms.Button();
            this.btnDeleteNote = new System.Windows.Forms.Button();
            this.listBoxNotesOfUser = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.BW = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNoteСhoice
            // 
            this.lblNoteСhoice.AutoSize = true;
            this.lblNoteСhoice.Location = new System.Drawing.Point(13, 13);
            this.lblNoteСhoice.Name = "lblNoteСhoice";
            this.lblNoteСhoice.Size = new System.Drawing.Size(86, 13);
            this.lblNoteСhoice.TabIndex = 0;
            this.lblNoteСhoice.Text = "Выбор заметки";
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(215, 228);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 3;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(108, 181);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 4;
            this.btnChange.Text = "Изменить";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnViewInfo
            // 
            this.btnViewInfo.Location = new System.Drawing.Point(3, 181);
            this.btnViewInfo.Name = "btnViewInfo";
            this.btnViewInfo.Size = new System.Drawing.Size(75, 23);
            this.btnViewInfo.TabIndex = 5;
            this.btnViewInfo.Text = "View Info";
            this.btnViewInfo.UseVisualStyleBackColor = true;
            this.btnViewInfo.Click += new System.EventHandler(this.btnViewInfo_Click);
            // 
            // tBoxNoteText
            // 
            this.tBoxNoteText.Location = new System.Drawing.Point(3, 0);
            this.tBoxNoteText.Name = "tBoxNoteText";
            this.tBoxNoteText.Size = new System.Drawing.Size(284, 175);
            this.tBoxNoteText.TabIndex = 6;
            this.tBoxNoteText.Text = "";
            // 
            // btnCreateNote
            // 
            this.btnCreateNote.Location = new System.Drawing.Point(30, 181);
            this.btnCreateNote.Name = "btnCreateNote";
            this.btnCreateNote.Size = new System.Drawing.Size(75, 25);
            this.btnCreateNote.TabIndex = 7;
            this.btnCreateNote.Text = "Создать";
            this.btnCreateNote.UseVisualStyleBackColor = true;
            this.btnCreateNote.Click += new System.EventHandler(this.btnCreateNote_Click);
            // 
            // btnDeleteNote
            // 
            this.btnDeleteNote.Location = new System.Drawing.Point(212, 181);
            this.btnDeleteNote.Name = "btnDeleteNote";
            this.btnDeleteNote.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteNote.TabIndex = 8;
            this.btnDeleteNote.Text = "Удалить";
            this.btnDeleteNote.UseVisualStyleBackColor = true;
            this.btnDeleteNote.Click += new System.EventHandler(this.btnDeleteNote_Click);
            // 
            // listBoxNotesOfUser
            // 
            this.listBoxNotesOfUser.FormattingEnabled = true;
            this.listBoxNotesOfUser.Location = new System.Drawing.Point(0, 0);
            this.listBoxNotesOfUser.Name = "listBoxNotesOfUser";
            this.listBoxNotesOfUser.Size = new System.Drawing.Size(148, 173);
            this.listBoxNotesOfUser.TabIndex = 9;
            this.listBoxNotesOfUser.SelectedIndexChanged += new System.EventHandler(this.listBoxNotesOfUser_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnUpdate);
            this.splitContainer1.Panel1.Controls.Add(this.listBoxNotesOfUser);
            this.splitContainer1.Panel1.Controls.Add(this.btnCreateNote);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tBoxNoteText);
            this.splitContainer1.Panel2.Controls.Add(this.btnDeleteNote);
            this.splitContainer1.Panel2.Controls.Add(this.btnLogOut);
            this.splitContainer1.Panel2.Controls.Add(this.btnChange);
            this.splitContainer1.Panel2.Controls.Add(this.btnViewInfo);
            this.splitContainer1.Size = new System.Drawing.Size(441, 252);
            this.splitContainer1.SplitterDistance = 147;
            this.splitContainer1.TabIndex = 10;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(30, 217);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // UserWindow
            // 
            this.ClientSize = new System.Drawing.Size(457, 281);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblNoteСhoice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "UserWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UserWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblNoteСhoice;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnViewInfo;
        private System.Windows.Forms.RichTextBox tBoxNoteText;
        private System.Windows.Forms.Button btnCreateNote;
        private System.Windows.Forms.Button btnDeleteNote;
        private System.Windows.Forms.ListBox listBoxNotesOfUser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnUpdate;
        private System.ComponentModel.BackgroundWorker BW;
    }
}