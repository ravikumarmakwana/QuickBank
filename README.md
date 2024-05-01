# QuickBank API

This API is designed to facilitate banking operations such as customer management, account handling, transactions management, interest calculation, and fixed deposits using ASP.NET Core 8 and Azure Functions.

## Table of Contents

- [Database Design](#database-design)
- [Key Features](#key-features)
- [Technologies Used](#technologies-used)
- [Functionality](#functionality)
- [Azure Functions](#azure-functions)

## Database Design
  ![quickbank](https://github.com/ravikumarmakwana/QuickBank/blob/master/DB%20Design/Database.png)

## Key Features

- **JWT Based Authentication and Authorization**
- **Customer Management**
- **Account Management**
- **Interest Management**
- **Transaction Management**
- **Fixed Deposits Management**
 
## Technologies Used

- ASP.NET Core 8
- C#
- Azure Functions
- Entity Framework Core (EF Core)
- SQL Server
- Auto Mapper

## Functionality

- **AuthenticationService**: Facilitates user authentication by verifying credentials, issuing access tokens and refresh tokens, and managing token expiration for secure user authentication and authorization.
- **AccountService**: Manages account operations like creating, closing, retrieving accounts, and quarterly interest calculations and deposits.
- **CustomerService**: Handles customer data management, including CRUD operations for addresses, personal identity information (PII), email, phone numbers, and customer status.
- **FixedDepositService**: Orchestrates operations related to managing fixed deposits, including creation, closure, renewal, interest calculation, and retrieval of fixed deposits.
- **InterestService**: Calculates interest for accounts and fixed deposits based on specified parameters.
- **TransactionService**: Manages transaction-related operations such as deposit, withdrawal, fund transfer, transaction retrieval, and updates to account status.
- **UserService**: Handles user registration logic, including creating a new user and associated customer entity.

## Azure Functions

1. **AddQuarterlyInterest**: The **AddQuarterlyInterest** Azure Function is designed to run once every quarter to calculate daily interest for accounts and add it based on the account type.
2. **UpdateAccountStatus**: The **UpdateAccountStatus** Azure function executes daily to manage account statuses based on transaction activity.
    - Specifically, this function checks transaction history and updates account statuses according to predefined criteria: -
      - If there have been no transactions in the past 60 days, the account status is changed to Dormant state indicating reduced activity but still active.
      - If there have been no transactions in the past 90 days, the account status is changed to deactivated, reflecting extended inactivity.
3. **UpdateFixedDeposits**: The **UpdateFixedDeposits** Azure function executes daily to manage active Fixed Deposits.
    - Its primary tasks include: -
      - **Calculate and Store Interest**: This function computes and stores interest for all active Fixed Deposits (FDs) currently held by customers. It calculates interest based on the specific parameters of each FD, such as the type of FD (regular, cumulative, or non-cumulative) and the interest rate.
      - **Manage Maturing FDs**: For FDs that have matured (reached their end date), the function checks the user's preferences associated with each FD. Depending on the customer's choice:
        - If the preference is to close the FD, the function initiates the closure process.
        - If the preference is to renew the FD, the function automatically renews the FD with updated terms.
