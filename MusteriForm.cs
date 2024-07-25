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
    public partial class MusteriForm : Form
    {
        ProjelerVTEntities entities = new ProjelerVTEntities();
        public MusteriForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            tumKayitleriGoster();
        }
        private void tumKayitleriGoster()
        {
            // var musteriListesi = entities.Musteri.ToList();
            //  dataGridView1.DataSource = musteriListesi;
            var musteriler = (from musteri in entities.Musteri
                              select new
                              {
                                  musteri.MusteriID,
                                    musteri.Ad,
                                    musteri.Soyad,
                                    musteri.Sehir                             
                              }).ToList();
            dataGridView1.DataSource = musteriler;
            dataGridView1.ClearSelection();
            MetinKutulariniTemizle();
        }
        private void MusteriForm_Load(object sender, EventArgs e)
        {
            tumKayitleriGoster();
            textBoxMusteriID.Text = "0";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Musteri musteri = new Musteri();
            musteri.Ad = textBoxAd.Text;
            musteri.Soyad = textBoxSoyad.Text;
            musteri.Sehir = textBoxSehir.Text;
            try
            {
                entities.Musteri.Add(musteri);
                entities.SaveChanges();
                MessageBox.Show("New record has been added successfully.");
                tumKayitleriGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during database operations . ERROR CODE: E3106\n" + ex.Message);
            }
            MetinKutulariniTemizle();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selection = dataGridView1.SelectedCells[0].RowIndex;
            textBoxMusteriID.Text = dataGridView1.Rows[selection].Cells[0].Value.ToString();
            textBoxAd.Text = dataGridView1.Rows[selection].Cells[1].Value.ToString();
            textBoxSoyad.Text = dataGridView1.Rows[selection].Cells[2].Value.ToString();
            textBoxSehir.Text = dataGridView1.Rows[selection].Cells[3].Value.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int musteriId =Convert.ToInt32(textBoxMusteriID.Text);
                var musteri = entities.Musteri.Find(musteriId);
                entities.Musteri.Remove(musteri);
                entities.SaveChanges();
                MessageBox.Show("Record has been deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during database operations or values cannot be empty . ERROR CODE: E3107\n" + ex.Message);
            }
            tumKayitleriGoster();
            MetinKutulariniTemizle();          
        }
        private void MetinKutulariniTemizle()
        {
            textBoxAd.Clear();
            textBoxSoyad.Clear();
            textBoxSehir.Clear();
            textBoxMusteriID.Text = "0";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int musteriId = Convert.ToInt32(textBoxMusteriID.Text);
                var musteri = entities.Musteri.Find(musteriId);
                musteri.Ad = textBoxAd.Text;
                musteri.Soyad = textBoxSoyad.Text;
                musteri.Sehir = textBoxSehir.Text;
                entities.SaveChanges();
                MessageBox.Show("Record has been updated successfully.");
                tumKayitleriGoster();
            }
            catch
            {
                MessageBox.Show("Error occurred during database operations or values cannot be empty . ERROR CODE: E3121\n");
            }
        }
    }
}
