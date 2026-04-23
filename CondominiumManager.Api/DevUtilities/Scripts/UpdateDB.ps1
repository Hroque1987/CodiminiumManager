Write-Host "⚠ This will apply ALL EF migrations to the database ⚠" -ForegroundColor Yellow
Write-Host ""

$proceed = Read-Host "Type UPDATE to continue"

if ($proceed -ne "UPDATE") {
    Write-Host "Cancelled."
    exit
}

Write-Host ""
Write-Host "Updating database with all module migrations..."
Write-Host "----------------------------------------------"




dotnet ef database update --project ../CondominiumManager.Identity --startup-project ../CondominiumManager.Api --context IdentityDbContext
dotnet ef database update --project ../CondominiumManager.Condominium --startup-project ../CondominiumManager.Api --context CondominiumDbContext
dotnet ef database update --project ../CondominiumManager.Finance --startup-project ../CondominiumManager.Api --context FinanceDbContext
dotnet ef database update --project ../CondominiumManager.Notifications --startup-project ../CondominiumManager.Api --context NotificationsDbContext