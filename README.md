# NpvCalculator
Net Present Value Calculator


[![Build Status](https://dev.azure.com/raqipinili/NpvCalculator/_apis/build/status/raqipinili.NpvCalculator?branchName=master)](https://dev.azure.com/raqipinili/NpvCalculator/_build/latest?definitionId=4&branchName=master) [![Build status](https://ci.appveyor.com/api/projects/status/20nni4b4546kj3wc/branch/master?svg=true)](https://ci.appveyor.com/project/raqipinili/npvcalculator/branch/master) [![CircleCI](https://circleci.com/gh/raqipinili/NpvCalculator.svg?style=svg)](https://circleci.com/gh/raqipinili/NpvCalculator) [![Build Status](https://travis-ci.org/raqipinili/NpvCalculator.svg?branch=master)](https://travis-ci.org/raqipinili/NpvCalculator) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/89befb46fbe94735ac48c1f861cb3a67)](https://www.codacy.com/app/raqipinili/NpvCalculator?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=raqipinili/NpvCalculator&amp;utm_campaign=Badge_Grade)


## How to run

* Clone or download the repository `https://github.com/raqipinili/NpvCalculator.git`

* **Backend**
* Open `NpvCalculator.Database.sln` solution in Visual Studio 2017/2019
* Publish `Security.SqlServer` and `NpvCalculator.SqlServer` SQL Project
* Make sure that the database and tables are created
* Open `NpvCalculator.sln` solution in Visual Studio 2017/2019 then run `NpvCalculator.Api` project
* Make sure `NpvCalculator.Api` project points to `http://localhost:5000/`

* **Frontend**
* Open cmd and point the directory to `NpvCalculator-Angular\`
* install the dependencies: `npm install`
* start the angular app: `npm start`
* open `http://localhost:4200/` in browser

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

