namespace ServerDictApp
{
    partial class QueryHistory
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
            this.SuspendLayout();
            // 
            // txtBox_query_history
            // 
            this.txtBox_query_history.Location = new System.Drawing.Point(0, 0);
            this.txtBox_query_history.Name = "txtBox_query_history";
            this.txtBox_query_history.Size = new System.Drawing.Size(717, 527);
            this.txtBox_query_history.TabIndex = 0;
            this.txtBox_query_history.Text = "";
            // 
            // QueryHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 525);
            this.Controls.Add(this.txtBox_query_history);
            this.Name = "QueryHistory";
            this.Text = "Query History";
            this.Load += new System.EventHandler(this.QueryHistory_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtBox_query_history;
    }
}