namespace ServerDictApp
{
    partial class ConfigurationForm
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
            this.btn_set = new System.Windows.Forms.Button();
            this.txt_address = new System.Windows.Forms.TextBox();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.lbl_address = new System.Windows.Forms.Label();
            this.lbl_port = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_capacity = new System.Windows.Forms.Label();
            this.txt_capacity = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_set
            // 
            this.btn_set.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_set.Location = new System.Drawing.Point(140, 256);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(164, 50);
            this.btn_set.TabIndex = 5;
            this.btn_set.Text = "Set";
            this.btn_set.UseVisualStyleBackColor = true;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // txt_address
            // 
            this.txt_address.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_address.Location = new System.Drawing.Point(140, 95);
            this.txt_address.Name = "txt_address";
            this.txt_address.Size = new System.Drawing.Size(249, 28);
            this.txt_address.TabIndex = 6;
            // 
            // txt_port
            // 
            this.txt_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_port.Location = new System.Drawing.Point(140, 153);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(249, 28);
            this.txt_port.TabIndex = 7;
            // 
            // lbl_address
            // 
            this.lbl_address.AutoSize = true;
            this.lbl_address.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_address.Location = new System.Drawing.Point(30, 98);
            this.lbl_address.Name = "lbl_address";
            this.lbl_address.Size = new System.Drawing.Size(80, 24);
            this.lbl_address.TabIndex = 8;
            this.lbl_address.Text = "Address";
            // 
            // lbl_port
            // 
            this.lbl_port.AutoSize = true;
            this.lbl_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_port.Location = new System.Drawing.Point(30, 156);
            this.lbl_port.Name = "lbl_port";
            this.lbl_port.Size = new System.Drawing.Size(43, 24);
            this.lbl_port.TabIndex = 9;
            this.lbl_port.Text = "Port";
            // 
            // txt_name
            // 
            this.txt_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_name.Location = new System.Drawing.Point(140, 36);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(249, 28);
            this.txt_name.TabIndex = 10;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(30, 40);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(61, 24);
            this.lbl_name.TabIndex = 11;
            this.lbl_name.Text = "Name";
            // 
            // lbl_capacity
            // 
            this.lbl_capacity.AutoSize = true;
            this.lbl_capacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_capacity.Location = new System.Drawing.Point(30, 212);
            this.lbl_capacity.Name = "lbl_capacity";
            this.lbl_capacity.Size = new System.Drawing.Size(81, 24);
            this.lbl_capacity.TabIndex = 13;
            this.lbl_capacity.Text = "Capacity";
            // 
            // txt_capacity
            // 
            this.txt_capacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_capacity.Location = new System.Drawing.Point(140, 209);
            this.txt_capacity.Name = "txt_capacity";
            this.txt_capacity.Size = new System.Drawing.Size(249, 28);
            this.txt_capacity.TabIndex = 12;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 318);
            this.Controls.Add(this.lbl_capacity);
            this.Controls.Add(this.txt_capacity);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.lbl_port);
            this.Controls.Add(this.lbl_address);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.txt_address);
            this.Controls.Add(this.btn_set);
            this.Name = "ConfigurationForm";
            this.Text = "Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.TextBox txt_address;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Label lbl_address;
        private System.Windows.Forms.Label lbl_port;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_capacity;
        private System.Windows.Forms.TextBox txt_capacity;
    }
}