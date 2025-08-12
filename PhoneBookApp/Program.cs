using System;
using System.Collections.Generic;
using System.IO;

namespace PhoneBook
{
    class Program
    {
        // Dictionary to store contacts: phone number as key, name as value
        static Dictionary<string, string> contacts = new Dictionary<string, string>();

        // File path for saving and loading contacts
        static string dataFile = "contacts.txt";

        static void Main(string[] args)
        {
            LoadContacts(); // Load contacts from file at program start

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to the simple Phone Book!");
            Console.ResetColor();

            while (true)
            {
                // Display the menu options in the new requested order
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Show Contacts");
                Console.WriteLine("3. Search Contact");
                Console.WriteLine("4. Edit Contact");
                Console.WriteLine("5. Delete Contact");
                Console.WriteLine("6. Clear All Contacts");
                Console.WriteLine("7. Exit");

                string choice = Console.ReadLine()!;

                // Handle user choice and call corresponding method
                if (choice == "1")
                {
                    AddContact();
                }
                else if (choice == "2")
                {
                    ShowContacts();
                }
                else if (choice == "3")
                {
                    SearchContact();
                }
                else if (choice == "4")
                {
                    EditContact();
                }
                else if (choice == "5")
                {
                    DeleteContact();
                }
                else if (choice == "6")
                {
                    ClearAllContacts();
                }
                else if (choice == "7")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Goodbye!");
                    Console.ResetColor();
                    break; // Exit the loop and end program
                }
                else
                {
                    // Invalid input handling
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option, please try again.");
                    Console.ResetColor();
                }
            }
        }

        // Validate if the phone number is exactly 10 digits and numeric
        static bool IsValidPhoneNumber(string phone)
        {
            if (phone.Length != 10)
                return false;

            foreach (char c in phone)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        // Add a new contact to the dictionary and save it to the file
        static void AddContact()
        {
            Console.Write("Enter contact name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter phone number: ");
            string phone = Console.ReadLine()!;

            // Validate phone number format
            if (!IsValidPhoneNumber(phone))
            {
                Console.WriteLine("Invalid phone number! It must be exactly 10 digits.");
                return;
            }

            // Check for duplicate phone numbers
            if (!contacts.ContainsKey(phone))
            {
                contacts.Add(phone, name);
                SaveContacts(); // Save contacts after adding
                Console.WriteLine("Contact added successfully!");
            }
            else
            {
                Console.WriteLine("This phone number already exists.");
            }
        }

        // Display all contacts sorted alphabetically by name
        static void ShowContacts()
        {
            Console.WriteLine("\nSaved Contacts:");

            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            var sortedContacts = new List<KeyValuePair<string, string>>(contacts);
            sortedContacts.Sort((a, b) => string.Compare(a.Value, b.Value, StringComparison.OrdinalIgnoreCase));

            foreach (var contact in sortedContacts)
            {
                Console.WriteLine($"Name: {contact.Value} - Phone: {contact.Key}");
            }
        }

        // Search contacts by partial name or phone number and display matches
        static void SearchContact()
        {
            Console.Write("Enter name or phone number to search: ");
            string query = Console.ReadLine()!;

            var foundContacts = new List<KeyValuePair<string, string>>();

            foreach (var contact in contacts)
            {
                if (contact.Key.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    contact.Value.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    foundContacts.Add(contact);
                }
            }

            if (foundContacts.Count == 0)
            {
                Console.WriteLine("No matching contacts found.");
            }
            else
            {
                Console.WriteLine($"\nFound {foundContacts.Count} contact(s):");
                foreach (var contact in foundContacts)
                {
                    Console.WriteLine($"Name: {contact.Value} - Phone: {contact.Key}");
                }
            }
        }

        // Edit existing contact by phone number or name with validations
        static void EditContact()
        {
            Console.Write("Enter phone number or name to edit: ");
            string input = Console.ReadLine()!;

            string? keyToEdit = null;

            // Find the contact key (phone) by phone or name input
            if (contacts.ContainsKey(input))
            {
                keyToEdit = input;
            }
            else
            {
                foreach (var contact in contacts)
                {
                    if (contact.Value.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        keyToEdit = contact.Key;
                        break;
                    }
                }
            }

            if (keyToEdit == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            Console.WriteLine($"Current Name: {contacts[keyToEdit]}, Phone: {keyToEdit}");

            Console.Write("Enter new name (leave empty to keep current): ");
            string newName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(newName))
            {
                newName = contacts[keyToEdit];
            }

            Console.Write("Enter new phone number (leave empty to keep current): ");
            string newPhone = Console.ReadLine()!;

            // Validate new phone number if provided
            if (!string.IsNullOrWhiteSpace(newPhone))
            {
                if (!IsValidPhoneNumber(newPhone))
                {
                    Console.WriteLine("Invalid phone number! It must be exactly 10 digits. Edit cancelled.");
                    return;
                }

                if (newPhone != keyToEdit && contacts.ContainsKey(newPhone))
                {
                    Console.WriteLine("This new phone number already exists. Edit cancelled.");
                    return;
                }
            }
            else
            {
                newPhone = keyToEdit;
            }

            // Update contact information and save
            contacts.Remove(keyToEdit);
            contacts.Add(newPhone, newName);
            SaveContacts();

            Console.WriteLine("Contact updated successfully.");
        }

        // Delete contact(s) by phone number or name with confirmation prompt
        static void DeleteContact()
        {
            Console.Write("Enter phone number or name to delete: ");
            string input = Console.ReadLine()!;

            var keysToRemove = new List<string>();

            // Find all matching contacts keys (phone numbers)
            if (contacts.ContainsKey(input))
            {
                keysToRemove.Add(input);
            }
            else
            {
                foreach (var contact in contacts)
                {
                    if (contact.Value.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        keysToRemove.Add(contact.Key);
                    }
                }
            }

            if (keysToRemove.Count == 0)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            // Ask for confirmation before deletion
            Console.WriteLine($"Are you sure you want to delete {keysToRemove.Count} contact(s)? (y/n)");
            string confirm = Console.ReadLine()!.ToLower();

            if (confirm == "y")
            {
                foreach (var key in keysToRemove)
                {
                    contacts.Remove(key);
                }
                SaveContacts(); // Save changes after deletion
                Console.WriteLine($"{keysToRemove.Count} contact(s) removed successfully.");
            }
            else
            {
                Console.WriteLine("Delete cancelled.");
            }
        }

        // Clear all contacts with confirmation prompt
        static void ClearAllContacts()
        {
            Console.WriteLine("Are you sure you want to delete ALL contacts? (y/n)");
            string confirm = Console.ReadLine()!.ToLower();

            if (confirm == "y")
            {
                contacts.Clear();
                SaveContacts(); // Save changes after clearing all
                Console.WriteLine("All contacts have been deleted.");
            }
            else
            {
                Console.WriteLine("Clear all cancelled.");
            }
        }

        // Save all contacts to the data file in "phone,name" format
        static void SaveContacts()
        {
            using StreamWriter writer = new StreamWriter(dataFile);
            foreach (var contact in contacts)
            {
                writer.WriteLine($"{contact.Key},{contact.Value}");
            }
        }

        // Load contacts from the data file into the dictionary
        static void LoadContacts()
        {
            if (!File.Exists(dataFile))
                return;

            using StreamReader reader = new StreamReader(dataFile);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    string phone = parts[0];
                    string name = parts[1];
                    if (!contacts.ContainsKey(phone))
                    {
                        contacts.Add(phone, name);
                    }
                }
            }
        }
    }
}
