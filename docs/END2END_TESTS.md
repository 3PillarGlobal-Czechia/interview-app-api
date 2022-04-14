# End 2 End Testing

Our E2E tests are focused on testing individual API endpoints and are responsible for their correctness and robustness. When making changes to the code base it is up to the developer to ensure that all present tests are succesfully passing and also, if applicable, adding new or modifying old tests.

## Overview

Our E2E tests follow the [AAA](https://automationpanda.com/2020/07/07/arrange-act-assert-a-pattern-for-writing-good-tests/) pattern. Individual test classes are run in parallel, each with it's own instance of an in memory database, guaranteeing that they cannot be affected by each other. Each individual database is seeded with data from the `Infrastructure.MyDbContext.Seed` method. Tests themselves can be run from the test explorer in Visual Studio.

## Rules for writing tests

* Newly created test classes **must** inherit from the `E2ETestsBase` class and the `IClassFixture<MyWebApplicationFactory<Startup>>` interface.
* They *should* follow the AAA pattern.
* They *can* use the prepared `HttpWrapper` class for invoking the API endpoints.


