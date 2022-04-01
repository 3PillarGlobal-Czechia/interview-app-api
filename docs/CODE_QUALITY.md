# Code Quality

Our codebase is focused on delivering features but ensuring overall quality and maintainability. For that purpose, we do have active measures in place that ensures that contribution can be made only if certain conditions are met.

## Unit testing
* Each developer is responsible to write unit tests during the development or follow TDD.
* Writing unit test after delivering features is unacceptable, because it would lead to worse test quality.
* We believe that code coverage makes sense only if 100% coverage is our goal.  Certain parts of the application are excluded from this metric (WebApi, Infrastructure).

## Static Analysis

* Sonarcloud provides static analysis and output will be printed inside the PR.
* Fix all issues possible and help us to keep the codebase clean.
* [SonarCloud Project](https://sonarcloud.io/project/overview?id=3PillarGlobal-Ostrava_interview-app-api)


## Formating

* Its developers responsibility to ensure code quality in terms of formating
* We use a Visual Studio extension called [CodeMaid](https://www.codemaid.net/).
* There is a configuration file attached to this project `resources\codemaid-config\codeMaid.config`
* For more information and setup check [CodeMaid setup guide](/docs/CODEMAID.md).


## Fast Feedback

After PR is created or updated, the automation starts. The GitHub workflow will run the build, tests, and analysis tool. It's the responsibility of a contributor to check the result of the workflow run and fix any reported issues.

## Code Review

The process of CR should focus on things that cannot be detected automatically.

* variable names
* design patterns
* architecture rules
* overall best practices