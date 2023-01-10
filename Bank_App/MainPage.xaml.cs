using System.IO;
using System.Reflection;
using Bank_App.Models;

namespace Bank_App;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();

        // Create required directories and files
        ProgramCore.CreateRequiredFiles();

    }
    private void HandleLoginClicked(object sender, EventArgs e)
    {
        // Get user data
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        User loadedUser = ProgramCore.Users.Login(username, password);

        if (loadedUser != null)
        {
            // Erase credential entries text
            usernameEntry.Text = String.Empty;
            passwordEntry.Text = String.Empty;  

            Navigation.PushAsync(new UserApplication(loadedUser));

        }
        else
        {
            // Display successfully message
            DisplayAlert("Invalid credentials", "Invalid username or password", "OK");
        }
        
        

    }

    private void HandleSignUpClicked(object sender, EventArgs e)
    {
        // REDIRECT TO SIGN UP HERE
        //DisplayAlert("Transaction Successful", "Signup script", "OK");
        Navigation.PushAsync(new SignupPage());

    }

}

