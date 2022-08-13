module Civ.Repository.SearchForGame

open System
open Civ
open Postgres

type Game =
    { Id: int
      Name: string
      Code: string }

let searchForGame (codeOrId: string) =
    let getGameQuery =
        """
            SELECT games.id as game_id, games.name, games.code
            FROM games
            WHERE games.id = @gameId OR games.code = @code    
        """
    let parameters = match Int32.TryParse codeOrId with
                        | true, i -> [ "gameId", Sql.int i; "code", Sql.string "" ]
                        | _ -> [ "gameId", Sql.int 0; "code", Sql.string codeOrId ]
    
    task {
        let! res = query getGameQuery parameters
                       (fun row -> { Id = row.int "game_id"
                                     Name = row.string "name"
                                     Code = row.string "code" })
        return res |> List.tryHead
    }
    