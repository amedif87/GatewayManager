Write-Host "Welcome to GatewayManager Tool for Database Migrations" -ForegroundColor Green

$connectionString = $connectionString = ($args -join ";")

Write-Output "Connection String: $connectionString"

if ([string]::IsNullOrEmpty($connectionString)) {
    Write-Error "Connection string is empty"
}
else {
    dotnet ef database update `
        --startup-project ./GatewayManager.API/GatewayManager.API.csproj `
        --project ./GatewayManager.Migrations/GatewayManager.Migrations.csproj `
        --connection $connectionString
}

