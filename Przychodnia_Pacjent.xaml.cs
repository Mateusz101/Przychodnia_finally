using PrzychodniaGIT;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System;
using System.Linq;

namespace Przychodnia_finally
{
    /// <summary>
    /// Interaction logic for Przychodnia.xaml
    /// </summary>
    public partial class Przychodnia_Pacjent : Window
    {
        Placowka p;
        Pacjent ZalogowanyPacjent = new();

        public Przychodnia_Pacjent()
        {
            p = new();
            InitializeComponent();
            WidocznoscVisits(false);
            WidocznoscInformations(false);
            WidocznoscVisit(true);
            WidocznoscHistory(false);
            Lista.ItemsSource = new ObservableCollection<Wizyta>(p.WizytyPacjenta(ZalogowanyPacjent.Pesel));
        }

        public Przychodnia_Pacjent(Placowka placowka, string login) : this()
        {
            ZalogowanyPacjent = placowka.Pacjenci.Find(p => p.Pesel == login);
            p = placowka;
            WidocznoscVisit(true);
        }
        public Przychodnia_Pacjent(Placowka placowka, string login, string haslo, string typ) : this(placowka, login)
        {
            WidocznoscVisit(false);
            WidocznoscInformations(true);
            WidocznoscVisits(false);
            WidocznoscHistory(false);

        }

