# ExpenseTracker-dotnet

**ExpenseTracker-dotnet** is an open-source web application developed using ASP.NET Core MVC, designed to help users efficiently manage and track their expenses. The application offers a user-friendly interface for recording, categorizing, and analyzing financial transactions, empowering users to make informed financial decisions.

## Features

- **User Authentication**: Secure user registration and login system to protect personal financial data.
- **Expense Management**: Add, edit, and delete expense records with detailed information such as amount, category, and date.
- **Category Organization**: Create and manage custom expense categories for better organization.
- **Data Visualization**: Interactive charts and graphs to visualize spending patterns and trends.
- **Responsive Design**: Optimized for various devices, ensuring accessibility on desktops, tablets, and smartphones.

## Technologies Used

- **Backend**: ASP.NET Core MVC
- **Frontend**: HTML, CSS, JavaScript
- **Database**: SQL Server
- **UI Components**: SyncFusion Components

## Installation

To set up the ExpenseTracker-dotnet application locally, follow these steps:

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/burakpekisik/ExpenseTracker-dotnet.git
   ```

2. **Navigate to the Project Directory**:

   ```bash
   cd ExpenseTracker-dotnet
   ```

3. **Restore NuGet Packages**:

   Open the solution in Visual Studio and restore the NuGet packages. Alternatively, you can use the .NET CLI:

   ```bash
   dotnet restore
   ```

4. **Configure the Database**:

   - Update the connection string in `appsettings.json` to match your SQL Server instance.
   - Apply the database migrations to set up the necessary tables:

     ```bash
     dotnet ef database update
     ```

5. **Run the Application**:

   Start the application using Visual Studio or the .NET CLI:

   ```bash
   dotnet run
   ```

   The application will be accessible at `http://localhost:5000`.

## Usage

1. **Access the Application**: Open your web browser and navigate to `http://localhost:5000`.
2. **Register an Account**: Create a new user account to start managing your expenses.
3. **Add Expenses**: Use the 'Add Expense' feature to record new transactions, specifying details such as amount, category, and date.
4. **View Reports**: Access the dashboard to view visual reports of your spending patterns.

## License

ExpenseTracker-dotnet is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.

## Acknowledgments

- YouTube(https://www.youtube.com/watch?v=zQ5eijfpuu8&t=796s): Used for the base for the project.

## Disclaimer

This application is intended for personal use and educational purposes. The maintainers are not responsible for any financial decisions or actions taken based on the use of this software. 
