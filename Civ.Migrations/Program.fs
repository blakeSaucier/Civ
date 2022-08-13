
open Evolve
open Npgsql
open Microsoft.Extensions.Configuration

let config =
    ConfigurationBuilder()
        .AddJsonFile("./appsettings.json")
        .AddEnvironmentVariables()
        .Build()

let evolve =
    Evolve(
        dbConnection = new NpgsqlConnection(config["Db"]),
        Locations = [| "Db/Migrations" |],
        IsEraseDisabled = true
    )
printfn "Starting migration"
evolve.Migrate()
printfn "Migration completed"