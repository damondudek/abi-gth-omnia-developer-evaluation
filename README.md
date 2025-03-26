# abi-gth-omnia-developer-evaluation
project created from a template

## to run
from web-api project, run migrations using these commands

if you need to change any entity, run this code to create a new migration
> dotnet ef migrations add NewMigration --project ../Ambev.DeveloperEvaluation.ORM

to update database
> dotnet ef database update --project ../Ambev.DeveloperEvaluation.ORM

## doubts
 * the documentation says that we need to do the authentication by username, not by email 
