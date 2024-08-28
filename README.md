# Task Web Application - Backend(.Net core)

This project is a Todo Task application built with ASP.NET Core and Entity Framework Core.

## Getting Started

### Prerequisites

Before you begin, ensure you have met the following requirements:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/get-started) (optional, for containerized deployment)

### Installation

1. **Clone the repository:**

    ```sh
    git clone https://github.com/npkarunarathne/TaskWebApp_Backend
    cd TaskWebApp_Backend
    ```

2. **Restore dependencies:**

    ```sh
    dotnet restore
    ```

3. **Set up the database:**

    Update the database: (Package Manager Console)

    ```sh
    update-database -context TaskWebAppContext
    ```

### Configuration

Ensure your `appsettings.json` or `appsettings.Development.json` is configured correctly to connect to your SQL Server instance. Below is an example configuration:

```json
"ConnectionStrings": {
  "TaskWebAppContextConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskWebApp;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
### Docker
If you want to containerize the application with Docker, follow these steps:

1.**Build the Docker image:**

```sh
docker build -f TaskWebApp/Dockerfile . -t nimesh/web
```
2.**Run the Docker container:**

```sh
docker run -it --rm -p "5500:80" nimesh/web
```

This will build the Docker image for your application and run it in a container, mapping port 5500 on your host machine to port 80 in the container.

3.**Running the Application**
Once you've set up everything, you can run the application using:

```sh
dotnet run
```
Navigate to https://localhost:5001 to see your application in action.



