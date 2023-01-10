using Bank_App.Models;

namespace Bank_App;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
		InitializeComponent();
	}


	public async void HandleSignUpClicked(object sender, EventArgs e)
	{
		// Take information from user
		string name = nameEntry.Text;
		string username = usernameEntry.Text;
		string password = passwordEntry.Text;

		// Create new account
		if(ProgramCore.Users.CreateAccount(name, username, password))
		{
            // Display success message
            await DisplayAlert("Success", "New user has been successfully created", "Got it");

			// Redirect to login page
			await Navigation.PopAsync();
        }
        else
		{
			// Display error message with the reason of failure
			await DisplayAlert("Username creation failed", "Username is already taken! Try again", "Retry");

			// Erase text from textboxes
			nameEntry.Text = String.Empty;
			usernameEntry.Text = String.Empty;
			passwordEntry.Text = String.Empty;
		}
		
	}

    public void GoBackClicked(object sender, EventArgs e)
    {
		// Go back to login page
		Navigation.PopAsync();

    }
}