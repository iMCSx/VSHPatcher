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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSH_Patcher
{
    public partial class VSHUpdater_GUI : Form
    {
        public VSHUpdater_GUI()
        {
            InitializeComponent();
        }

        private void boxVshOrigine_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (FileList.Length > 0)
            {
                boxVshOrigine.Text = FileList.ElementAt(0);
            }
        }

        private void boxVshOrigine_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void btnSelectVsh_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath,
                Filter = "Signed ELF|*.self",
                FileName = "vsh"
            };

            if (op.ShowDialog() == DialogResult.OK)
                boxVshOrigine.Text = op.FileName;
        }

        private void RestoreGUI()
        {
            lblinfo.TextInvoke("IDLE");
            btnPatch.EnableInvoke(true);
            progressBar.StyleInvoke(ProgressBarStyle.Blocks);
        }

        private void WorkingStuff()
        {
            // Disable button
            btnPatch.EnableInvoke(false);

            // Copy from the textbox the file
            string file = boxVshOrigine.Text;

            // Setup our progressbar
            progressBar.StyleInvoke(ProgressBarStyle.Marquee);

            // If not empty well we start steps
            if (!string.IsNullOrEmpty(file))
            {
                // Unpacking
                lblinfo.TextInvoke("Unpacking VSH...");
                Thread.Sleep(1000);
                if (VSH.Unpack(file))
                {
                    // Patching
                    lblinfo.TextInvoke("Patching VSH...");
                    Thread.Sleep(1000);

                    bool isPatched = VSH.IsPatched(VSH.WorkingDir + VSH.Unpacked_Name);

                    // If he's patched, we ask to user if he want continue (why not?)
                    if (isPatched)
                    {
                        if (MessageBox.Show("This VSH is already patched, none modification will be applied, continue anyway ?", "File Already patched", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        {
                            RestoreGUI();
                            return;
                        }
                    }

                    // Patch !
                    if (VSH.PatchPSN(VSH.WorkingDir + VSH.Unpacked_Name))
                    {
                        // Rebuild
                        lblinfo.TextInvoke("Rebuild VSH...");
                        Thread.Sleep(1000);
                        if (VSH.Pack(VSH.WorkingDir + VSH.Patched_Name))
                        {
                            string currentDir = Application.StartupPath;
                            string newVsh = currentDir + @"\" + VSH.Name;
                            string newVsh_patched = currentDir + @"\" + VSH.Patched_Name;

                            // Delete if exits
                            VSH.DeleteFile(newVsh);
                            VSH.DeleteFile(newVsh_patched);

                            // Move our final files on the main folder
                            File.Move(VSH.WorkingDir + VSH.Name, newVsh);
                            File.Move(VSH.WorkingDir + VSH.Patched_Name, newVsh_patched);

                            // If files exists that's finished.
                            if (File.Exists(newVsh) && File.Exists(newVsh_patched))
                            {
                                progressBar.StyleInvoke(ProgressBarStyle.Blocks);
                                lblinfo.TextInvoke("DONE");

                                MessageBox.Show(string.Format("Successfully patched!\r\n\r\nFile saved on this location : {0}\r\n\r\nCredits :\r\n\r\n- The PS3ITA Team\r\n- Naehrwert (Scetool)\r\n- HeAd (Testing)\r\n\r\nCreated by iMCSx.", newVsh), "Success !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                Process.Start(currentDir);

                                if (MessageBox.Show("Do you want visit my twitter profile ?", "Follow me !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                    Process.Start("http://www.twitter.com/iMCSx");
                            }
                            else
                            {
                                MessageBox.Show("Impossible to move final files, check into the folder 'Tools'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Process.Start(VSH.WorkingDir);
                            }

                        }
                        else MessageBox.Show("Packing failed, some suggestions :\r\n\r\n- Check if a file path is not invalid.\r\n- Check if you have the rights permissions to edit/move/create a file.\r\n- Check if the folder 'Tools' exists with everything on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else MessageBox.Show("Patching failed, too recent VSH maybe?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Unpacking failed, some suggestions :\r\n\r\n- Check if a file path is not invalid.\r\n- Check if you have the rights permissions to edit/move/create a file.\r\n- Check if the folder 'Tools' exists with everything on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Impossible to grab the file from the textbox, strange error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            RestoreGUI();

            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("A step failed :\r\n\r\n{0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RestoreGUI();
            }
        }

        private void btnPatch_Click(object sender, EventArgs e)
        {
            // Create a task calling a custom void (no freezing ui)
            Task T = new Task(() => WorkingStuff());

            // Start our task !
            T.Start();
        }

        private void VSHUpdater_GUI_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(VSH.WorkingDir))
            {
                MessageBox.Show("The folder 'Tools' is not in the exe folder. You need to have a folder called 'Tools' containing scetool to use this program.\r\n\r\nIf you don't understand, download the full source code on GitHub and look into the 'bin' folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }
    }
}
