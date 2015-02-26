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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSH_Patcher
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VSHUpdater_GUI());
        }
    }
}
