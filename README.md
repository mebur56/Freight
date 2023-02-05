# Freights

This is a sample project build on React buildin on Webpack with Typescript,

This Project is the backend application, his counterpart is needed to be fully functional. See [Freight-Frontend](https://github.com/mebur56/Freight-Frontend)


## Getting Started

### Prerequisites

To run this project you will need 
- .NetCore 6.0:
    - Recomended install via IDE [Visual studio 2022 ](https://visualstudio.microsoft.com/pt-br/vs/community/) 
    - Other [.Net 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Database [MySQL](https://dev.mysql.com/downloads/mysql/)




### Installing

Open the project and set the ``appsettings.json`` variables to correspond your database E.G:

```
 "mysql": {
      "server": "serverName",
      "port": databasePort,
      "database": "databaseName",
      "username": "username",
      "password": "password"
    }
```

<!-- ## Running unit tests

TODO -->

## Running

You can run the project directly by the [Visual studio 2022 ](https://visualstudio.microsoft.com/pt-br/vs/community/) or you can go to Application folder and run 
```dotnet run```

The project will automatically start on http://localhost:3000
