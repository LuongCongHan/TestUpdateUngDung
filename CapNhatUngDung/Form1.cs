using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoUpdaterDotNET;

namespace CapNhatUngDung
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //AutoUpdater.Start("http://laptrinhvb.net/update.xml");
            AutoUpdater.Start("https://raw.githubusercontent.com/LuongCongHan/TestUpdateUngDung/master/CapNhatUngDung/CapNhatXML.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            string version = fvi.FileVersion;
            label1.Text = "Phiên bản: " + version;
            AutoUpdater.DownloadPath = "update";// tự đọng ở TEMP
            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 15 * 1000,
                //SynchronizingObject = this;
            };
            timer.Elapsed += delegate
            {
                //AutoUpdater.Start("http://laptrinhvb.net/update.xml");
                AutoUpdater.Start("https://raw.githubusercontent.com/LuongCongHan/TestUpdateUngDung/master/CapNhatUngDung/CapNhatXML.xml");
            };
            timer.Start();
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            
            if (args.IsUpdateAvailable)
            {
                DialogResult dialogResult;
                dialogResult =
                        MessageBox.Show(
                            $@"Bạn ơi, phần mềm của bạn có phiên bản mới {args.CurrentVersion}. Phiên bản bạn đang sử dụng hiện tại  {args.InstalledVersion}. Bạn có muốn cập nhật phần mềm không?", @"Cập nhật phần mềm",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);
                //AutoUpdater.ShowUpdateForm(args);
                //AutoUpdater.OpenDownloadPage = true;
                if (dialogResult.Equals(DialogResult.Yes) || dialogResult.Equals(DialogResult.OK))
                {
                    try
                    {
  
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            Application.Exit();
                        }
                        this.Text="Khong cai duoc";
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                //MessageBox.Show(args.ExecutablePath + " - " + args.InstallerArgs);
            }
            else
            {
                MessageBox.Show(@"Phiên bản bạn đang sử dụng đã được cập nhật mới nhất.", @"Cập nhật phần mềm",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Launch the application with some options set.
        /// </summary>
        static void LaunchCommandLineApp()
        {
            // For the example
            const string ex1 = "C:\\";
            const string ex2 = "C:\\Dir";

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "dcm2jpg.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                
                // Log error.
            }
        }
        Uri uri = new Uri("http://example.com/files/example.exe");
        string filename = @"C:\Users\**\AppData\Local\Temp\example.exe";
        string filecaidat = @"C:\Users\Dell Latitude 3540\Desktop\File cai dat CapNhatUngDung InstallForge\CapNhatUngDungv1.0.0.2.exe";

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(filecaidat);
        }

        private void TaiVaCaiDat()
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                else
                {
                    WebClient wc = new WebClient();
                    wc.DownloadFileAsync(uri, filename);
                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = 0;
            }
        }
        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("Download complete!, running exe", "Completed!");
                Process.Start(filename);
            }
            else
            {
                MessageBox.Show("Unable to download exe, please check your connection", "Download failed!");
            }
        }
        }
}
