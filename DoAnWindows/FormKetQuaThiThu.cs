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
    public partial class FormKetQuaThiThu : Form
    {
        public string _username;
        public int[] _ArrDapAn = new int[20];
        public int[] _ArrTraLoi = new int[20];
        public int[] _ArrCauHoi = new int[20];

        public FormKetQuaThiThu()
        {
            InitializeComponent();
        }

        private void FormKetQuaThiThu_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string a = "select * from ThongTin where Username = @user";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@user", SqlDbType.VarChar);
            param[0].Value = _username;
            dt = DataSQL.LayDuLieu(a, param);
            lbHoTen.Text = dt.Rows[0][2].ToString();

            int traloidung, traloisai, chuatraloi;
            traloidung = traloisai = chuatraloi = 0;
            for (int i = 0; i < 20; i++)
            {
                if (_ArrTraLoi[i] == 0)
                {
                    chuatraloi++;
                }
                else
                {
                    if (_ArrTraLoi[i] == _ArrDapAn[i])
                    {
                        traloidung++;
                    }
                    else
                    {
                        traloisai++;
                    }
                }
            }

            lbTraLoiDung.Text = traloidung.ToString();
            lbTraLoiSai.Text = traloisai.ToString();
            lbChuaTraLoi.Text = chuatraloi.ToString();

            if ((20 - (traloisai + chuatraloi)) >= 16)
            {
                lbKetQua.Text = "ĐẠT";
            }
            else
            {
                lbKetQua.Text = "KHÔNG ĐẠT";
            }
        }

        private void btnDapAn_Click(object sender, EventArgs e)
        {
            FormDapAn form = new FormDapAn();
            form.ArrCauHoi = this._ArrCauHoi;
            form.ArrDapAn = this._ArrDapAn;
            form.ArrTraLoi = this._ArrTraLoi;
            form.Show();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            FormQuanLy fom = new FormQuanLy();
            fom._user = this._username;
            fom.Show();
            this.Hide();
        }

    }
}
