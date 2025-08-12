# ☎️ PhoneBook-C# - Simple Console Phone Book Application

<p align="center">
  <img src="image/PhoneBook.png" alt="PhoneBook Logo" width="300"/>
</p>

Welcome to **PhoneBook-CSharp**, a clean and efficient console-based phone book application written in C# that lets you manage your contacts with ease.

---

## 🧩 Overview

PhoneBook-CSharp provides a straightforward way to:

* ✅ **Add new contacts** with name and phone number  
* 🗂️ **Display all saved contacts** sorted alphabetically by name  
* ❌ **Delete contacts** by name or phone number  
* ✏️ **Edit existing contacts** (update name or number)  
* 🔍 **Search contacts** by partial or full name/phone number  
* 🎨 **Console colors** for better user interaction and feedback  
* ✅ **Validate phone numbers** to ensure only 10-digit numeric input 

---

## 🔧 Features

### ✅ Contact Management

* Add, view, edit, and delete contacts  
* Store contacts in an efficient Dictionary data structure (phone number as key)  
* Search contacts with case-insensitive partial matching  
* Validate phone numbers (only digits, exact length 10)  

### 🖥️ Console Interface

* Interactive menu-driven UI  
* Color-coded messages for success, errors, and prompts  

#### 🔒 Delete Confirmation  
Before deleting any contact, the program asks:  
**"Are you sure you want to delete? (y/n)"**  
This prevents accidental deletions and keeps your contacts safe.

#### 💾 Persistent Storage (Save & Load)  
Contacts are now automatically saved to a text file (`contacts.txt`) and loaded when the program starts.  
This means your contacts won’t be lost when you close the app!

#### 🧹 Clear All Contacts Option  
A new menu option **"Clear All Contacts"** allows you to delete all contacts at once, with a confirmation prompt to avoid mistakes.

---

## 📁 Project Structure

* `Program.cs` – Main entry point and all logic for the phone book app  
* Uses C# Dictionary to store contacts in-memory  
* Console.ReadLine and WriteLine for user input and output  

---

## 🚀 Getting Started

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine  
* A terminal or command prompt (VS Code terminal is perfect!)  

### Running the app

```bash
git clone https://github.com/Behnoushin/PhoneBook-CSharp
cd PhoneBook-CSharp

dotnet run
```

Crafted with 🩷 by Behnoushin (Behnoush Shahraeini)