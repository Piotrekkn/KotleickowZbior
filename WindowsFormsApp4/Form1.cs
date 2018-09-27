using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace bazadanychcalosc
{
    public partial class Form1 : Form
    {
        #region Zmienne

        MySqlConnection polaczenie = new MySqlConnection("server=localhost; user=root; database=bazadanych");
        MySqlCommand komenda;
        MySqlDataReader czytnik;
        string zapytanie = "";
        List<Hopek> lista = new List<Hopek>();
       Hopek currHopek = null;

       



        public Form1()
        {
            InitializeComponent();
        }

    

        private void Form1_Load(object sender, EventArgs e)
        {
            InicjalizacjaDanych();
        }

        private void lista_lisb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lista_lisb.SelectedIndex == -1)
                {
                    name_texb.Text = "";
                    age_numUD.Value = 0;
                    currHopek = null;
                }
                else
                {
                    string[] tab = lista_lisb.Text.Split('.');
                    Hopek a = lista.FirstOrDefault(x => x.Name.Equals(tab[1].Trim()));
                    currHopek = a;

                    if (a != null)
                    {
                        name_texb.Text = a.Name;
                        age_numUD.Value = a.Age;
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("problem", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void InicjalizacjaDanych()
        {
            try
            {
                if (polaczenie.State == ConnectionState.Closed)
                    polaczenie.Open();

                zapytanie = "select * from users";
                komenda = new MySqlCommand(zapytanie, polaczenie);
                czytnik = komenda.ExecuteReader();

                lista.Clear();
                lista_lisb.Items.Clear();
                int licznik = 1;

                if (czytnik.HasRows)
                {
                    while (czytnik.Read())
                    {
                        lista.Add(new Hopek(czytnik.GetInt32("id"), czytnik["name"].ToString(), czytnik.GetInt32("age")));
                        lista_lisb.Items.Add(string.Format("{0}. {1}", licznik++, czytnik["name"].ToString()));
                    }
                }

            }
            catch (Exception ex)
            {
                string byk = string.Format("Problem podczas inicjalizacji danych:\n{0}", ex.Message);
                MessageBox.Show(byk, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                polaczenie.Close();

                if (czytnik != null)
                {
                    czytnik.Dispose();
                    czytnik = null;
                }
            }
        }

        void WyczyscListe()
        {
            lista.Clear();
            lista_lisb.SelectedIndex = -1;
            lista_lisb.Items.Clear();
        }
        #endregion Ogólne

        private void pobierz_btn_Click(object sender, EventArgs e)
        {
            InicjalizacjaDanych();
        }

        private void aktualizuj_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int IndeksWybranejPostaci = lista_lisb.SelectedIndex;
                if (currHopek != null)
                {
                    if (polaczenie.State == ConnectionState.Closed)
                        polaczenie.Open();

                    zapytanie = string.Format("update users set name = '{0}', age = {1} where id = {2}", name_texb.Text, age_numUD.Value, currHopek.Id);
                    komenda = new MySqlCommand(zapytanie, polaczenie);
                    komenda.ExecuteNonQuery();

                    MessageBox.Show("Hopek updated.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InicjalizacjaDanych();
                    lista_lisb.SelectedIndex = IndeksWybranejPostaci;
                }
                else
                {
                    MessageBox.Show("Kogo?.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string byk = string.Format("", ex.Message);
                MessageBox.Show(byk, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void wyczysc_btn_Click(object sender, EventArgs e)
        {
            WyczyscListe();
        }

        private void wyjscie_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void dodaj_btn_Click(object sender, EventArgs e)
        {
            try
            {
                //int IndeksWybranejPostaci = lista_lisb.SelectedIndex;
                //if (currHopek == null)
                //{
                if (polaczenie.State == ConnectionState.Closed)
                    polaczenie.Open();

                zapytanie = string.Format("insert into users(name, age) values ('{0}', {1})", name_texb.Text, age_numUD.Value);
                komenda = new MySqlCommand(zapytanie, polaczenie);
                komenda.ExecuteNonQuery();

                MessageBox.Show("Postać dodana.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InicjalizacjaDanych();
                //    lista_lisb.SelectedIndex = IndeksWybranejPostaci;
                //}
                //else
                //{
                //    MessageBox.Show("Wybierz postać którą chcesz zaktualizować.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                string byk = string.Format("", ex.Message);
                MessageBox.Show(byk, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usun_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (lista_lisb.SelectedIndex != -1)
                {
                    if (polaczenie.State == ConnectionState.Closed)
                        polaczenie.Open();


                    int IndeksWybranejPostaci = lista_lisb.SelectedIndex;
                    if (currHopek != null)
                    {
                        zapytanie = string.Format("delete from users where id = {0}", currHopek.Id);
                        komenda = new MySqlCommand(zapytanie, polaczenie);
                        komenda.ExecuteNonQuery();

                        MessageBox.Show("Postać usunięta.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InicjalizacjaDanych();

                    }
                }
                else
                {
                    MessageBox.Show("Kogo usunac?", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Nie dziala", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                polaczenie.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}