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
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dfsSearchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // readFileButton
            // 
            this.readFileButton.Location = new System.Drawing.Point(297, 302);
            this.readFileButton.Margin = new System.Windows.Forms.Padding(4);
            this.readFileButton.Name = "readFileButton";
            this.readFileButton.Size = new System.Drawing.Size(140, 81);
            this.readFileButton.TabIndex = 0;
            this.readFileButton.Text = "BFS";
            this.readFileButton.UseVisualStyleBackColor = true;
            this.readFileButton.Click += new System.EventHandler(this.readFileButton_Click);
            // 
            // linkTextBox
            // 
            this.linkTextBox.Location = new System.Drawing.Point(221, 26);
            this.linkTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.linkTextBox.Multiline = true;
            this.linkTextBox.Name = "linkTextBox";
            this.linkTextBox.Size = new System.Drawing.Size(541, 203);
            this.linkTextBox.TabIndex = 1;
            // 
            // goalLinkTextBox
            // 
            this.goalLinkTextBox.Location = new System.Drawing.Point(624, 255);
            this.goalLinkTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.goalLinkTextBox.Name = "goalLinkTextBox";
            this.goalLinkTextBox.Size = new System.Drawing.Size(139, 22);
            this.goalLinkTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(545, 258);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Đích đến";
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.Location = new System.Drawing.Point(297, 250);
            this.sourceTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.Size = new System.Drawing.Size(139, 22);
            this.sourceTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 254);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nguồn";
            // 
            // dfsSearchButton
            // 
            this.dfsSearchButton.Location = new System.Drawing.Point(622, 302);
            this.dfsSearchButton.Margin = new System.Windows.Forms.Padding(4);
            this.dfsSearchButton.Name = "dfsSearchButton";
            this.dfsSearchButton.Size = new System.Drawing.Size(140, 81);
            this.dfsSearchButton.TabIndex = 4;
            this.dfsSearchButton.Text = "DFS";
            this.dfsSearchButton.UseVisualStyleBackColor = true;
            this.dfsSearchButton.Click += new System.EventHandler(this.dfsSearchButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 396);
            this.Controls.Add(this.dfsSearchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourceTextBox);
            this.Controls.Add(this.goalLinkTextBox);
            this.Controls.Add(this.linkTextBox);
            this.Controls.Add(this.readFileButton);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.TextBox sourceTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dfsSearchButton;
    }
}

