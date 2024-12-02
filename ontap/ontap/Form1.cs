using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ontap
{
    public partial class Form1 : Form
    {
        DataUtils du = new DataUtils();
        public Form1()
        {
            InitializeComponent();
            HienThi();
        }
        public void HienThi()
        {
            dgvSinhVien.DataSource = du.getAllSV();
        }
        public SinhVien GetSinhVien()
        {
            return new SinhVien(txtMasv.Text, txtMonHoc.Text, float.Parse(txtDiemLan1.Text), float.Parse(txtDiemLan2.Text));
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            du.them(GetSinhVien());
            HienThi();
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtMasv.Text = dgvSinhVien.Rows[index].Cells[0].Value.ToString();
                txtMonHoc.Text = dgvSinhVien.Rows[index].Cells[1].Value.ToString();
                txtDiemLan1.Text = dgvSinhVien.Rows[index].Cells[2].Value.ToString();
                txtDiemLan2.Text = dgvSinhVien.Rows[index].Cells[3].Value.ToString();

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            du.sua(GetSinhVien());
            HienThi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            du.xoa(GetSinhVien());
            HienThi();
        }
    }
}
