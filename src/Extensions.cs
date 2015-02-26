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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSH_Patcher
{
    static class Extensions
    {
        public static void ValueInvoke(this ProgressBar widget, int value)
        {
            widget.Invoke(new EventHandler(delegate { widget.Value = value; widget.Refresh(); }));
        }

        public static void ValueMaxInvoke(this ProgressBar widget, int value)
        {
            widget.Invoke(new EventHandler(delegate { widget.Maximum = value; }));
        }

        public static void StepInvoke(this ProgressBar widget, int value)
        {
            widget.Invoke(new EventHandler(delegate { widget.Step = value; }));
        }

        public static void PerformStepInvoke(this ProgressBar widget)
        {
            widget.Invoke(new EventHandler(delegate { widget.PerformStep(); }));
        }

        public static void StyleInvoke(this ProgressBar widget, ProgressBarStyle progStyle)
        {
            widget.Invoke(new EventHandler(delegate { widget.Style = progStyle; }));
        }

        public static void EnableInvoke(this Button widget, bool value)
        {
            widget.Invoke(new EventHandler(delegate { widget.Enabled = value; widget.Update(); }));
        }

        public static void TextInvoke(this Label widget, string value)
        {
            widget.Invoke(new EventHandler(delegate { widget.Text = value; widget.Refresh(); }));
        }
    }
}
