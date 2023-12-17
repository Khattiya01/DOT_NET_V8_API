# DB migration
Add-Migration "init"
Update-Database
dotnet ef migrations add "messageUpdateDatabase"

# Auth
dotnet user-jwts create