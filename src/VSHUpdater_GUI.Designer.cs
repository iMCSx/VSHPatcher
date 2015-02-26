/*********************************************************************************************
 * 
 * iMCSx's VSH Patcher
 * 
 * The Github address for the full source code  http://www.github.com/iMCSx/VSHPatcher
 * 
 * A simple application to decrypt / patch / encrypt any vsh.self.
 * The patch remove 2 PowerPC instructions and allow you to connect on PSN server.
 * 
 * After some look into IDA it's possible to make only 1 instruction edit to patch the vsh
 * But i'll follow the PS3ITA instructions, i take any risk.
 * 
 * I'm not responsible about anything on your PS3.
 * If something happen, just re-install your firmware.
 * 
 * Tested on many firmwares and it works fine...
 * 
 * Credits :
 * - The PS3ITA Team
 * - Naehrwert (Scetool)
 * - HeAd (Testing)
 * 
 ********************************************************************************************/

namespace VSH_Patcher
{
    partial class VSHUpdater_GUI
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.boxVshOrigine = new System.Windows.Forms.TextBox();
            this.lblvsh = new System.Windows.Forms.Label();
            this.btnSelectVsh = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnPatch = new System.Windows.Forms.Button();
            this.lblinfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // boxVshOrigine
            // 
            this.boxVshOrigine.AllowDrop = true;
            this.boxVshOrigine.Location = new System.Drawing.Point(14, 36);
            this.boxVshOrigine.Name = "boxVshOrigine";
            this.boxVshOrigine.Size = new System.Drawing.Size(269, 20);
            this.boxVshOrigine.TabIndex = 2;
            this.boxVshOrigine.DragDrop += new System.Windows.Forms.DragEventHandler(this.boxVshOrigine_DragDrop);
            this.boxVshOrigine.DragEnter += new System.Windows.Forms.DragEventHandler(this.boxVshOrigine_DragEnter);
            // 
            // lblvsh
            // 
            this.lblvsh.AutoSize = true;
            this.lblvsh.Location = new System.Drawing.Point(11, 20);
            this.lblvsh.Name = "lblvsh";
            this.lblvsh.Size = new System.Drawing.Size(130, 13);
            this.lblvsh.TabIndex = 1;
            this.lblvsh.Text = "Drag\'n drop your vsh.self :";
            // 
            // btnSelectVsh
            // 
            this.btnSelectVsh.Location = new System.Drawing.Point(289, 36);
            this.btnSelectVsh.Name = "btnSelectVsh";
            this.btnSelectVsh.Size = new System.Drawing.Size(33, 20);
            this.btnSelectVsh.TabIndex = 2;
            this.btnSelectVsh.Text = "...";
            this.btnSelectVsh.UseVisualStyleBackColor = true;
            this.btnSelectVsh.Click += new System.EventHandler(this.btnSelectVsh_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(14, 91);
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(308, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 3;
            // 
            // btnPatch
            // 
            this.btnPatch.Location = new System.Drawing.Point(14, 62);
            this.btnPatch.Name = "btnPatch";
            this.btnPatch.Size = new System.Drawing.Size(308, 23);
            this.btnPatch.TabIndex = 1;
            this.btnPatch.Text = "Patch !";
            this.btnPatch.UseVisualStyleBackColor = true;
            this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
            // 
            // lblinfo
            // 
            this.lblinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinfo.Location = new System.Drawing.Point(14, 117);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(308, 23);
            this.lblinfo.TabIndex = 5;
            this.lblinfo.Text = "Idle";
            this.lblinfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VSHUpdater_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.lblinfo);
            this.Controls.Add(this.btnPatch);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnSelectVsh);
            this.Controls.Add(this.lblvsh);
            this.Controls.Add(this.boxVshOrigine);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "VSHUpdater_GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iMCSx\'s VSH Patcher";
            this.Load += new System.EventHandler(this.VSHUpdater_GUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox boxVshOrigine;
        private System.Windows.Forms.Label lblvsh;
        private System.Windows.Forms.Button btnSelectVsh;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnPatch;
        private System.Windows.Forms.Label lblinfo;
    }
}

