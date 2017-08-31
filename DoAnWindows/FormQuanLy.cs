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
    public partial class FormQuanLy : Form
    {
        public string _user;
        public Boolean CachThi;

        public FormQuanLy()
        {
            InitializeComponent();
        }

        private void FormQuanLy_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string a = "select * from ThongTin where Username = @user";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@user", SqlDbType.VarChar);
            param[0].Value = _user;
            dt = DataSQL.LayDuLieu(a, param);
            lbHoTen.Text = dt.Rows[0][2].ToString();
            lbNgaySinh.Text = dt.Rows[0][3].ToString();
            lbDiaChi.Text = dt.Rows[0][4].ToString();
            lbSDT.Text = dt.Rows[0][5].ToString();

            a = "select ThoiGian, DiemSo from ThongKe where username = @user";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@user", SqlDbType.VarChar);
            para[0].Value = _user;
            dt = DataSQL.LayDuLieu(a, para);

            string[] arr = new string[4];
            ListViewItem itm;
            if (dt.Rows.Count == 0)
            {
                arr[0] = "Chưa có dữ liệu.";
                itm = new ListViewItem(arr);
                LV.Items.Add(itm);
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = dt.Rows[i][0].ToString();
                    arr[1] = dt.Rows[i][1].ToString();
                    itm = new ListViewItem(arr);
                    LV.Items.Add(itm);
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

        private void btnThiThu_Click(object sender, EventArgs e)
        {
            CachThi = false;
            FormThi form = new FormThi();
            form._username = this._user;
            form._CachThi = this.CachThi;
            form.Show();
            this.Hide();
        }

        private void btnBatDauThi_Click(object sender, EventArgs e)
        {
            CachThi = true;
            FormThi form = new FormThi();
            form._username = this._user;
            form._CachThi = this.CachThi;
            form.Show();
            this.Hide();
        }
    }
}
