namespace DoAnAI
{
    partial class Form1
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
            this.readFileButton = new System.Windows.Forms.Button();
            this.linkTextBox = new System.Windows.Forms.TextBox();
            this.goalLinkTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // readFileButton
            // 
            this.readFileButton.Location = new System.Drawing.Point(325, 244);
            this.readFileButton.Name = "readFileButton";
            this.readFileButton.Size = new System.Drawing.Size(105, 66);
            this.readFileButton.TabIndex = 0;
            this.readFileButton.Text = "Đọc file";
            this.readFileButton.UseVisualStyleBackColor = true;
            this.readFileButton.Click += new System.EventHandler(this.readFileButton_Click);
            // 
            // linkTextBox
            // 
            this.linkTextBox.Location = new System.Drawing.Point(166, 21);
            this.linkTextBox.Multiline = true;
            this.linkTextBox.Name = "linkTextBox";
            this.linkTextBox.Size = new System.Drawing.Size(407, 166);
            this.linkTextBox.TabIndex = 1;
            // 
            // goalLinkTextBox
            // 
            this.goalLinkTextBox.Location = new System.Drawing.Point(325, 204);
            this.goalLinkTextBox.Name = "goalLinkTextBox";
            this.goalLinkTextBox.Size = new System.Drawing.Size(105, 20);
            this.goalLinkTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Đích đến";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 322);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.goalLinkTextBox);
            this.Controls.Add(this.linkTextBox);
            this.Controls.Add(this.readFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readFileButton;
        private System.Windows.Forms.TextBox linkTextBox;
        private System.Windows.Forms.TextBox goalLinkTextBox;
        private System.Windows.Forms.Label label1;
    }
}

