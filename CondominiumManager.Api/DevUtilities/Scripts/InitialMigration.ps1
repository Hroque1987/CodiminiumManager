param(
    [string]$name,
    [string]$module
)

if (-not $name -or -not $module) {
    Write-Host "❌ You must provide a migration name and Module. Example:"
    Write-Host "./migrate.ps1 AddUserPassword all or Choose: Identity = 1, Condominium = 2, Finance = 3, Notifications = 4  "
    exit
}

$validModules = @("all","1","2","3","4","Identity","Condominium","Finance","Notifications")

if ($module -notin $validModules) {
    Write-Host "❌ Invalid module. Use: all, 1-4 or module name."
    exit
}

$ErrorActionPreference = "Stop"

Write-Host "⚠⚠⚠ WARNING ⚠⚠⚠"
Write-Host "This will run EF migrations for: $module"
Write-Host ""

$proceed = Read-Host "Type YES to continue"

if ($proceed -ne "YES") {
    Write-Host "Cancelled."
    exit
}

Write-Host ""
Write-Host "Starting migrations..."
Write-Host "----------------------"

if ($module -eq "Identity" -or $module -eq "all" -or $module -eq "1") {
    dotnet ef migrations add "$name`_Identity" --context IdentityDbContext --project ../CondominiumManager.Identity --startup-project ../CondominiumManager.Api --output-dir Infrastructure/Migrations
}
if ($module -eq "Condominium" -or $module -eq "all"  -or $module -eq "2") {
   dotnet ef migrations add "$name`_Condominium" --context CondominiumDbContext --project ../CondominiumManager.Condominium --startup-project ../CondominiumManager.Api --output-dir Infrastructure/Migrations
}
if ($module -eq "Finance" -or $module -eq "all"  -or $module -eq "3") {
    dotnet ef migrations add "$name`_Finance" --context FinanceDbContext --project ../CondominiumManager.Finance --startup-project ../CondominiumManager.Api --output-dir Infrastructure/Migrations
}
if ($module -eq "Notifications" -or $module -eq "all"  -or $module -eq "4") {
    dotnet ef migrations add "$name`_Notifications" --context NotificationsDbContext --project ../CondominiumManager.Notifications --startup-project ../CondominiumManager.Api --output-dir Infrastructure/Migrations
}

