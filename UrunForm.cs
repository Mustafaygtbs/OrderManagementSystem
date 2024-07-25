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
    public partial class UrunForm : Form
    {
        ProjelerVTEntities entities = new ProjelerVTEntities();
        public UrunForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tumKayitleriGoster();
        }
        private void tumKayitleriGoster()
        {
            var Urunler = entities.Urun.ToList();
            dataGridView1.DataSource = Urunler;
            dataGridView1.ClearSelection();
            MetinKutulariniTemizle();
        }

        private void MetinKutulariniTemizle()
        {
           textBoxUrunAd.Clear();
           textBoxUrunFiyat.Clear();
           textBoxUrunID.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Urun urun = new Urun();
            urun.Ad = textBoxUrunAd.Text;
            urun.Fiyat = Convert.ToInt32(textBoxUrunFiyat.Text);
            try
            {
                entities.Urun.Add(urun);
                entities.SaveChanges();
                MessageBox.Show("New record has been added successfully.");
                tumKayitleriGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during database operations . ERROR CODE: E3108\n" + ex.Message);
            }
            MetinKutulariniTemizle();
        }

        private void UrunForm_Load(object sender, EventArgs e)
        {
            tumKayitleriGoster();
            textBoxUrunID.Text = "0";
        }

      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selection = dataGridView1.SelectedCells[0].RowIndex;
            textBoxUrunID.Text = dataGridView1.Rows[selection].Cells[0].Value.ToString();
            textBoxUrunAd.Text = dataGridView1.Rows[selection].Cells[1].Value.ToString();
            textBoxUrunFiyat.Text = dataGridView1.Rows[selection].Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int UrunId = Convert.ToInt32(textBoxUrunID.Text);
                var urun = entities.Urun.Find(UrunId);
                entities.Urun.Remove(urun);
                entities.SaveChanges();
                MessageBox.Show("Record has been deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during database operations or values cannot be empty . ERROR CODE: E0611\n" + ex.Message);
            }
            tumKayitleriGoster();
            MetinKutulariniTemizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
            int UrunId = Convert.ToInt32(textBoxUrunID.Text);
            var urun = entities.Urun.Find(UrunId);
            urun.Ad = textBoxUrunAd.Text;
            urun.Fiyat = Convert.ToInt32(textBoxUrunFiyat.Text);
            entities.SaveChanges();
            MessageBox.Show("Record has been updated successfully.");
            tumKayitleriGoster();
            }
            catch
            {
                MessageBox.Show("Error occurred during database operations or values cannot be empty . ERROR CODE: E0612\n");
            }

           
        }
    }
}
