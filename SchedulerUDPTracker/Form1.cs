using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApplication1 {
    public partial class Form1 : Form {

        private UDPListener Listener;
        public Form1() {
            InitializeComponent();
            this.Listener = new UDPListener(Notify);
            this.Listener.Begin("localhost", 6666);

            this.HideThis();
        }

        private void Notify(string msg) {
            this.notifyIcon1.ShowBalloonTip(2000, "", msg, ToolTipIcon.Info);
            this.textBox1.Invoke(new Action(() => {
                this.textBox1.AppendText(string.Format("{0}\r\n{1}\r\n\r\n", DateTime.Now, msg));
            }));
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void 主界面ToolStripMenuItem_Click(object sender, EventArgs e) {
            this.ShowThis();
        }

        private void HideThis() {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.SetVisibleCore(false);
        }

        private void ShowThis() {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.SetVisibleCore(true);
        }

        private void Form1_Resize(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.HideThis();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e) {
            if (this.WindowState != FormWindowState.Minimized)
                this.HideThis();
            else
                this.ShowThis();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.textBox1.Text = "";
        }
    }
}
