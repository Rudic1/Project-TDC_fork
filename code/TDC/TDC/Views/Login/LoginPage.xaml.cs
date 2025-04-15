using TDC.Services;
using TDC.Models;
using TDC.IRepository; // Wichtig für IAccountRepository
using TDC.Views; // für die MainPage Navigation (oder deine Zielseite)

namespace TDC.Views.Login;

public partial class LoginPage : ContentPage
{
    private readonly UserService _userService;
    private readonly IAccountRepository _accountRepository;

    public LoginPage(UserService userService, IAccountRepository accountRepository)
    {
        InitializeComponent();
        _userService = userService;
        _accountRepository = accountRepository;
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text ?? string.Empty;
        string password = PasswordEntry.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ErrorMessageLabel.Text = "Bitte Benutzername und Passwort eingeben.";
            ErrorMessageLabel.IsVisible = true;
            return;
        }

        // ---> HIER ist der korrekte Aufruf:
        Account? account = _accountRepository.AuthenticateUser(username, password);
        // ---> KEIN direkter Aufruf von GetDummyAccounts hier! <---

        if (account != null)
        {
            // Annahme: Dein UserService hat eine Methode wie Login(Account user)
            _userService.Login(account); // Passe dies an deinen UserService an!

            ErrorMessageLabel.IsVisible = false; // Fehlermeldung ausblenden

            // Erfolgreich eingeloggt -> zur Zielseite navigieren
            // Verwende relative Navigation oder stelle sicher, dass die absolute Route korrekt ist
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}"); // Beispiel für MainPage
        }
        else
        {
            ErrorMessageLabel.Text = "Falscher Benutzername oder Passwort.";
            ErrorMessageLabel.IsVisible = true;
        }
    }
}