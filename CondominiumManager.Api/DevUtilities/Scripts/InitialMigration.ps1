Write-Host "⚠⚠⚠ WARNING ⚠⚠⚠"
Write-Host "This will run EF migrations for ALL modules (Identity, Condominium, Finance, Notifications)"
Write-Host ""

$proceed = Read-Host "Type YES to continue"

if ($proceed -ne "YES") {
    Write-Host "Cancelled."
    exit
}

Write-Host ""
Write-Host "Starting migrations..."
Write-Host "----------------------"

dotnet ef migrations add InitialEntity --context IdentityDbContext --project ../CondominiumManager.Identity --startup-project ../CondominiumManager.Api --output-dir Infrastructure/Migrations
dotnet ef migrations add InitialCondominium --context CondominiumDbContext --project ../CondominiumManager.Condominium --startup-project ../CondominiumManager.Api --context CondominiumDbContext --output-dir Infrastructure/Migrations
dotnet ef migrations add InitialFinance --context FinanceDbContext --project ../CondominiumManager.Finance --startup-project ../CondominiumManager.Api --context FinanceDbContext --output-dir Infrastructure/Migrations
dotnet ef migrations add InitialNotifications --context NotificationsDbContext --project ../CondominiumManager.Notifications --startup-project ../CondominiumManager.Api --context NotificationsDbContext --output-dir Infrastructure/Migrations