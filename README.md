# NpvCalculator
Net Present Value Calculator


[![Build Status](https://dev.azure.com/raqipinili/NpvCalculator/_apis/build/status/raqipinili.NpvCalculator?branchName=master)](https://dev.azure.com/raqipinili/NpvCalculator/_build/latest?definitionId=4&branchName=master)


## How to run

clone or download repository
```
https://github.com/raqipinili/NpvCalculator.git
```

**Backend**

Open the NpvCalculator.sln solution in Visual Studio 2017/2019
Publish the SQL project
```
Right click NpvCalculator.SqlServer and then Publish (setup database user and pass)
```

or double click this file then click Publish (setup database user and pass)

```
NpvCalculator.SqlServer.publish.xml
```
Make sure that the database and tables are created

Run the webapi project project in Visual Studio

or open cmd and change the directory to
```
NpvCalculator\
```
then run this command
```
dotnet run --project NpvCalculator.Api
```

**Frontend**

in cmd change the directory to
```
NpvCalculator-Angular\
```

run this command
```
npm install
```

start the angular app
```
npm start
```

open http://localhost:4200/ in browser

## Installed Packages

**Backend**

* xunit
* Moq

**Frontend**

* bootstrap
* fontawesome
* ngx-bootstrap
* ngx-datatable
* chart.js
* ng2-charts@2.2.3
* chartjs-plugin-annotation
* ~~@angular/material~~
* @auth0/angular-jwt

## Technologies

* ASP.NET Core 2.2 WebAPI
* Angular 7.2.0
* SQL Server 2017
* Visual Studio 2019
* Visual Studio Code

## Features

* Calculate Net Present Value
* Calculate Present Value
* Calculate Future Value
* Net Present Value form values can persist to database (every Calculate button click, but can be turned off)
* Automatically load user's last saved form values
* Added Charts
* Implemented Authentication and Authorization (JWT)
* Implemented User Registration, can select permission
* Added form validations
* Added some Unit Test

## Todo

* Implement save feature for Present Value and Future Value
* Implement autoload saved values for Present Value and Future Value
* Add feature that can compare Net Present Value results
* Add SSL support
* Add more Unit Test
* Implement mediator pattern using MediatR library

