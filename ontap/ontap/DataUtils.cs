using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ontap
{
    internal class DataUtils
    {
        static string filePath = "D:\\Tischop\\ontap\\ontap\\BangDiem.xml";
        static XmlDocument doc = new XmlDocument();
        static XmlElement root;

        public DataUtils()
        {
            doc.Load(filePath);
            root = doc.DocumentElement;
        }
        public List<SinhVien> getAllSV()
        {
            List<SinhVien> li = new List<SinhVien>();
            XmlNodeList sinhviens = doc.SelectNodes("/bangdiem/sinhvien");
            foreach (XmlNode node in sinhviens)
            {
                li.Add(new SinhVien(node.SelectSingleNode("@masv").InnerText, node.SelectSingleNode("@monhoc").InnerText , float.Parse(node.SelectSingleNode("diemlan1").InnerText),float.Parse(node.SelectSingleNode("diemlan2").InnerText)));
            }
            return li;
        }

        public bool kt(SinhVien s)
        {
            return getAllSV().Any(p => p.MaSv == s.MaSv && p.MonHoc == s.MonHoc);
        }
        public bool them(SinhVien s)
        {
            if(kt(s))
                {
                MessageBox.Show("Thong tin da ton tai");
                return false;

                }
            XmlElement sinhvien = doc.CreateElement("sinhvien");
            sinhvien.SetAttribute("masv", s.MaSv); // Gán thuộc tính "masv"
            sinhvien.SetAttribute("monhoc", s.MonHoc); // Gán thuộc tính "monhoc"

            // Tạo các phần tử con <diemlan1> và <diemlan2>
            XmlElement diemlan1 = doc.CreateElement("diemlan1");
            diemlan1.InnerText = s.DiemLan1.ToString(); // Điểm lần 1
            XmlElement diemlan2 = doc.CreateElement("diemlan2");
            diemlan2.InnerText = s.DiemLan2.ToString(); // Điểm lần 2

            // Thêm các phần tử con vào <sinhvien>
            sinhvien.AppendChild(diemlan1);
            sinhvien.AppendChild(diemlan2);

            // Thêm phần tử <sinhvien> vào gốc
            root.AppendChild(sinhvien);

            // Lưu thay đổi vào file XML
            doc.Save(filePath);

            return true; // Thêm thành công
        }

        public bool sua(SinhVien s)
        {
            XmlNode node = doc.SelectSingleNode($"/bangdiem/sinhvien[@masv='{s.MaSv}' and @monhoc='{s.MonHoc}']");
            if (node != null)
            {
                // Sửa điểm lần 1
                node.SelectSingleNode("diemlan1").InnerText = s.DiemLan1.ToString();
                // Sửa điểm lần 2
                node.SelectSingleNode("diemlan2").InnerText = s.DiemLan2.ToString();

                // Lưu thay đổi vào file XML
                doc.Save(filePath);
                return true; // Sửa thành công
            }
            return false; // Không tìm thấy sinh viên
        }
        public bool xoa(SinhVien s)
        {
            // Tìm phần tử <sinhvien> cần xóa
            XmlNode node = doc.SelectSingleNode($"/bangdiem/sinhvien[@masv='{s.MaSv}' and @monhoc='{s.MonHoc}']");
            if (node != null)
            {
                // Xóa node khỏi gốc
                root.RemoveChild(node);

                // Lưu thay đổi vào file XML
                doc.Save(filePath);
                return true; // Xóa thành công
            }
            return false; // Không tìm thấy sinh viên
        }
    }
    
}