        private void Add_visit_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscVisit(true);
            WidocznoscInformations(false);
            WidocznoscVisits(false);
            WidocznoscHistory(false);

        }
        private void Visits_history_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscInformations(false);
            WidocznoscVisits(true);
            WidocznoscVisit(false);
            WidocznoscHistory(false);
        }
        private void Patient_History_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscInformations(false);
            WidocznoscVisits(false);
            WidocznoscVisit(false);
            WidocznoscHistory(true);
            history_ListBox.ItemsSource = new ObservableCollection<Diagnoza>(ZalogowanyPacjent.HistoriaWizyt);
        }

        private void Informations_Button_Click_2(object sender, RoutedEventArgs e)
        {
            WidocznoscVisits(false);
            WidocznoscInformations(true);
            WidocznoscVisit(false);
            WidocznoscHistory(false);
        }



        private void wylogowanie_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objSecondWindow = new MainWindow(p);
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();
        }

        //Zmiana hasła
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Zmiana_Hasla objSecondWindow = new Zmiana_Hasla(p, ZalogowanyPacjent);
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();
        }

        private void BtnSortujWizyty_Click(object sender, RoutedEventArgs e)
        {
            if (ZalogowanyPacjent != null)
            {
                p.SortujWizyta();
                Lista.ItemsSource = new ObservableCollection<Wizyta>(p.WizytyPacjenta(ZalogowanyPacjent.Pesel));
            }
        }
        private void BtnUsun_Wizyte_Click(object sender, RoutedEventArgs e)
        {
            if (Lista.SelectedIndex > -1)
            {
                Wizyta w = Lista.SelectedItem as Wizyta;
                p.AnulujWizytePacjent(w.Pacjent.Pesel, w.Data, w.Godzina);
                Lista.ItemsSource = new ObservableCollection<Wizyta>(p.WizytyPacjenta(ZalogowanyPacjent.Pesel));

                Lekarz l = p.Lekarze.Find(doc => doc.Pesel == w.Lekarz.Pesel);
                l.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(w.Data, w.Godzina));
            }
        }
        private void BtnWszystkieWizyty_Click(object sender, RoutedEventArgs e)
        {
            Lista.ItemsSource = new ObservableCollection<Wizyta>(p.WizytyPacjenta(ZalogowanyPacjent.Pesel));
        }
        private void BtnSzukajLekarz_Click(object sender, RoutedEventArgs e)
        {
            if (Lekarze.SelectedItem != null)
            {
                List<Tuple<string, string>> imie_Nazwisko = new();
                p.Lekarze.ForEach(l => imie_Nazwisko.Add(new Tuple<string, string>(l.Imie, l.Nazwisko)));
                Lista.ItemsSource = new ObservableCollection<Wizyta>(p.WizytyPacjenta(ZalogowanyPacjent.Pesel).FindAll(w => w.Lekarz.Imie == p.Lekarze[Lekarze.SelectedIndex].Imie && w.Lekarz.Nazwisko == p.Lekarze[Lekarze.SelectedIndex].Nazwisko));
            }
        }
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            List<TimeSpan> godziny = new()
            {
                new TimeSpan(8,0,0),
                new TimeSpan(8,30,0),
                new TimeSpan(9,0,0),
                new TimeSpan(9,30,0),
                new TimeSpan(10,0,0),
                new TimeSpan(10,30,0),
                new TimeSpan(11,0,0),
                new TimeSpan(11,30,0),
                new TimeSpan(12,0,0),
                new TimeSpan(12,30,0),
                new TimeSpan(13,0,0),
                new TimeSpan(13,30,0),
                new TimeSpan(14,0,0),
                new TimeSpan(14,30,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0)
            };
            Lekarz lekarz = new();
            lekarz = p.Lekarze.Find(w => w.Imie == p.Lekarze[wybor_Lekarza.SelectedIndex].Imie && w.Nazwisko == p.Lekarze[wybor_Lekarza.SelectedIndex].Nazwisko);

            if (calendar.SelectedDate != null)
            {
                DateTime data = calendar.SelectedDate.Value.Date;
                IEnumerable<Tuple<DateTime, TimeSpan>> krotki = lekarz.Zaplanowane_Wizyty.Where(kvp => kvp.Value == true).Select(kvp => kvp.Key);
                IEnumerable<TimeSpan> terminy = krotki.Where(kvp => kvp.Item1 == data).Select(kvp => kvp.Item2);
                terminy.ToList().ForEach(e => godziny.Remove(e));

                if (lekarz.GodzinyPracy.ContainsKey(data.DayOfWeek))
                {
                    wybor_terminu.IsEnabled = true;
                    wybor_terminu.SelectedIndex = 0;
                    Tuple<TimeSpan, TimeSpan> godzinyPrzyjec = lekarz.GodzinyPracy[data.DayOfWeek];
                    List<TimeSpan> niepracujace = new();
                    niepracujace = godziny.FindAll(g => g < godzinyPrzyjec.Item1 || g >= godzinyPrzyjec.Item2);
                    niepracujace.ToList().ForEach(e => godziny.Remove(e));
                    wybor_terminu.ItemsSource = new ObservableCollection<TimeSpan>(godziny);
                }

                else
                {
                    wybor_terminu.SelectedIndex = -1;
                    wybor_terminu.IsEnabled = false;
                }
            }

        }

        private void wybor_Lekarza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<TimeSpan> godziny = new()
            {
                new TimeSpan(8,0,0),
                new TimeSpan(8,30,0),
                new TimeSpan(9,0,0),
                new TimeSpan(9,30,0),
                new TimeSpan(10,0,0),
                new TimeSpan(10,30,0),
                new TimeSpan(11,0,0),
                new TimeSpan(11,30,0),
                new TimeSpan(12,0,0),
                new TimeSpan(12,30,0),
                new TimeSpan(13,0,0),
                new TimeSpan(13,30,0),
                new TimeSpan(14,0,0),
                new TimeSpan(14,30,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0)
            };
            Lekarz lekarz = new();
            lekarz = p.Lekarze.Find(w => w.Pesel == p.Lekarze[wybor_Lekarza.SelectedIndex].Pesel); // this

            if (calendar.SelectedDate != null)
            {
                DateTime data = calendar.SelectedDate.Value.Date;
                IEnumerable<Tuple<DateTime, TimeSpan>> krotki = lekarz.Zaplanowane_Wizyty.Where(kvp => kvp.Value == true).Select(kvp => kvp.Key);
                IEnumerable<TimeSpan> terminy = krotki.Where(kvp => kvp.Item1 == data).Select(kvp => kvp.Item2);
                terminy.ToList().ForEach(e => godziny.Remove(e));

                if (lekarz.GodzinyPracy.ContainsKey(data.DayOfWeek))
                {
                    wybor_terminu.IsEnabled = true;
                    wybor_terminu.SelectedIndex = 0;
                    Tuple<TimeSpan, TimeSpan> godzinyPrzyjec = lekarz.GodzinyPracy[data.DayOfWeek];
                    List<TimeSpan> niepracujace = new();
                    niepracujace = godziny.FindAll(g => g < godzinyPrzyjec.Item1 || g >= godzinyPrzyjec.Item2);
                    niepracujace.ToList().ForEach(e => godziny.Remove(e));
                    wybor_terminu.ItemsSource = new ObservableCollection<TimeSpan>(godziny);
                }

                else
                {
                    wybor_terminu.SelectedIndex = -1;
                    wybor_terminu.IsEnabled = false;
                }
            }

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Lekarz lekarz = p.Lekarze.Find(w => w.Imie == p.Lekarze[wybor_Lekarza.SelectedIndex].Imie && w.Nazwisko == p.Lekarze[wybor_Lekarza.SelectedIndex].Nazwisko);
            if (wybor_Lekarza == null || wybor_terminu.SelectedIndex == -1 || !calendar.SelectedDate.HasValue)
            {
                MessageBox.Show("Fill the fields");
            }
            else
            {
                DateTime data = calendar.SelectedDate.Value.Date;
                if (data < DateTime.Now)
                {
                    MessageBox.Show("Proszę wybrać poprawną datę");
                    return;
                }
                p.DodajWizyte(new Wizyta(data.ToString("dd-MM-yyyy"), lekarz, ZalogowanyPacjent, (TimeSpan)wybor_terminu.SelectedItem));
                MessageBox.Show("Wizyta została dodana");
            }

        }



        //Widoczność
        private void WidocznoscHistory(bool visibility)
        {
            if (visibility)
            {
                history_ListBox.Visibility = Visibility.Visible;
                history_txtbox.Visibility = Visibility.Visible;
            }
            else
            {
                history_ListBox.Visibility = Visibility.Hidden;
                history_txtbox.Visibility = Visibility.Hidden;
            }
        }

        private void WidocznoscVisits(bool visibility)
        {
            if (visibility)
            {
                Lista.Visibility = Visibility.Visible;
                b1.Visibility = Visibility.Visible;
                b2.Visibility = Visibility.Visible;
                b3.Visibility = Visibility.Visible;
                b4.Visibility = Visibility.Visible;
                Lekarze.Visibility = Visibility.Visible;
                Lista.ItemsSource = new ObservableCollection<Wizyta>(p.WizytyPacjenta(ZalogowanyPacjent.Pesel));
                List<Tuple<string, string>> imie_Nazwisko = new();
                p.Lekarze.ForEach(l => imie_Nazwisko.Add(new Tuple<string, string>(l.Imie, l.Nazwisko)));
                Lekarze.ItemsSource = new ObservableCollection<Tuple<string, string>>(imie_Nazwisko);
            }
            else
            {
                Lista.Visibility = Visibility.Hidden;
                b1.Visibility = Visibility.Hidden;
                b2.Visibility = Visibility.Hidden;
                b3.Visibility = Visibility.Hidden;
                b4.Visibility = Visibility.Hidden;
                Lekarze.Visibility = Visibility.Hidden;
            }
        }
        private void WidocznoscInformations(bool visibility)
        {
            if (visibility)
            {
                pola.Visibility = Visibility.Visible;
                pola1.Visibility = Visibility.Visible;
                pola2.Visibility = Visibility.Visible;
                pola3.Visibility = Visibility.Visible;
                pola4.Visibility = Visibility.Visible;
                pola5.Visibility = Visibility.Visible;
                Imie.Visibility = Visibility.Visible;
                Nazwisko.Visibility = Visibility.Visible;
                Pesel.Visibility = Visibility.Visible;
                Data_Urodzenia.Visibility = Visibility.Visible;
                Plec.Visibility = Visibility.Visible;
                Zdjecie.Visibility = Visibility.Visible;
                moj_Profil.Visibility = Visibility.Visible;
                Change_Button.Visibility = Visibility.Visible;
                PasswordHidden.Visibility = Visibility.Visible;
                Imie.Text = ZalogowanyPacjent.Imie;
                Nazwisko.Text = ZalogowanyPacjent.Nazwisko;
                Pesel.Text = ZalogowanyPacjent.Pesel;
                Data_Urodzenia.Text = ZalogowanyPacjent.DataUrodzenia.ToString("dd-MM-yyyy");
                if (ZalogowanyPacjent.Plec == EnumPlec.M)
                {
                    Plec.Text = "Man";
                    Zdjecie.Source = new BitmapImage(new Uri(@"/Man avatar.png", UriKind.Relative));
                }
                else
                {
                    Plec.Text = "Woman";
                    Zdjecie.Source = new BitmapImage(new Uri(@"/woman2.png", UriKind.Relative));
                }
            }
            else
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
                Zdjecie.Visibility = Visibility.Hidden;
                moj_Profil.Visibility = Visibility.Hidden;
                Change_Button.Visibility = Visibility.Hidden;
                PasswordHidden.Visibility = Visibility.Hidden;
            }
        }
        private void WidocznoscVisit(bool visibility)
        {
            if (visibility)
            {
                List<TimeSpan> godziny = new()
            {
                new TimeSpan(8,0,0),
                new TimeSpan(8,30,0),
                new TimeSpan(9,0,0),
                new TimeSpan(9,30,0),
                new TimeSpan(10,0,0),
                new TimeSpan(10,30,0),
                new TimeSpan(11,0,0),
                new TimeSpan(11,30,0),
                new TimeSpan(12,0,0),
                new TimeSpan(12,30,0),
                new TimeSpan(13,0,0),
                new TimeSpan(13,30,0),
                new TimeSpan(14,0,0),
                new TimeSpan(14,30,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0)
            };
                wybor_terminu.Visibility = Visibility.Visible;
                wybor_Lekarza.Visibility = Visibility.Visible;
                date_text.Visibility = Visibility.Visible;
                doctor_text.Visibility = Visibility.Visible;
                calendar.Visibility = Visibility.Visible;
                create_butto.Visibility = Visibility.Visible;
                Add_visit1.Visibility = Visibility.Visible;
                List<Tuple<string, string>> imie_Nazwisko2 = new();
                p.Lekarze.ForEach(l => imie_Nazwisko2.Add(new Tuple<string, string>(l.Imie, l.Nazwisko)));
                wybor_Lekarza.ItemsSource = new ObservableCollection<Tuple<string, string>>(imie_Nazwisko2);
                wybor_Lekarza.SelectedIndex = 0;
                wybor_terminu.ItemsSource = new ObservableCollection<TimeSpan>(godziny);

            }
            else
            {
                wybor_terminu.Visibility = Visibility.Hidden;
                wybor_Lekarza.Visibility = Visibility.Hidden;
                date_text.Visibility = Visibility.Hidden;
                doctor_text.Visibility = Visibility.Hidden;
                calendar.Visibility = Visibility.Hidden;
                create_butto.Visibility = Visibility.Hidden;
                Add_visit1.Visibility = Visibility.Hidden;
            }
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}