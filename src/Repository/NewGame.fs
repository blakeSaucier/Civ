module Civ.Repository.NewGame
open Civ
open Postgres
open Npgsql.FSharp

type Civ =
    { Name: string
      Color: string }

type NewGameRequest =
    { Name: string
      Code: string
      Password: string
      Civs: Civ list }

let persistNewGame (game: NewGameRequest) =
    let insertGame =
        """
        INSERT INTO games (name, code, password, created, updated)
            VALUES (@name, @code, @password, now(), now());
        """
        
    let insertCivs =
        """
        INSERT INTO civilizations (name, color, game_id, created, updated)
            VALUES (@name, @color, @game_id, now(), now());
        """
        
    task {
        let! _ = nonQuery insertGame [ "name", Sql.string game.Name
                                       "code", Sql.string game.Code
                                       "password", Sql.string game.Password ]
        let! gameId = querySingle "SELECT id FROM games Where code = @code;" [ "code", Sql.string game.Code ] (fun row -> row.int "id")
        let generateCivInserts game =
            game.Civs |> List.map (fun c ->
                [
                    "name", Sql.string c.Name
                    "color", Sql.string c.Color
                    "game_id", Sql.int gameId
                ])
        let! _ =
            connect()
            |> Sql.executeTransactionAsync [ insertCivs, (generateCivInserts game) ]
        return gameId
    }