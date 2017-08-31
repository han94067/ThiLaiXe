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
    public partial class FormThi : Form
    {
        public FormThi()
        {
            InitializeComponent();
        }

        public string _username;
        public Boolean _CachThi;
        private int[] ArrCauHoi = new int[20];
        private int[] ArrDapAn = new int[20];
        private int[] ArrTraLoi = new int[20];
        private int temp;
        private int Stt = 0;
        private int giay = 0;
        private int phut = 15;

        public void LoadCauHoi(int _Stt)
        {
            lbSoCau.Text = (_Stt + 1).ToString();
            rdCauC.Visible = true;
            rdCauD.Visible = true;
            btnStop.Visible = false;
            btnNext.Visible = true;
            picBox.Visible = false;

            //Hiển thị đáp án đã chọn
            if (ArrTraLoi[_Stt] == 1)
            {
                rdCauA.Checked = true;
            }
            else if (ArrTraLoi[_Stt] == 2)
            {
                rdCauB.Checked = true;
            }
            else if (ArrTraLoi[_Stt] == 3)
            {
                rdCauC.Checked = true;
            }
            else if (ArrTraLoi[_Stt] == 4)
            {
                rdCauD.Checked = true;
            }
            else
            {
                rdCauA.Checked = false;
                rdCauB.Checked = false;
                rdCauC.Checked = false;
                rdCauD.Checked = false;
            }

            //Lấy dữ liệu từ SQL
            DataTable dt = new DataTable();
            string a = "select * from Question where SttCau = @CauHoi";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CauHoi", SqlDbType.Int);
            param[0].Value = ArrCauHoi[_Stt].ToString();
            dt = DataSQL.LayDuLieu(a, param);
            lbCauHoi.Text = dt.Rows[0][1].ToString();
            rdCauA.Text = dt.Rows[0][2].ToString();
            rdCauB.Text = dt.Rows[0][3].ToString();
            rdCauC.Text = dt.Rows[0][4].ToString();
            rdCauD.Text = dt.Rows[0][5].ToString();
            temp = (int)dt.Rows[0][0];

            //gán câu hỏi có hình ảnh
            if (temp == 111 || temp == 112 || temp == 113)
            {
                picBox.Visible = true;
                picBox.Image = DoAnWindows.Properties.Resources.h1;
            }
            if (temp == 114 || temp == 115 || temp == 116)
            {
                picBox.Visible = true;
                picBox.Image = DoAnWindows.Properties.Resources.h2;
            }
            if (temp == 117)
            {
                picBox.Visible = true;
                picBox.Image = DoAnWindows.Properties.Resources.h3;
            }
            if (temp == 118)
            {
                picBox.Visible = true;
                picBox.Image = DoAnWindows.Properties.Resources.h4;
            }

            //Ẩn hiện các radiobutton, button.
            if (rdCauC.Text == "")
            {
                rdCauC.Visible = false;
            }
            if (rdCauD.Text == "")
            {
                rdCauD.Visible = false;
            }
            if (_Stt == 0)
            {
                btnPrevious.Visible = false;
            }
            if (_Stt == 19)
            {
                btnStop.Visible = true;
                btnNext.Visible = false;
            }
        }

        private void FormTracNghiem_Load(object sender, EventArgs e)
        {
            btnPrevious.Visible = false;
            btn1.BackColor = Color.Red;

            // Tạo 20 câu hỏi ngẫu nhiên
            Random ran = new Random();
            for (int i = 0; i < 20; i++)
            {
                ArrCauHoi[i] = ran.Next(1, 119);
            abc:
                for (int j = 0; j < 20; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (ArrCauHoi[i] == ArrCauHoi[j])
                    {
                        ArrCauHoi[i] = ran.Next(1, 119);
                        goto abc;
                    }
                }
            }

            // add đáp án đúng vào mảng ArrDapAn
            int k = 0;
            while (k != 20)
            {
                DataTable dt = new DataTable();
                string a = "select DapAn from Question where SttCau = @CauHoi";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@CauHoi", SqlDbType.Int);
                param[0].Value = ArrCauHoi[k].ToString();
                dt = DataSQL.LayDuLieu(a, param);
                ArrDapAn[k] = (int)dt.Rows[0][0];
                k++;
            }

            LoadCauHoi(Stt);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            Stt++;
            LoadCauHoi(Stt);
            CheckCauHoi();
            KiemTraDaChon();
            CauHoiDangChon(Stt);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            KiemTraChon();
            Stt--;
            LoadCauHoi(Stt);
            CheckCauHoi();
            KiemTraDaChon();
            CauHoiDangChon(Stt);
        }

        private void KiemTraChon()
        {
            if (rdCauA.Checked == true)
            {
                ArrTraLoi[Stt] = 1;
            }
            else if (rdCauB.Checked == true)
            {
                ArrTraLoi[Stt] = 2;
            }
            else if (rdCauC.Checked == true)
            {
                ArrTraLoi[Stt] = 3;
            }
            else if (rdCauD.Checked == true)
            {
                ArrTraLoi[Stt] = 4;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            KiemTraChon();
            KiemTraKetQua();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (giay != 00)
                giay--;
            else
            {
                giay = 59;
                if (phut != 00)
                    phut--;
                else
                {
                    phut = giay = 00;
                    timer1.Stop();
                    MessageBox.Show("Hết Thời Gian !", "THÔNG BÁO", MessageBoxButtons.OK);
                    KiemTraKetQua();
                }
            }
            if (giay < 10)
            {
                lbGiay.Text = "0" + giay.ToString();
            }
            else if (phut < 10)
            {
                lbPhut.Text = "0" + phut.ToString();
                lbGiay.Text = giay.ToString();
            }
            else
            {
                lbPhut.Text = phut.ToString();
                lbGiay.Text = giay.ToString();
            }
        }

        private void KiemTraKetQua()
        {
            int _Score = 0;
            for (int i = 0; i < 20; i++)
            {
                if (ArrDapAn[i] == ArrTraLoi[i])
                {
                    _Score++;
                }
            }

            if (_CachThi == false)
            {

                string q = "insert into ThongKe(username, ThoiGian, DiemSo) values (@User, @ThoiGian, @DiemSo)";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@User", SqlDbType.VarChar);
                param[0].Value = _username;
                param[1] = new SqlParameter("@ThoiGian", SqlDbType.VarChar);
                param[1].Value = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + "   " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                param[2] = new SqlParameter("@DiemSo", SqlDbType.Int);
                param[2].Value = _Score;
                DataSQL.NhapDuLieu(q, param);

                FormKetQuaThiThu b = new FormKetQuaThiThu();
                b._username = this._username;
                b._ArrDapAn = this.ArrDapAn;
                b._ArrTraLoi = this.ArrTraLoi;
                b._ArrCauHoi = this.ArrCauHoi;
                b.Show();
                this.Hide();
            }
            else
            {
                FormKetQuaThi a = new FormKetQuaThi();
                a._username = this._username;
                a._ArrDapAn = this.ArrDapAn;
                a._ArrTraLoi = this.ArrTraLoi;
                a.Show();
                this.Hide();
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            KiemTraChon();
            LoadCauHoi(0);
            Stt = 0;
            CheckCauHoi();
            KiemTraDaChon();
            btn1.BackColor = Color.Red;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(1);
            Stt = 1;
            CheckCauHoi();
            KiemTraDaChon();
            btn2.BackColor = Color.Red;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(2);
            Stt = 2;
            CheckCauHoi();
            KiemTraDaChon();
            btn3.BackColor = Color.Red;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(3);
            Stt = 3;
            CheckCauHoi();
            KiemTraDaChon();
            btn4.BackColor = Color.Red;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(4);
            Stt = 4;
            CheckCauHoi();
            KiemTraDaChon();
            btn5.BackColor = Color.Red;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(5);
            Stt = 5;
            CheckCauHoi();
            KiemTraDaChon();
            btn6.BackColor = Color.Red;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(6);
            Stt = 6;
            CheckCauHoi();
            KiemTraDaChon();
            btn7.BackColor = Color.Red;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(7);
            Stt = 7;
            CheckCauHoi();
            KiemTraDaChon();
            btn8.BackColor = Color.Red;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(8);
            Stt = 8;
            CheckCauHoi();
            KiemTraDaChon();
            btn9.BackColor = Color.Red;
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(9);
            Stt = 9;
            CheckCauHoi();
            KiemTraDaChon();
            btn10.BackColor = Color.Red;
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(10);
            Stt = 10;
            CheckCauHoi();
            KiemTraDaChon();
            btn11.BackColor = Color.Red;
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(11);
            Stt = 11;
            CheckCauHoi();
            KiemTraDaChon();
            btn12.BackColor = Color.Red;
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(12);
            Stt = 12;
            CheckCauHoi();
            KiemTraDaChon();
            btn13.BackColor = Color.Red;
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(13);
            Stt = 13;
            CheckCauHoi();
            KiemTraDaChon();
            btn14.BackColor = Color.Red;
        }

        private void btn15_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(14);
            Stt = 14;
            CheckCauHoi();
            KiemTraDaChon();
            btn15.BackColor = Color.Red;
        }

        private void btn16_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(15);
            Stt = 15;
            CheckCauHoi();
            KiemTraDaChon();
            btn16.BackColor = Color.Red;
        }

        private void btn17_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(16);
            Stt = 16;
            CheckCauHoi();
            KiemTraDaChon();
            btn17.BackColor = Color.Red;
        }

        private void btn18_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(17);
            Stt = 17;
            CheckCauHoi();
            KiemTraDaChon();
            btn18.BackColor = Color.Red;
        }

        private void btn19_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(18);
            Stt = 18;
            CheckCauHoi();
            KiemTraDaChon();
            btn19.BackColor = Color.Red;
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            btnPrevious.Visible = true;
            KiemTraChon();
            LoadCauHoi(19);
            Stt = 19;
            CheckCauHoi();
            KiemTraDaChon();
            btn20.BackColor = Color.Red;
        }

        private void CheckCauHoi()
        {
            btn1.BackColor = Color.Transparent;
            btn2.BackColor = Color.Transparent;
            btn3.BackColor = Color.Transparent;
            btn4.BackColor = Color.Transparent;
            btn5.BackColor = Color.Transparent;
            btn6.BackColor = Color.Transparent;
            btn7.BackColor = Color.Transparent;
            btn8.BackColor = Color.Transparent;
            btn9.BackColor = Color.Transparent;
            btn10.BackColor = Color.Transparent;
            btn11.BackColor = Color.Transparent;
            btn12.BackColor = Color.Transparent;
            btn13.BackColor = Color.Transparent;
            btn14.BackColor = Color.Transparent;
            btn15.BackColor = Color.Transparent;
            btn16.BackColor = Color.Transparent;
            btn17.BackColor = Color.Transparent;
            btn18.BackColor = Color.Transparent;
            btn19.BackColor = Color.Transparent;
            btn20.BackColor = Color.Transparent;
        }

        private void CauHoiDangChon(int a)
        {
            switch (a)
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

        private void KiemTraDaChon()
        {
            for(int i = 0; i < 20; i++)
            {
                if (ArrTraLoi[i] != 0)
                {
                    switch (i)
                    {
                        case 0:
                            btn1.BackColor = Color.YellowGreen;
                            break;
                        case 1:
                            btn2.BackColor = Color.YellowGreen;
                            break;
                        case 2:
                            btn3.BackColor = Color.YellowGreen;
                            break;
                        case 3:
                            btn4.BackColor = Color.YellowGreen;
                            break;
                        case 4:
                            btn5.BackColor = Color.YellowGreen;
                            break;
                        case 5:
                            btn6.BackColor = Color.YellowGreen;
                            break;
                        case 6:
                            btn7.BackColor = Color.YellowGreen;
                            break;
                        case 7:
                            btn8.BackColor = Color.YellowGreen;
                            break;
                        case 8:
                            btn9.BackColor = Color.YellowGreen;
                            break;
                        case 9:
                            btn10.BackColor = Color.YellowGreen;
                            break;
                        case 10:
                            btn11.BackColor = Color.YellowGreen;
                            break;
                        case 11:
                            btn12.BackColor = Color.YellowGreen;
                            break;
                        case 12:
                            btn13.BackColor = Color.YellowGreen;
                            break;
                        case 13:
                            btn14.BackColor = Color.YellowGreen;
                            break;
                        case 14:
                            btn15.BackColor = Color.YellowGreen;
                            break;
                        case 15:
                            btn16.BackColor = Color.YellowGreen;
                            break;
                        case 16:
                            btn17.BackColor = Color.YellowGreen;
                            break;
                        case 17:
                            btn18.BackColor = Color.YellowGreen;
                            break;
                        case 18:
                            btn19.BackColor = Color.YellowGreen;
                            break;
                        case 19:
                            btn20.BackColor = Color.YellowGreen;
                            break;
                    }
                }
            }
        }
    }
}