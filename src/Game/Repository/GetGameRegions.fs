module Civ.Game.Repository.GetGameRegions

open Civ
open Postgres
open FSharp.Control.Tasks
    
type Region =
    { CivId: int
      Name: string
      Color: string
      Code: string }

type CivGame =
    { Id: int
      Name: string
      Code: string
      Regions: Region list }
    
let getGameRegions (gameId: int) =
    let getGame =
        """
            SELECT games.id as game_id, games.name as game_name, games.code as game_code, 
                   c.id as civ_id, c.name as civ_name, c.color as civ_color,
                    cr.region as region
            FROM games
                     JOIN civilizations c on games.id = c.game_id
                     JOIN civilization_regions cr on c.id = cr.civ_id
            WHERE games.id = @gameId   
        """
        
    task {
        let! regionsAssignedToCivs = query getGame [ "gameId", Sql.int gameId ]
                                       (fun row ->
                                        {| GameId = row.int "game_id"
                                           Name = row.string "game_name"
                                           Code = row.string "game_code"
                                           CivId = row.int "civ_id"
                                           CivName = row.string "civ_name"
                                           CivColor = row.string "civ_color"
                                           Region = row.string "region" |})
        match regionsAssignedToCivs with
        | [] -> return None
        | _ -> return Some 
                          { Id = regionsAssignedToCivs.Head.GameId
                            Name = regionsAssignedToCivs.Head.Name
                            Code = regionsAssignedToCivs.Head.Code
                            Regions = regionsAssignedToCivs |> List.map (fun r -> { CivId = r.CivId
                                                                                    Color = r.CivColor
                                                                                    Name = r.CivName
                                                                                    Code = r.Region }) }
    }


