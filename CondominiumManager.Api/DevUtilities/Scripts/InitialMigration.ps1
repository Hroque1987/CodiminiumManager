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

dotnet ef migrations add InitialIdentity --project ../CondominiumManager.Identity --startup-project ../CondominiumManager.Api --output-dir Infrastructure/Migrations
dotnet ef migrations add InitialCondominium --project ../CondominiumManager.Condominium --startup-project ../CondominiumManager.Api --context CondominiumDbContext --output-dir Infrastructure/Migrations
dotnet ef migrations add InitialFinance --project ../CondominiumManager.Finance --startup-project ../CondominiumManager.Api --context FinanceDbContext --output-dir Infrastructure/Migrations
dotnet ef migrations add InitialNotifications --project ../CondominiumManager.Notifications --startup-project ../CondominiumManager.Api --context NotificationsDbContext --output-dir Infrastructure/Migrations