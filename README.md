# Splitwise Clone

Welcome to the **Splitwise Clone**! This app simplifies managing shared expenses, whether it's splitting rent with roommates, sharing dinner bills, or organizing group trips. Keep track of who owes what and settle up with friends easily.
![image](https://github.com/user-attachments/assets/4a37fd51-f186-48c6-96a5-84740e928e23)


## Features

- **Track Expenses & IOUs**: Record and manage shared expenses.
- **Settle Up Effortlessly**: Easily clear outstanding balances with friends.
- **Transparent Records**: View clear financial histories and reports.

## Tech Stack

- **Backend**: C# with ASP.NET Core MVC
- **Frontend**: Razor Views (HTML, CSS, JavaScript)
- **Database**: Entity Framework Core with SQL Server
- **Security**: BCrypt password hashing

## Getting Started

### Prerequisites

- **.NET Core SDK**
- **SQL Server**
- **Entity Framework Core Tools**

### Setup

1. **Clone the repository**:

```bash
git clone https://github.com/your-username/splitwise-clone.git 
cd splitwise-clone
```


2. **Configure the database**: Update your connection string in `appsettings.json`.

3. **Apply migrations**:

```bash
dotnet ef database update
```

4. **Run the app**:

```bash
dotnet run
```
Access the app at `http://localhost:5000`.

## Project Overview

- **Controllers**: Handles user actions (signup, login, logout, expenses).
- **Models**: Defines core entities like `User`, `Transaction`, and `Participant`.
- **Views**: Razor views for rendering the front-end UI.

