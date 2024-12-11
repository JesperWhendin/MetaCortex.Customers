# Group Project: Microservices

This repository is part of a group project where we are developing a distributed application composed of several microservices. Each team member is collectively responsible for an API Gateway, aswell as individually responsible for a specific part of the system:

- [MetaCortex.App](https://github.com/GabrielRai/MetaCortex.App) - Maintained by the whole group, contains the API Gateway.
- MetaCortex.Customers - Maintained by me, responsible for customer management (this repository).
- [MetaCortex.Orders](https://github.com/anders0b/MetaCortex.Orders) - Maintained by [Anders0b](https://github.com/anders0b), responsible for order management.
- [MetaCortex.Payments](https://github.com/Heimbrand/MetaCortex.Payments) - Maintained by [Heimbrand](https://github.com/Heimbrand), responsible for handling payments.
- [MetaCortex.Products](https://github.com/GabrielRai/MetaCortex.Products) - Maintained by [GabrielRai](https://github.com/GabrielRai), responsible for product information.

Together, these microservices, as well as an API-Gateway, form a complete system where each service has a clear role and responsibility.

- [MetaCortex.Full.Development](https://github.com/GabrielRai/MetaCortex.Full.Development)
   - The complete repository where everything is integrated into one solution.
   - Used for visualization and testing purposes.

---

## Customer API Documentation

## Overview
The Customer API provides endpoints to manage customer resources. Below is a detailed guide to each endpoint, including request and response details.

### Base URL
```
/api/customer
```

---

## Endpoints

### 1. Get All Customers
**GET** `/`

Retrieve all customers from the database.

#### Request
- No parameters required.

#### Response
- **200 OK**: Returns a list of customers.
- **404 Not Found**: No customers found.

#### Example Response (200 OK)
```json
[
  {
    "id": "123",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "isvip": false,
    "allownotifications": false
  },
  {
    "id": "124",
    "name": "Jane Smith",
    "email": "jane.smith@example.com",
    "isvip": true,
    "allownotifications": true
  }
]
```

---

### 2. Get Customer by ID
**GET** `/{id}`

Retrieve a customer by their unique ID.

#### Request
- **Path Parameter**: `id` (string) - The unique ID of the customer.

#### Response
- **200 OK**: Returns the customer object.
- **404 Not Found**: Customer not found.

#### Example Response (200 OK)
```json
{
    "id": "123",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "isvip": false,
    "allownotifications": false
}
```

---

### 3. Get Customer by Email
**GET** `/email/{email}`

Retrieve a customer by their email address.

#### Request
- **Path Parameter**: `email` (string) - The email address of the customer.

#### Response
- **200 OK**: Returns the customer object.
- **404 Not Found**: Customer not found.

#### Example Response (200 OK)
```json
{
    "id": "123",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "isvip": false,
    "allownotifications": false
}
```
---

### 4. Get Customer by notifications allowed.
**GET** `/notifications`

Retrieve a list of customers with notifications allowed.

### Request
- No parameters required.

#### Response
- **200 OK**: Returns a list of customers.
- **404 Not Found**: No customers found.

#### Example Response (200 OK)
```json
[
  {
    "id": "123",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "isvip": false,
    "allownotifications": false
  },
  {
    "id": "124",
    "name": "Jane Smith",
    "email": "jane.smith@example.com",
    "isvip": true,
    "allownotifications": true
  }
]
```

---

### 5. Add a Customer
**POST** `/`

Add a new customer to the database.

#### Request
- **Body**: JSON object containing customer details.
  ```json
  {
    "name": "John Doe",
    "email": "john.doe@example.com",
    "isvip": false,
    "allownotifications": false
  }
  ```

#### Response
- **201 Created**: Customer successfully created. Returns the created customer object with its ID.

#### Example Response (201 Created)
```json
{
    "id": "123",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "isvip": false,
    "allownotifications": false
}
```

---

### 6. Update a Customer
**PUT** `/`

Update an existing customer in the database.

#### Request
- **Body**: JSON object containing updated customer details.
  ```json
  {
    "id": "123",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "isvip": false,
    "allownotifications": false
  }
  ```

#### Response
- **200 OK**: Customer successfully updated. Returns the updated customer object.

#### Example Response (200 OK)
```json
{
    "id": "123",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "isvip": false,
    "allownotifications": false
}
```

---

### 7. Delete a Customer
**DELETE** `/{id}`

Delete a customer by their unique ID.

#### Request
- **Path Parameter**: `id` (string) - The unique ID of the customer to be deleted.

#### Response
- **200 OK**: Customer successfully deleted.

#### Example Response (200 OK)
```json
"Entity deleted."
```
---

## Summary

- **GET /api/customer** - Retrieves all customers.
- **GET /api/customer/id/{id}** - Retrieves a customer by ID.
- **GET /api/customer/email/{email}** - Retrieves a customer by email.
- **GET /api/customers/notifications** - Retrieves all customers that allow notifications.
- **POST /api/customer** - Creates a new customer.
- **DELETE /api/customer/{id}** - Deletes a customer by ID.
- **PUT /api/customer** - Updates an existing customer.
