module Civ.Game.Handlers.New

open Civ
open Civ.Models.Game
open Giraffe
open Microsoft.AspNetCore.Http
open GameCode
open Repository.NewGame
open FSharp.Json
    
let submitGame: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! body = ctx.ReadBodyFromRequestAsync()
            let newGame = Json.deserialize<CreateNewCivGame> body
            let newGameRequest =
                { Name = newGame.gameName
                  Password = newGame.password
                  Code = generateCode()
                  Civs = newGame.civs |> List.map (fun c -> { Name = c.name; Color = c.color }) }
            let! gameId = persistNewGame newGameRequest
            return! json {| GameId = gameId |} next ctx
        }
