# RTL Movie Scrapper Service

RTL Movie Scrapper service built in Asp.Net Core with EntityFramework. This RESTful service provides CRUD functionality for Shows and respective casts. 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

The following needs to be installed to run the project:

 - Visual Studio 2017 Community Edition
 - SQL Server 
 - .Net SDK Version 2.1.1 or higher

### Installing

To get the project running, first you need to download the project from GitHub:

```
git clone <link_to_repository>
```

Once the project is downloaded, open the **RTLMovieScrapper.sln** in Visual Studio 2017. At this point, make sure your SQL database is up and running.
Also make sure that the **RTLMovieScrapper** project is selected as the startup project.

Before the project can run, you need to download the dependencies. Go to `tools -> NuGet Package Manager -> Package Manager Console` and type the following:
```
dotnet restore RTLMovieScrapper.sln
```
This will automatically restore all the dependencies.

Once the dependencies are downloaded, run the following command in the package manager console:
```
Update-Database
```
This will run the SQL commands to setup the database. For different-different SQL servers change the connection string in AppSettings.
You can now run the project by starting the `IIS Express` configuration. Once the project is running, navigate in your browser to `https://localhost:{SSL_port}/swagger` to see the information regarding the endpoints.

#### Installation Notes

##### Visual Studio

Visual Studio may ask you to **'Install Missing Features'** when opening the project. Just click the **Install** button to let Visual Studio automatically install the missing features.
Once that is done, reopen the project and proceed with the rest of the installation.

![visual_studio_missing_features](readme_images/install_missing_features.png "Visual Studio asking to install missing features")

Also make sure that you are running at least **.Net SDK version 2.1.1**.

##### Docker

Docker is not implemented yet. It is to do.
## Testing

This section will explain how to add and run tests.

### Create

I haven't added any Testing project. If you wish, you can add nUnit or xUnit test.
[Fact]
```
will be run as a test.

### Execution

To execute this project's tests, go to the project's base folder and run:

```
dotnet test RTLMovieScrapper.Tests
```
This will run all the tests within the RTLMovieScrapper.Tests project.

## Deployment

```
This section isn't created yet
```
