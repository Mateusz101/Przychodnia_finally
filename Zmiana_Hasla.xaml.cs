using PrzychodniaGIT;
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

namespace Przychodnia_finally
{
    /// <summary>
    /// Interaction logic for Zmiana_Hasla.xaml
    /// </summary>
    public partial class Zmiana_Hasla : Window
    {
        Placowka p = new();
        Pacjent ZalogowanyPacjent = new();
        Lekarz ZalogowanyLekarz = new();

        public Zmiana_Hasla()
        {
            InitializeComponent();
        }
        public Zmiana_Hasla(Placowka p, Osoba pacjent) : this()
        {
            this.p = p;
            if (pacjent is Lekarz)
            {
                ZalogowanyLekarz = pacjent as Lekarz;
            }
            else
            {
                ZalogowanyPacjent = pacjent as Pacjent;
            }
        }
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (OldPassword.Password == "" || NewPassword.Password == "" || RepeatPassword.Password == "")
            {
                MessageBox.Show("Proszę uzupełnić wszystkie pola");
            }
            else if (NewPassword.Password != RepeatPassword.Password)
            {
                MessageBox.Show("Nowe hasło się różni!");
            }
            else
            {
                if (ZalogowanyLekarz.Imie == string.Empty)
                {
                    OldPassword.Password = p.Konta[ZalogowanyPacjent.Pesel];
                    if (OldPassword.Password.ToString() == p.Konta[ZalogowanyPacjent.Pesel])
                    {
                        p.Konta[ZalogowanyPacjent.Pesel] = NewPassword.Password.ToString();
                        MessageBox.Show("Hasło zostalo zmienione");
                        Przychodnia_Pacjent objSecondWindow = new Przychodnia_Pacjent(p, ZalogowanyPacjent.Pesel);
                        this.Visibility = Visibility.Hidden;
                        objSecondWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Stare hasło jest nieprawidłowe");
                    }
                }
                else
                {
                    OldPassword.Password = p.Konta[ZalogowanyLekarz.Pesel];
                    if (OldPassword.Password.ToString() == p.Konta[ZalogowanyLekarz.Pesel])
                    {
                        p.Konta[ZalogowanyLekarz.Pesel] = NewPassword.Password.ToString();
                        MessageBox.Show("Hasło zostalo zmienione");
                        Przychodnia_Doktor objSecondWindow = new Przychodnia_Doktor(p, ZalogowanyLekarz.Pesel, NewPassword.ToString(), "Lekarz");
                        this.Visibility = Visibility.Hidden;
                        objSecondWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Stare hasło jest nieprawidłowe");
                    }
                }
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (ZalogowanyLekarz.Imie != string.Empty)
            {
                Przychodnia_Doktor objSecondWindow = new Przychodnia_Doktor(p, ZalogowanyLekarz.Pesel, NewPassword.ToString(), "Lekarz");
                this.Visibility = Visibility.Hidden;
                objSecondWindow.Show();
            }
            else
            {
                Przychodnia_Pacjent objSecondWindow = new Przychodnia_Pacjent(p, ZalogowanyPacjent.Pesel, NewPassword.ToString(), "Pacjent");
                this.Visibility = Visibility.Hidden;
                objSecondWindow.Show();
            }


        }
        private void MyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NewPassword.Focus();
            }
        }
        private void MyTextBox_KeyDown2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RepeatPassword.Focus();
            }
        }
        private void MyTextBox_KeyDown3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ChangePassword_Click(sender, e);
            }
        }

    }
}
