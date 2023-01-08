using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Atm
{
    class Program
    {
        static void Main(string[] args)
        {
            // Welcome the user to the bank
            Console.WriteLine("Welcome to the bank!");

            // Create a dictionary to store the users and their accounts
            var users = new Dictionary<string, List<Account>> //TODO these also need t obe doubles somehow i would assume...
            {
                { "user1", new List<Account> { new Account("salary", 10000), new Account("savings", 50000) } },
                { "user2", new List<Account> { new Account("salary", 20000), new Account("savings", 30000) } },
                { "user3", new List<Account> { new Account("salary", 40000), new Account("savings", 60000) } },
                { "user4", new List<Account> { new Account("salary", 20000), new Account("savings", 100000) } },
                { "user5", new List<Account> { new Account("salary", 5000), new Account("savings", 7000) } }
            };

            // Keep asking for user input until the user logs out
            while (true)
            {
                // Ask the user for their user number and pin
                Console.Write("Enter your user number: ");
                string userNumber = Console.ReadLine();
                Console.Write("Enter your pin: ");
                string pin = Console.ReadLine();

                // Check if the user exists in the dictionary
                if (users.ContainsKey(userNumber) && users[userNumber][0].Pin == pin)
                {
                    // The user has successfully logged in, show the main menu
                    while (true)
                    {
                        Console.WriteLine("What do you want to do?");
                        Console.WriteLine("1. See your accounts and balance");
                        Console.WriteLine("2. Transfer between accounts");
                        Console.WriteLine("3. Withdraw money");
                        Console.WriteLine("4. Log out");                   //TODO its not actually logging me out :( just starting the menu over 

                        // Get the user's choice
                        Console.Write("Enter a number: ");
                        string choice = Console.ReadLine();

                        // Perform the chosen action
                        switch (choice)
                        {
                            case "1":
                                // Show the user's accounts and balances
                                Console.WriteLine("Your accounts:");
                                foreach (var account in users[userNumber])
                                {
                                    Console.WriteLine($"{account.Name}: {account.Balance}");
                                }
                                break;
                            case "2":
                                // Transfer money between accounts
                                Console.WriteLine("Select an account to transfer from:"); //TODO gotta swap from int to possibly doubles? 
                                for (int i = 0; i < users[userNumber].Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {users[userNumber][i].Name} ({users[userNumber][i].Balance})");
                                }
                                int from = int.Parse(Console.ReadLine()) - 1;
                                Console.WriteLine("Select an account to transfer to:");
                                for (int i = 0; i < users[userNumber].Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {users[userNumber][i].Name} ({users[userNumber][i].Balance})");
                                }
                                int to = int.Parse(Console.ReadLine()) - 1;
                                Console.Write("Enter the amount to transfer: ");
                                int amount = int.Parse(Console.ReadLine());
                                if (users[userNumber][from].Balance >= amount)
                                {
                                    users[userNumber][from].Balance -= amount;
                                    users[userNumber][to].Balance += amount;
                                    Console.WriteLine("Money transferred successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient funds to transfer the amount specified.");
                                }
                                break;
                            case "3":
                                // Withdraw money
                                Console.WriteLine("Select an account to withdraw from:");
                                for (int i = 0; i < users[userNumber].Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {users[userNumber][i].Name} ({users[userNumber][i].Balance})");
                                }
                                int withdrawFrom = int.Parse(Console.ReadLine()) - 1;
                                Console.Write("Enter the amount to withdraw: ");
                                int withdrawAmount = int.Parse(Console.ReadLine()); //TODO ive remembered being told in class that these cant hold Öre as in decimals so i gotta fix it 
                                if (users[userNumber][withdrawFrom].Balance >= withdrawAmount)
                                {
                                    Console.Write("Enter your pin to confirm: ");
                                    string confirmPin = Console.ReadLine();
                                    if (users[userNumber][withdrawFrom].Pin == confirmPin)
                                    {
                                        users[userNumber][withdrawFrom].Balance -= withdrawAmount;
                                        Console.WriteLine("Withdrawal successful!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect pin, withdrawal cancelled.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient funds to withdraw the amount specified.");
                                }
                                break;
                            case "4":
                                // Log out
                                Console.WriteLine("You have been logged out."); //TODO fix this part 
                                break;
                            default:
                                // Invalid selection
                                Console.WriteLine("Invalid selection.");
                                break;
                        }

                        // Wait for the user to press enter before showing the main menu again
                        Console.WriteLine("Press enter to go back to the main menu."); //TODO slightly buggy 
                        Console.ReadLine();
                    }
                }
                else
                {
                    // The user number or pin was incorrect
                    Console.WriteLine("Invalid user number or pin.");
                }
            }
        }
    }

    // Account class to represent an account
    class Account
    {
        public string Name { get; set; }
        public int Balance { get; set; }
        public string Pin { get; set; }

        public Account(string name, int balance, string pin = "0000")
        {
            Name = name;
            Balance = balance;
            Pin = pin;
        }
    }
}
