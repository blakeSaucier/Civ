module Civ.Repository.SetCivRegion

open System.Threading.Tasks
open Civ
open Postgres
open Civ.Models.Game

let setCivRegion (gameId: int) (setRegion: SetRegionToCiv) =
    let doesExistSql =
        "select civilization_regions.id from civilization_regions
            inner join civilizations c on c.id = civilization_regions.civ_id
            inner join games g on g.id = c.game_id
        where g.id = @gameId and region = @regionCode"
    
    let insertCivRegionSql =
        "insert into civilization_regions 
            (region, notes, civ_id, created, updated)
            values (@regionCode, '', @civId, now(), now())"
    
    let updateCivRegionSql =
        "update civilization_regions
            set civ_id = @civId, updated = now()
            where id = @civRegionId"
                
    let doesExistParams = [
        "@gameId", Sql.int gameId
        "@regionCode", Sql.string setRegion.RegionCode
        "@civId", Sql.int setRegion.CivId
    ]
    
    task {
        let! existingCivRegion = query doesExistSql doesExistParams (fun r ->
            {| Id = r.int "id" |})
        match existingCivRegion with
        | [] ->
            let! _ = query insertCivRegionSql doesExistParams (fun _ -> ())
            return ()
        | [ id ] -> 
            let! _ = query updateCivRegionSql [ "civRegionId", Sql.int id.Id
                                                "civId", Sql.int setRegion.CivId ]
                                                (fun _ -> ())
            return ()
        | _ ->
            failwith $"Failed to update region {setRegion.RegionCode} for civ {setRegion.CivId}"
    }
    
let deleteCivRegion (gameId: int) (setRegion: SetRegionToCiv) =
    let doesExistSql =
        "select civilization_regions.id from civilization_regions
            inner join civilizations c on c.id = civilization_regions.civ_id
            inner join games g on g.id = c.game_id
        where g.id = @gameId and region = @regionCode"
        
    let deleteSql = "delete from civilization_regions where id = @civRegionId"
    
    let doesExistParams = [
        "@gameId", Sql.int gameId
        "@regionCode", Sql.string setRegion.RegionCode
        "@civId", Sql.int setRegion.CivId
    ]

    task {
        let! civRegions = query doesExistSql doesExistParams (fun r -> {| Id = r.int "id" |})
        let! _ = 
            civRegions
            |> List.map (fun id ->
                query deleteSql [ "civRegionId", Sql.int id.Id ] (fun r -> ()))
            |> Task.WhenAll
        return ()
    }