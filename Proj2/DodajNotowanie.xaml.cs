using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proj2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proj2
{
    /// <summary>
    /// Logika interakcji dla klasy DodajNotowanie.xaml
    /// </summary>
    public partial class DodajNotowanie : Window
    {
        public DodajNotowanie()
        {
            InitializeComponent();

            MyDbContext db = new MyDbContext();
            this.GridNotowania.ItemsSource = db.Notowania.ToList();

            List<string> NazwaAktywa=new List<string>();
            foreach (var item in db.Aktywas.ToList())
            {
                NazwaAktywa.Add(item.NazwaAktywa);
            }
            
            this.AktywaListView.ItemsSource = NazwaAktywa;

            this.DataContext = new MainViewModel();



        }
        private int aktywoID;
        private int notowanieID;
        private void AktywaListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                if (this.AktywaListView.SelectedIndex >= 0)
                {
                    if (this.AktywaListView.SelectedItems.Count >= 0)
                    {
                        MyDbContext db = new MyDbContext();
                        string d = (string)this.AktywaListView.SelectedItems[0];
                        var id = from b in db.Aktywas
                                 where b.NazwaAktywa == d
                                 select b;
                        Aktywa obj = id.SingleOrDefault();
                        int IdAktywa = obj.Idaktywa;
                        aktywoID = IdAktywa;
                        
                        this.GridNotowania.ItemsSource = db.Notowania.Where(a => a.Idaktywa == IdAktywa).ToList();
                    }
                }
            }
        }

        private void DodajNotowanieButton_Click(object sender, RoutedEventArgs e)
        {
            decimal cenaMax,cenaMin,cenaOtwarcia,cenaZamkniecia;
            DateTime dataGodzina;
            MyDbContext db = new MyDbContext();
            if (Decimal.TryParse(CenaMaxTextBoxDodaj.Text, out cenaMax) && Decimal.TryParse(CenaMinTextBoxDodaj.Text, out cenaMin)
                && DateTime.TryParse(DataGodzinaTextBoxDodaj.Text, out dataGodzina) && Decimal.TryParse(CenaOtwarciaTextBoxDodaj.Text, out cenaOtwarcia)
                && Decimal.TryParse(CenaZamknieciaTextBoxDodaj.Text, out cenaZamkniecia)) { }
            else
            {
                MessageBox.Show("Zły format danych.");
                return;
            }
            if (this.AktywaListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Wybierz aktywo.");
                return;
            }
                
            var notowanie = new Notowanium()
            {
                Idaktywa=aktywoID,
                CenaMax = cenaMax,
                CenaMin = cenaMin,
                DataIgodzina = dataGodzina,
                CenaOtwarcia = cenaOtwarcia,
                CenaZamkniecia = cenaZamkniecia
            };

            db.Notowania.Add(notowanie);
            db.SaveChanges();
            this.GridNotowania.ItemsSource = db.Notowania.Where(a => a.Idaktywa == aktywoID).ToList();

            CenaOtwarciaTextBoxDodaj.Clear();
            CenaZamknieciaTextBoxDodaj.Clear();
            DataGodzinaTextBoxDodaj.Clear();
            CenaMinTextBoxDodaj.Clear();
            CenaMaxTextBoxDodaj.Clear();
            MessageBox.Show("Dodano notowanie.");
        }

        private void GridNotowania_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.GridNotowania.SelectedIndex >= 0)
            {
                if (this.GridNotowania.SelectedItems.Count >= 0)
                {
                    Notowanium d = (Notowanium)this.GridNotowania.SelectedItems[0];
                    this.CenaOtwarciaTextBoxEdytuj.Text = Convert.ToString(d.CenaOtwarcia);
                    this.CenaZamknieciaTextBoxEdytuj.Text = Convert.ToString(d.CenaZamkniecia);
                    this.DataGodzinaTextBoxEdytuj.Text = Convert.ToString(d.DataIgodzina);
                    this.CenaMinTextBoxEdytuj.Text = Convert.ToString(d.CenaMin);
                    this.CenaMaxTextBoxEdytuj.Text = Convert.ToString(d.CenaMax);
                    this.notowanieID = d.Idnotowania;
                }
            }
        }

        private void EdytujNotowanieButton_Click(object sender, RoutedEventArgs e)
        {
            decimal cenaMax, cenaMin, cenaOtwarcia, cenaZamkniecia;
            DateTime dataGodzina;
            MyDbContext db = new MyDbContext();
            if (Decimal.TryParse(CenaMaxTextBoxEdytuj.Text, out cenaMax) && Decimal.TryParse(CenaMinTextBoxEdytuj.Text, out cenaMin)
                && DateTime.TryParse(DataGodzinaTextBoxEdytuj.Text, out dataGodzina) && Decimal.TryParse(CenaOtwarciaTextBoxEdytuj.Text, out cenaOtwarcia)
                && Decimal.TryParse(CenaZamknieciaTextBoxEdytuj.Text, out cenaZamkniecia)) { }
            else
            {
                MessageBox.Show("Zły format danych.");
                return;
            }
            if (this.AktywaListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Wybierz aktywo.");
                return;
            }
            var r = from b in db.Notowania
                    where b.Idaktywa == this.aktywoID
                    select b;

            Notowanium obj = r.FirstOrDefault();

            if (obj != null)
            {
                obj.CenaOtwarcia = cenaOtwarcia;
                obj.CenaZamkniecia = cenaZamkniecia;
                obj.DataIgodzina =dataGodzina;
                obj.CenaMin = cenaMin;
                obj.CenaMax = cenaMax;
            }
            db.SaveChanges();
            this.GridNotowania.ItemsSource = db.Notowania.Where(a => a.Idaktywa == aktywoID).ToList();

            CenaOtwarciaTextBoxEdytuj.Clear();
            CenaZamknieciaTextBoxEdytuj.Clear();
            DataGodzinaTextBoxEdytuj.Clear();
            CenaMinTextBoxEdytuj.Clear();
            CenaMinTextBoxEdytuj.Clear();
        }

        private void UsunNotowanieButton_Click(object sender, RoutedEventArgs e)
        {
            MyDbContext db = new MyDbContext();
            var r = from b in db.Notowania
                    where b.Idnotowania == this.notowanieID
                    select b;

            Notowanium obj = r.SingleOrDefault();
            if (obj != null)
            {
                db.Remove(obj);
                db.SaveChanges();
            }

            this.GridNotowania.ItemsSource = db.Notowania.Where(a => a.Idaktywa == aktywoID).ToList();

        }
    }
}
