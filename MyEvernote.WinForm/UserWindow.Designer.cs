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
            this.coBoxUserNotes = new System.Windows.Forms.ComboBox();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnViewInfo = new System.Windows.Forms.Button();
            this.tBoxNoteText = new System.Windows.Forms.RichTextBox();
            this.btnCreateNote = new System.Windows.Forms.Button();
            this.btnDeleteNote = new System.Windows.Forms.Button();
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
            // coBoxUserNotes
            // 
            this.coBoxUserNotes.FormattingEnabled = true;
            this.coBoxUserNotes.Location = new System.Drawing.Point(16, 29);
            this.coBoxUserNotes.Name = "coBoxUserNotes";
            this.coBoxUserNotes.Size = new System.Drawing.Size(121, 21);
            this.coBoxUserNotes.TabIndex = 1;
            this.coBoxUserNotes.SelectedIndexChanged += new System.EventHandler(this.coBoxUserNotes_SelectedIndexChanged);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(387, 426);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 3;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(186, 155);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 4;
            this.btnChange.Text = "Изменить";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnViewInfo
            // 
            this.btnViewInfo.Location = new System.Drawing.Point(299, 155);
            this.btnViewInfo.Name = "btnViewInfo";
            this.btnViewInfo.Size = new System.Drawing.Size(75, 23);
            this.btnViewInfo.TabIndex = 5;
            this.btnViewInfo.Text = "View Info";
            this.btnViewInfo.UseVisualStyleBackColor = true;
            this.btnViewInfo.Click += new System.EventHandler(this.btnViewInfo_Click);
            // 
            // tBoxNoteText
            // 
            this.tBoxNoteText.Location = new System.Drawing.Point(186, 29);
            this.tBoxNoteText.Name = "tBoxNoteText";
            this.tBoxNoteText.Size = new System.Drawing.Size(285, 120);
            this.tBoxNoteText.TabIndex = 6;
            this.tBoxNoteText.Text = "";
            // 
            // btnCreateNote
            // 
            this.btnCreateNote.Location = new System.Drawing.Point(16, 126);
            this.btnCreateNote.Name = "btnCreateNote";
            this.btnCreateNote.Size = new System.Drawing.Size(75, 23);
            this.btnCreateNote.TabIndex = 7;
            this.btnCreateNote.Text = "Создать";
            this.btnCreateNote.UseVisualStyleBackColor = true;
            this.btnCreateNote.Click += new System.EventHandler(this.btnCreateNote_Click);
            // 
            // btnDeleteNote
            // 
            this.btnDeleteNote.Location = new System.Drawing.Point(396, 155);
            this.btnDeleteNote.Name = "btnDeleteNote";
            this.btnDeleteNote.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteNote.TabIndex = 8;
            this.btnDeleteNote.Text = "Удалить";
            this.btnDeleteNote.UseVisualStyleBackColor = true;
            this.btnDeleteNote.Click += new System.EventHandler(this.btnDeleteNote_Click);
            // 
            // UserWindow
            // 
            this.ClientSize = new System.Drawing.Size(474, 461);
            this.Controls.Add(this.btnDeleteNote);
            this.Controls.Add(this.btnCreateNote);
            this.Controls.Add(this.tBoxNoteText);
            this.Controls.Add(this.btnViewInfo);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.coBoxUserNotes);
            this.Controls.Add(this.lblNoteСhoice);
            this.Name = "UserWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UserWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblNoteСhoice;
        private System.Windows.Forms.ComboBox coBoxUserNotes;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnViewInfo;
        private System.Windows.Forms.RichTextBox tBoxNoteText;
        private System.Windows.Forms.Button btnCreateNote;
        private System.Windows.Forms.Button btnDeleteNote;
    }
}