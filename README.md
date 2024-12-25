# Talabat Order Management System

**Talabat** is a robust, web-based order management system designed to streamline the management of customers, restaurants, food orders, and payments. This project emphasizes scalability, maintainability, and user-friendliness with features tailored for both employees and customers.

## Features

- **Account Management**: User registration, login, and profile management with secure JWT-based authentication.
- **Basket Management**: Add, update, and delete items from the customer basket.
- **Order Management**:
  - Place, view, and track customer orders.
  - Delivery method selection for seamless service.
- **Payment Integration**: Secure payment processing with Stripe, including webhook support for real-time status updates.
- **Product Catalog**:
  - View products with filtering and pagination.
  - Browse categories and brands.
- **Caching**: Improved performance using response caching for product data.

## Technologies Used

- **Backend**: ASP.NET Core with RESTful APIs.
- **Frontend**: Angular for the customer-facing application.
- **Database**: SQL Server using Entity Framework Core.
- **Design Patterns**:
  - **Onion Architecture**: Ensures separation of concerns.
  - **UnitOfWork and Repository**: Organized and maintainable data access.

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node.js](https://nodejs.org/) and Angular CLI
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- **Redis** for caching (Ensure Redis is installed and running on your system)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Komyy45/Talabat.git
2. Navigate to the Project Directory
    ```bash
    cd Talabat

3. Restore Dependencies
   ```bash
    dotnet restore

5. Run the Backend Application
    ```bash
    dotnet run

6. Install Frontend Dependencies
7. Navigate to the Angular client app directory
    ```bash
    cd AngularClient

8. Install dependencies
   ```bash
    npm install

9. Run the Angular Client
    ```bash
    ng serve



## API Endpoints Overview

### Account Endpoints
- **POST /api/Account/login**: Login to the system.
- **POST /api/Account/register**: Register a new user.
- **GET /api/Account**: Retrieve the current user's information.
- **GET /api/Account/Address**: Get the logged-in user's address.
- **PUT /api/Account/Address**: Update the user's address.
- **GET /api/Account/EmailExists**: Check if an email already exists.

### Basket Endpoints
- **GET /api/Basket?id={id}**: Retrieve the customer's basket.
- **POST /api/Basket**: Update the customer's basket.
- **DELETE /api/Basket?id={id}**: Delete the customer's basket.

### Order Endpoints
- **POST /api/Orders**: Create a new order.
- **GET /api/Orders/{id}**: Retrieve order details by ID.
- **GET /api/Orders**: Get all orders for the logged-in user.
- **GET /api/Orders/DeliveryMethods**: Retrieve available delivery methods.

### Payment Endpoints
- **POST /api/Payments/{basketId}**: Create or update a payment intent for a basket.
- **POST /api/Payments/webhook**: Handle Stripe webhooks for payment updates.

### Product Endpoints
- **GET /api/Products**: Get a list of products with filtering and pagination.
- **GET /api/Products/{id}**: Retrieve details of a specific product by ID.
- **GET /api/Products/Brands**: Get a list of product brands.
- **GET /api/Products/Categories**: Get a list of product categories.

