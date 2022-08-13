module Civ.Repository.Civs

open Civ
open Postgres
open Civ.Models.Game

let getAllCivsForGame (gameId: int) =
    let sql = "select * from civilizations where game_id = @gameId"
    let queryParams = [
        "gameId", Sql.int gameId
    ]
    query sql queryParams (fun r ->
        { Civ.Id = r.int "id"
          Name = r.string "name"
          Color = r.string "color" })
    