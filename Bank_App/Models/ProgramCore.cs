using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bank_App.Models
{
    public static class ProgramCore
    {

        public static string directoryPath = "C:\\Users\\Edy\\Dropbox\\PC\\Desktop\\Bank_App\\Bank_App\\Bank_App";
        public static string usersPath = directoryPath + "//Users";
        public static string transactionsPath = directoryPath + "//transactions.txt";

        // ? represents the type of transaction
        // {} represents the username of the user that has finalised transaction
        // <> represents the amount
        private static string TRANSACTION_TEMPLATE = "? {}~<>";

        public static void CreateRequiredFiles()
        {
            if(!Directory.Exists(usersPath))
                Directory.CreateDirectory(usersPath);

            if (!File.Exists(transactionsPath))
                File.WriteAllText(transactionsPath, String.Empty);
        }




        public static class Users
        {
            public static bool CreateAccount(string name, string username, string password)
            {
                // Check if username already exists in database
                if (CheckUsernameExists(username))
                    return false;

                // Create an new user object
                BankAccount newBankAcc = new BankAccount(CreateIdNumber());
                User newUser = new User(name, username, password, newBankAcc);

                // Save new user to its own file located in Users directory
                SaveToDatabase(newUser);

                return true;
            }

            public static User Login(string username, string password)
            {
                // Check if username file does exist in database
                string path = usersPath + "//" + username + ".json";

                if(File.Exists(path))
                {
                    // Check for credentials
                    User loadedUser = LoadAccount(username);

                    // Check if input password matches the password read from database
                    if(String.Equals(password, loadedUser.Password))
                    {
                        // Login accepted
                        return loadedUser;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            public static void SaveToDatabase(User newUser)
            {
                string path = usersPath + "//" + newUser.Username + ".json";

                //Convert object to json
                string json = JsonConvert.SerializeObject(newUser);

                //Write json to a file
                File.WriteAllText(path, json);
            }
            

            public static User LoadAccount(string username)
            {
                // Check if username does exist in database
                if (!CheckUsernameExists(username))
                    return null;

                string path = usersPath + "//" + username + ".json";
                string json = File.ReadAllText(path);

                User loadedUser = JsonConvert.DeserializeObject<User>(json);
                return loadedUser;
            }

            private static string CreateIdNumber()
            {
                string newId = "";

                for(int i = 0; i < 10; i++)
                {
                    int randomNumber = new Random().Next(1, 11);

                    newId += randomNumber.ToString();
                }

                return newId;
            }

            private static bool CheckUsernameExists(string username)
            {
                string path = usersPath + "//" + username + ".json";

                return File.Exists(path);
            }
        
        }



        public static class Transaction
        {
            public static void Deposit(User user, double amount)
            {
                user.Account.Balance += amount;

                Users.SaveToDatabase(user);

                AddTransaction("deposit", user.Username, amount);
            }

            public static bool Withdraw(User user, double amount)
            {
                if(user.Account.Balance >= amount)
                {
                    user.Account.Balance -= amount;

                    Users.SaveToDatabase(user);

                    AddTransaction("withdrawal", user.Username, amount);

                    return true;
                }

                return false;
            }
        
            public static List<string> TransactionList()
            {
                return File.ReadAllLines(transactionsPath).ToList();
            }


            private static void AddTransaction(string transactionType, string username, double amount)
            {
                string transactionHistory = File.ReadAllText(transactionsPath);
                string transactionDetails = TRANSACTION_TEMPLATE.Replace("?", transactionType)
                    .Replace("{}", username)
                    .Replace("<>", amount.ToString());


                File.WriteAllText(transactionsPath, transactionHistory + "\n" + transactionDetails);
            }
            
        }



    }
}
