# DB migration
Add-Migration "init"
Update-Database

# Auth
dotnet user-jwts create