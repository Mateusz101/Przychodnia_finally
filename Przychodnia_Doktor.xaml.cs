using PrzychodniaGIT;
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


namespace Przychodnia_finally
{
    /// <summary>
    /// Interaction logic for Przychodnia.xaml
    /// </summary>
    public partial class Przychodnia_Doktor : Window
    {
        Placówka p;
        Lekarz ZalogowanyLekarz = new();

        public Przychodnia_Doktor()
        {
            p = new();
            InitializeComponent();
            WidocznoscAppointments(true);
            WidocznoscWydaniaRecepty(false);
            WidocznoscHistorii(false);
            WidocznoscProfilu(false);
        }

        public Przychodnia_Doktor(Placówka placowka, string login) : this()
        {
            p = placowka;
            ZalogowanyLekarz = placowka.Lekarze.Find(p => p.Pesel == login);

        }
        public Przychodnia_Doktor(Placówka placowka, string login, string haslo, string typ) : this(placowka, login)
        {
            WidocznoscAppointments(false);
            WidocznoscWydaniaRecepty(false);
            WidocznoscHistorii(false);
            WidocznoscProfilu(true);
            Imie.Text = ZalogowanyLekarz.Imie;
            Nazwisko.Text = ZalogowanyLekarz.Nazwisko;
            Pesel.Text = ZalogowanyLekarz.Pesel;
            Data_Urodzenia.Text = ZalogowanyLekarz.DataUrodzenia.ToString("dd-MM-yyyy");
            if (ZalogowanyLekarz.Plec == EnumPlec.M)
            {
                Plec.Text = "Man";
                Zdjecieplec.Source = new BitmapImage(new Uri(@"/doctors.png", UriKind.Relative));
            }
            else
            {
                Plec.Text = "Woman";
            }
        }


        private void wylogowanie_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objSecondWindow = new MainWindow(p);
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();
        }
        
