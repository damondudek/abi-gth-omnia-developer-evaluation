# abi-gth-omnia-developer-evaluation

This project is built using **C# .NET Core**, with integrations for **RabbitMQ**, **Rebus**, and **Entity Framework**. It serves as a starting point for creating APIs that handle authentication, cart management, product details, user management, and sales operations.

---

## Getting Started

### Prerequisites
To run this project, ensure the following tools are installed on your system:
- **.NET Core SDK (v8.0)**: [Download here](https://dotnet.microsoft.com/)
- **Docker** (optional): For containerized deployment
- **RabbitMQ**: Messaging system setup ([RabbitMQ setup guide](https://www.rabbitmq.com/documentation.html))
- **PostgreSQL**: For database operations ([PostgreSQL Documentation](https://www.postgresql.org/docs/))
- **Postman**: For testing APIs ([Postman Documentation](https://documenter.getpostman.com/view/27971159/2sB2cPkR3g))

---

## How to Run

### Database Migrations
- Before running the application, set up the database by creating and applying migrations
- Run it from **Ambev.DeveloperEvaluation.ORM** project path


1. **Create a new migration**:
    ```bash
    dotnet ef migrations add MigrationName
    ```

2. **Apply the migrations to the database**:
    ```bash
    dotnet ef database update
    ```

Ensure that the correct `.csproj` file is specified in the `--project` option.

---

## Application APIs

### Planned APIs:
- **Authentication API**: Handles login via username (not email).
- **Cart API**: Manages cart operations (clarification needed on the "date" field in this API – is it creation data? If so, keep it within the system domain).
- **Products API**: Fetch product information and manage inventory.
- **Users API**: Handles user registration, updates, and roles.
- **Sales API**: Mentioned in the documentation but needs alignment with the first-page requirements.

---

## Common Issues and Clarifications

1. **Authentication Details**:
   - Username is the preferred method of authentication.
   - Email-based authentication is not supported.

2. **Cart API Date Field**:
   - If this date refers to cart creation, it should remain part of the system domain for consistency.

3. **API Confusion**:
   - The documentation mentions creating APIs for authentication, carts, products, and users. However, the first page references a "sales" API. Ensure project requirements align with this detail for proper implementation.

---

## Postman Documentation

For detailed API testing and examples, refer to the [Postman Documentation](https://documenter.getpostman.com/view/27971159/2sB2cPkR3g).

---

## Dependencies

### Core Technologies
- **C# .NET Core 8**: Framework for building the application
- **Entity Framework**: ORM for database interactions
- **RabbitMQ**: Message broker for communication
- **Rebus**: Library for message-based workflows
- **PostgreSQL**: Relational Database

---

## Additional Notes

- Ensure RabbitMQ is running before starting the application.
- Align with the client’s specifications for creating APIs (authentication, carts, products, users, and potentially sales).

---

Feel free to adapt or expand this documentation based on new requirements or further clarifications. Let me know if you’d like additional sections or deeper explanations!