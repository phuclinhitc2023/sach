using BTVNWF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTVNWF
{
    public partial class Form1 : Form
    {
        private readonly List<SinhVien> sinhViens;
        public Form1()
        {
            sinhViens = new List<SinhVien>();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool gioiTinh = true;
            if (checkBox1.Checked) gioiTinh = true;
            if (checkBox2.Checked) gioiTinh = false;
            int.TryParse(textBox4.Text, out int sdt);
            var sinhVien = new SinhVien()
            {
                MASV = Guid.NewGuid().ToString(),
                HoTen = textBox1.Text,
                NgaySinh = dateTimePicker1.Value,
                GioiTinh = gioiTinh,
                DiaChi = textBox2.Text,
                Email = textBox3.Text,
                SDT = sdt
            };
            sinhViens.Add(sinhVien);
            var listViewItem = new ListViewItem(textBox1.Text);
            listViewItem.SubItems.Add(sinhVien.GioiTinh ? "Nam" : "Nữ");
            listViewItem.SubItems.Add(sinhVien.NgaySinh.ToString("dd/MM/yyyy"));
            listViewItem.SubItems.Add(sinhVien.DiaChi);
            listViewItem.SubItems.Add(sinhVien.Email);
            listViewItem.SubItems.Add("0" + sdt.ToString());
            listView1.Items.Add(listViewItem);

            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text =  null;
            checkBox1.Checked = checkBox2.Checked = false;
        }

        private void RenderListView()
        {
            foreach(var sinhVien in sinhViens)
            {
                var listViewItem = new ListViewItem(sinhVien.HoTen);
                listViewItem.SubItems.Add(sinhVien.GioiTinh.ToString());
                listViewItem.SubItems.Add(sinhVien.NgaySinh.ToString("dd/MM/yyyy"));
                listViewItem.SubItems.Add(sinhVien.DiaChi);
                listViewItem.SubItems.Add(sinhVien.Email);
                listViewItem.SubItems.Add("0" + sinhVien.SDT.ToString());
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView1.Items)
            {
                if(item.Selected)
                {
                    textBox1.Text = item.SubItems[0].Text;
                    if (item.SubItems[1].Text == "Nam") checkBox1.Checked = true;
                    else if (item.SubItems[1].Text == "Nữ") checkBox2.Checked = true;
                    DateTime.TryParse(item.SubItems[2].Text, out DateTime dateTime);
                    dateTimePicker1.Value = dateTime;
                    textBox2.Text = item.SubItems[3].Text;
                    textBox3.Text = item.SubItems[4].Text;
                    textBox4.Text = item.SubItems[5].Text;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked) checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked) checkBox1.Checked = false;
        }
    }
}
