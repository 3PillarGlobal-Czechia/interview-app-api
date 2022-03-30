# Code Quality

Our codebase is focused on delivering features but ensuring overall quality and maintainability. For that purposes we do have active measures in place that ensures that contribution can be made only if ceartan conditions are met.

## Unit testing
* Each developer is responsible to write unit test during the development or follow TDD.
* Writing unit test after delivering features is unacceptable, because it would lead to worse test quality.
* We beleive that code coverage makes sence only if 100% coverage is our goal.  Certain parts of the application are excluded from this metric (WebApi, Infrastructure).

## Static Analysis

* Sonarcloud provides static analysis and output will be printent inside the PR.
* Fix all issues possible and help us to keep codebase clean.
* [SonarCloud Project](https://sonarcloud.io/project/overview?id=3PillarGlobal-Ostrava_interview-app-api)


## Formating

* Its developers responsibility to ensure code quality in terms of formating
* We use a Visual Studio extension called [CodeMaid](https://www.codemaid.net/).
* There is a configuration file attached to this project `resources\codemaid-config\codeMaid.config`
* For more information and setup check [CodeMaid setup guide](/docs/CODEMAID.md).


## Fast Feedback

After PR is created automation kicks in an github workflow will run build, tests, and analysis tool. Its responsibility of a contributor to check result of workflow run and fix any reported issues.

## Code Review

Process of CR should focus on things that cannot be detected automaticaly.

* variable names
* design patterns
* architecure rules
* overall best practices
