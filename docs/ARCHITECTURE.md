# Architecture

We use principles of Onion, Hexagonal, Clean, and Ports & Adapters architecture. Our architecture ensures that domain and application logic stays independent from the implementation logic of the presentation or persistence layer and strictly separates the responsibilities of certain parts of the codebase.

## High level view
![architecture](/resources/images/architecture.png)

## Architecture Components

### WebApi
An outer layer that works as a presenter of the whole stack. Its responsibility is to define a communication interface through REST API with clients.

Another responsibility is to boot up the whole system and take care of all dependency wiring.


### Application
Main part of the stack. Contains bussines logic organised into use cases. 
Contains interfaces for implementation in outer layers(repositories, configuration, etc)

### Domain

Models, Enums and Exceptions are placed in domain, in the center of the whole application.

### Infrastructure

Outer layer responsible for persistance and 3rd party service access. In our case, it contains EF Core entities and repository patern implementation.