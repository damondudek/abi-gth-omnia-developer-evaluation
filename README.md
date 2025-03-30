# abi-gth-omnia-developer-evaluation
project created from a template

## to run
from Ambev.DeveloperEvaluation.ORM project, create and run migrations using these commands

if you need to create a new migration, run the code bellow
> dotnet ef migrations add MigrationName


to update database
> dotnet ef database update

## doubts
 * the documentation says that we need to do the authentication by username, not by email
 * I couldn't understand what's date on cart api (if it's a data of creation, we need to left it on system domain)


[postman documentation](https://documenter.getpostman.com/view/27971159/2sB2cPkR3g)