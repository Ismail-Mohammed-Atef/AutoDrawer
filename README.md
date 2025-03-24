# WPF Auto Drawer

## Overview
WPF Auto Drawer is a desktop application for creating and managing different shapes with advanced drawing capabilities. Built using MVVM architecture, design patterns, and Entity Framework, the application allows users to save and load their drawings, maintaining shapes and layouts across sessions. It features a tabbed system similar to AutoCAD and integrates authentication using Identity.

## Features
- **Shape Drawing**: Supports drawing and editing various shapes.
- **MVVM Architecture**: Ensures separation of concerns and maintainability.
- **Design Patterns**: Utilizes Factory Pattern for shape creation, Repository Pattern for data handling, and Command Pattern for undo/redo functionality.
- **Entity Framework**: Saves drawings per user and loads them with exact configurations.
- **Tabbed System**: Multi-document interface similar to AutoCAD.
- **Authentication & Authorization**: Secure login and user management with Identity.
- **Undo/Redo Functionality**: Provides enhanced editing capabilities.
- **Grid & Snap Support**: Improves precision in shape placement.
- **Theme Customization**: Dark/light mode and UI adjustments.

## Installation
1. Clone the repository.
2. Open the project in Visual Studio.
3. Restore NuGet packages.
4. Configure the database connection in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=your-db;User Id=your-user;Password=your-password;"
     }
   }
   ```
5. Apply migrations and update the database:
   ```sh
   dotnet ef database update
   ```
6. Build and run the application.

## Usage
1. Register/Login using Identity authentication.
2. Open a new tab to start a drawing session.
3. Draw shapes and save your work.
4. Load previously saved files with preserved layouts.
5. Utilize grid/snapping for precision.
6. Switch between tabs to work on multiple drawings simultaneously.

## Requirements
- **Windows OS**: 10 or later
- **.NET Version**: .NET 8 or later
- **Database**: SQL Server or SQLite
- **Authentication**: Identity with JWT support

## License
This project is licensed under the MIT License.

## Contact
For support or inquiries, reach out via email at ismail.mohammed.atef@gmail.com.


