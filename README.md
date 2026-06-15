# Products API - Technical Assessment

## Overview

This project is a RESTful Web API developed using .NET 8 and Clean Architecture principles.

The application provides Product CRUD operations and demonstrates:

* Clean Architecture
* CQRS using MediatR
* Repository Pattern
* Unit of Work Pattern
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger Documentation
* xUnit & Moq Unit Testing
* Docker Support

---

## Project Structure

Products.API

* Controllers
* Authentication
* Swagger Configuration

Products.APPLICATION

* Commands
* Queries
* DTOs
* Validators
* Business Logic

Products.DOMAIN

* Entities

Products.INFRASTRUCTURE

* DbContext
* Repositories
* Unit Of Work
* Token Service

---

## Technologies Used

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* MediatR
* FluentValidation
* JWT Authentication
* Swagger/OpenAPI
* xUnit
* Moq
* Docker

---

## Authentication

JWT Authentication has been implemented.

### Login Endpoint

POST /api/authorize/login

After successful login, use the generated JWT token in Swagger Authorization.

Format:

Bearer {token}

---

## API Endpoints

### Products

GET /api/products

GET /api/products/{id}

POST /api/products

PUT /api/products/{id}

DELETE /api/products/{id}

---

## Testing

Implemented using:

* xUnit
* Moq
* FluentAssertions

Covered Scenarios:

* Add Product
* Update Product
* Delete Product
* Get Product By Id
* Get All Products

---

## Running Locally

### Restore Packages

dotnet restore

### Build

dotnet build

### Apply Migration

dotnet ef database update

### Run

dotnet run

---

## Swagger

After running the application:

[https://localhost:{port}/swagger](https://localhost:{port}/swagger)

---

## Docker

### Build Image

docker build -t products-api .

### Run Container

docker run -d -p 8080:8080 --name products-container products-api

### Swagger in Docker

http://localhost:8080/swagger

---

## Design Patterns

* Clean Architecture
* CQRS
* Repository Pattern
* Unit Of Work
* Dependency Injection

---

## Author

Ankush Rai
