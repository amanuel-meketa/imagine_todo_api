## Taskify

Taskify is a robust backend Task Management System developed using C# ASP.NET Core 8, designed around Clean Architecture principles. It incorporates key architectural patterns such as MediatoR, CQRS, Automapper, and Repository Pattern. For streamlined deployment and scalability, the system is containerized using Docker and orchestrated with Docker Compose.

### Features
- **Token-Based Authentication**
- **Fluent Validation:** Ensures data integrity through robust input validation.
- **MediatoR:** Organized request and response handling for improved maintainability.
- **CQRS Pattern:** Enhanced separation of concerns and scalability.
- **Automapper:** Simplifies object-to-object mapping.
- **Repository Pattern:** Facilitates data access.
- **Clean Architecture Principles:** Ensures maintainability, scalability, and testability.
- **Global Exception Handling:** Robust error handling for reliability.
- **Docker Containerization:** Easy deployment and scalability across environments.
- **Docker Compose:** Includes services for task management, PostgreSQL database, and PGAdmin.
- **Health Checker:** Monitors system health for reliability.
- **Rate Limiting:** Controls incoming request rates for security and performance.
- **Serilog and Seq Server:** Provides structured logging and centralized log management.
- **Swagger Documentation:** Interactive API documentation available via Swagger at [http://localhost:8080/swagger](http://localhost:8080/swagger).

### Setup Instructions
1. **Clone Repository:** Clone the repository to your local machine.
2. **Navigate to Docker Compose File:** Open a terminal and go to the directory with the Docker Compose file.
3. **Run Docker Compose:** Start the services using `docker-compose up -d`.
4. **Access Swagger Documentation:** Visit [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html) in your browser.

### Credentials
- **Admin Credentials:** admin@email.com / P@$$w0rd
- **User Credentials:** user@email.com / P@$$w0rd

### Important Note
Before executing any API, ensure you log in as an admin or a user and paste the response token into the Authorization in the Swagger by starting with "Bearer" and space like this:
Bearer efjsifjnifebibwfnejnf

### Conclusion
Taskify offers a seamless Task Management solution, leveraging modern technologies and best practices. With robust features and reliable performance, it ensures a smooth user experience while maintaining security and scalability.
