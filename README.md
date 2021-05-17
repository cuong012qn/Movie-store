#Target Dotnet Core 5.0  
#Package  
-API  
Microsoft.AspNetCore.Authentication.JwtBearer@5.0.0  
Microsoft.AspNetCore.Mvc.NewtonsoftJson@5.0.0  
Microsoft.EntityFrameworkCore@5.0.0  
Microsoft.EntityFrameworkCore.Design@5.0.0  
Microsoft.EntityFrameworkCore.SqlServer@5.0.0  
Microsoft.EntityFrameworkCore.Tools@5.0.0  
Swashbuckle.AspNetCore@5.6.3  
  
-Data\
Microsoft.AspNetCore.Http.Features@5.0.0\
Microsoft.EntityFrameworkCore.Design@5.0.0\
Microsoft.EntityFrameworkCore@5.0.0\
Microsoft.EntityFrameworkCore.Tools@5.0.0\
Microsoft.EntityFrameworkCore.SqlServer@5.0.0\
Microsoft.Extensions.Configuration@5.0.0\
Microsoft.Extensions.Configuration.Abstractions@5.0.0\
Microsoft.Extensions.Configuration.Json@5.0.0\
\
-FE\
Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation@5.0.5\
Microsoft.VisualStudio.Web.CodeGeneration.Design@5.0.2\
Newtonsoft.Json@13.0.1\
System.IdentityModel.Tokens.Jwt@6.11.0\
\
\
\
# Movie-store
Add appsettings.json
```json
{
  "AllowedHosts": "*",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Secretkey": "Your secret key",
  "Api": "your host",
  "ConnectionStrings": {
    "MovieDBContextConnection": "Server=Name server;Database=DBName;"
  },
}
```
Create seed Model\
Move -> Movie-Store-Data\
Create appsettings.json\
Run command: \
dotnet ef migrations add InitalDB\
dotnet ef database update\
-> Copy appsettings.json form Data to API -> Run Movie-Store-API first time for seed data
