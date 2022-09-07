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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proj2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            MyDbContext db = new MyDbContext();
            InitializeComponent();
            var query = (from k in db.Aktywas
                        join d  in db.Notowania on k.Idaktywa equals d.Idaktywa
                        select new 
                        {
                            ID=k.Idaktywa,
                            NazwaAktywa=k.NazwaAktywa,
                            AktualnaCena=d.CenaMax,
                            DataNotowania=d.DataIgodzina
                        }
                        into ss
                        group ss by ss.ID into g
                        select g.OrderByDescending(r => r.DataNotowania).FirstOrDefault()
                        ).ToList();
       
            this.DataGridMain.ItemsSource = query;
            
        }

        

        private void ZarządzajAktywamiOpenWindow(object sender, RoutedEventArgs e)
        {
            ZarządzajAktywamiForm dodajAktywoFormularz = new ZarządzajAktywamiForm();
            dodajAktywoFormularz.Show();
        }

        private void DodajNotowanieOpenWindow(object sender, RoutedEventArgs e)
        {
            DodajNotowanie dodajNotowanieForm = new DodajNotowanie();
            dodajNotowanieForm.Show();
        }
    }
}
