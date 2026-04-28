Write-Host "⚠⚠⚠ DANGER ZONE ⚠⚠⚠" -ForegroundColor Red
Write-Host "This will DROP ALL databases for all modules!" -ForegroundColor Yellow
Write-Host ""

$proceed = Read-Host "Type DROP to continue"

if ($proceed -ne "DROP") {
    Write-Host "Cancelled."
    exit
}

Write-Host ""
Write-Host "Dropping databases..."
Write-Host "----------------------"




dotnet ef database drop --project ../CondominiumManager.Identity --startup-project ../CondominiumManager.Api --context IdentityDbContext
dotnet ef database drop --project ../CondominiumManager.Condominium --startup-project ../CondominiumManager.Api --context CondominiumDbContext
dotnet ef database drop --project ../CondominiumManager.Finance --startup-project ../CondominiumManager.Api --context FinanceDbContext
dotnet ef database drop --project ../CondominiumManager.Notifications --startup-project ../CondominiumManager.Api --context NotificationsDbContext