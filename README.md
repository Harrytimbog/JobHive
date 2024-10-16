
# JobHive

JobHive is a job board application where job seekers can browse available job listings, employers can post job vacancies, and administrators can manage the platform. This application is built using **ASP.NET Core MVC** and allows for role-based access control to ensure employers can only manage their own job postings, while admins have full control over the app.

## Features

- **Job Seekers**: 
  - Browse job listings posted by employers.
  - View job details including title, description, company, and location.

- **Employers**: 
  - Post job vacancies.
  - Edit or delete job postings.
  - Manage only the job postings they created.
  
- **Admins**: 
  - Manage the entire platform.
  - Edit, delete, or approve any job postings.
  
## Technology Stack

- **Backend**: ASP.NET Core MVC
- **Frontend**: Razor Views with Bootstrap for styling
- **Database**: Entity Framework Core with SQL Server
- **Authentication and Authorization**: ASP.NET Core Identity
- **In-Memory Database** (for unit tests)

## Installation

To set up this project on your local machine, follow the steps below:

### Prerequisites

- [.NET 6.0 SDK or higher](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio 2022 or Visual Studio Code

### Steps

1. **Clone the repository**:

   ```bash
   git clone https://github.com/Harrytimbog/JobHive.git
   cd JobHive
   ```

2. ### Set up the database:

Open the `appsettings.json` file and update the connection string to point to your SQL Server instance:

```json
"ConnectionStrings": {
  "Database": "Server=YOUR_SERVER_NAME;Database=JobHiveDb;Trusted_Connection=True;"
}
```

Run the following commands in the terminal to apply the migrations and create the database:

```bash
dotnet ef database update
```

3. ### Run the application:

Use the following command to run the application:

```bash
dotnet run
```

The app should now be running at https://localhost:5001 or http://localhost:5000.

## Roles and Permissions

JobHive uses **ASP.NET Core Identity** to manage user roles. The following roles exist in the application:

    Admin: Can manage all job listings and users on the platform.
    Employer: Can create, edit, and delete only their own job postings.
    Job Seeker: Can browse job listings but cannot modify or manage any job postings.

## Usage

### Employers

    Register as an Employer and log in.
    Navigate to the Job Postings section and click Create to post a new job vacancy.
    Once a job is posted, you can edit or delete it, but only for the jobs you created.

### Job Seekers

    Browse the job listings on the homepage.
    Click on a job to see more details such as the description, company, and location.

### Admins

    Admin users have access to manage all job postings and users.
    Admins can also delete any inappropriate job postings or users from the platform.

## Screenshots

### Job Listings Page

Employer Job Management

## Testing

JobHive comes with unit tests for key parts of the application, including the repository and controllers. To run the tests, use the following command:

```bash
dotnet test
```

## Contributing

Contributions are welcome! If you'd like to improve this project, please fork the repository and create a pull request. Alternatively, open an issue to discuss changes.

    Fork the project.
    Create your feature branch (git checkout -b feature/NewFeature).
    Commit your changes (git commit -m 'Add some NewFeature').
    Push to the branch (git push origin feature/NewFeature).
    Open a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Contact

For any inquiries or questions, you can contact me at your-email@example.com.

```sql
```

You can now copy this entire markdown block into your `README.md` file! Let me know if you need any more adjustments.
