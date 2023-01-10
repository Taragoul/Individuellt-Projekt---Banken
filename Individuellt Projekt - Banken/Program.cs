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
            var users = new Dictionary<string, Account[]>
            {
                { "user1", new Account[] { new Account("salary", 10000.0, "1234"), new Account("savings", 60000.0, "1234") } },
                { "user2", new Account[] { new Account("salary", 20000.0, "5678"), new Account("savings", 70000.0, "5678") } },
                { "user3", new Account[] { new Account("salary", 30000.0, "9101"), new Account("savings", 80000.0, "9101") } },
                { "user4", new Account[] { new Account("salary", 40000.0, "1121"), new Account("savings", 90000.0, "1121") } },
                { "user5", new Account[] { new Account("salary", 50000.0, "3141"), new Account("savings", 10000.0, "3141") } }
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
                        Console.WriteLine("4. Log out");

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
                                Console.WriteLine("Select an account to transfer from:");
                                for (int i = 0; i < users[userNumber].Length; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {users[userNumber][i].Name} ({users[userNumber][i].Balance})");
                                }
                                int from = int.Parse(Console.ReadLine()) - 1;
                                Console.WriteLine("Select an account to transfer to:");
                                for (int i = 0; i < users[userNumber].Length; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {users[userNumber][i].Name} ({users[userNumber][i].Balance})");
                                }
                                int to = int.Parse(Console.ReadLine()) - 1;
                                Console.Write("Enter the amount to transfer: ");
                                double amount = double.Parse(Console.ReadLine());
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
                                for (int i = 0; i < users[userNumber].Length; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {users[userNumber][i].Name} ({users[userNumber][i].Balance})");
                                }
                                int withdrawFrom = int.Parse(Console.ReadLine()) - 1;
                                Console.Write("Enter the amount to withdraw: ");
                                double withdrawAmount = double.Parse(Console.ReadLine()) - 1;
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
                                Console.WriteLine("You have been logged out.");
                                break;
                            default:
                                // Invalid selection
                                Console.WriteLine("Invalid selection.");
                                break;
                        }
                        // Wait for the user to press enter before showing the main menu again
                        Console.WriteLine("Press enter to go back to the main menu.");
                        Console.ReadLine();
                        if (choice == "4")
                        {
                            break;  //Breaking out of while loop
                        }


                        else
                        {
                            // The user number or pin was incorrect
                            Console.WriteLine("Invalid user number or pin.");
                        }
                    }
                }
            }
        }


        // Account class to represent an account
        class Account
        {
            public string Name { get; set; }
            public double Balance { get; set; }
            public string Pin { get; set; }

            public Account(string name, double balance, string pin)
            {
                Name = name;
                Balance = balance;
                Pin = pin;
            }
        }
    }
}