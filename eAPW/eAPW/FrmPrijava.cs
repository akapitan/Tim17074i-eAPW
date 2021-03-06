﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace eAPW
{
    /// <summary>
    /// Forma za prijavu
    /// </summary>
    public partial class FrmPrijava : Form
    {
        /// <summary>
        /// Konstruktor forme za prijavu
        /// </summary>
        public FrmPrijava()
        {
            InitializeComponent();
            txtLozinka.PasswordChar = '#';
            txtLokacija.Text = ConfigurationManager.AppSettings["LokacijaNaziv"];
            
        }
        /// <summary>
        /// Funkcija za provjeru korisničkih podataka za prijavu
        /// </summary>
        private void ProvjeriZaposlenika()
        {
            using (var db = new ProgramskoInzenjerstvoDBEntities())
            {
                var korisnik = (from z in db.Zaposleniks where z.korisnickoIme == txtKorisnicko.Text select z).SingleOrDefault();


                if (korisnik == null)
                {
                    MessageBox.Show("Krivo korisničko ime");
                }
                else
                {

                    var jeAdmin = korisnik.Zaposlenik_has_Tip.Where(x => x.id_tip == 1).SingleOrDefault();
                    

                    if (korisnik.Lokacija.id == int.Parse(ConfigurationManager.AppSettings["LokacijaID"]) || jeAdmin != null)
                    {
                        if (korisnik.lozinka.Trim() == txtLozinka.Text)
                        {
                            MessageBox.Show("Uspješna prijava");
                            FrmGlavna MDI = new FrmGlavna(korisnik);
                            MDI.Show();
                            this.Hide();
                        }
                        else MessageBox.Show("Pokušaj ponovo");
                    }
                    else
                    {
                        MessageBox.Show("Niste prijavljeni na odabrano radno mjesto ili nemate administarsu ulogu");
                    }
                    
                }
            }
        }

        /// <summary>
        /// Pokretanje projere korisničkih podataka klikom na gumb prijava.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrijava_Click(object sender, EventArgs e)
        {
            ProvjeriZaposlenika();
        }

        /// <summary>
        /// Pokretanje projere korisničkih podataka klikom na gumb "prijava"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ProvjeriZaposlenika();

            this.Close();
        }
        
        /// <summary>
        /// Izlaz iz aplikacije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrijavaOdustani_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Provjera korisničkih podataka klikom na enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtKorisnicko_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProvjeriZaposlenika();
            }
        }

        /// <summary>
        /// Provjera korisničkih podataka klikom na gumb enter kad je fokusiran na txtLozinka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLozinka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProvjeriZaposlenika();
            }
        }

        /// <summary>
        /// Izlaz iz aplikacije klikom gumba "odustani".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrijavaOdustani_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// pozivanje help dokumenta klikom na F1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="hlpevent"></param>
        private void FrmPrijava_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Help.ShowHelp(this,"Help.chm", HelpNavigator.Topic, "Prijava.htm");
        }
    }
}
