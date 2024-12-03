# Customer API Documentation

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
    "email": "john.doe@example.com"
  },
  {
    "id": "124",
    "name": "Jane Smith",
    "email": "jane.smith@example.com"
  }
]
```

---

### 2. Get Customer by ID
**GET** `/id/{id}`

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
  "email": "john.doe@example.com"
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
  "email": "john.doe@example.com"
}
```

---

### 4. Add a Customer
**POST** `/`

Add a new customer to the database.

#### Request
- **Body**: JSON object containing customer details.
  ```json
  {
    "name": "John Doe",
    "email": "john.doe@example.com"
  }
  ```

#### Response
- **201 Created**: Customer successfully created. Returns the created customer object with its ID.

#### Example Response (201 Created)
```json
{
  "id": "123",
  "name": "John Doe",
  "email": "john.doe@example.com"
}
```

---

### 5. Update a Customer
**PUT** `/`

Update an existing customer in the database.

#### Request
- **Body**: JSON object containing updated customer details.
  ```json
  {
    "id": "123",
    "name": "John Doe",
    "email": "john.doe@example.com"
  }
  ```

#### Response
- **200 OK**: Customer successfully updated. Returns the updated customer object.

#### Example Response (200 OK)
```json
{
  "id": "123",
  "name": "John Doe",
  "email": "john.doe@example.com"
}
```

---

### 6. Delete a Customer
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
- **POST /api/customer** - Creates a new customer.
- **DELETE /api/customer/{id}** - Deletes a customer by ID.
- **PUT /api/customer** - Updates an existing customer.
