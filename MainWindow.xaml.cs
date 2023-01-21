using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Przychodnia_finally
{

    public partial class MainWindow : Window
    {
        Placówka p;
        public MainWindow()
        {
            p = new();
            InitializeComponent();
        }

        public MainWindow(Placówka placówka) : this()
        {
            p = placówka;
        }

        //Przycisk przekierowuje do okienka rejestracji pacjenta
        private void ZarejestrujSie_Button_Click(object sender, RoutedEventArgs e)
        {
            Tworzenie_Konta_Pacjenta objSecondWindow = new Tworzenie_Konta_Pacjenta(p);
            this.Visibility = Visibility.Hidden;
            objSecondWindow.Show();

        }

        //Zmiana wartości comboboxa decyduje o występowaniu przycisku rejestracji konta
        private void Profesja_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Jesli  profesja rozna od pacjenta to przycisk rejestracji znika. W przeciwnym razie pojawia sie
            if (Profesja.SelectedIndex != 1)
            {
                ZarejestrujSie_Button.Visibility = Visibility.Hidden;
            }
            else
            {
                ZarejestrujSie_Button.Visibility = Visibility.Visible;
            }
        }

        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        private void MenuZapisz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                int ad = p.Konta.Count;
                p.ZapiszDC(filename);
            }
        }

        private void MenuOtworz_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Plik XML (*.xml)|*.xml";
            bool? result= openFileDialog.ShowDialog();
            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                p = Placówka.OdczytDC(filePath);
            }
        }
        private void ShowPasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Visible;
            PasswordHidden.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = PasswordHidden.Password;
            Eye_Close.Source = new BitmapImage(new Uri(@"img/oko2.jpg", UriKind.Relative));
        }

        private void HidePasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Hidden;
            PasswordHidden.Visibility = Visibility.Visible;
            Eye_Close.Source = new BitmapImage(new Uri(@"/oko.png", UriKind.Relative));
        }
        private void Zaloguj_Button_Click(object sender, RoutedEventArgs e)
        {
            int ad = p.Konta.Count;
            if (Login_Text.Text.Length > 0 && PasswordHidden.Password.Length == 0)
            {
                MessageBox.Show("Pole hasło nie może być puste!");
            }
            else if (Login_Text.Text.Length == 0 && PasswordHidden.Password.Length > 0)
            {
                MessageBox.Show("Pole login nie może być puste!");
            }
            else if (Login_Text.Text.Length == 0 && PasswordHidden.Password.Length == 0)
            {
                MessageBox.Show("Proszę uzupełnić login i hasło");
            }
            else
            {
                string password = PasswordHidden.Password;
                if (Login_Text.Text == "ADMIN" && password.ToString() == "ADMIN" && Profesja.Text.ToString() == "ADMIN")
                {
                    MessageBox.Show("Logowanie poprawne");
                    Przychodnia_ADMIN objSecondWindow = new Przychodnia_ADMIN(p);
                    this.Visibility = Visibility.Hidden;
                    objSecondWindow.Show();
                }
                
                else if (p.Konta.ContainsKey(Login_Text.Text))
                {
                    if (p.Konta[Login_Text.Text] == PasswordUnmask.Text.ToString() || p.Konta[Login_Text.Text] == password.ToString())
                    {
                        if (Profesja.Text.ToString() == "Patient")
                        {
                            if (p.Pacjenci.Find(p => p.Pesel == Login_Text.Text) != null)
                            {
                                MessageBox.Show("Logowanie poprawne");
                                Przychodnia_Pacjent objSecondWindow = new Przychodnia_Pacjent(p, Login_Text.Text);
                                this.Visibility = Visibility.Hidden;
                                objSecondWindow.Show();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Złe hasło!");
                                return;
                            }
                        }
                        if (Profesja.Text.ToString() == "Doctor")
                        {

                            if (p.Lekarze.Find(p => p.Pesel == Login_Text.Text) != null)
                            {
                                MessageBox.Show("Logowanie poprawne");
                                Przychodnia_Doktor objSecondWindow = new Przychodnia_Doktor(p, Login_Text.Text, password.ToString(), "Lekarz");
                                this.Visibility = Visibility.Hidden;
                                objSecondWindow.Show();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Złe hasło!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Złe hasło!");
                    }
                }
                else
                {
                    MessageBox.Show("Złe hasło!");
                }
            }
        }
    }
}