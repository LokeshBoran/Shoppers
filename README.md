# Shoppers

TOOLS USED:
* Visual Studio Code (also Visual Studio 2015 was used to create SLN files so as it can be opened in VS2015)
* To compile and run angular app node enviromnet needs to be installed with following tools:
  * yo, gulp, bower etc.
* InMemory database has been used.
* Generic Repository Pattern has been implemented for CRUD also WebRepository has been implemented to make REST calls.
* DI, EF and Web API are used as requested.

# How to run
* Open two terminals, go to "Shoppers.Catalogue" and "Shoppers.Pricing", then type `dotnet restore && dotnet run`
* To run angular app go to "Shoppers.Web" and type `npm install && bower install` (when you visit the folder for the first time) then do `gulp serve` to launch the app. Please make sure that Both APIs are up and running otherwise angular app will fail to load the page, as I mentioned error handling is not done due to lack of time.

Assumptions and remarks.

* Extensive error handling is not done is the code sample.
* Code sample is written in Dotnet Core 1.0.
* Frontend Sample is written in angular.js
* The pricing service is intentionally written in this manner, the intention was to just demonstrate inter service calls using REST.
* Detailed loging has not been done.
* Angular app is just a once pager, just makes a get call to pricing api and post call on page load to add new price. Purly to demonstrate API calls.
* Currently CORS are set such that all requests from all hosts will be entertained.
* The arch. diagram does not cover high availablity for database instances. It is assumed that database base are up and running and a reachable from the docker containers.
* No security measures have been taken.
* Microservices have been designed to be idemopent (except inmemory database).


Please let me know if something else is needed.
