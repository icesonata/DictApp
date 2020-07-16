namespace ProxyDictApp
{
    partial class FormProxy
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
            this.txtBox_query_history = new System.Windows.Forms.RichTextBox();
            this.btn_turnon = new System.Windows.Forms.Button();
            this.btn_turnoff = new System.Windows.Forms.Button();
            this.btn_dictserverconfig = new System.Windows.Forms.Button();
            this.btn_proxyconfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBox_query_history
            // 
            this.txtBox_query_history.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_query_history.Location = new System.Drawing.Point(330, 23);
            this.txtBox_query_history.Name = "txtBox_query_history";
            this.txtBox_query_history.ReadOnly = true;
            this.txtBox_query_history.Size = new System.Drawing.Size(618, 421);
            this.txtBox_query_history.TabIndex = 0;
            this.txtBox_query_history.Text = "";
            // 
            // btn_turnon
            // 
            this.btn_turnon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_turnon.Location = new System.Drawing.Point(14, 23);
            this.btn_turnon.Name = "btn_turnon";
            this.btn_turnon.Size = new System.Drawing.Size(152, 48);
            this.btn_turnon.TabIndex = 1;
            this.btn_turnon.Text = "Turn on";
            this.btn_turnon.UseVisualStyleBackColor = true;
            this.btn_turnon.Click += new System.EventHandler(this.btn_turnon_Click);
            // 
            // btn_turnoff
            // 
            this.btn_turnoff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_turnoff.Location = new System.Drawing.Point(172, 23);
            this.btn_turnoff.Name = "btn_turnoff";
            this.btn_turnoff.Size = new System.Drawing.Size(152, 48);
            this.btn_turnoff.TabIndex = 2;
            this.btn_turnoff.Text = "Turn off";
            this.btn_turnoff.UseVisualStyleBackColor = true;
            this.btn_turnoff.Click += new System.EventHandler(this.btn_turnoff_Click);
            // 
            // btn_dictserverconfig
            // 
            this.btn_dictserverconfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dictserverconfig.Location = new System.Drawing.Point(14, 90);
            this.btn_dictserverconfig.Name = "btn_dictserverconfig";
            this.btn_dictserverconfig.Size = new System.Drawing.Size(310, 48);
            this.btn_dictserverconfig.TabIndex = 3;
            this.btn_dictserverconfig.Text = "Configure Dictionary Server";
            this.btn_dictserverconfig.UseVisualStyleBackColor = true;
            this.btn_dictserverconfig.Click += new System.EventHandler(this.btn_dictserverconfig_Click);
            // 
            // btn_proxyconfig
            // 
            this.btn_proxyconfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_proxyconfig.Location = new System.Drawing.Point(14, 157);
            this.btn_proxyconfig.Name = "btn_proxyconfig";
            this.btn_proxyconfig.Size = new System.Drawing.Size(310, 48);
            this.btn_proxyconfig.TabIndex = 4;
            this.btn_proxyconfig.Text = "Configure Proxy Server";
            this.btn_proxyconfig.UseVisualStyleBackColor = true;
            this.btn_proxyconfig.Click += new System.EventHandler(this.btn_proxyconfig_Click);
            // 
            // FormProxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 456);
            this.Controls.Add(this.btn_proxyconfig);
            this.Controls.Add(this.btn_dictserverconfig);
            this.Controls.Add(this.btn_turnoff);
            this.Controls.Add(this.btn_turnon);
            this.Controls.Add(this.txtBox_query_history);
            this.Name = "FormProxy";
            this.Text = "Proxy";
            this.Load += new System.EventHandler(this.FormProxy_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtBox_query_history;
        private System.Windows.Forms.Button btn_turnon;
        private System.Windows.Forms.Button btn_turnoff;
        private System.Windows.Forms.Button btn_dictserverconfig;
        private System.Windows.Forms.Button btn_proxyconfig;
    }
}

