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


            
            
            
        }
        private int dId;
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
                        dId = IdAktywa;
                        
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
                MessageBox.Show("Zły format danych");
                return;
            }
                
            var notowanie = new Notowanium()
            {
                Idaktywa=dId,
                CenaMax = cenaMax,
                CenaMin = cenaMin,
                DataIgodzina = dataGodzina,
                CenaOtwarcia = cenaOtwarcia,
                CenaZamkniecia = cenaZamkniecia
            };

            db.Notowania.Add(notowanie);
            db.SaveChanges();
        }
            
        
        
    }
}
