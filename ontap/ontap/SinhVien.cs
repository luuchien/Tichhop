using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ontap
{
    public class SinhVien
    {
        public string MaSv { get; set; }
        public string MonHoc { get; set; }
        public float DiemLan1 { get; set; }
        public float DiemLan2 { get; set; }

        public SinhVien() { }
        public SinhVien(string maSv, string monHoc, float diemLan1, float diemLan2)
        {
            MaSv = maSv;
            MonHoc = monHoc;
            DiemLan1 = diemLan1;
            DiemLan2 = diemLan2;
        }

    }
}