        //MENU (lewa strona)
        private void BtnWizytyMENU_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscWydaniaRecepty(false);
            WidocznoscHistorii(false);
            WidocznoscProfilu(false);
            WidocznoscAppointments(true);
            WizytyLekarza_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizytyDanegoLekarza(ZalogowanyLekarz.Pesel));
        }

        private void BtnWydajRecepteMENU_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscProfilu(false);
            WidocznoscHistorii(false);
            WidocznoscAppointments(false);
            WidocznoscWydaniaRecepty(true);
            Pacjenci_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizytyDanegoLekarza(ZalogowanyLekarz.Pesel));
            
        }


        private void BtnInformacjeMENU_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscWydaniaRecepty(false);
            WidocznoscHistorii(false);
            WidocznoscProfilu(true);
            WidocznoscAppointments(false);
            Imie.Text = ZalogowanyLekarz.Imie;
            Nazwisko.Text = ZalogowanyLekarz.Nazwisko;
            Pesel.Text = ZalogowanyLekarz.Pesel;
            Data_Urodzenia.Text = ZalogowanyLekarz.DataUrodzenia.ToString("dd-MM-yyyy");
            if (ZalogowanyLekarz.Plec == EnumPlec.M)
            {
                Plec.Text = "Man";
                Zdjecieplec.Source = new BitmapImage(new Uri(@"/doctors.png", UriKind.Relative));
            }
            else
            {
                Plec.Text = "Woman";
            }
        }

        private void BtnHistoriaPacjentowMENU_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscAppointments(false);
            WidocznoscWydaniaRecepty(false);
            WidocznoscHistorii(true);
            WidocznoscProfilu(false);

            if (ZalogowanyLekarz != null)
            {
                WidocznoscAppointments(false);
                WidocznoscWydaniaRecepty(false);

                WszyscyPacjenci_ListBox.ItemsSource = new ObservableCollection<Pacjent>(p.Pacjenci);

            }
        }

        private void WidocznoscAppointments(bool visibility)
        {
            if (!visibility)
            {
                BtnWszystkieWizyty.Visibility = Visibility.Hidden;
                BtnWizytaWDniu.Visibility = Visibility.Hidden;
                BtnSortuj_Wizyty.Visibility = Visibility.Hidden;
                BtnUsun_Wizyte.Visibility = Visibility.Hidden;
                WizytyLekarza_ListBox.Visibility = Visibility.Hidden;
                calendar.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnWszystkieWizyty.Visibility = Visibility.Visible;
                BtnWizytaWDniu.Visibility = Visibility.Visible;
                BtnSortuj_Wizyty.Visibility = Visibility.Visible;
                BtnUsun_Wizyte.Visibility = Visibility.Visible;
                WizytyLekarza_ListBox.Visibility = Visibility.Visible;
                calendar.Visibility = Visibility.Visible;
            }
        }

        private void WidocznoscWydaniaRecepty(bool visibility)
        {
            if (!visibility)
            { 
                BtnSzukajPesel.Visibility = Visibility.Hidden;
                BtnZaakceptujDiagnoze.Visibility = Visibility.Hidden;
                LblChoroba.Visibility = Visibility.Hidden;
                LblPESEL.Visibility = Visibility.Hidden;
                LblRecepta.Visibility = Visibility.Hidden;
                Pacjenci_ListBox.Visibility= Visibility.Hidden;
                TxtBoxChoroba.Visibility = Visibility.Hidden;
                TxtBoxPesel.Visibility = Visibility.Hidden;
                TxtBoxRecepta.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnSzukajPesel.Visibility = Visibility.Visible;
                BtnZaakceptujDiagnoze.Visibility= Visibility.Visible;
                LblRecepta.Visibility = Visibility.Visible;
                LblPESEL.Visibility = Visibility.Visible;
                LblChoroba.Visibility= Visibility.Visible;
                Pacjenci_ListBox.Visibility= Visibility.Visible;
                TxtBoxRecepta.Visibility= Visibility.Visible;
                TxtBoxPesel.Visibility= Visibility.Visible; 
                TxtBoxChoroba.Visibility = Visibility.Visible;
            }
        }

        private void WidocznoscProfilu(bool visibility)
        {
            if (!visibility) 
            {
                pola.Visibility = Visibility.Hidden;
                pola1.Visibility = Visibility.Hidden;
                pola2.Visibility = Visibility.Hidden;
                pola3.Visibility = Visibility.Hidden;
                pola4.Visibility = Visibility.Hidden;
                pola5.Visibility = Visibility.Hidden;
                Imie.Visibility = Visibility.Hidden;
                Nazwisko.Visibility = Visibility.Hidden;
                Pesel.Visibility = Visibility.Hidden;
                Data_Urodzenia.Visibility = Visibility.Hidden;
                Plec.Visibility = Visibility.Hidden;
                Zdjecieplec.Visibility = Visibility.Hidden;
                moj_Profil.Visibility = Visibility.Hidden;
                Change_Button.Visibility = Visibility.Hidden;
                PasswordHidden.Visibility = Visibility.Hidden;
            }
            else
            {
                pola.Visibility= Visibility.Visible;
                pola1.Visibility= Visibility.Visible;
                pola2.Visibility= Visibility.Visible;
                pola3.Visibility= Visibility.Visible;
                pola4.Visibility= Visibility.Visible;
                pola5.Visibility= Visibility.Visible;
                Imie.Visibility = Visibility.Visible;
                Nazwisko.Visibility = Visibility.Visible;
                Pesel.Visibility = Visibility.Visible;
                Data_Urodzenia.Visibility= Visibility.Visible;
                Plec.Visibility= Visibility.Visible;
                Zdjecieplec.Visibility = Visibility.Visible;
                moj_Profil.Visibility= Visibility.Visible;
                Change_Button.Visibility= Visibility.Visible;
                PasswordHidden.Visibility= Visibility.Visible;
            }
        }

        private void WidocznoscHistorii(bool visibility)
        {
            if (visibility) 
            {
                BtnPeselHistoria.Visibility= Visibility.Visible;
                LblPacjenci.Visibility= Visibility.Visible;
                TxtBoxSzukajPeselHistoria.Visibility = Visibility.Visible;
                WszyscyPacjenci_ListBox.Visibility = Visibility.Visible;
                Historia_ListBox.Visibility= Visibility.Visible;
            }
            else
            {
                BtnPeselHistoria.Visibility = Visibility.Hidden;
                LblPacjenci.Visibility = Visibility.Hidden;
                TxtBoxSzukajPeselHistoria.Visibility = Visibility.Hidden;
                WszyscyPacjenci_ListBox.Visibility = Visibility.Hidden;
                Historia_ListBox.Visibility = Visibility.Hidden;
            }

        }

        //Appointments
        private void BtnSortujWizyty_Click(object sender, RoutedEventArgs e)
        {

            if (ZalogowanyLekarz != null)
            {
                p.SortujWizyta();
                WizytyLekarza_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizytyDanegoLekarza(ZalogowanyLekarz.Pesel));
            }

        }

        private void BtnUsun_Wizyte_Click(object sender, RoutedEventArgs e)
        {
            if (WizytyLekarza_ListBox.SelectedIndex > -1)
            {
                Wizyta w = WizytyLekarza_ListBox.SelectedItem as Wizyta;
                p.AnulujWizyteJakoLekarz(w.Pacjent.Pesel, w.Data, w.Godzina);

                ZalogowanyLekarz.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(w.Data, w.Godzina));

                WizytyLekarza_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizytyDanegoLekarza(ZalogowanyLekarz.Pesel));
            }
        }

        private void BtnWizytaWDniu_Click(object sender, RoutedEventArgs e)
        {
            if(calendar.SelectedDate.HasValue)
            {
                DateTime data = calendar.SelectedDate.Value.Date;
                WizytyLekarza_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.LekarzWDanymDniu(ZalogowanyLekarz.Pesel, data));
            }
        }

        private void BtnWszystkieWizyty_Click(object sender, RoutedEventArgs e)
        {
            WizytyLekarza_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizytyDanegoLekarza(ZalogowanyLekarz.Pesel));
        }
        
        //Prescription
        private void BtnSzukajPesel_Click(object sender, RoutedEventArgs e)
        {
            if (TxtBoxPesel.Text.Length > 0)
            {
                Pacjenci_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizytyDanejOsobyUDanegoLekarza(ZalogowanyLekarz.Pesel, TxtBoxPesel.Text.ToString()));
            }
        }

        private void BtnZaakceptujDiagnoze_Click(object sender, RoutedEventArgs e)
        {
            if(Pacjenci_ListBox.SelectedIndex > -1 && TxtBoxRecepta.Text.Length > 0 && TxtBoxRecepta.Text.Length > 0) 
            {
        
                p.ZakonczWizyte(new(Pacjenci_ListBox.SelectedItem as Wizyta, TxtBoxChoroba.Text.ToString(), TxtBoxRecepta.Text.ToString()));
                
                MessageBox.Show("Confirmed.");


                WidocznoscWydaniaRecepty(false);
                WizytyLekarza_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizytyDanegoLekarza(ZalogowanyLekarz.Pesel));
                WidocznoscAppointments(true);
            }
            else
            {
                MessageBox.Show("Something is missing");
            }
        }

        //History
        private void BtnPeselHistoria_Click(object sender, RoutedEventArgs e)
        {
            Pacjent pacjent = new();

            if(WszyscyPacjenci_ListBox.SelectedIndex > -1)
            {
                pacjent = WszyscyPacjenci_ListBox.SelectedItem as Pacjent;
                if (pacjent.HistoriaWizyt.Count == 0)
                {
                    Historia_ListBox.ItemsSource = null;
                }
                else
                {
                    Historia_ListBox.ItemsSource = new ObservableCollection<Diagnoza>(pacjent.HistoriaWizyt);
                }
            }

            if(p.Pacjenci.Find(p=>p.Pesel==TxtBoxSzukajPeselHistoria.Text.ToString())!=null)
            {
                pacjent = p.Pacjenci.Find(p => p.Pesel == TxtBoxSzukajPeselHistoria.Text.ToString());
                if(pacjent.HistoriaWizyt.Count == 0)
                {
                    Historia_ListBox.ItemsSource = null;
                }
                else
                {
                    Historia_ListBox.ItemsSource = new ObservableCollection<Diagnoza>(pacjent.HistoriaWizyt);
                }
            }
        }
        
        //Information
        private void Change_Button_Click(object sender, RoutedEventArgs e)
        {
            Zmiana_Hasla objSecondWindow = new Zmiana_Hasla(p, ZalogowanyLekarz);
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();
        }
    }
}