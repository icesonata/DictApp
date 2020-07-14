namespace LoadBalancer
{
    partial class LoadBalancer
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
            this.btn_turnoff = new System.Windows.Forms.Button();
            this.txtBox_query_log = new System.Windows.Forms.RichTextBox();
            this.btn_turnon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_turnoff
            // 
            this.btn_turnoff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_turnoff.Location = new System.Drawing.Point(12, 86);
            this.btn_turnoff.Name = "btn_turnoff";
            this.btn_turnoff.Size = new System.Drawing.Size(124, 56);
            this.btn_turnoff.TabIndex = 1;
            this.btn_turnoff.Text = "Turn off";
            this.btn_turnoff.UseVisualStyleBackColor = true;
            this.btn_turnoff.Click += new System.EventHandler(this.btn_turnoff_Click);
            // 
            // txtBox_query_log
            // 
            this.txtBox_query_log.Location = new System.Drawing.Point(142, 22);
            this.txtBox_query_log.Name = "txtBox_query_log";
            this.txtBox_query_log.ReadOnly = true;
            this.txtBox_query_log.Size = new System.Drawing.Size(626, 300);
            this.txtBox_query_log.TabIndex = 2;
            this.txtBox_query_log.Text = "";
            // 
            // btn_turnon
            // 
            this.btn_turnon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_turnon.Location = new System.Drawing.Point(12, 22);
            this.btn_turnon.Name = "btn_turnon";
            this.btn_turnon.Size = new System.Drawing.Size(124, 56);
            this.btn_turnon.TabIndex = 3;
            this.btn_turnon.Text = "Turn on";
            this.btn_turnon.UseVisualStyleBackColor = true;
            this.btn_turnon.Click += new System.EventHandler(this.btn_turnon_Click);
            // 
            // LoadBalancer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 334);
            this.Controls.Add(this.btn_turnon);
            this.Controls.Add(this.txtBox_query_log);
            this.Controls.Add(this.btn_turnoff);
            this.Name = "LoadBalancer";
            this.Text = "Load Balancer";
            this.Load += new System.EventHandler(this.LoadBalancer_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_turnoff;
        private System.Windows.Forms.RichTextBox txtBox_query_log;
        private System.Windows.Forms.Button btn_turnon;
    }
}

