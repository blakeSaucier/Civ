module Civ.Postgres

open Npgsql.FSharp
open Microsoft.Extensions.Configuration

let config =
    ConfigurationBuilder()
        .AddJsonFile("./appsettings.json")
        .AddEnvironmentVariables()
        .Build()        

let private conn = config["Db"]

let connect () =
    Sql.connect conn
    
let query q parameters mapper =
    connect ()
    |> Sql.query q
    |> Sql.parameters parameters
    |> Sql.executeAsync mapper
    
let querySingle q parameters mapper =
    connect ()
    |> Sql.query q
    |> Sql.parameters parameters
    |> Sql.executeRowAsync mapper
    
let nonQuery q parameters =
    connect ()
    |> Sql.query q
    |> Sql.parameters parameters
    |> Sql.executeNonQueryAsync