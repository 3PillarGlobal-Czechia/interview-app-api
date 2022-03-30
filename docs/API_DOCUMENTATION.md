# API Documentation

To ensure the easiest work for consumers of our API, documentation is crucial. 
We use OpenAPI documentation generated through `Swashbuckle.AspNetCore.Swagger` package. This documentation is further used to generato clinet for application UI.

Each controller and method must follow naming conventions and rules: 

* versioned folder structure build around use cases
* one endpoint per controller
* group methods by using same controller name
* user singular for resource names
* be carefull: some annotations are required!

Before delivery to PR, ensure API is properly documented.