# Bank Dashboard Web Application

## Project Overview
The Bank Dashboard Web Application is an ASP.NET Core MVC-based web app designed to provide a structured and secure banking experience for both users and administrators. It follows a two-tier architecture, separating the application into a business layer that exposes web services and a presentation layer that serves the user interface.

## Project Architecture
This project utilizes the data web service built in tutorial 7 and expands upon it by creating a standalone ASP.NET Core MVC application. The architecture consists of two primary layers:

### 1. Business Layer (Web Services)
- This layer handles all business logic, data processing, and database interactions.
- It is accessible via the `/api/` endpoint.
- Implements RESTful web services using ASP.NET Web API.
- Provides APIs for retrieving user account information, processing money transfers, and fetching transaction history.
- Returns responses in JSON format.

### 2. Presentation Layer (Web UI)
- This layer serves as the user interface of the application.
- Accessible through the root `/` endpoint.
- Built using ASP.NET MVC, HTML, CSS, and JavaScript.
- Uses JavaScript (via Fetch API or Axios) to communicate with the business layer asynchronously.
- Dynamically updates UI components based on API responses.

### 3. Home Controller (Integration Point)
- Renders the initial webpage when users access the root URL (`localhost:port/`).
- Serves the HTML structure and includes JavaScript files.
- JavaScript files interact with the business layer APIs to fetch and display user data.

## Features
### User Dashboard
Registered users can manage their financial data and perform banking transactions through a user-friendly interface. Key features include:
1. **User Profile Information**
   - Display of user name and profile picture.
   - Contact information (email, phone number).
   - Options to update personal details like password and contact info.
2. **Account Summary**
   - Overview of the user’s accounts, including account numbers and current balances.
   - Quick access to transaction history.
3. **Transaction History**
   - View recent transactions with details like date, description, and amount.
   - Filtering options to view transactions within a specific date range.
4. **Money Transfer**
   - Users can transfer money between their own accounts or to other users.
   - Input validation for recipient details, amount, and description.
   - Error handling for invalid or failed transactions.
5. **Security Features**
   - Secure login/logout functionality.
   - Ensures that users can only access their own data.

### Admin Dashboard
The admin dashboard provides functionalities for bank administrators to manage users and transactions efficiently.
1. **Admin Profile Information**
   - Display of admin name and profile picture.
   - Contact information with options to update details.
2. **User Management**
   - Create, edit, or deactivate user accounts.
   - Reset user passwords.
   - Search users by name, account number, or other filters.
3. **Transaction Management**
   - View transaction records for all users.
   - Search, filter, and sort transactions.
4. **Security and Access Control**
   - Logs and audit trails for tracking admin activities and system changes.

## Technologies Used
- **Backend:** ASP.NET Core MVC, ASP.NET Web API, Entity Framework
- **Frontend:** HTML, CSS, JavaScript (Fetch API)
- **Database:** SQL Server (via Entity Framework)
- **API Client Library:** RestSharp

## Future Enhancements
- Implement two-factor authentication for added security.
- Add email/SMS notifications for transactions.
- Introduce role-based access control for better permission management.
- Enhance UI responsiveness for mobile devices.

## Authors
- [Mouktada Salman](https://github.com/MouktadaSalman)
- [Ahmed Youseif](https://github.com/Ahmedo-o)
- [M.Jauhar](https://github.com/MasterBam)

## License
This project is for educational purposes only.

