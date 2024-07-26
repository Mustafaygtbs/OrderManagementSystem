using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManagementSystem
{
    public partial class SiparisForm : Form
    {
        ProjelerVTEntities entities = new ProjelerVTEntities();
        public SiparisForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selection = dataGridView1.SelectedCells[0].RowIndex;
            textBoxAdet.Text = dataGridView1.Rows[selection].Cells[4].Value.ToString();
            comboBoxMusteriId.SelectedValue = dataGridView1.Rows[selection].Cells[2].Value;
            comboBoxUrunId.SelectedValue = dataGridView1.Rows[selection].Cells[3].Value;
            dateTimePickerTarih.Value = Convert.ToDateTime(dataGridView1.Rows[selection].Cells[1].Value);
            textBoxSiparisNo.Text = dataGridView1.Rows[selection].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TumKayitlariGoster();
            MusteriSayisiGuncelle();
        }
        private void TumKayitlariGoster()
        {
            var siparisler = (from siparis in entities.Siparis
                              select new
                              {
                                  siparis.SiparisNo,
                                  siparis.Tarih,
                                  siparis.MusteriID,
                                  siparis.UrunID,
                                  siparis.Adet,
                              }).ToList();
            dataGridView1.DataSource = siparisler;
            dataGridView1.ClearSelection();
            textBoxSiparisNo.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TumKayitlariGoster();
            Siparis siparis = new Siparis();
            siparis.Tarih = dateTimePickerTarih.Value;
            siparis.MusteriID = Convert.ToInt32(comboBoxMusteriId.SelectedValue.ToString());
            siparis.UrunID = Convert.ToInt32(comboBoxUrunId.SelectedValue.ToString());
            try
            {
                siparis.Adet = Convert.ToInt32(textBoxAdet.Text);
                entities.Siparis.Add(siparis);
                entities.SaveChanges();
                MessageBox.Show("New record has been added successfully.");
            }
            catch
            {
                MessageBox.Show("Error occurred during database operations . ERROR CODE: E3154 \n");
            }
            TumKayitlariGoster();
            MusteriSayisiGuncelle();
        }
        private void MusteriSayisiGuncelle()
        {
            labelUrunSayisi.Text = entities.Siparis.Count().ToString();
        }

        private void SiparisForm_Load(object sender, EventArgs e)
        {
            TumKayitlariGoster();
            var musteriler = (from musteri in entities.Musteri
                              select new
                              {
                                  musteri.MusteriID,
                                  musteri.Ad,
                                  musteri.Soyad
                              }).ToList();
            comboBoxMusteriId.ValueMember = "MusteriId";
            comboBoxMusteriId.DisplayMember = "Ad" + "Soyad";
            comboBoxMusteriId.DataSource = musteriler;

            var urunler = (from urun in entities.Urun
                           select new
                           {
                               urun.UrunID,
                               urun.Ad,
                               urun.Fiyat
                           }).ToList();
            comboBoxUrunId.ValueMember = "UrunId";
            comboBoxUrunId.DisplayMember = "Ad" + "Fiyat";
            comboBoxUrunId.DataSource = urunler;
            TumKayitlariGoster();
            dataGridView1.ClearSelection();
            MusteriSayisiGuncelle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
            int siparisNo = Convert.ToInt32(textBoxSiparisNo.Text);
            var siparis = entities.Siparis.Find(siparisNo); 
            entities.Siparis.Remove(siparis);
            entities.SaveChanges();
            MessageBox.Show("Record has been deleted successfully.");
            }
            catch
            {
                MessageBox.Show("Error occurred during database operations . ERROR CODE: E3170 \n");
            }
            TumKayitlariGoster();
            MusteriSayisiGuncelle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int siparisNo = Convert.ToInt32(textBoxSiparisNo.Text);
                var siparis = entities.Siparis.Find(siparisNo);
                siparis.Tarih = dateTimePickerTarih.Value;
                siparis.MusteriID = Convert.ToInt32(comboBoxMusteriId.SelectedValue.ToString());
                siparis.UrunID = Convert.ToInt32(comboBoxUrunId.SelectedValue.ToString());
                siparis.Adet = Convert.ToInt32(textBoxAdet.Text);
                entities.SaveChanges();
                MessageBox.Show("Record has been updated successfully.");
            }
            catch
            {
                MessageBox.Show("Error occurred during database operations . ERROR CODE: E3155 \n");
            }
            TumKayitlariGoster();
            MusteriSayisiGuncelle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int musteriId = Convert.ToInt32(comboBoxMusteriId.SelectedValue.ToString());
            var siparisler = (from siparis in entities.Siparis
                              where siparis.MusteriID == musteriId
                              select siparis).ToList();
            dataGridView1.DataSource = siparisler;
            dataGridView1.ClearSelection();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int urunId = Convert.ToInt32(comboBoxUrunId.SelectedValue.ToString());
            var siparisler = (from siparis in entities.Siparis
                              where siparis.UrunID == urunId
                              select siparis).ToList();
                         
            dataGridView1.DataSource = siparisler;
            dataGridView1.ClearSelection();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePickerTarih.Value.Date;
            var siparisler = (from siparis in entities.Siparis
                              where siparis.Tarih == date
                              select siparis).ToList();
            dataGridView1.DataSource = siparisler;
            dataGridView1.ClearSelection();
        }
    }
}
