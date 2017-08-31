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
    public partial class FormDangKy : Form
    {
        public FormDangKy()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn thực sự muốn thoát ?", "THOÁT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(txtXNPASS.Text))
            {
                string a = "insert into ThongTin(Username, Pass, HoTen, NgaySinh, DiaChi, SDT) values (@User, @Pass, @HoTen, @NgaySinh, @DiaChi, @SDT)";
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@User", SqlDbType.VarChar);
                param[0].Value = txtUsername.Text;
                param[1] = new SqlParameter("@Pass", SqlDbType.VarChar);
                param[1].Value = txtPassword.Text;
                param[2] = new SqlParameter("@HoTen", SqlDbType.NVarChar);
                param[2].Value = txtHoTen.Text;
                param[3] = new SqlParameter("@NgaySinh", SqlDbType.NVarChar);
                param[3].Value = cbbNgay.Text + "/" + cbbThang.Text + "/" + cbbNam.Text;
                param[4] = new SqlParameter("@DiaChi", SqlDbType.NVarChar);
                param[4].Value = txtDiaChi.Text;
                param[5] = new SqlParameter("@SDT", SqlDbType.NVarChar);
                param[5].Value = txtSDT.Text;

                bool check = DataSQL.NhapDuLieu(a, param);
                if (!check)
                {
                    lbPass.ForeColor = Color.Black;
                    lbThongBao.Visible = true;
                    lbThongBao.Text = "Username đã được sử dụng.";
                    lbUsername.ForeColor = Color.Red;
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Đăng ký thành công.", "Thông Báo", MessageBoxButtons.OK);
                    if (dialog == DialogResult.OK)
                    {
                        FormDangNhap form = new FormDangNhap();
                        form._username = txtUsername.Text;
                        form._password = txtPassword.Text;
                        form.Show();
                        this.Hide();
                    }
                }
            }
            else
            {
                lbUsername.ForeColor = Color.Black;
                lbThongBao.Visible = true;
                lbThongBao.Text = "Mật khẩu không chính xác.";
                lbPass.ForeColor = Color.Red;
            }
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            FormBatDau form = new FormBatDau();
            form.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                picTrue.Visible = false;
                picFalse.Visible = false;
            }
            else
            {
                DataTable dt = new DataTable();
                string a = "select * from ThongTin where Username = @user";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@user", SqlDbType.VarChar);
                param[0].Value = txtUsername.Text;
                dt = DataSQL.LayDuLieu(a, param);
                if (dt.Rows.Count == 0)
                {
                    picTrue.Visible = true;
                    picFalse.Visible = false;
                }
                else
                {
                    picTrue.Visible = false;
                    picFalse.Visible = true;
                }
            }

            if (txtUsername.Text != "" && txtPassword.Text != "" && txtXNPASS.Text != "" && txtHoTen.Text != "" && txtDiaChi.Text != "" && txtSDT.Text != "" && cbbNgay.Text != "" && cbbThang.Text != "" && cbbNam.Text != "")
            {
                btnDangKy.Enabled = true;
            }
            else
            {
                btnDangKy.Enabled = false;
            }
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {
            lbThongBao.Visible = false;
            for (int i = 1; i <= 31; i++)
            {
                cbbNgay.Items.Add(i);
            }
            for (int i = 1; i <= 12; i++)
            {
                cbbThang.Items.Add(i);
            }
            for (int i = 1930; i <= 2016; i++)
            {
                cbbNam.Items.Add(i);
            }
        }

        private void cbbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbNgay.Items.Remove(29);
            cbbNgay.Items.Remove(30);
            cbbNgay.Items.Remove(31);
            string NamSinh = cbbNam.Text;
            string ThangSinh = cbbThang.Text;
            int NS, TS;
            if (NamSinh == "")
            {
                NS = 1930;
            }
            else
            {
                NS = int.Parse(NamSinh);
            }
            if (ThangSinh == "")
            {
                TS = 1;
            }
            else
            {
                TS = int.Parse(ThangSinh);
            }

            if ((NS % 400 == 0) || (NS % 4 == 0 && NS % 100 != 0))
            {
                if (TS == 2)
                {
                    cbbNgay.Items.Add(29);
                }
                else if (TS == 4 || TS == 6 || TS == 9 || TS == 11)
                {
                    cbbNgay.Items.Add(29);
                    cbbNgay.Items.Add(30);
                }
                else
                {
                    cbbNgay.Items.Add(29);
                    cbbNgay.Items.Add(30);
                    cbbNgay.Items.Add(31);
                }
            }
            else
            {
                if (TS == 2) { }
                else if (TS == 4 || TS == 6 || TS == 9 || TS == 11)
                {
                    cbbNgay.Items.Add(29);
                    cbbNgay.Items.Add(30);
                }
                else
                {
                    cbbNgay.Items.Add(29);
                    cbbNgay.Items.Add(30);
                    cbbNgay.Items.Add(31);
                }
            }
        }

        private void cbbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbNgay.Items.Remove(29);
            cbbNgay.Items.Remove(30);
            cbbNgay.Items.Remove(31);
            string NamSinh = cbbNam.Text;
            string ThangSinh = cbbThang.Text;
            int NS, TS;
            if (NamSinh == "")
            {
                NS = 1930;
            }
            else
            {
                NS = int.Parse(NamSinh);
            }
            if (ThangSinh == "")
            {
                TS = 1;
            }
            else
            {
                TS = int.Parse(ThangSinh);
            }

            if ((NS % 400 == 0) || (NS % 4 == 0 && NS % 100 != 0))
            {
                if (TS == 2)
                {
                    cbbNgay.Items.Add(29);
                }
                else if (TS == 4 || TS == 6 || TS == 9 || TS == 11)
                {
                    cbbNgay.Items.Add(29);
                    cbbNgay.Items.Add(30);
                }
                else
                {
                    cbbNgay.Items.Add(29);
                    cbbNgay.Items.Add(30);
                    cbbNgay.Items.Add(31);
                }
            }
            else
            {
                if (TS == 2) { }
                else if (TS == 4 || TS == 6 || TS == 9 || TS == 11)
                {
                    cbbNgay.Items.Add(29);
                    cbbNgay.Items.Add(30);
                }
                else
                {
                    cbbNgay.Items.Add(29);
                    cbbNgay.Items.Add(30);
                    cbbNgay.Items.Add(31);
                }
            }
        }

        private void txtHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = true;
                MessageBox.Show("Hãy nhập họ và tên.", "", MessageBoxButtons.OK);
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z'))
            {
                e.Handled = true;
                MessageBox.Show("Hãy nhập số điện thoại.", "", MessageBoxButtons.OK);
            }
        }
    }
}