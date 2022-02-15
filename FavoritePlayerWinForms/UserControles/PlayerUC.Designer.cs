
namespace FavoritePlayerWinForms.UserControles
{
    partial class PlayerUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picture = new System.Windows.Forms.PictureBox();
            this.name = new System.Windows.Forms.Label();
            this.dressNo = new System.Windows.Forms.Label();
            this.position = new System.Windows.Forms.Label();
            this.cbCapitan = new System.Windows.Forms.CheckBox();
            this.cbFavorite = new System.Windows.Forms.CheckBox();
            this.btnImageUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.Cursor = System.Windows.Forms.Cursors.Default;
            this.picture.ErrorImage = null;
            this.picture.Image = global::FavoritePlayerWinForms.Properties.Resources.no_image;
            this.picture.InitialImage = null;
            this.picture.Location = new System.Drawing.Point(3, 3);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(159, 138);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            this.picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picture_MouseDown);
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(178, 12);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(46, 17);
            this.name.TabIndex = 1;
            this.name.Text = "label1";
            // 
            // dressNo
            // 
            this.dressNo.AutoSize = true;
            this.dressNo.Location = new System.Drawing.Point(178, 38);
            this.dressNo.Name = "dressNo";
            this.dressNo.Size = new System.Drawing.Size(46, 17);
            this.dressNo.TabIndex = 2;
            this.dressNo.Text = "label2";
            // 
            // position
            // 
            this.position.AutoSize = true;
            this.position.Location = new System.Drawing.Point(178, 65);
            this.position.Name = "position";
            this.position.Size = new System.Drawing.Size(46, 17);
            this.position.TabIndex = 3;
            this.position.Text = "label3";
            // 
            // cbCapitan
            // 
            this.cbCapitan.AutoSize = true;
            this.cbCapitan.Enabled = false;
            this.cbCapitan.Location = new System.Drawing.Point(181, 93);
            this.cbCapitan.Name = "cbCapitan";
            this.cbCapitan.Size = new System.Drawing.Size(78, 21);
            this.cbCapitan.TabIndex = 4;
            this.cbCapitan.Text = "Capitan";
            this.cbCapitan.UseVisualStyleBackColor = true;
            // 
            // cbFavorite
            // 
            this.cbFavorite.AutoSize = true;
            this.cbFavorite.Enabled = false;
            this.cbFavorite.Location = new System.Drawing.Point(181, 120);
            this.cbFavorite.Name = "cbFavorite";
            this.cbFavorite.Size = new System.Drawing.Size(81, 21);
            this.cbFavorite.TabIndex = 5;
            this.cbFavorite.Text = "Favorite";
            this.cbFavorite.UseVisualStyleBackColor = true;
            // 
            // btnImageUpdate
            // 
            this.btnImageUpdate.Enabled = false;
            this.btnImageUpdate.Location = new System.Drawing.Point(265, 78);
            this.btnImageUpdate.Name = "btnImageUpdate";
            this.btnImageUpdate.Size = new System.Drawing.Size(70, 48);
            this.btnImageUpdate.TabIndex = 6;
            this.btnImageUpdate.Text = "Update image";
            this.btnImageUpdate.UseVisualStyleBackColor = true;
            this.btnImageUpdate.Click += new System.EventHandler(this.btnImageUpdate_Click);
            // 
            // PlayerUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnImageUpdate);
            this.Controls.Add(this.cbFavorite);
            this.Controls.Add(this.cbCapitan);
            this.Controls.Add(this.position);
            this.Controls.Add(this.dressNo);
            this.Controls.Add(this.name);
            this.Controls.Add(this.picture);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "PlayerUC";
            this.Size = new System.Drawing.Size(350, 145);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayerUC_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label dressNo;
        private System.Windows.Forms.Label position;
        private System.Windows.Forms.CheckBox cbCapitan;
        private System.Windows.Forms.CheckBox cbFavorite;
        private System.Windows.Forms.Button btnImageUpdate;
    }
}
