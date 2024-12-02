using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
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
            dgvSinhVien.DataSource = du.getAllSv();
            cbbMonHoc.Items.AddRange(du.getMonHoc().ToArray());
            cbbMonHoc.SelectedIndex = 0;

        }
        public SinhVien GetSinhVien()
        {
            return new SinhVien(txtMaSv.Text, cbbMonHoc.Text, float.Parse(txtDiemLan1.Text), float.Parse(txtDiemLan2.Text));
        }
        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if(index >=0)
            {
                txtMaSv.Text = dgvSinhVien.Rows[index].Cells[0].Value.ToString();
                cbbMonHoc.Text = dgvSinhVien.Rows[index].Cells[1].Value.ToString();
                txtDiemLan1.Text = dgvSinhVien.Rows[index].Cells[2].Value.ToString();
                txtDiemLan2.Text = dgvSinhVien.Rows[index].Cells[3].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            du.them(GetSinhVien());
            HienThi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            du.sua(GetSinhVien());
            HienThi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            du.xoa(GetSinhVien());
            HienThi();
        }
    }
}
