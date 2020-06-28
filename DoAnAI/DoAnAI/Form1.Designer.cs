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
            this.bidirectionalSearch = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.btnNext = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // readFileButton
            // 
            this.readFileButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.readFileButton.Location = new System.Drawing.Point(308, 246);
            this.readFileButton.Name = "readFileButton";
            this.readFileButton.Size = new System.Drawing.Size(105, 66);
            this.readFileButton.TabIndex = 0;
            this.readFileButton.Text = "BFS";
            this.readFileButton.UseVisualStyleBackColor = false;
            this.readFileButton.Click += new System.EventHandler(this.readFileButton_Click);
            // 
            // linkTextBox
            // 
            this.linkTextBox.Location = new System.Drawing.Point(295, 12);
            this.linkTextBox.Multiline = true;
            this.linkTextBox.Name = "linkTextBox";
            this.linkTextBox.Size = new System.Drawing.Size(407, 166);
            this.linkTextBox.TabIndex = 1;
            // 
            // goalLinkTextBox
            // 
            this.goalLinkTextBox.Location = new System.Drawing.Point(597, 207);
            this.goalLinkTextBox.Name = "goalLinkTextBox";
            this.goalLinkTextBox.Size = new System.Drawing.Size(105, 20);
            this.goalLinkTextBox.TabIndex = 2;
            this.goalLinkTextBox.Text = " ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(538, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Đích đến";
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.Location = new System.Drawing.Point(337, 207);
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.Size = new System.Drawing.Size(105, 20);
            this.sourceTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nguồn";
            // 
            // dfsSearchButton
            // 
            this.dfsSearchButton.BackColor = System.Drawing.Color.SandyBrown;
            this.dfsSearchButton.Location = new System.Drawing.Point(597, 246);
            this.dfsSearchButton.Name = "dfsSearchButton";
            this.dfsSearchButton.Size = new System.Drawing.Size(105, 66);
            this.dfsSearchButton.TabIndex = 4;
            this.dfsSearchButton.Text = "DFS";
            this.dfsSearchButton.UseVisualStyleBackColor = false;
            this.dfsSearchButton.Click += new System.EventHandler(this.dfsSearchButton_Click);
            // 
            // bidirectionalSearch
            // 
            this.bidirectionalSearch.BackColor = System.Drawing.Color.Orchid;
            this.bidirectionalSearch.Location = new System.Drawing.Point(455, 247);
            this.bidirectionalSearch.Name = "bidirectionalSearch";
            this.bidirectionalSearch.Size = new System.Drawing.Size(101, 65);
            this.bidirectionalSearch.TabIndex = 6;
            this.bidirectionalSearch.Text = "BDS";
            this.bidirectionalSearch.UseVisualStyleBackColor = false;
            this.bidirectionalSearch.Click += new System.EventHandler(this.bidirectionalSearch_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 33);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(250, 248);
            this.webBrowser.TabIndex = 7;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.PeachPuff;
            this.btnNext.Location = new System.Drawing.Point(12, 287);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "Start";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Url:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(739, 322);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.bidirectionalSearch);
            this.Controls.Add(this.dfsSearchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourceTextBox);
            this.Controls.Add(this.goalLinkTextBox);
            this.Controls.Add(this.linkTextBox);
            this.Controls.Add(this.readFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Button bidirectionalSearch;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label3;
    }
}

