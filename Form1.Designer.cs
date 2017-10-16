namespace WindowsFormsApplication1
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
            this.onBtn = new System.Windows.Forms.Button();
            this.offBtn = new System.Windows.Forms.Button();
            this.listComBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // onBtn
            // 
            this.onBtn.Location = new System.Drawing.Point(44, 100);
            this.onBtn.Name = "onBtn";
            this.onBtn.Size = new System.Drawing.Size(75, 64);
            this.onBtn.TabIndex = 0;
            this.onBtn.Text = "Włącz";
            this.onBtn.UseVisualStyleBackColor = true;
            this.onBtn.Click += new System.EventHandler(this.onBtn_Click);
            // 
            // offBtn
            // 
            this.offBtn.Location = new System.Drawing.Point(163, 100);
            this.offBtn.Name = "offBtn";
            this.offBtn.Size = new System.Drawing.Size(75, 64);
            this.offBtn.TabIndex = 1;
            this.offBtn.Text = "Wyłącz";
            this.offBtn.UseVisualStyleBackColor = true;
            this.offBtn.Click += new System.EventHandler(this.offBtn_Click);
            // 
            // listComBox
            // 
            this.listComBox.FormattingEnabled = true;
            this.listComBox.Location = new System.Drawing.Point(13, 13);
            this.listComBox.Name = "listComBox";
            this.listComBox.Size = new System.Drawing.Size(165, 21);
            this.listComBox.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.listComBox);
            this.Controls.Add(this.offBtn);
            this.Controls.Add(this.onBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button onBtn;
        private System.Windows.Forms.Button offBtn;
        private System.Windows.Forms.ComboBox listComBox;
    }
}

