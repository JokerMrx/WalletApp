# Project Setup Instructions

## Prerequisites
Make sure you have the following installed on your machine:
- [.NET SDK](https://dotnet.microsoft.com/download) (.NET 7).
- [PostgreSQL](https://www.postgresql.org/download/)

## Cloning the Repository
To start working with the project, clone it to your local machine using the following command:

```bash
git clone https://github.com/JokerMrx/WalletApp.git
```

Navigate to the project folder:

```bash
cd WalletApp
```

## Setting Up Environment Variables
1. Create an .env file in the root of your project if it doesn’t already exist.
2. Add the following line to define your PostgreSQL connection string:

   ```bash
   DATABASE_URL="Host=<Host>;Port=<Post>;Database=<Database_Name>;Username=<Username>;Password=<Password>"
   ```

## Running Migrations
To apply migrations and create the database, run the following commands:

1. Open a terminal in the project’s root folder.
2. Run the command to update the database using Entity Framework:

   ```bash
   dotnet ef database update --project WalletApp.BL/ --startup-project WalletApp.API/
   ```

> **Note:** Ensure that Entity Framework Tools are installed. If they aren’t, you can install them by running:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

## Additional Commands for Migrations
- **Creating a new migration**:
   ```bash
   dotnet ef migrations add MigrationName --project ./WalletApp.BL/
   ```
- **Updating the database to the latest version**:
   ```bash
   dotnet ef database update --project WalletApp.BL/ --startup-project WalletApp.API/
   ```

## Running the Project
After applying migrations, you can run the project with:

```bash
dotnet run
```

The project will be available at [https://localhost:7023](http://localhost:7023) (or another URL depending on the configuration).
