# Passenger
Building the first serious application following the course of Piotr Gankiewicz (link to the course at the end)

## Getting Started
The repository contains application created in Visual Studio Code 2017 with C# (.Net Core) technology.

## What is Passenger?

Application backend created in ASP .Net MVC technology. The task of the application is to establish common commuting. 

## What is the purpose of coding Passenger application?

Learning to build an application using the HTTP protocol, to know what is MVC and how it's works. 

## Application construction

The entire application is build from 5 different projects (using onion architecture):

* Api - contains MVC, controllers, appsettings (saparated from Core)
* Core - center of onion with Domain classes
* Infrastructure - contains all business logic e.g. services, handlers, mappers, repositories, extensions
* Tests - project for unit tests
* Tests end to end - project for integration tests
 
## What's interesting here?

* DTO
* Mapper
* Moq
* Autofac
* CommandHandler & CommandDispatcher
* JWT

## What do I still miss here?

It is not a complete application. There is still a lot missing in it. Below I will give you some things that I would like to add in the near  future:

* TESTS!!!
* More Controllers
* Geolocation
* More connections between Domain classes

I will not add a frontend, focus only on the backend in this application.

## Thanks

I would like to thank Piotr from this place for sharing such a great course. He opened my eyes and showed me how to program a professional.
 
Links:
http://piotrgankiewicz.com/courses/becoming-a-software-developer/
