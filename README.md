# ProjectSem3 NGO

This is a web application for a non-governmental organization (NGO) to manage donations, programs, and volunteers.

## Project Overview

The project is built with ASP.NET MVC and uses Entity Framework for data access. It is divided into two main projects:

*   **Domain**: Contains the database models, repositories, and the unit of work pattern.
*   **WebMvc**: Contains the controllers, views, and other web-related components.

## Getting Started

To get started with the project, follow these steps:

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/your-username/ProjectSem3-NGO.git
    ```
2.  **Open the solution in Visual Studio:**
    Open the `ProjectSem3.sln` file in Visual Studio.
3.  **Build the solution:**
    Press `Ctrl + Shift + B` to build the project. This will restore the NuGet packages.
4.  **Configure the database:**
    *   Open the `Web.config` file in the `WebMvc` project.
    *   Locate the `<connectionStrings>` section and update the `ProjectSem3Entities` connection string with your SQL Server details.
    *   Run the `ProjectSem3_NGO.sql` script in your SQL Server to create the database and tables.
5.  **Run the application:**
    Press `F5` to run the application.

## Usage

The application has the following features:

*   **Home:** The main page of the application, which displays a summary of the NGO's activities.
*   **Donate:** A page where users can donate to the NGO.
*   **Programs:** A page that lists the NGO's programs.
*   **About:** A page that provides information about the NGO.
*   **Contact:** A page where users can contact the NGO.
*   **Partners:** A page that lists the NGO's partners.
*   **Admin Area:** A restricted area for administrators to manage the application's content.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
