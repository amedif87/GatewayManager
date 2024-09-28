Write-Host "Welcome to GatewayManager Tool for Database Migrations" -ForegroundColor Green

$migrationName = $args[0]

if ([string]::IsNullOrEmpty($migrationName)) {
    Write-Error "Migration name is empty"
}
else {
    dotnet ef migrations add $migrationName `
        --startup-project ./GatewayManager.API/GatewayManager.API.csproj `
        --project ./GatewayManager.Migrations/GatewayManager.Migrations.csproj `

}

