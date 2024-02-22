# ASP .NET Core 7.0 Web API, backend for Audiobook platform

It's an ASP .NET WEB API application with integrated authentication and authorization, user account management, CRUD operation with all basic entities including audiobook and audio files, sorting and filtering of audiobooks by different criteria, rating&views of audiobooks, and user reviews. The application uses Entity Framework Core to connect and manage a SQL Server database.

The client side of this app is [here](https://github.com/AdrianFoxy/AudioBookPlatformProject).
Also, click here to see the [demo](https://foxwithbook.azurewebsites.net/).

## How to Use
1. Clone the project from GitHub to your local machine.
2. Open the project in Visual Studio or your favorite code editor.
3. Set up the database connection string in the `appsettings.json` file and run the SQL Server database.
4. Make sure you have the EF (Entity Framework) tools installed. Now, to migrate do the following commands:
    `dotnet ef migrations add InitialMigrations`, `dotnet ef database update`
5. Set up the `Secret` for the token and `GoogleClientID` for Google auth in the `appsettings.json`. If you are using Azure key vault, set up `KeyVaultURL`.
6. Run the project and test the API through the Swagger interface.
