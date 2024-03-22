Taskify        
Taskify is a robust backend Task Management System developed using C# ASP.NET Core 8, designed around Clean Architecture principles. It incorporates key architectural patterns such as MediatoR, CQRS, Automapper, and Repository Pattern. For streamlined deployment and scalability, the system is containerized using Docker and orchestrated with Docker Compose. It comprises three services: the task service, PostgreSQL database, and PGAdmin for efficient database management.

Features              
Token-Based Authentication: Secure authentication mechanism for user access.                     
CRUD Functionality for Tasks and Users: Comprehensive operations for managing tasks and users.              
Task Assignment: Assign tasks efficiently within the system.               
Fluent Validation: Ensures data integrity through robust input validation.                 
MediatoR: Organized request and response handling for improved maintainability.             
CQRS Pattern: Enhanced separation of concerns and scalability.                 
Automapper: Simplifies object-to-object mapping, reducing boilerplate code.                 
Repository Pattern: Facilitates data access and unit testing.                
Clean Architecture Principles: Ensures maintainability, scalability, and testability.                    
Swagger Documentation: Interactive API documentation available via Swagger at http://localhost:8080/swagger.                
Global Exception Handling: Robust error handling for reliability.             
Docker Containerization: Easy deployment and scalability across environments.                     
Docker Compose: Includes services for task management, PostgreSQL database, and PGAdmin.       
                    
Setup Instructions                          
To set up Taskify locally using Docker Compose:                         

Clone the Repository: Clone the repository to your local machine.          
Navigate to Docker Compose File: Open a terminal and navigate to the directory containing the Docker Compose file (docker-compose.yml).            
Run Docker Compose: Execute docker-compose up -d to start the services in detached mode.       
         
Conclusion         
Taskify offers a seamless Task Management solution, leveraging modern technologies and best practices. With robust features and reliable performance, it ensures a smooth user experience while maintaining security and scalability.

Credentials           
Admin Credentials: admin@email.com / P@$$w0rd            
User Credentials: user@email.com / P@$$w0rd        

Credits        
Taskify is developed and maintained by Amanuel Eshete.   
