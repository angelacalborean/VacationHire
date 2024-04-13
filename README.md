
# Vacation HIRE

Vacation Hire is a solution developed in .net 8 that will allow you to manage the rental of different assets, handling of invoices.

## Features
- Inventory management handled by the VacationHire.AdministrativeApi 
- Invoice management handled by VacationHire.InvoicesApi
- Rental management handled by VacationHire.RentalsApi
- Rental approval workflow - wip
- Return approval workflow - wip

## Requirements
- An SQL Database was used for persistence layer; In order to start the application you should create a database (local or in cloud), create the schema (see the script under SqlScripts folder) and update the connection string in the settings
- For the approval workflow an Azure Storage Queue was created, now the code has an hardcoded dummy value: either comment out the code that add messages to queue or create a queue and make sure you have the needed permissions
- For retriving the exchange rate a API access key is needed
- Basic authentication is required for accessing the endpoints and access to different endpoints is granted based on authenticated user different roles
- Use "user@vacationhire.com" and "DummyPassword"
- In order to run integration tests, you need to have jMeter installed



## Documentation
All the REST API documentation is available in the Swagger UI.

From technical perspective, the solution is divided in 3 REST API projects, that can be deployed independently and that share the same database.
Each project deals with a different aspect of the business and should access the database using different roles based on their needs.

### System architecture
![Architecture](https://github.com/angelacalborean/VacationHire/blob/main/Documentation/Vacation%20Hire%20-%20Physical%20Architecture.png)

### Rental process
Logged in user wants to rent an item
![Initating rental process](https://github.com/angelacalborean/VacationHire/blob/main/Documentation/Vacation%20Hire%20-%20Initiating%20Rental.png)

Checking and processing a rental
![Rental processing](https://github.com/angelacalborean/VacationHire/blob/main/Documentation/Vacation%20Hire%20-%20Processing%20rental.png)

Return process
![Return process](https://github.com/angelacalborean/VacationHire/blob/main/Documentation/Vacation%20Hire%20-%20User%20returns%20car.png)

## Administrative API Reference

Run first this project if you want to populated the database with default values.

Used by an administrator to add new assets / delete them.

For testing purposes you should call to see the available cars and retrieve an car id to work with it.

#### Get all carassets

```https
  GET /api/carassets
```

## Rentals API Reference

After you have an valid car asset it, you can test the rental process. Calling user should be authenticated and have the needed roles.

#### Rent a car

```https
  GET /api/cars/{itemId}/rent
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `itemId`      | `int` | **Required**. The id of the asset to rent. |

An acknowledgement response is sent back to the user.
