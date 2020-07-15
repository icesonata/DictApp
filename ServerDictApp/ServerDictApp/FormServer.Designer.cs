namespace network_programming_midterm_2
{
    partial class FormServer
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.box_queries = new System.Windows.Forms.RichTextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_shutdown = new System.Windows.Forms.Button();
            this.btn_query_history = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.box_queries);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 412);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Queries";
            // 
            // box_queries
            // 
            this.box_queries.Location = new System.Drawing.Point(22, 29);
            this.box_queries.Name = "box_queries";
            this.box_queries.ReadOnly = true;
            this.box_queries.Size = new System.Drawing.Size(434, 362);
            this.box_queries.TabIndex = 0;
            this.box_queries.Text = "";
            this.box_queries.TextChanged += new System.EventHandler(this.box_queries_TextChanged);
            // 
            // btn_start
            // 
            this.btn_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.Location = new System.Drawing.Point(551, 34);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(164, 50);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "Start Server";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_shutdown
            // 
            this.btn_shutdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_shutdown.Location = new System.Drawing.Point(551, 103);
            this.btn_shutdown.Name = "btn_shutdown";
            this.btn_shutdown.Size = new System.Drawing.Size(164, 50);
            this.btn_shutdown.TabIndex = 3;
            this.btn_shutdown.Text = "Shut down";
            this.btn_shutdown.UseVisualStyleBackColor = true;
            this.btn_shutdown.Click += new System.EventHandler(this.btn_shutdown_Click);
            // 
            // btn_query_history
            // 
            this.btn_query_history.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_query_history.Location = new System.Drawing.Point(551, 172);
            this.btn_query_history.Name = "btn_query_history";
            this.btn_query_history.Size = new System.Drawing.Size(164, 50);
            this.btn_query_history.TabIndex = 4;
            this.btn_query_history.Text = "History";
            this.btn_query_history.UseVisualStyleBackColor = true;
            this.btn_query_history.Click += new System.EventHandler(this.btn_query_history_Click);
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 437);
            this.Controls.Add(this.btn_query_history);
            this.Controls.Add(this.btn_shutdown);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormServer";
            this.Text = "Dictionary Server 0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox box_queries;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_shutdown;
        private System.Windows.Forms.Button btn_query_history;
    }
}

