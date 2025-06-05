# Flex - Gym Management System

## Contributors
- Ayaan Khan (22i-0832)
- Minahil Ali (22i-0849)
- Mishal (221-1291)

## Project Overview
This project was the Final Project to Database Systems Course at FAST NUCES (Spring 2024).

This project is a Gym Management System designed to streamline the operations of a gym. It provides functionalities for gym owners, trainers, and members to manage various aspects of gym activities, including:

- Member registration and login.
- Trainer registration and login.
- Creating and managing workout plans and diet plans.
- Viewing and filtering workout and diet plans.
- Feedback system for trainers and members.
- Generating reports for gym activities.
- Administrative options for gym owners.

## Tools, Languages, and Frameworks Used
- **Programming Language**: C#
- **Database**: SQL (projDB[1].sql)
- **IDE**: Visual Studio
- **Framework**: .NET Framework

## Project Structure
The project consists of multiple C# files, each representing different functionalities of the system. Key files include:
- `adminlogin.cs`: Handles admin login functionality.
- `memberlogin.cs`: Handles member login functionality.
- `trainerLogin.cs`: Handles trainer login functionality.
- `memberCreateDietPlan.cs`: Allows members to create diet plans.
- `TrainerCreateWorkoutPlan.cs`: Allows trainers to create workout plans.
- `gymreport.cs`: Generates gym reports.
- `projDB[1].sql`: Contains the database schema and queries.

## How to Run the Project
1. **Setup the Database**:
   - Open the `projDB[1].sql` file in SQL Server Management Studio.
   - Execute the SQL script to set up the database.

2. **Open the Project**:
   - Open the solution file `Solution1.ssmssln` in Visual Studio.

3. **Build the Project**:
   - Build the solution to ensure all dependencies are resolved.

4. **Run the Project**:
   - Start the project by running the main program file `Program.cs`.

## Features
- **Member Management**:
  - Sign up and login.
  - Create and view diet and workout plans.
  - Provide feedback.

- **Trainer Management**:
  - Sign up and login.
  - Create and manage diet and workout plans.
  - View feedback from members.

- **Admin Management**:
  - Login to access administrative options.
  - Generate reports.

- **Reports**:
  - Generate detailed reports for gym activities.

## License
This project is licensed under the MIT License.