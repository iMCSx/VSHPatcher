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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSH_Patcher
{
    class VSH
    {
        public static string WorkingDir = Application.StartupPath + @"\Tools\";

        public static string Unpacked_Name = "vsh_unpacked.elf";
        public static string Patched_Name = "vsh_patched.elf";
        public static string Origin_Name = "vsh_origin.self";
        public static string Name = "vsh.self";

        public static byte[] MagicHeader = new byte[4] { 0x53, 0x43, 0x45, 0x00 };

        private static string SplitFileName(string path)
        {
            int length = path.LastIndexOf('\\');
            string fileName = path.Substring(length);
            return fileName;
        }

        // Well we don't repet the same things everywhere...
        public static void DeleteFile(string file)
        {
            if (File.Exists(file)) File.Delete(file);
        }

        // Check if the header is a signed elf by SCE.
        public static bool IsSelfSigned(string fullpath)
        {
            if (File.Exists(fullpath))
            {
                List<byte> magicFile = File.ReadAllBytes(fullpath).ToList().GetRange(0, 4);
                if (ArrayCompare(magicFile.ToArray(), MagicHeader))
                    return true;
            }
            else MessageBox.Show("Is self signed : File not found");

            return false;
        }
        
        // Decrypt the self and give the elf
        public static bool Unpack(string fullpath)
        {
            if (File.Exists(fullpath))
            {
                DeleteFile(WorkingDir + Origin_Name);

                File.Copy(fullpath, WorkingDir + Origin_Name);

                string fileToUnpack = SplitFileName(fullpath).Replace("\\", "");
                string arguments = string.Format("scetool.exe --decrypt {0} {1}", Origin_Name, Unpacked_Name);

                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", "/C " + arguments);

                Process p = new Process();

                startInfo.CreateNoWindow = true;
                startInfo.WorkingDirectory = WorkingDir;
                startInfo.EnvironmentVariables["CYGWIN"] = "nodosfilewarning";
                startInfo.RedirectStandardInput = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;

                p = Process.Start(startInfo);
                p.WaitForExit();
                p.Close();

                if (File.Exists(WorkingDir + Unpacked_Name))
                    return true;
            }
            else MessageBox.Show("Unpacking failed : File not found");

            return false;
        }

        // Encrypt the elf into a VSH signed elf format.
        public static bool Pack(string filepath_elf)
        {
            if (File.Exists(filepath_elf))
            {
                string fileToPack = SplitFileName(filepath_elf).Replace("\\", "");

                // Build our command line using the default vsh.self as template.
                string arguments = string.Format("scetool --template {0} --sce-type=SELF --compress-data=TRUE --encrypt {1} {2}", Origin_Name, fileToPack, Name);

                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", "/C " + arguments);

                Process p = new Process();

                startInfo.CreateNoWindow = true;
                startInfo.WorkingDirectory = WorkingDir;
                startInfo.EnvironmentVariables["CYGWIN"] = "nodosfilewarning";
                startInfo.RedirectStandardInput = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;

                p = Process.Start(startInfo);
                p.WaitForExit();
                p.Close();

                if (File.Exists(WorkingDir + Name))
                {
                    DeleteFile(WorkingDir + Origin_Name);
                    return true;
                }
            }
            else MessageBox.Show("Packing failed : File not found");

            return false;
        }

        // Check if the VSH is already patched
        public static bool IsPatched(string filepath_elf)
        {
            if (File.Exists(filepath_elf))
            {
                // Get the data of the file into a list (much faster!)
                List<byte> elf_data = File.ReadAllBytes(filepath_elf).ToList();
                byte[] patch_sequence = StringToByteArray("2F8000026000000060000000");

                for (int i = 0; i < elf_data.Count; i = i + 4)
                {
                    // Out of array check
                    if ((elf_data.Count - i) < 12)
                        break;

                    // Get our sequence to compare
                    byte[] sequence = elf_data.GetRange(i, 12).ToArray();

                    // If equals then return true
                    if (ArrayCompare(sequence, patch_sequence)) return true;
                }

            }
            else MessageBox.Show("Packing failed : File not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static bool PatchPSN(string filepath_elf)
        {
            if (File.Exists(filepath_elf))
            {
                // Get the data of the file into a list (much faster!)
                List<byte> elf_data = File.ReadAllBytes(filepath_elf).ToList();
                byte[] ps3ita_patch_sequence = StringToByteArray("2F800002409E003C48000010");
                byte[] ps3ita_patched = StringToByteArray("2F8000026000000060000000");
                byte[] ps3ita_patch = StringToByteArray("6000000060000000"); 

                for (int i = 0; i < elf_data.Count; i = i + 4)
                {
                    // Out of array check
                    if ((elf_data.Count - i) < 12)
                        break;

                    // Get our sequence to compare
                    byte[] sequence = elf_data.GetRange(i, 12).ToArray();

                    // Much faster than enumerable function
                    if (ArrayCompare(sequence, ps3ita_patch_sequence) || ArrayCompare(sequence, ps3ita_patched))
                    {
                        int offset = i+4; //  -4 * 4; 
                        elf_data.RemoveRange(offset, ps3ita_patch.Length);
                        elf_data.InsertRange(offset, ps3ita_patch);

                        // I'm want to be sure it is working...
                        if (ArrayCompare(elf_data.GetRange(offset, 8).ToArray(), ps3ita_patch))
                        {
                            // Write our file patched !
                            File.WriteAllBytes(WorkingDir + Patched_Name, elf_data.ToArray());

                            if (File.Exists(WorkingDir + Patched_Name))
                            {
                                // Delete the elf unpacked if exists.
                                DeleteFile(WorkingDir + Unpacked_Name);
                                return true;
                            }
                        }
                        else MessageBox.Show("The patch failed, report to iMCSx this error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else MessageBox.Show("Packing failed : File not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        // Custom function to check arrays faster
        public static bool ArrayCompare(byte[] b1, byte[] b2)
        {
            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; i++)
                    if (b1[i] != b2[i])
                        return false;
                return true;
            }
            return false;
        }

        // Convert hex string to bytes
        internal static byte[] StringToByteArray(string hex)
        {
            string replace = hex.Replace("0x", "").Replace(" ", "");
            string Stringz = replace.Insert(replace.Length - 1, "0");

            int Odd = replace.Length;
            bool Nombre;
            if (Odd % 2 == 0)
                Nombre = true;
            else
                Nombre = false;
            try
            {
                if (Nombre == true)
                {
                    return Enumerable.Range(0, replace.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(replace.Substring(x, 2), 16))
                 .ToArray();
                }
                else
                {
                    return Enumerable.Range(0, replace.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(Stringz.Substring(x, 2), 16))
                 .ToArray();
                }
            }
            catch { throw new System.ArgumentException("Value not possible.", "Byte Array"); }
        }
    }
}
