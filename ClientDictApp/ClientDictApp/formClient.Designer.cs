namespace ClientDictApp
{
    partial class FormClient
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
            this.txt_encoded = new System.Windows.Forms.TextBox();
            this.btn_translate = new System.Windows.Forms.Button();
            this.txt_decoded = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_translation_history = new System.Windows.Forms.Button();
            this.btn_logout = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_encoded
            // 
            this.txt_encoded.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_encoded.Location = new System.Drawing.Point(186, 28);
            this.txt_encoded.Name = "txt_encoded";
            this.txt_encoded.Size = new System.Drawing.Size(620, 28);
            this.txt_encoded.TabIndex = 0;
            // 
            // btn_translate
            // 
            this.btn_translate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_translate.Location = new System.Drawing.Point(830, 20);
            this.btn_translate.Name = "btn_translate";
            this.btn_translate.Size = new System.Drawing.Size(138, 41);
            this.btn_translate.TabIndex = 2;
            this.btn_translate.Text = "Dịch";
            this.btn_translate.UseVisualStyleBackColor = true;
            this.btn_translate.Click += new System.EventHandler(this.button_translate_Click);
            // 
            // txt_decoded
            // 
            this.txt_decoded.Location = new System.Drawing.Point(16, 27);
            this.txt_decoded.Name = "txt_decoded";
            this.txt_decoded.ReadOnly = true;
            this.txt_decoded.Size = new System.Drawing.Size(745, 312);
            this.txt_decoded.TabIndex = 3;
            this.txt_decoded.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nhập từ cần dịch:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.CausesValidation = false;
            this.groupBox1.Controls.Add(this.txt_decoded);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(26, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(780, 356);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bản dịch";
            // 
            // btn_translation_history
            // 
            this.btn_translation_history.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_translation_history.Location = new System.Drawing.Point(830, 68);
            this.btn_translation_history.Name = "btn_translation_history";
            this.btn_translation_history.Size = new System.Drawing.Size(138, 41);
            this.btn_translation_history.TabIndex = 11;
            this.btn_translation_history.Text = "Lịch sử";
            this.btn_translation_history.UseVisualStyleBackColor = true;
            this.btn_translation_history.Click += new System.EventHandler(this.btn_translation_history_Click);
            // 
            // btn_logout
            // 
            this.btn_logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_logout.Location = new System.Drawing.Point(830, 115);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(138, 41);
            this.btn_logout.TabIndex = 12;
            this.btn_logout.Text = "Đăng xuất";
            this.btn_logout.UseVisualStyleBackColor = true;
            this.btn_logout.Click += new System.EventHandler(this.btn_logout_Click);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 436);
            this.Controls.Add(this.btn_logout);
            this.Controls.Add(this.btn_translation_history);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_translate);
            this.Controls.Add(this.txt_encoded);
            this.Name = "FormClient";
            this.Text = "Dictionary - Client";
            this.Load += new System.EventHandler(this.FormClient_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_encoded;
        private System.Windows.Forms.Button btn_translate;
        private System.Windows.Forms.RichTextBox txt_decoded;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_translation_history;
        private System.Windows.Forms.Button btn_logout;
    }
}

