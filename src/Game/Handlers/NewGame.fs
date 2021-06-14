module Civ.Game.Handlers.New

open Civ.Game
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks
open GameCode
open Repository.NewGame
open FSharp.Json
    
type NewCivGameDto =
    { gameName: string
      password: string
      civCount: int
      civs: {| name: string; color: string |} list }

let submitGame: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! body = ctx.ReadBodyFromRequestAsync()
            let newGame = Json.deserialize<NewCivGameDto> body
            let newGameRequest =
                { Name = newGame.gameName
                  Password = newGame.password
                  Code = generateCode()
                  Civs = newGame.civs |> List.map (fun c -> { Name = c.name; Color = c.color }) }
            let! gameId = persistNewGame newGameRequest
            return! json {| GameId = gameId |} next ctx
        }
