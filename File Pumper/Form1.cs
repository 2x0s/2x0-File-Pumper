using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace File_Pumper
{
    public partial class Form1 : Form
    {
        [DllImport("user32")]
        private static extern bool ReleaseCapture();

        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wp, int lp);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 161, 2, 0);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog x = new OpenFileDialog())
            {
                x.Filter = "exe file (*.exe)|*.exe";
                if (x.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = x.FileName;
                }
                else
                {
                    textBox1.Clear();
                }
            }





        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = Convert.ToString(trackBar1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "exe file (*.exe)|*.exe";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                long FileSize = trackBar1.Value;
                File.Copy(textBox1.Text, sfd.FileName);
                if (radioButton1.Checked)
                {
                    FileSize = FileSize * 1024;
                }
                if (radioButton2.Checked)
                {
                    FileSize = FileSize * 1048576;
                }
                FileStream FileToPump = File.OpenWrite(sfd.FileName);
                long Size = FileToPump.Seek(0, SeekOrigin.End);
                while (Size < FileSize)
                {
                    FileToPump.WriteByte(0);
                    Size += 1;
                }
                FileToPump.Close();
                if (Size == FileSize)
                {
                    MessageBox.Show("Pumped!", "2x0 File Pumper!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error!", "Ouch Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
