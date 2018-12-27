namespace Pozorišne_predstave
{
    partial class PoTrupamaForm
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
            this.IzvrsiButton = new System.Windows.Forms.Button();
            this.IzadjiButton = new System.Windows.Forms.Button();
            this.TrupaCombo = new System.Windows.Forms.ComboBox();
            this.TrupaLabel = new System.Windows.Forms.Label();
            this.GlumacView = new System.Windows.Forms.DataGridView();
            this.KomadiView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GlumacView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KomadiView)).BeginInit();
            this.SuspendLayout();
            // 
            // IzvrsiButton
            // 
            this.IzvrsiButton.Location = new System.Drawing.Point(590, 78);
            this.IzvrsiButton.Name = "IzvrsiButton";
            this.IzvrsiButton.Size = new System.Drawing.Size(75, 23);
            this.IzvrsiButton.TabIndex = 0;
            this.IzvrsiButton.Text = "Izvrši";
            this.IzvrsiButton.UseVisualStyleBackColor = true;
            this.IzvrsiButton.Click += new System.EventHandler(this.IzvrsiButton_Click);
            // 
            // IzadjiButton
            // 
            this.IzadjiButton.Location = new System.Drawing.Point(590, 121);
            this.IzadjiButton.Name = "IzadjiButton";
            this.IzadjiButton.Size = new System.Drawing.Size(75, 23);
            this.IzadjiButton.TabIndex = 1;
            this.IzadjiButton.Text = "Izađi";
            this.IzadjiButton.UseVisualStyleBackColor = true;
            this.IzadjiButton.Click += new System.EventHandler(this.IzadjiButton_Click);
            // 
            // TrupaCombo
            // 
            this.TrupaCombo.FormattingEnabled = true;
            this.TrupaCombo.Location = new System.Drawing.Point(54, 24);
            this.TrupaCombo.Name = "TrupaCombo";
            this.TrupaCombo.Size = new System.Drawing.Size(168, 21);
            this.TrupaCombo.TabIndex = 2;
            this.TrupaCombo.SelectedIndexChanged += new System.EventHandler(this.TrupaCombo_SelectedIndexChanged);
            // 
            // TrupaLabel
            // 
            this.TrupaLabel.AutoSize = true;
            this.TrupaLabel.Location = new System.Drawing.Point(13, 24);
            this.TrupaLabel.Name = "TrupaLabel";
            this.TrupaLabel.Size = new System.Drawing.Size(35, 13);
            this.TrupaLabel.TabIndex = 3;
            this.TrupaLabel.Text = "Trupa";
            // 
            // GlumacView
            // 
            this.GlumacView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GlumacView.Location = new System.Drawing.Point(12, 62);
            this.GlumacView.Name = "GlumacView";
            this.GlumacView.Size = new System.Drawing.Size(253, 150);
            this.GlumacView.TabIndex = 4;
            // 
            // KomadiView
            // 
            this.KomadiView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.KomadiView.Location = new System.Drawing.Point(271, 24);
            this.KomadiView.Name = "KomadiView";
            this.KomadiView.Size = new System.Drawing.Size(313, 188);
            this.KomadiView.TabIndex = 5;
            // 
            // PoTrupamaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 241);
            this.Controls.Add(this.KomadiView);
            this.Controls.Add(this.GlumacView);
            this.Controls.Add(this.TrupaLabel);
            this.Controls.Add(this.TrupaCombo);
            this.Controls.Add(this.IzadjiButton);
            this.Controls.Add(this.IzvrsiButton);
            this.Name = "PoTrupamaForm";
            this.Text = "Po trupama";
            ((System.ComponentModel.ISupportInitialize)(this.GlumacView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KomadiView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button IzvrsiButton;
        private System.Windows.Forms.Button IzadjiButton;
        private System.Windows.Forms.ComboBox TrupaCombo;
        private System.Windows.Forms.Label TrupaLabel;
        private System.Windows.Forms.DataGridView GlumacView;
        private System.Windows.Forms.DataGridView KomadiView;
    }
}