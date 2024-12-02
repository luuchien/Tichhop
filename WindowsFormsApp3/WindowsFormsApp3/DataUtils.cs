using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp3
{
    internal class DataUtils
    {
        static string filepath = "D:\\Tischop\\WindowsFormsApp3\\WindowsFormsApp3\\BangDiem.xml";
        static XmlDocument doc = new XmlDocument();
        static XmlElement root;

        public DataUtils()
        {
            doc.Load(filepath);
            root = doc.DocumentElement;
        }

        public List<SinhVien> getAllSv()
        {
            List<SinhVien> li = new List<SinhVien>();
            XmlNodeList sinhvien = doc.SelectNodes("/bangdiem/sinhvien");
            foreach (XmlNode node in sinhvien)
            {
                li.Add(new SinhVien(node.SelectSingleNode("@masv").InnerText, node.SelectSingleNode("@monhoc").InnerText, float.Parse(node.SelectSingleNode("diemlan1").InnerText), float.Parse(node.SelectSingleNode("diemlan2").InnerText)));
            }
            return li;
        }
        public HashSet<string> getMonHoc()
        {
            XmlNodeList sinhvien = doc.SelectNodes("/bangdiem/sinhvien");
            HashSet<string> mon = new HashSet<string>();
            foreach (XmlNode node in sinhvien)
            {
                string monHoc = node.Attributes["monhoc"].InnerText;
                if (!mon.Contains(monHoc))
                {
                    mon.Add(monHoc);
                }

            }
            return mon;
        }
        public bool check(SinhVien s)
        {
            return getAllSv().Any(x => x.MaSv == s.MaSv && x.MonHoc == s.MonHoc);
        }
        public bool them(SinhVien s)
        {
            if (check(s))
            {
                MessageBox.Show("Thong tin da ton tai");
                return false;
            }

            XmlElement sinhvien = doc.CreateElement("sinhvien");
            sinhvien.SetAttribute("masv", s.MaSv);
            sinhvien.SetAttribute("monhoc", s.MonHoc);
            XmlElement diemlan1 = doc.CreateElement("diemlan1");
            diemlan1.InnerText = s.DiemLan1.ToString();
            sinhvien.AppendChild(diemlan1);

            XmlElement diemlan2 = doc.CreateElement("diemlan2");
            diemlan2.InnerText = s.DiemLan2.ToString();
            sinhvien.AppendChild(diemlan2);
            root.AppendChild(sinhvien);
            doc.Save(filepath);
            return true;
        }
        public bool sua(SinhVien s)
        {
            XmlNode node = doc.SelectSingleNode($"/bangdiem/sinhvien[@masv = '{s.MaSv}'and @monhoc = '{s.MonHoc}']");
            if (node != null)
            {
                node.SelectSingleNode("diemlan1").InnerText = s.DiemLan1.ToString() ;
                node.SelectSingleNode("diemlan2").InnerText = s.DiemLan2.ToString();
                doc.Save(filepath);
                return true;
            }
            return false;
        }
        public bool xoa(SinhVien s) {
            XmlNode node = doc.SelectSingleNode($"/bangdiem/sinhvien[@masv = '{s.MaSv}'and @monhoc = '{s.MonHoc}']");
            if (node != null)
            {
                root.RemoveChild(node);
                doc.Save(filepath);
                return true;
            }
            return false;
        }

    }
}
