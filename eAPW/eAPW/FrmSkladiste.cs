﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace eAPW
{
    public partial class FrmSkladiste : Form
    {
        public FrmSkladiste()
        {
            InitializeComponent();
            ispisDatagrid();
        }

        private void ispisDatagrid()
        {
            using (var db = new ProgramskoInzenjerstvoDBEntities())
            {
                var data = (from x in db.Djelovis
                            select new
                            {
                                ID = x.id,
                                Naziv = x.naziv,
                                Kategorija = x.Kategorija1.naziv,
                                ModelVozila = x.Model_vozila.naziv
                            
                            }).ToList();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = data;
                
            }
        }
        private void ispisDatagrid2(string pretraga)
        {
            using (var db = new ProgramskoInzenjerstvoDBEntities())
            {
                var data = (from x in db.Djelovis where x.naziv.Contains(pretraga)
                            select new
                            {
                                ID = x.id,
                                Naziv = x.naziv,
                                Kategorija = x.Kategorija1.naziv,
                                ModelVozila = x.Model_vozila.naziv

                            }).ToList();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = data;




            }
        }

        private void ispisDatagridNaSkladistu()
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int idValue = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());

            using (var db = new ProgramskoInzenjerstvoDBEntities())
            {
                var data = (from x in db.Lokacija_has_djelovi
                            where x.id_djelovi == idValue
                            select new
                            {
                                Lokacija = x.Lokacija.naziv,
                                Kolicina = x.kolicina
                            }).ToList();
                dgvDjeloviNaSkladistima.DataSource = data;

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ispisDatagridNaSkladistu();
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPretraga_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void dgvDjeloviNaSkladistima_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dgvDjeloviNaSkladistima.CurrentCell.RowIndex;
                int idValue = int.Parse(dgvDjeloviNaSkladistima.Rows[rowIndex].Cells[1].Value.ToString());

                txtPromjeniNaSkladistu.Text = idValue.ToString();
            }
            catch (Exception)
            {

                
            }
            
        }

        private void btnPromjeniNaSkladistu_Click(object sender, EventArgs e)
        {
            
            int idValueDjelovi = int.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
            string idValueLokacija = (dgvDjeloviNaSkladistima.Rows[dgvDjeloviNaSkladistima.CurrentCell.RowIndex].Cells[0].Value.ToString());
            
            using (var db = new ProgramskoInzenjerstvoDBEntities())
            {
                var promjena = (from x in db.Lokacija_has_djelovi where idValueDjelovi == x.id_djelovi && idValueLokacija == x.Lokacija.naziv select x).SingleOrDefault();
                promjena.kolicina = int.Parse(txtPromjeniNaSkladistu.Text);
                db.SaveChanges();
            }
            ispisDatagridNaSkladistu();
        }

        public void pripremiMail(string primateljEmail, List<Djelovi> listaDjelova)
        {
            MailMessage mail = new MailMessage("akapitan@foi.hr", primateljEmail);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "mail.foi.hr";
            client.Credentials = new System.Net.NetworkCredential("akapitan", "foi-OranGisi13");
            mail.Subject = "Stigli su Vaši naručeni proizvodi";
            string mailBody = "Sljedeći proizvodi su ponovo u našoj ponudi : \n";
            foreach (Djelovi dio in listaDjelova)
            {
                mailBody += "   -" + dio.naziv + "\n";
            }
            mailBody += " \n Bokili";
            mail.Body = mailBody;
            client.Send(mail);
        }

        public void provjeriRezervacije()
        {
            using (var db = new ProgramskoInzenjerstvoDBEntities())
            {
                foreach (Rezervacija rez in db.Rezervacijas.Where(x => x.izvrseno == false))
                {
                    List<Djelovi> listaDjelovaRezervacija = new List<Djelovi>();

                    foreach (Rezervacija_has_Djelovi rhd in rez.Rezervacija_has_Djelovi)
                    {
                        Djelovi dio = db.Djelovis.Where(x => x.id == rhd.int_djelovi).Single();
                        
                    }
                    if (listaDjelovaRezervacija.Count > 0 && listaDjelovaRezervacija.Count == rez.Rezervacija_has_Djelovi.Count)
                    {
                        pripremiMail(rez.kupacEmail, listaDjelovaRezervacija);
                        Rezervacija rezervacija = db.Rezervacijas.Single(x => x.id == rez.id);
                        rezervacija.izvrseno = true;


                    }
                }
                db.SaveChanges();
            }
        }
    }


}