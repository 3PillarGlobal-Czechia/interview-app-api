# Architecture

Application is built with principles of Onion, Hexagonal, Clean, and Ports & Adapters architecture. Our architecture ensures that domain and application logic stays independent from implementation logic of presentation or persistance layer and strictly separates responsibilities of certain parts of the codebase.

## High level view
![architecture](/resources/images/architecture.png)

## Components

### WebApi
Outer layer that contains the presenter of the whole stack. Its responsibility is to define communication interface through REST API with clients, in our case, with appli 

Another responsibility is to boot up the whole system and take care of all dependency wiring.

### Application


### Domain

Models, Enums and Exceptions are placed in domain, in the center of the whole application.

### Infrastructure