using PrzychodniaGIT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Przychodnia_finally
{
    /// <summary>
    /// Interaction logic for Przychodnia.xaml
    /// </summary>
    public partial class Przychodnia_ADMIN : Window
    {
        Placowka p;
        public Przychodnia_ADMIN()
        {
            p = new();
            InitializeComponent();
        }

        public Przychodnia_ADMIN(Placowka placowka) : this()
        {
            p = placowka;
            WidocznoscWizyt(true);
            WidocznoscLekarz(false);
            WidocznoscPacjent(false);
            WidocznoscAddPacjent(false);
            WidocznoscDodaniaLekarza(false);
        }

        private void wylogowanie_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objSecondWindow = new MainWindow(p);
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();
        }



        private void BtnDoktorzyMENU_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscLekarz(true);
            WidocznoscWizyt(false);
            WidocznoscPacjent(false);
            WidocznoscAddPacjent(false);
            WidocznoscDodaniaLekarza(false);
            if (p.Lekarze.Count > 0)
            {
                Doktorzy_ListBox.ItemsSource = new ObservableCollection<Lekarz>(p.Lekarze);
            }
        }

        private void BtnWszystkiewizytyMENU_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscWizyt(true);
            WidocznoscLekarz(false);
            WidocznoscPacjent(false);
            WidocznoscAddPacjent(false);
            WidocznoscDodaniaLekarza(false);

            Wizyty_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizyty());

        }

        private void BtnPatientsMENU_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscWizyt(false);
            WidocznoscLekarz(false);
            WidocznoscPacjent(true);
            WidocznoscAddPacjent(false);
            WidocznoscDodaniaLekarza(false);

            Pacjenci_ListBox.ItemsSource = new ObservableCollection<Pacjent>(p.Pacjenci);
        }

        private void BtnUsunWizyte_Click(object sender, RoutedEventArgs e)
        {
            if (Wizyty_ListBox.SelectedIndex > -1)
            {
                Wizyta w = Wizyty_ListBox.SelectedItem as Wizyta;
                p.AnulujWizytePacjent(w.Pacjent.Pesel, w.Data, w.Godzina);
                Wizyty_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizyty());

                Lekarz l = p.Lekarze.Find(dok => dok.Pesel == w.Lekarz.Pesel);
                l.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(w.Data, w.Godzina));
            }
        }

        public void WidocznoscWizyt(bool visibility)
        {
            if (visibility)
            {
                Wizyty_ListBox.Visibility = Visibility.Visible;
                LblWizyty.Visibility = Visibility.Visible;
                calendar1.Visibility = Visibility.Visible;
                BtnAllApp.Visibility = Visibility.Visible;
                BtnDate.Visibility = Visibility.Visible;
                BtnUsunWizyte.Visibility = Visibility.Visible;
            }
            else
            {
                Wizyty_ListBox.Visibility = Visibility.Hidden;
                LblWizyty.Visibility = Visibility.Hidden;
                calendar1.Visibility = Visibility.Hidden;
                BtnAllApp.Visibility = Visibility.Hidden;
                BtnDate.Visibility = Visibility.Hidden;
                BtnUsunWizyte.Visibility = Visibility.Hidden;
            }
        }

        public void WidocznoscLekarz(bool visibility)
        {
            if (visibility)
            {
                BtnAllDocs.Visibility = Visibility.Visible;
                BtnPesel.Visibility = Visibility.Visible;
                Txtboxpesel.Visibility = Visibility.Visible;
                Doktorzy_ListBox.Visibility = Visibility.Visible;
                BtnUsundoc.Visibility = Visibility.Visible;
                LblDoctorzy.Visibility = Visibility.Visible;
            }
            else
            {
                LblDoctorzy.Visibility = Visibility.Hidden;
                BtnAllDocs.Visibility = Visibility.Hidden;
                BtnPesel.Visibility = Visibility.Hidden;
                Txtboxpesel.Visibility = Visibility.Hidden;
                Doktorzy_ListBox.Visibility = Visibility.Hidden;
                BtnUsundoc.Visibility = Visibility.Hidden;

            }
        }

        public void WidocznoscPacjent(bool visibility)
        {
            if (visibility)
            {
                LblPacjenci.Visibility = Visibility.Visible;
                BtnAllPat.Visibility = Visibility.Visible;
                BtnPeselpac.Visibility = Visibility.Visible;
                Txtboxpeselpac.Visibility = Visibility.Visible;
                Pacjenci_ListBox.Visibility = Visibility.Visible;
                BtnUsunpacj.Visibility = Visibility.Visible;

            }
            else
            {
                LblPacjenci.Visibility = Visibility.Hidden;
                BtnAllPat.Visibility = Visibility.Hidden;
                BtnPeselpac.Visibility = Visibility.Hidden;
                Txtboxpeselpac.Visibility = Visibility.Hidden;
                Pacjenci_ListBox.Visibility = Visibility.Hidden;
                BtnUsunpacj.Visibility = Visibility.Hidden;
            }
        }

        public void WidocznoscAddPacjent(bool visibility)
        {
            if (visibility)
            {
                LblAddPat.Visibility = Visibility.Visible;
                TxtBoxPesel.Visibility = Visibility.Visible;
                TxtBoxData_Urodzenia.Visibility = Visibility.Visible;
                TxtBoxImie.Visibility = Visibility.Visible;
                TxtBoxNazwisko.Visibility = Visibility.Visible;
                TxtBoxPassword.Visibility = Visibility.Visible;
                BtnReset.Visibility = Visibility.Visible;
                BtnAddPat.Visibility = Visibility.Visible;
                Txthaslo.Visibility = Visibility.Visible;
                Txtimie.Visibility = Visibility.Visible;
                Txtnazwisko.Visibility = Visibility.Visible;
                Txtpesel.Visibility = Visibility.Visible;
                Txturodzenie.Visibility = Visibility.Visible;
                Txtplec.Visibility = Visibility.Visible;
                Txthaslo.Visibility = Visibility.Visible;
                Plec.Visibility = Visibility.Visible;
            }
            else
            {
                LblAddPat.Visibility = Visibility.Hidden;
                TxtBoxPesel.Visibility = Visibility.Hidden;
                TxtBoxData_Urodzenia.Visibility = Visibility.Hidden;
                TxtBoxImie.Visibility = Visibility.Hidden;
                TxtBoxNazwisko.Visibility = Visibility.Hidden;
                TxtBoxPassword.Visibility = Visibility.Hidden;
                BtnReset.Visibility = Visibility.Hidden;
                BtnAddPat.Visibility = Visibility.Hidden;
                Txthaslo.Visibility = Visibility.Hidden;
                Txtimie.Visibility = Visibility.Hidden;
                Txtnazwisko.Visibility = Visibility.Hidden;
                Txtpesel.Visibility = Visibility.Hidden;
                Txturodzenie.Visibility = Visibility.Hidden;
                Txtplec.Visibility = Visibility.Hidden;
                Txthaslo.Visibility = Visibility.Hidden;
                Plec.Visibility = Visibility.Hidden;
            }
        }
        private void BtnDate_Click(object sender, RoutedEventArgs e)
        {
            if (calendar1.SelectedDate != null)
            {
                Wizyty_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizyty().Where(p => p.Data == calendar1.SelectedDate.Value.Date));
            }
        }

        private void BtnAllApp_Click(object sender, RoutedEventArgs e)
        {
            Wizyty_ListBox.ItemsSource = new ObservableCollection<Wizyta>(p.WszystkieWizyty());
        }

        private void BtnUsundoc_Click(object sender, RoutedEventArgs e)
        {
            if (Doktorzy_ListBox.SelectedIndex > -1)
            {
                Lekarz lek = Doktorzy_ListBox.SelectedItem as Lekarz;
                Lekarz l = p.Lekarze.Find(m => m.Pesel == lek.Pesel);
                p.UsunLekarza(l.Pesel);
                Doktorzy_ListBox.ItemsSource = new ObservableCollection<Lekarz>(p.Lekarze);
            }
        }

        private void BtnAllDocs_Click(object sender, RoutedEventArgs e)
        {
            if (p.Lekarze.Count() > 0)
            {
                Doktorzy_ListBox.ItemsSource = new ObservableCollection<Lekarz>(p.Lekarze);
            }
        }

        private void BtnPesel_Click(object sender, RoutedEventArgs e)
        {
            if (Txtboxpesel.Text.Length > 0 && Txtboxpesel.Text != "Write pesel there")
            {
                Doktorzy_ListBox.ItemsSource = new ObservableCollection<Lekarz>(p.Lekarze.FindAll(p => p.Pesel == Txtboxpesel.Text.ToString()));
            }
        }

        private void BtnUsunpacj_Click(object sender, RoutedEventArgs e)
        {
            if (Pacjenci_ListBox.SelectedIndex > -1)
            {
                Pacjent pac = Pacjenci_ListBox.SelectedItem as Pacjent;
                Pacjent pa = p.Pacjenci.Find(m => m.Pesel == pac.Pesel);


                List<Wizyta> wizytyanulowane = new List<Wizyta>();
                wizytyanulowane = p.Wizyty.FindAll(p => p.Pacjent.Pesel == pa.Pesel);

                foreach (Wizyta w in wizytyanulowane)
                {
                    Lekarz l = w.Lekarz;
                    Lekarz test = p.Lekarze.Find(p => p.Pesel == w.Lekarz.Pesel);
                    test.Zaplanowane_Wizyty.Remove(new Tuple<DateTime, TimeSpan>(w.Data, w.Godzina));
                }

                p.UsuńPacjenta(pa.Pesel);
                Pacjenci_ListBox.ItemsSource = new ObservableCollection<Pacjent>(p.Pacjenci);

            }
        }

        private void BtnPeselpac_Click(object sender, RoutedEventArgs e)
        {
            if (Txtboxpeselpac.Text.Length > 0 && Txtboxpeselpac.Text != "Write pesel there")
            {
                Pacjenci_ListBox.ItemsSource = new ObservableCollection<Pacjent>(p.Pacjenci.FindAll(p => p.Pesel == Txtboxpeselpac.Text.ToString()));
            }
        }

        private void BtnAllPat_Click(object sender, RoutedEventArgs e)
        {
            Pacjenci_ListBox.ItemsSource = new ObservableCollection<Pacjent>(p.Pacjenci);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Imie(object sender, RoutedEventArgs e)
        {
            TxtBoxImie.Text = "";
        }
        private void Click_Nazwisko(object sender, RoutedEventArgs e)
        {
            TxtBoxNazwisko.Text = "";
        }
        private void Click_DataUrodzenia(object sender, RoutedEventArgs e)
        {
            TxtBoxData_Urodzenia.Text = "";
        }
        private void Click_Pesel(object sender, RoutedEventArgs e)
        {
            TxtBoxPesel.Text = "";
        }
        private void Click_Haslo(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDodajPacjentaMENUClick(object sender, RoutedEventArgs e)
        {
            WidocznoscAddPacjent(true);
            WidocznoscLekarz(false);
            WidocznoscPacjent(false);
            WidocznoscWizyt(false);
            WidocznoscDodaniaLekarza(false);
        }

        private void BtnDodajLekarzaMENU_Click(object sender, RoutedEventArgs e)
        {
            WidocznoscAddPacjent(false);
            WidocznoscLekarz(false);
            WidocznoscPacjent(false);
            WidocznoscWizyt(false);
            WidocznoscDodaniaLekarza(true);
        }


        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            TxtBoxImie.Text = "";
            TxtBoxNazwisko.Text = "";
            TxtBoxData_Urodzenia.Text = "";
            TxtBoxPesel.Text = "";
        }

        private void BtnAddPat_Click(object sender, RoutedEventArgs e)
        {
            if (TxtBoxImie.Text.Length > 0 && TxtBoxNazwisko.Text.Length > 0 && TxtBoxData_Urodzenia.Text.Length > 0 && Plec.Text.Length > 0 && TxtBoxPesel.Text.Length > 0 && TxtBoxPassword.Password.Length > 0)
            {
                if (!DateTime.TryParseExact(TxtBoxData_Urodzenia.Text,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
                {
                    MessageBox.Show("Wrong data format");
                    return;
                }
                if (res > DateTime.Now)
                {
                    MessageBox.Show("Wrong date format");
                    return;
                }
                if (!Regex.IsMatch(TxtBoxPesel.Text, @"\d{11}"))
                {
                    MessageBox.Show("Wrong Pesel format.");
                    return;
                }
                if (Plec.Text == "Woman")
                {
                    Pacjent p1 = new Pacjent(TxtBoxImie.Text, TxtBoxNazwisko.Text, TxtBoxData_Urodzenia.Text, TxtBoxPesel.Text, EnumPlec.K);
                    if (p.Lekarze.Find(lek => lek.Pesel == p1.Pesel) == null)
                    {
                        if (p.HasloRejestracjaPacjent(TxtBoxPesel.Text, TxtBoxPassword.Password))
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p1);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong");
                            return;
                        }

                    }

                    else if (p.Pacjenci.Find(pac => pac.Pesel == TxtBoxPesel.Text) != null)
                    {
                        MessageBox.Show("Account for this PESEL already exists.");
                        return;
                    }

                    else
                    {
                        Lekarz lek = p.Lekarze.Find(lek => lek.Pesel == p1.Pesel);
                        string haslo = p.Konta[p1.Pesel];

                        if (lek.Imie == p1.Imie && lek.Nazwisko == p1.Nazwisko && lek.DataUrodzenia == p1.DataUrodzenia && lek.Plec == p1.Plec && haslo == TxtBoxPassword.Password)
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p1);
                            return;
                        }

                        else
                        {
                            MessageBox.Show("You must fill the exact doctor values.");
                            return;
                        }
                    }
                }
                else
                {
                    Pacjent p2 = new Pacjent(TxtBoxImie.Text, TxtBoxNazwisko.Text, TxtBoxData_Urodzenia.Text, TxtBoxPesel.Text, EnumPlec.M);
                    if (p.Lekarze.Find(lek => lek.Pesel == p2.Pesel) == null)
                    {
                        if (p.HasloRejestracjaPacjent(TxtBoxPesel.Text, TxtBoxPassword.Password))
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p2);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong");
                            return;
                        }

                    }

                    else if (p.Pacjenci.Find(pac => pac.Pesel == TxtBoxPesel.Text) != null)
                    {
                        MessageBox.Show("Account for this PESEL already exists.");
                        return;
                    }

                    else
                    {
                        Lekarz lek = p.Lekarze.Find(lek => lek.Pesel == p2.Pesel);
                        string haslo = p.Konta[p2.Pesel];

                        if (lek.Imie == p2.Imie && lek.Nazwisko == p2.Nazwisko && lek.DataUrodzenia == p2.DataUrodzenia && lek.Plec == p2.Plec && haslo == TxtBoxPassword.Password)
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p2);
                            return;
                        }

                        else
                        {
                            MessageBox.Show("You must fill the exact doctor values.");
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill the fields");
            }
        }

        private bool SprawdzGodzine(string t1, string t2)
        {
            if (t1 == "" && t2 == "")
            {
                return true;
            }
            TimeSpan time;
            TimeSpan time2;
            if (TimeSpan.TryParse(t1, out time) && TimeSpan.TryParse(t2, out time2))
            {

                if (time < p.GodzinaOtwarcia || time > p.GodzinaZamkniecia || time2 < p.GodzinaOtwarcia || time2 > p.GodzinaZamkniecia)
                {
                    return false;
                }
                else if (time >= time2 || (time.Minutes != 30 && time.Minutes != 0) || (time2.Minutes != 0 && time2.Minutes != 30))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        private TimeSpan zamianaczasu(string czas)
        {
            TimeSpan t = new();
            if (czas == "")
            {
                return new TimeSpan(12, 0, 0);
            }
            else if (TimeSpan.TryParse(czas, out t))
            {
                return t;
            }
            else
            {
                return t;
            }
        }
        private void BtnAddDocl_Click(object sender, RoutedEventArgs e)
        {
            string input = TxtBoxpn1.Text;
            List<string> textboxy = new()
            { TxtBoxpn1.Text, TxtBoxpn2.Text, TxtBoxwt1.Text, TxtBoxwt2.Text, TxtBoxsr1.Text, TxtBoxsr2.Text, TxtBoxczw1.Text,
                TxtBoxczw2.Text, TxtBoxpt1.Text, TxtBoxpt2.Text, TxtBoxsob1.Text, TxtBoxsob2.Text, TxtBoxnd1.Text, TxtBoxnd2.Text };

            bool pn = SprawdzGodzine(textboxy[0], textboxy[1]);
            bool wt = SprawdzGodzine(textboxy[2], textboxy[3]);
            bool sr = SprawdzGodzine(textboxy[4], textboxy[5]);
            bool czw = SprawdzGodzine(textboxy[6], textboxy[7]);
            bool pt = SprawdzGodzine(textboxy[8], textboxy[9]);
            bool sob = SprawdzGodzine(textboxy[10], textboxy[11]);
            bool nd = SprawdzGodzine(textboxy[12], textboxy[13]);

            if (pn && wt && sr && czw && pt && sob && nd)
            {
                TimeSpan tim1 = zamianaczasu(TxtBoxpn1.Text);
                TimeSpan tim2 = zamianaczasu(TxtBoxpn2.Text);
                TimeSpan tim3 = zamianaczasu(TxtBoxwt1.Text);
                TimeSpan tim4 = zamianaczasu(TxtBoxwt2.Text);
                TimeSpan tim5 = zamianaczasu(TxtBoxsr1.Text);
                TimeSpan tim6 = zamianaczasu(TxtBoxsr2.Text);
                TimeSpan tim7 = zamianaczasu(TxtBoxczw1.Text);
                TimeSpan tim8 = zamianaczasu(TxtBoxczw2.Text);
                TimeSpan tim9 = zamianaczasu(TxtBoxpt1.Text);
                TimeSpan tim10 = zamianaczasu(TxtBoxpt2.Text);
                TimeSpan tim11 = zamianaczasu(TxtBoxsob1.Text);
                TimeSpan tim12 = zamianaczasu(TxtBoxsob2.Text);
                TimeSpan tim13 = zamianaczasu(TxtBoxnd1.Text);
                TimeSpan tim14 = zamianaczasu(TxtBoxnd2.Text);



                if (TxtBoxImiel.Text.Length > 0 && TxtBoxNazwiskol.Text.Length > 0 && TxtBoxData_Urodzenial.Text.Length > 0 && Plecl.Text.Length > 0 && TxtBoxPesell.Text.Length > 0 && TxtBoxPasswordl.Password.Length > 0 && TxtBoxfunkcja.Text.Length > 0)
                {
                    if (!DateTime.TryParseExact(TxtBoxData_Urodzenial.Text,
                    new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                    }, null, System.Globalization.DateTimeStyles.None,
                    out DateTime res))
                    {
                        MessageBox.Show("Wrong data format");
                        return;
                    }
                    if (res > DateTime.Now)
                    {
                        MessageBox.Show("Wrong date format");
                        return;
                    }
                    if (!Regex.IsMatch(TxtBoxPesell.Text, @"^\d{11}$"))
                    {
                        MessageBox.Show("Wrong Pesel format.");
                        return;
                    }

                    if (p.Lekarze.Find(lek => lek.Pesel == TxtBoxPesell.Text) == null && p.Pacjenci.Find(pac => pac.Pesel == TxtBoxPesell.Text) == null)
                    {
                        EnumPlec plecc = new();
                        if (Plecl.Text == "Woman")
                        {
                            plecc = EnumPlec.K;
                        }
                        else
                        {
                            plecc = EnumPlec.M;
                        }

                        Lekarz lek = new(TxtBoxImiel.Text, TxtBoxNazwiskol.Text, TxtBoxData_Urodzenial.Text, TxtBoxPesell.Text, plecc, TxtBoxfunkcja.Text,
                                new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>
                        {
                            { DayOfWeek.Monday, new Tuple<TimeSpan, TimeSpan>(tim1, tim2) },
                            { DayOfWeek.Tuesday, new Tuple<TimeSpan, TimeSpan>(tim3, tim4) },
                            { DayOfWeek.Wednesday, new Tuple<TimeSpan, TimeSpan>(tim5, tim6) },
                            { DayOfWeek.Thursday, new Tuple<TimeSpan, TimeSpan>(tim7, tim8) },
                            { DayOfWeek.Friday, new Tuple<TimeSpan, TimeSpan>(tim9, tim10) },
                            { DayOfWeek.Saturday, new Tuple<TimeSpan, TimeSpan>(tim11, tim12) },
                            { DayOfWeek.Sunday, new Tuple<TimeSpan, TimeSpan>(tim13, tim14) }
                        });

                        if (tim1.Hours == 12 && tim2.Hours == 12)
                        {
                            lek.GodzinyPracy.Remove(DayOfWeek.Monday);
                        }
                        if (tim3.Hours == 12 && tim4.Hours == 12)
                        {
                            lek.GodzinyPracy.Remove(DayOfWeek.Tuesday);
                        }
                        if (tim5.Hours == 12 && tim6.Hours == 12)
                        {
                            lek.GodzinyPracy.Remove(DayOfWeek.Wednesday);
                        }
                        if (tim7.Hours == 12 && tim8.Hours == 12)
                        {
                            lek.GodzinyPracy.Remove(DayOfWeek.Thursday);
                        }
                        if (tim9.Hours == 12 && tim10.Hours == 12)
                        {
                            lek.GodzinyPracy.Remove(DayOfWeek.Friday);
                        }
                        if (tim11.Hours == 12 && tim12.Hours == 12)
                        {
                            lek.GodzinyPracy.Remove(DayOfWeek.Saturday);
                        }
                        if (tim13.Hours == 12 && tim14.Hours == 12)
                        {
                            lek.GodzinyPracy.Remove(DayOfWeek.Sunday);
                        }
                        //if(TxtBoxImiel.Text.Length > 0 && TxtBoxNazwiskol.Text.Length>0 && TxtBoxPesell.Text.Length>0 && TxtBoxData_Urodzenial.)
                        p.DodajLekarza(lek);
                        p.DodajKonto(TxtBoxPesell.Text, TxtBoxPasswordl.Password);
                        MessageBox.Show("Success");
                        return;
                    }

                    else if (p.Lekarze.Find(lek => lek.Pesel == TxtBoxPesell.Text) != null)
                    {
                        MessageBox.Show("Doctor already exists");
                        return;
                    }

                    else
                    {
                        Pacjent pac = p.Pacjenci.Find(p => p.Pesel == TxtBoxPesell.Text);
                        EnumPlec plecc = new();

                        if (Plecl.Text == "Woman")
                        {
                            plecc = EnumPlec.K;
                        }
                        else
                        {
                            plecc = EnumPlec.M;
                        }

                        string haslo = p.Konta[pac.Pesel];
                        Lekarz lekarztestowy = new(TxtBoxImiel.Text, TxtBoxNazwiskol.Text, TxtBoxData_Urodzenial.Text, TxtBoxPesell.Text, plecc);
                        if (pac.Imie == lekarztestowy.Imie && pac.Nazwisko == lekarztestowy.Nazwisko && pac.DataUrodzenia == lekarztestowy.DataUrodzenia && pac.Plec == lekarztestowy.Plec && haslo == TxtBoxPasswordl.Password)
                        {
                            Lekarz lek = new(TxtBoxImiel.Text, TxtBoxNazwiskol.Text, TxtBoxData_Urodzenial.Text, TxtBoxPesell.Text, plecc, TxtBoxfunkcja.Text,
                                         new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>
                          {
                                    { DayOfWeek.Monday, new Tuple<TimeSpan, TimeSpan>(tim1, tim2) },
                                    { DayOfWeek.Tuesday, new Tuple<TimeSpan, TimeSpan>(tim3, tim4) },
                                    { DayOfWeek.Wednesday, new Tuple<TimeSpan, TimeSpan>(tim5, tim6) },
                                    { DayOfWeek.Thursday, new Tuple<TimeSpan, TimeSpan>(tim7, tim8) },
                                    { DayOfWeek.Friday, new Tuple<TimeSpan, TimeSpan>(tim9, tim10) },
                                    { DayOfWeek.Saturday, new Tuple<TimeSpan, TimeSpan>(tim11, tim12) },
                                    { DayOfWeek.Sunday, new Tuple<TimeSpan, TimeSpan>(tim13, tim14) }
                          });

                            if (tim1.Hours == 12 && tim2.Hours == 12)
                            {
                                lek.GodzinyPracy.Remove(DayOfWeek.Monday);
                            }
                            if (tim3.Hours == 12 && tim4.Hours == 12)
                            {
                                lek.GodzinyPracy.Remove(DayOfWeek.Tuesday);
                            }
                            if (tim5.Hours == 12 && tim6.Hours == 12)
                            {
                                lek.GodzinyPracy.Remove(DayOfWeek.Wednesday);
                            }
                            if (tim7.Hours == 12 && tim8.Hours == 12)
                            {
                                lek.GodzinyPracy.Remove(DayOfWeek.Thursday);
                            }
                            if (tim9.Hours == 12 && tim10.Hours == 12)
                            {
                                lek.GodzinyPracy.Remove(DayOfWeek.Friday);
                            }
                            if (tim11.Hours == 12 && tim12.Hours == 12)
                            {
                                lek.GodzinyPracy.Remove(DayOfWeek.Saturday);
                            }
                            if (tim13.Hours == 12 && tim14.Hours == 12)
                            {
                                lek.GodzinyPracy.Remove(DayOfWeek.Sunday);
                            }
                            //if(TxtBoxImiel.Text.Length > 0 && TxtBoxNazwiskol.Text.Length>0 && TxtBoxPesell.Text.Length>0 && TxtBoxData_Urodzenial.)
                            p.DodajLekarza(lek);
                            MessageBox.Show("Success");
                            return;
                        }

                        else
                        {
                            MessageBox.Show("You must fill the exact doctor values.");
                            return;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("fill the fields");
                    return;
                }

            }
            else
            {
                MessageBox.Show("Something went wrong.");
            }
        }

        private void WidocznoscDodaniaLekarza(bool visiblity)
        {
            if (visiblity)
            {
                TxtBoxImiel.Visibility = Visibility.Visible;
                TxtBoxNazwiskol.Visibility = Visibility.Visible;
                TxtBoxData_Urodzenial.Visibility = Visibility.Visible;
                TxtBoxPasswordl.Visibility = Visibility.Visible;
                TxtBoxPesell.Visibility = Visibility.Visible;
                Plecl.Visibility = Visibility.Visible;
                BtnAddDocl.Visibility = Visibility.Visible;
                BtnResetl.Visibility = Visibility.Visible;
                Txtpn.Visibility = Visibility.Visible;
                Txtwt.Visibility = Visibility.Visible;
                Txtsr.Visibility = Visibility.Visible;
                Txtczw.Visibility = Visibility.Visible;
                Txtpt.Visibility = Visibility.Visible;
                Txtsob.Visibility = Visibility.Visible;
                Txtnd.Visibility = Visibility.Visible;
                TxtBoxpn1.Visibility = Visibility.Visible;
                TxtBoxwt1.Visibility = Visibility.Visible;
                TxtBoxsr1.Visibility = Visibility.Visible;
                TxtBoxczw1.Visibility = Visibility.Visible;
                TxtBoxpt1.Visibility = Visibility.Visible;
                TxtBoxsob1.Visibility = Visibility.Visible;
                TxtBoxnd1.Visibility = Visibility.Visible;
                TxtBoxpn2.Visibility = Visibility.Visible;
                TxtBoxwt2.Visibility = Visibility.Visible;
                TxtBoxsr2.Visibility = Visibility.Visible;
                TxtBoxczw2.Visibility = Visibility.Visible;
                TxtBoxpt2.Visibility = Visibility.Visible;
                TxtBoxsob2.Visibility = Visibility.Visible;
                TxtBoxnd2.Visibility = Visibility.Visible;
                Txtimiel.Visibility = Visibility.Visible;
                Txtnazwiskol.Visibility = Visibility.Visible;
                Txtpesell.Visibility = Visibility.Visible;
                Txturodzeniel.Visibility = Visibility.Visible;
                Txtplecl.Visibility = Visibility.Visible;
                Txthaslol.Visibility = Visibility.Visible;
                LblAddDocl.Visibility = Visibility.Visible;
                TxtBoxfunkcja.Visibility = Visibility.Visible;
                Txtspec.Visibility = Visibility.Visible;
            }
            else
            {
                TxtBoxImiel.Visibility = Visibility.Hidden;
                TxtBoxNazwiskol.Visibility = Visibility.Hidden;
                TxtBoxData_Urodzenial.Visibility = Visibility.Hidden;
                TxtBoxPasswordl.Visibility = Visibility.Hidden;
                TxtBoxPesell.Visibility = Visibility.Hidden;
                Plecl.Visibility = Visibility.Hidden;
                BtnAddDocl.Visibility = Visibility.Hidden;
                BtnResetl.Visibility = Visibility.Hidden;
                Txtpn.Visibility = Visibility.Hidden;
                Txtwt.Visibility = Visibility.Hidden;
                Txtsr.Visibility = Visibility.Hidden;
                Txtczw.Visibility = Visibility.Hidden;
                Txtpt.Visibility = Visibility.Hidden;
                Txtsob.Visibility = Visibility.Hidden;
                Txtnd.Visibility = Visibility.Hidden;
                TxtBoxpn1.Visibility = Visibility.Hidden;
                TxtBoxwt1.Visibility = Visibility.Hidden;
                TxtBoxsr1.Visibility = Visibility.Hidden;
                TxtBoxczw1.Visibility = Visibility.Hidden;
                TxtBoxpt1.Visibility = Visibility.Hidden;
                TxtBoxsob1.Visibility = Visibility.Hidden;
                TxtBoxnd1.Visibility = Visibility.Hidden;
                TxtBoxpn2.Visibility = Visibility.Hidden;
                TxtBoxwt2.Visibility = Visibility.Hidden;
                TxtBoxsr2.Visibility = Visibility.Hidden;
                TxtBoxczw2.Visibility = Visibility.Hidden;
                TxtBoxpt2.Visibility = Visibility.Hidden;
                TxtBoxsob2.Visibility = Visibility.Hidden;
                TxtBoxnd2.Visibility = Visibility.Hidden;
                Txtimiel.Visibility = Visibility.Hidden;
                Txtnazwiskol.Visibility = Visibility.Hidden;
                Txtpesell.Visibility = Visibility.Hidden;
                Txturodzeniel.Visibility = Visibility.Hidden;
                Txtplecl.Visibility = Visibility.Hidden;
                Txthaslol.Visibility = Visibility.Hidden;
                LblAddDocl.Visibility = Visibility.Hidden;
                TxtBoxfunkcja.Visibility = Visibility.Hidden;
                Txtspec.Visibility = Visibility.Hidden;
            }
        }
        private void BtnResetl_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


