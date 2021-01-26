# Hanh.ApplicationProcess.Application

This is a test application with AspnetCore and Aurelia with base functionalities and based on Clean-Architecture.

## Getting Started

1. run `nuget restore` on solution projects
2. run `cd Hahn.ApplicatonProcess.December2020.Web\ClientApp` then run `npm install`
3. to run applications you have two options:
    - using proxy server and run front-end and back-end seperately
    -  using built-in `aurelia-cli` inside the project and let the aspnet core handle it.
  
  options mentioned above can be found in `startup.cs` in Web Application Project.

### OR...

Just hit the Start button inside Visual Studio toolbar. still does the work :D
(well may be not the npm packages)

### Voila! 
  there you go. the application is ready to launch!

## Architecure
  Well I have been using clean architecture and using Data / Presistance Layer, Doomain Layer and Web / Presentation Layer. [just to keep things simple!]

  I have used repositories with unit of work and in this case I have used Generic Repositories to show that they can be handy on general purposes. unit of work is there just to handle transaction based database actions, which was not necessary in this project. so keep calm if you find it irrelevant to the project concept. it's just used to save changes on database with EFCore.

  speaking of EFCore, I have used the in-memory database with help of EFCore. and to switch from virtual to real persistant databases you can connect to SQL Server just by changing `inMemoryDatabase: false` in this statement `services.AddDatabase(Configuration, inMemoryDatabase: true);` which is inside `startup.cs` file.

  interfaces and implementations of the datbase relevant concepts are seperated in `Domain` and `Data` projects. we can have the implementations changed in case of changing the database type by the power of modularity of the system.

## Logging
  this application uses `Serilog` for logging and can be configured in `appsettings.json` file. and also every request to the server will be logged.

## Validation
  this application has both front-end and back-end validation. with help of `fluentValidation` on aspnet core and `aurelia-validation` on aurelia-framework.

## Swagger
  I have implemented swagger with the sample data requested for easing sample requests. documentation and sampling are done by summery comments above the controller actions.

## Localization

I have used `Microsoft.Extensions.Localization` to localize all strings in back-end API and also used `aurelia-i18n` and `i18next` on front-end framework for UserInterface localization procedure.

## Aurelia
  actually it was my first try with the Aurelia framework and it was new to me. so I had to search the web a lot. and it was fun working with it. but my speciality is more with Angular Framework. it was great to see that aurelia works with webpack and TypeScript and also the modularity of the application framework was great. I have used `BootstrapFormRenderer` inside both forms for update and insert. but the code was just copied from the main website of the aurelia and I had no time to look into it. so please don't judge me on that ;D

## Keep things simple
  to reduce the projects in number I thought of a trick to keep my front-end application within the same project as the back-end lives. this helped me to keep the application safe by preventing the `CORS` and using Https on both sides which is more safe. to enable webpack live reload on DEV environment I added https to the webpack devServer and looked on the back-end port number `44397`. so please if you had a problem with the wss sockets and got https error look into the `webpack.config.js` file inside the `ClientApp/webpack.config.js` and change the port number to the one running the back-end application.

## Note:Modals for Confirmation have changed
  as I wanted to keep an eye on creativity I implemented both `aurelia-modal` and `sweetalert`
  and I used the later on all user confirmations.
  so you can look at the implementation to see how the modals work.
