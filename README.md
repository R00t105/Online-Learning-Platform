# Online Learning Platform

This project is an **Online Learning Platform** developed as part of a **DEPI graduation project** using **ASP.NET Core**. The platform allows users to browse courses, enroll in them, track their progress, and manage profiles. It includes user authentication, role-based access control, and various administrative functionalities.

## Project Overview
The objective of this project is to create a full-featured online learning platform that manages online courses and student enrollments. The platform provides functionalities for students to track their course progress and for administrators to manage the system.

## Features
- **3-Tier Architecture**: Separated layers for Data Access, Business Logic, and Presentation.
- **Generic Repository and Unit of Work**: For data management and transactions.
- **Authentication and Authorization**: Secured user management with role-based access control.
- **User Profiles**: Allows users to track progress and view their enrolled courses.
- **Course Enrollment**: Users can enroll in multiple courses and track their progress.
- **Admin Dashboard**: For managing courses, users, and roles.
- **Search and Filtering**: Find courses easily with search and filter functionality.
- **Progress Tracking**: Keep track of users' progress throughout the courses.
- **Validation and View Models**: Ensures data integrity and efficient presentation layer.

## Technologies Used
- **ASP.NET Core** (MVC Framework)
- **Entity Framework Core** (Database ORM)
- **User Authentication & Role Management**
- **Bootstrap** (for UI design)
- **SQL Server** (Database)

## Project Architecture
- **Data Access Layer (DAL)**: 
  - Models
  - Data (DbContext)
  - Configurations
- **Business Logic Layer (BLL)**:
  - Interfaces
  - Repositories
- **Presentation Layer (UI)**:
  - Controllers
  - Views
  - View Models


## Project Duration
The entire project was completed within 5 days.

## Database Design
The project utilizes a well-structured **ERD** and database **schema** to ensure smooth data operations and relationships between users, courses, and progress tracking.

## Getting Started
To run the project locally:

1. Clone the repository:

   ```bash
   git clone https://github.com/R00t105/Online-Learning-Platform.git
