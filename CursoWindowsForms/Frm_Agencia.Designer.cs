namespace CursoWindowsForms
{
    partial class Frm_Agencia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Agencia));
            this.Tls_Principal = new System.Windows.Forms.ToolStrip();
            this.ApagatoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Tls_Principal.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tls_Principal
            // 
            this.Tls_Principal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApagatoolStripButton});
            this.Tls_Principal.Location = new System.Drawing.Point(0, 0);
            this.Tls_Principal.Name = "Tls_Principal";
            this.Tls_Principal.Size = new System.Drawing.Size(345, 25);
            this.Tls_Principal.TabIndex = 57;
            this.Tls_Principal.Text = "toolStrip1";
            // 
            // ApagatoolStripButton
            // 
            this.ApagatoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ApagatoolStripButton.Image = global::CursoWindowsForms.Properties.Resources.ExcluirBarra;
            this.ApagatoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ApagatoolStripButton.Name = "ApagatoolStripButton";
            this.ApagatoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ApagatoolStripButton.Text = "toolStripButton1";
            this.ApagatoolStripButton.Click += new System.EventHandler(this.ApagatoolStripButton_Click);
            // 
            // Frm_Agencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 320);
            this.Controls.Add(this.Tls_Principal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Agencia";
            this.Text = "Frm_Busca";
            this.Tls_Principal.ResumeLayout(false);
            this.Tls_Principal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip Tls_Principal;
        private System.Windows.Forms.ToolStripButton ApagatoolStripButton;
    }
}