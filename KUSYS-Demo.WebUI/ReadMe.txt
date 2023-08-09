
export ASPNETCORE_ENVIRONMENT=Production
export ASPNETCORE_ENVIRONMENT=Development

dotnet ef migrations add firstmigration  --context KusysDbContext --startup-project ../KUSYS-Demo.WebUI

dotnet ef database update  --context KusysDbContext --startup-project ../KUSYS-Demo.WebUI

dotnet ef migrations remove --context KusysDbContext --startup-project ../KUSYS-Demo.WebUI


dotnet ef migrations add firstmigration  --context ApplicationIdentityDbContext --startup-project ../KUSYS-Demo.WebUI

dotnet ef database update  --context ApplicationIdentityDbContext --startup-project ../KUSYS-Demo.WebUI

dotnet ef migrations remove --context ApplicationIdentityDbContext --startup-project ../KUSYS-Demo.WebUI