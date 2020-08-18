namespace Currency_Conversion_Application
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
            this.ConvertBtn = new System.Windows.Forms.Button();
            this.input = new System.Windows.Forms.TextBox();
            this.iputlable = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.conbolable = new System.Windows.Forms.Label();
            this.ratevalue = new System.Windows.Forms.Label();
            this.Value = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConvertBtn
            // 
            this.ConvertBtn.Location = new System.Drawing.Point(425, 41);
            this.ConvertBtn.Name = "ConvertBtn";
            this.ConvertBtn.Size = new System.Drawing.Size(75, 23);
            this.ConvertBtn.TabIndex = 0;
            this.ConvertBtn.Text = "Convert";
            this.ConvertBtn.UseVisualStyleBackColor = true;
            this.ConvertBtn.Click += new System.EventHandler(this.ConvertBtn_Click);
            // 
            // input
            // 
            this.input.Location = new System.Drawing.Point(81, 43);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(111, 20);
            this.input.TabIndex = 1;
            // 
            // iputlable
            // 
            this.iputlable.AutoSize = true;
            this.iputlable.Location = new System.Drawing.Point(81, 27);
            this.iputlable.Name = "iputlable";
            this.iputlable.Size = new System.Drawing.Size(111, 13);
            this.iputlable.TabIndex = 2;
            this.iputlable.Text = "Enter a currencey in £";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(232, 43);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Text = "Select one";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // conbolable
            // 
            this.conbolable.AutoSize = true;
            this.conbolable.Location = new System.Drawing.Point(232, 26);
            this.conbolable.Name = "conbolable";
            this.conbolable.Size = new System.Drawing.Size(162, 13);
            this.conbolable.TabIndex = 4;
            this.conbolable.Text = "Select the currency to convert to";
            // 
            // ratevalue
            // 
            this.ratevalue.AutoSize = true;
            this.ratevalue.Location = new System.Drawing.Point(235, 71);
            this.ratevalue.Name = "ratevalue";
            this.ratevalue.Size = new System.Drawing.Size(71, 13);
            this.ratevalue.TabIndex = 6;
            this.ratevalue.Text = "Currenct rate ";
            // 
            // Value
            // 
            this.Value.AutoSize = true;
            this.Value.Location = new System.Drawing.Point(235, 113);
            this.Value.Name = "Value";
            this.Value.Size = new System.Drawing.Size(90, 13);
            this.Value.TabIndex = 7;
            this.Value.Text = "Value will be here";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 26);
            this.button1.TabIndex = 8;
            this.button1.Text = "Search conversion history";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 277);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Value);
            this.Controls.Add(this.ratevalue);
            this.Controls.Add(this.conbolable);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.iputlable);
            this.Controls.Add(this.input);
            this.Controls.Add(this.ConvertBtn);
            this.Name = "Form1";
            this.Text = "Currency Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConvertBtn;
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.Label iputlable;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label conbolable;
        private System.Windows.Forms.Label ratevalue;
        private System.Windows.Forms.Label Value;
        private System.Windows.Forms.Button button1;
    }
}

