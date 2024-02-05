using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LamViecVoiFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDirection_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"C:\Users\Dell Latitude 3540\Desktop\File cai dat CapNhatUngDung InstallForge\Thumuc4");
            Directory.Delete(@"C:\Users\Dell Latitude 3540\Desktop\File cai dat CapNhatUngDung InstallForge\Thumuc3",true);
            var files= Directory.GetDirectories(@"C:\Users\Dell Latitude 3540\Desktop\File cai dat CapNhatUngDung InstallForge");
            string txt = null;
            foreach (var item in files)
            {
                txt += item + Environment.NewLine;
            }
            label1.Text = txt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string v1 = "1.23.56.1487";
            string v2 = "1.24.55.487";

            string txt = "";
            var version1 = new Version(txtV1.Text);
            var version2 = new Version(txtV2.Text);

            var result = version1.CompareTo(version2);
            if (result > 0)
                txt="version1 is greater";
            else if (result < 0)
                txt="version2 is greater";
            else
                txt="versions are equal";

            label1.Text = txt;

            //var ass = Assembly.GetExecutingAssembly();
            //label1.Text = ass.Location;
            //Process.Start(ass.Location);
        }
        

    }
}
