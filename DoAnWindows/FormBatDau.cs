using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWindows
{
    public partial class FormBatDau : Form
    {
        public FormBatDau()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn thực sự muốn thoát ?", "THOÁT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btndn_Click(object sender, EventArgs e)
        {
            FormDangNhap form = new FormDangNhap();
            form.Show();
            this.Hide();
        }

        private void btndk_Click(object sender, EventArgs e)
        {
            FormDangKy form = new FormDangKy();
            form.Show();
            this.Hide();
        }
    }
}
