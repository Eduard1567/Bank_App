using Bank_App.Models;

namespace Bank_App;

public partial class UserApplication : ContentPage
{
	private User user;

	public UserApplication(User loadedUser)
	{
		InitializeComponent();

		this.user = loadedUser;

		nameLabel.Text = "Welcome " + user.Name;
		balanceLabel.Text = "Balance: " + user.Account.Balance.ToString();
    }


	public void OnDepositClicked(object sender, EventArgs e)
	{
		double depositAmount;
		try
		{
			depositAmount = double.Parse(depositAmountEntry.Text);
		}
		catch
		{
            DisplayAlert("Invalid amount", "You should enter a valid amount of money!", "OK");
			depositAmountEntry.Text = String.Empty;
			return;
        }

		ProgramCore.Transaction.Deposit(user, depositAmount);

		DisplayAlert("Success", depositAmount.ToString() + " have been successfully added to your account!", "OK");

		// Load updated account
		this.user = ProgramCore.Users.LoadAccount(user.Username);

		// Update balance in GUI
		balanceLabel.Text = "Balance: " + user.Account.Balance.ToString();
	}

    public void OnWithdrawClicked(object sender, EventArgs e)
    {
		double withdrawalAmount = double.Parse(withdrawAmountEntry.Text);

		if(!ProgramCore.Transaction.Withdraw(user, withdrawalAmount))
		{
			DisplayAlert("Invalid amount", "You don't have enough money in your account!", "OK");
        }
		else
		{
            DisplayAlert("Success", "You've successfully withdrawn " + withdrawalAmount + " from your account!", "OK");

            // Load updated account
            this.user = ProgramCore.Users.LoadAccount(user.Username);

            // Update balance in GUI
            balanceLabel.Text = "Balance: " + user.Account.Balance.ToString();
        }


    }

    public async void OnLogoutClicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Logout", "Are you sure you want to log out of your account?", "Yes", "No");

		if (result)
			await Navigation.PopAsync();
		

    }

	public async void OnDeleteAccountClicked(object sender, EventArgs e)
	{
        var result = await DisplayAlert("Logout", "Are you sure you want to log out of your account?", "Yes", "No");

        if (result)
            await Navigation.PopAsync();

    }

    public void OnTransactionHistoryClicked(object sender, EventArgs e)
    {
		string transactionHistory = "";

		foreach(string t in ProgramCore.Transaction.TransactionList())
		{
			if(t.Contains(user.Username))
			{
				string transactionType = t.Substring(0, t.IndexOf(" "));
				string transactionAmount = t.Substring(t.IndexOf('~') + 1);
                string transactionDetail = "Type of transaction: " + transactionType
					+ ". Amount: " + transactionAmount;

				transactionHistory += transactionDetail + "\n";
			}	
		}

        DisplayAlert("Transactions List", transactionHistory, "OK");

    }
}