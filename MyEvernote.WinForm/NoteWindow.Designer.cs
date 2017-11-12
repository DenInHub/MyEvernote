namespace MyEvernote.WinForm
{
    partial class NoteWindow
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
            this.btnSaveNote = new System.Windows.Forms.Button();
            this.btnCancelCreation = new System.Windows.Forms.Button();
            this.coBoxCategory = new System.Windows.Forms.ComboBox();
            this.TxtBoxTitleNote = new System.Windows.Forms.RichTextBox();
            this.TxtBoxTextNote = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkedListBoxShared = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTipShowInfo = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnSaveNote
            // 
            this.btnSaveNote.Location = new System.Drawing.Point(405, 193);
            this.btnSaveNote.Name = "btnSaveNote";
            this.btnSaveNote.Size = new System.Drawing.Size(75, 23);
            this.btnSaveNote.TabIndex = 0;
            this.btnSaveNote.Text = "Save";
            this.btnSaveNote.UseVisualStyleBackColor = true;
            this.btnSaveNote.Click += new System.EventHandler(this.btnSaveNote_Click);
            // 
            // btnCancelCreation
            // 
            this.btnCancelCreation.Location = new System.Drawing.Point(486, 193);
            this.btnCancelCreation.Name = "btnCancelCreation";
            this.btnCancelCreation.Size = new System.Drawing.Size(75, 23);
            this.btnCancelCreation.TabIndex = 1;
            this.btnCancelCreation.Text = "Отмена";
            this.btnCancelCreation.UseVisualStyleBackColor = true;
            this.btnCancelCreation.Click += new System.EventHandler(this.btnCancelCreation_Click);
            // 
            // coBoxCategory
            // 
            this.coBoxCategory.FormattingEnabled = true;
            this.coBoxCategory.Location = new System.Drawing.Point(295, 27);
            this.coBoxCategory.Name = "coBoxCategory";
            this.coBoxCategory.Size = new System.Drawing.Size(121, 21);
            this.coBoxCategory.TabIndex = 2;
            // 
            // TxtBoxTitleNote
            // 
            this.TxtBoxTitleNote.Location = new System.Drawing.Point(139, 25);
            this.TxtBoxTitleNote.Name = "TxtBoxTitleNote";
            this.TxtBoxTitleNote.Size = new System.Drawing.Size(127, 23);
            this.TxtBoxTitleNote.TabIndex = 3;
            this.TxtBoxTitleNote.Text = "";
            // 
            // TxtBoxTextNote
            // 
            this.TxtBoxTextNote.Location = new System.Drawing.Point(0, 91);
            this.TxtBoxTextNote.Name = "TxtBoxTextNote";
            this.TxtBoxTextNote.Size = new System.Drawing.Size(561, 96);
            this.TxtBoxTextNote.TabIndex = 4;
            this.TxtBoxTextNote.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(266, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Текст";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Категория";
            // 
            // checkedListBoxShared
            // 
            this.checkedListBoxShared.FormattingEnabled = true;
            this.checkedListBoxShared.Location = new System.Drawing.Point(0, 231);
            this.checkedListBoxShared.Name = "checkedListBoxShared";
            this.checkedListBoxShared.Size = new System.Drawing.Size(119, 94);
            this.checkedListBoxShared.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-3, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Разрешить доступ пользователям";
            // 
            // NoteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 411);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkedListBoxShared);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtBoxTextNote);
            this.Controls.Add(this.TxtBoxTitleNote);
            this.Controls.Add(this.coBoxCategory);
            this.Controls.Add(this.btnCancelCreation);
            this.Controls.Add(this.btnSaveNote);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "NoteWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NoteWindow";
            this.Load += new System.EventHandler(this.NoteWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveNote;
        private System.Windows.Forms.Button btnCancelCreation;
        private System.Windows.Forms.ComboBox coBoxCategory;
        private System.Windows.Forms.RichTextBox TxtBoxTitleNote;
        private System.Windows.Forms.RichTextBox TxtBoxTextNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox checkedListBoxShared;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTipShowInfo;
    }
}