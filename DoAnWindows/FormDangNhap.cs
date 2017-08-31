using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DoAnWindows
{
    public partial class FormDangNhap : Form
    {
        public string _username, _password;

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            txtUser.Text = _username;
            txtPass.Text = _password;
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            FormDangKy form = new FormDangKy();
            form.Show();
            this.Hide();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string a = "select * from ThongTin where Username = @user";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@user", SqlDbType.VarChar);
            param[0].Value = txtUser.Text;
            dt = DataSQL.LayDuLieu(a, param);

            if (dt.Rows.Count == 0)
            {
                lbThongBao2.Visible = true;
            }
            else
            {
                if (txtPass.Text.Equals(dt.Rows[0][1]))
                {
                    DialogResult dialog = MessageBox.Show("Đăng nhập thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                    if (dialog == DialogResult.OK)
                    {
                        FormQuanLy form = new FormQuanLy();
                        form._user = txtUser.Text;
                        form.Show();
                        this.Hide();
                    }
                }
                else
                {
                    lbThongBao2.Visible = true;
                }
            }
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txtUser.Text != "" && txtPass.Text != "")
            {
                btnDangNhap.Enabled = true;
            }
            else
            {
                btnDangNhap.Enabled = false;
            }
        }
    }
}