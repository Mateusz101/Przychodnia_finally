using PrzychodniaGIT;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;
using System;

namespace Przychodnia_finally
{
    /// <summary>
    /// Interaction logic for Tworzenie_Konta_Pacjenta.xaml
    /// </summary>
    public partial class Tworzenie_Konta_Pacjenta : Window
    {
        Placowka p = new();
        public Tworzenie_Konta_Pacjenta()
        {
            p = Placowka.OdczytDC("przychodnia.xml");
            InitializeComponent();
        }
        public Tworzenie_Konta_Pacjenta(Placowka placowka) : this()
        {
            p = placowka;
        }
        //Funkcja przycisku reset
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Imie.Text = "Jan";
            Nazwisko.Text = "Kowalski";
            Pesel.Text = "69463683526";
            Data_Urodzenia.Text = "08.06.1975";
            Plec.Text = "Man";
            PasswordHidden.Password = "";
        }
        //Funkcja przycisku wroc
        private void WrocButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objSecondWindow = new MainWindow(p);
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();
        }
        //Funkcje usunięcia wartości poprzez podwójne kliknięcie
        private void Click_Imie(object sender, RoutedEventArgs e)
        {
            Imie.Text = "";
        }
        private void Click_Nazwisko(object sender, RoutedEventArgs e)
        {
            Nazwisko.Text = "";
        }
        private void Click_DataUrodzenia(object sender, RoutedEventArgs e)
        {
            Data_Urodzenia.Text = "";
        }
        private void Click_Pesel(object sender, RoutedEventArgs e)
        {
            Pesel.Text = "";
        }
        private void Click_Haslo(object sender, RoutedEventArgs e)
        {
            PasswordHidden.Password = "";
        }
        //Funkcja przycisku zapisz
        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {

            if (Imie.Text.Length > 0 && Nazwisko.Text.Length > 0 && Data_Urodzenia.Text.Length > 0 && Plec.Text.Length > 0 && Pesel.Text.Length > 0 && PasswordHidden.Password.Length > 0)
            {
                if (!DateTime.TryParseExact(Data_Urodzenia.Text,
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
                if (!Regex.IsMatch(Pesel.Text, @"^\d{11}$"))
                {
                    MessageBox.Show("Wrong Pesel format.");
                    return;
                }
                if (Plec.Text == "Woman")
                {
                    Pacjent p1 = new Pacjent(Imie.Text, Nazwisko.Text, Data_Urodzenia.Text, Pesel.Text, EnumPlec.K);
                    if (p.Lekarze.Find(lek => lek.Pesel == p1.Pesel) == null)
                    {
                        if (p.HasloRejestracjaPacjent(Pesel.Text, PasswordHidden.Password))
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p1);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong");
                            return;
                        }

                    }

                    else if (p.Pacjenci.Find(pac => pac.Pesel == Pesel.Text) != null)
                    {
                        MessageBox.Show("Account for this PESEL already exists.");
                        return;
                    }

                    else
                    {
                        Lekarz lek = p.Lekarze.Find(lek => lek.Pesel == p1.Pesel);
                        string haslo = p.Konta[p1.Pesel];

                        if (lek.Imie == p1.Imie && lek.Nazwisko == p1.Nazwisko && lek.DataUrodzenia == p1.DataUrodzenia && lek.Plec == p1.Plec && haslo == PasswordHidden.Password)
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p1);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
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
                    Pacjent p2 = new Pacjent(Imie.Text, Nazwisko.Text, Data_Urodzenia.Text, Pesel.Text, EnumPlec.M);
                    if (p.Lekarze.Find(lek => lek.Pesel == p2.Pesel) == null)
                    {
                        if (p.HasloRejestracjaPacjent(Pesel.Text, PasswordHidden.Password))
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p2);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong");
                            return;
                        }

                    }

                    else if (p.Pacjenci.Find(pac => pac.Pesel == Pesel.Text) != null)
                    {
                        MessageBox.Show("Account for this PESEL already exists.");
                        return;
                    }

                    else
                    {
                        Lekarz lek = p.Lekarze.Find(lek => lek.Pesel == p2.Pesel);
                        string haslo = p.Konta[p2.Pesel];

                        if (lek.Imie == p2.Imie && lek.Nazwisko == p2.Nazwisko && lek.DataUrodzenia == p2.DataUrodzenia && lek.Plec == p2.Plec && haslo == PasswordHidden.Password)
                        {
                            MessageBox.Show("Added successfully");
                            p.DodajPacjenta(p2);
                            MainWindow objSecondWindow = new MainWindow(p);
                            this.Visibility = Visibility.Hidden;
                            objSecondWindow.Show();
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
        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        private void ShowPasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Visible;
            PasswordHidden.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = PasswordHidden.Password;
            Eye_Close.Source = new BitmapImage(new Uri(@"/oko2.jpg", UriKind.Relative));
        }

        private void HidePasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Hidden;
            PasswordHidden.Visibility = Visibility.Visible;
            Eye_Close.Source = new BitmapImage(new Uri(@"/oko.png", UriKind.Relative));
        }
        private void MyTextBox_KeyDown1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Nazwisko.Focus();
            }
        }
        private void MyTextBox_KeyDown2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Data_Urodzenia.Focus();
            }
        }
        private void MyTextBox_KeyDown3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Pesel.Focus();
            }
        }
        private void MyTextBox_KeyDown4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Plec.Focus();
            }
        }
        private void MyTextBox_KeyDown5(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PasswordHidden.Focus();
            }
        }
        private void MyTextBox_KeyDown6(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ZapiszButton_Click(sender, e);
            }
        }

    }
}