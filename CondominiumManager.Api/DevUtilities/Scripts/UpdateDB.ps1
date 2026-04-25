param(
    [string]$module
)
if (-not $module) {
    Write-Host "❌ You must provide a migration name. Example:"
    Write-Host "./UpdateDb.ps1 all or Choose: Identity = 1, Condominium = 2, Finance = 3, Notifications = 4  "
    exit
}
$validModules = @("all","1","2","3","4","Identity","Condominium","Finance","Notifications")

if ($module -notin $validModules) {
    Write-Host "❌ Invalid module. Use: all, 1-4 or module name."
    exit
}

Write-Host "⚠ This will apply $module EF migrations to the database ⚠" -ForegroundColor Yellow
Write-Host ""

$proceed = Read-Host "Type UPDATE to continue"

if ($proceed -ne "UPDATE") {
    Write-Host "Cancelled."
    exit
}

Write-Host ""
Write-Host "Updating database with $module module migrations..."
Write-Host "----------------------------------------------"

if ($module -eq "Identity" -or $module -eq "all" -or $module -eq "1") {
    dotnet ef database update --project ../CondominiumManager.Identity --startup-project ../CondominiumManager.Api --context IdentityDbContext
}
if ($module -eq "Condominium" -or $module -eq "all"  -or $module -eq "2") {
  dotnet ef database update --project ../CondominiumManager.Condominium --startup-project ../CondominiumManager.Api --context CondominiumDbContext
}
if ($module -eq "Finance" -or $module -eq "all"  -or $module -eq "3") {
    dotnet ef database update --project ../CondominiumManager.Finance --startup-project ../CondominiumManager.Api --context FinanceDbContext
}
if ($module -eq "Notifications" -or $module -eq "all"  -or $module -eq "4") {
   dotnet ef database update --project ../CondominiumManager.Notifications --startup-project ../CondominiumManager.Api --context NotificationsDbContext

}





