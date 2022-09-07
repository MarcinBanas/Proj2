using Proj2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy ZarządzajAktywamiForm.xaml
    /// </summary>
    public partial class ZarządzajAktywamiForm : Window
    {
        public ZarządzajAktywamiForm()
        {
            InitializeComponent();

            MyDbContext db = new MyDbContext();
           
            //var aktywa = from d in db.Aktywas select new {id=d.Idaktywa,nazwa=d.NazwaAktywa,kod=d.KodAktywa};
            this.GridAktywa.ItemsSource = db.Aktywas.ToList();
        }

        private void DodajAktywoButton_Click(object sender, RoutedEventArgs e)
        {
            MyDbContext db = new MyDbContext();
            var aktywo = new Aktywa()
            {
                NazwaAktywa = NazwaAktywaDodajTextBox.Text,
                KodAktywa = KodAktywaDodajTextBox.Text
            };
            db.Aktywas.Add(aktywo);
            db.SaveChanges();
            NazwaAktywaDodajTextBox.Clear();
            KodAktywaDodajTextBox.Clear();
            this.GridAktywa.ItemsSource = db.Aktywas.ToList();
        }

        private void ZaładujAktywaButton_Click(object sender, RoutedEventArgs e)
        {
            MyDbContext db = new MyDbContext();
            //var aktywa = from d in db.Aktywas select new { Id = d.Idaktywa, Nazwa = d.NazwaAktywa, Kod = d.KodAktywa };
            this.GridAktywa.ItemsSource = db.Aktywas.ToList();
        }

        private void UsunAktywoButton_Click(object sender, RoutedEventArgs e)
        {
            MyDbContext db = new MyDbContext();
            var r = from b in db.Aktywas
                    where b.Idaktywa == this.dID
                    select b;
            
           
            Aktywa obj = r.FirstOrDefault();
            if (obj != null)
            {
                db.Remove(obj);
                db.SaveChanges();
            }
            
            this.GridAktywa.ItemsSource = db.Aktywas.ToList();
            
        }
        private int dID;
        private void GridAktywa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.GridAktywa.SelectedIndex >= 0)
            {
                if (this.GridAktywa.SelectedItems.Count >= 0)
                {
                    Aktywa d = (Aktywa)this.GridAktywa.SelectedItems[0];
                    this.NazwaAktywaEdytujTextBox.Text = d.NazwaAktywa;
                    this.KodAktywaEdytujTextBox.Text = d.KodAktywa;
                    this.dID = d.Idaktywa;
                }
            }
        }

        private void EdytujAktywoButton_Click(object sender, RoutedEventArgs e)
        {
            MyDbContext db = new MyDbContext();
            var r = from b in db.Aktywas
                    where b.Idaktywa == this.dID
                    select b;

            Aktywa obj = r.SingleOrDefault();
            
            if(obj != null)
            {
                obj.NazwaAktywa = this.NazwaAktywaEdytujTextBox.Text;
                obj.KodAktywa = this.KodAktywaEdytujTextBox.Text;
            }
            db.SaveChanges();
            this.GridAktywa.ItemsSource = db.Aktywas.ToList();
            NazwaAktywaEdytujTextBox.Clear();
            KodAktywaEdytujTextBox.Clear();
        }
    }
}
