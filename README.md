Taskify
This project is a backend Task Management System developed using C# ASP.NET Core 8, following Clean Architecture principles. It offers comprehensive functionality including token-based authentication, CRUD operations for tasks and users, task assignment, user privilege management, and various architectural patterns such as MediatoR, CQRS pattern, Automapper, and Repository Pattern. To facilitate easy deployment and scalability, the system is containerized using Docker and managed with Docker Compose, which includes three services: the task service, PostgreSQL database, and PGAdmin to visualize and interact with the database.

Features
Token-Based Authentication
CRUD Functionality for Tasks and Users.
Task Assignment
Fluent Validation: Utilizes Fluent Validation for input validation to ensure data integrity.
MediatoR: Implements MediatoR for handling requests and responses, promoting better organization and maintainability of application logic.
CQRS Pattern: Utilizes the CQRS pattern for improved separation of concerns and scalability by segregating command and query responsibilities.
Automapper: Uses Automapper for object-to-object mapping, simplifying data transfer between layers and reducing boilerplate code.
Repository Pattern: Follows the Repository Pattern for data access, enhancing code maintainability and facilitating unit testing.
Clean Architecture Principles: Built following Clean Architecture principles, enhancing maintainability, scalability, and testability of the system.
Swagger Documentation: Interactive API documentation provided through Swagger.
Global Exception Handling: Implements global exception handling to ensure robustness and reliability, providing consistent error handling throughout the application.
Docker Containerization: The application is containerized using Docker, enabling easy deployment and scalability across various environments.
Docker Compose: Includes a Docker Compose file with three services: task service, PostgreSQL database, and PGAdmin for database visualization and interaction.


Setup Instructions
To set up the User Management System locally using Docker Compose, follow these steps:

Clone the Repository: Clone this repository to your local machine using git clone.
Navigate to Docker Compose File: Open a terminal or command prompt, navigate to the directory containing the Docker Compose file (docker-compose.yml).
Run Docker Compose: Execute the following command to start the Docker Compose services in detached mode:
docker-compose up -d

Conclusion
The User Management System is a robust and scalable backend solution built with modern technologies and best practices. From token-based authentication to global exception handling, every aspect of the application is designed to deliver a seamless user experience while ensuring reliability, security, and maintainability.

Contributing
Contributions to the User Management System are welcome! If you encounter any issues or have suggestions for improvements, feel free to comment.

Credits
The User Management System is developed and maintained by Amanuel Eshete.
