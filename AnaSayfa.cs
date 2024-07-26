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
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UrunTopSayi urunForm = new UrunTopSayi();
            urunForm.Show();
          //  this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusteriForm musteriForm = new MusteriForm();
            musteriForm.Show();
            //this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SiparisForm siparisForm = new SiparisForm();
            siparisForm.Show();
            //this.Hide();
        }
    }
}
