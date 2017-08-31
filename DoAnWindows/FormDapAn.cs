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
    public partial class FormDapAn : Form
    {
        public int[] ArrDapAn = new int[20];
        public int[] ArrTraLoi = new int[20];
        public int[] ArrCauHoi = new int[20];
        private int temp;

        public FormDapAn()
        {
            InitializeComponent();
        }

        private void LoadDuLieu(int Stt)
        {
            pic.Visible = false;

            DataTable dt = new DataTable();
            string a = "select * from Question where SttCau = @CauHoi";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CauHoi", SqlDbType.Int);
            param[0].Value = ArrCauHoi[Stt].ToString();
            dt = DataSQL.LayDuLieu(a, param);
            lbCauHoi.Text = "Câu " + (Stt + 1).ToString() + ": " + dt.Rows[0][1].ToString();
            temp = (int)dt.Rows[0][0];

            if (ArrTraLoi[Stt] == 1)
            {
                lbTraLoi.Text = dt.Rows[0][2].ToString();
            }
            else if (ArrTraLoi[Stt] == 2)
            {
                lbTraLoi.Text = dt.Rows[0][3].ToString();
            }
            else if (ArrTraLoi[Stt] == 3)
            {
                lbTraLoi.Text = dt.Rows[0][4].ToString();
            }
            else if (ArrTraLoi[Stt] == 4)
            {
                lbTraLoi.Text = dt.Rows[0][5].ToString();
            }
            else
            {
                lbTraLoi.Text = "Chưa chọn câu trả lời !";
            }

            if (temp == 111 || temp == 112 || temp == 113)
            {
                pic.Visible = true;
                pic.Image = DoAnWindows.Properties.Resources.h1;
            }
            if (temp == 114 || temp == 115 || temp == 116)
            {
                pic.Visible = true;
                pic.Image = DoAnWindows.Properties.Resources.h2;
            }
            if (temp == 117)
            {
                pic.Visible = true;
                pic.Image = DoAnWindows.Properties.Resources.h3;
            }
            if (temp == 118)
            {
                pic.Visible = true;
                pic.Image = DoAnWindows.Properties.Resources.h4;
            }

            if (ArrDapAn[Stt] == 1)
            {
                lbDapAn.Text = dt.Rows[0][2].ToString();
            }
            else if (ArrDapAn[Stt] == 2)
            {
                lbDapAn.Text = dt.Rows[0][3].ToString();
            }
            else if (ArrDapAn[Stt] == 3)
            {
                lbDapAn.Text = dt.Rows[0][4].ToString();
            }
            else if (ArrDapAn[Stt] == 4)
            {
                lbDapAn.Text = dt.Rows[0][5].ToString();
            }
        }

        private void FormDapAn_Load(object sender, EventArgs e)
        {
            LoadDuLieu(0);
            for (int i = 0; i < 20; i++)
            {
                if (ArrDapAn[i] != ArrTraLoi[i])
                {
                    switch (i)
                    {
                        case 0:
                            btn1.BackColor = Color.Red;
                            break;
                        case 1:
                            btn2.BackColor = Color.Red;
                            break;
                        case 2:
                            btn3.BackColor = Color.Red;
                            break;
                        case 3:
                            btn4.BackColor = Color.Red;
                            break;
                        case 4:
                            btn5.BackColor = Color.Red;
                            break;
                        case 5:
                            btn6.BackColor = Color.Red;
                            break;
                        case 6:
                            btn7.BackColor = Color.Red;
                            break;
                        case 7:
                            btn8.BackColor = Color.Red;
                            break;
                        case 8:
                            btn9.BackColor = Color.Red;
                            break;
                        case 9:
                            btn10.BackColor = Color.Red;
                            break;
                        case 10:
                            btn11.BackColor = Color.Red;
                            break;
                        case 11:
                            btn12.BackColor = Color.Red;
                            break;
                        case 12:
                            btn13.BackColor = Color.Red;
                            break;
                        case 13:
                            btn14.BackColor = Color.Red;
                            break;
                        case 14:
                            btn15.BackColor = Color.Red;
                            break;
                        case 15:
                            btn16.BackColor = Color.Red;
                            break;
                        case 16:
                            btn17.BackColor = Color.Red;
                            break;
                        case 17:
                            btn18.BackColor = Color.Red;
                            break;
                        case 18:
                            btn19.BackColor = Color.Red;
                            break;
                        case 19:
                            btn20.BackColor = Color.Red;
                            break;
                    }
                }
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            LoadDuLieu(0);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            LoadDuLieu(1);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            LoadDuLieu(2);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            LoadDuLieu(3);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            LoadDuLieu(4);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            LoadDuLieu(5);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            LoadDuLieu(6);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            LoadDuLieu(7);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            LoadDuLieu(8);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            LoadDuLieu(9);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            LoadDuLieu(10);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            LoadDuLieu(11);
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            LoadDuLieu(12);
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            LoadDuLieu(13);
        }

        private void btn15_Click(object sender, EventArgs e)
        {
            LoadDuLieu(14);
        }

        private void btn16_Click(object sender, EventArgs e)
        {
            LoadDuLieu(15);
        }

        private void btn17_Click(object sender, EventArgs e)
        {
            LoadDuLieu(16);
        }

        private void btn18_Click(object sender, EventArgs e)
        {
            LoadDuLieu(17);
        }

        private void btn19_Click(object sender, EventArgs e)
        {
            LoadDuLieu(18);
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            LoadDuLieu(19);
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
