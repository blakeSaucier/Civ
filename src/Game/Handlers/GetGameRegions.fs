module Civ.Game.Handlers.GameRegions

open Civ.Game.Repository.GetGameRegions
open Civ.Game.Repository.Regions
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks
open Giraffe

type RegionViewModel =
    { CivId: int
      CivName: string
      RegionName: string
      Assigned: bool
      Color: string
      Code: string
      Coords: float list list }
    
type CivRegionsViewModel =
    { GameId: int
      Name: string
      Code: string
      Regions: RegionViewModel list }

let mergeRegionsAssignedToCivs (civGame: CivGame) =
    let regionsViewModel = allRegions |> List.map (fun ar ->
        let assignedRegion = civGame.Regions |> List.tryFind (fun r -> ar.Code = r.Code)
        { CivId = assignedRegion |> Option.map (fun o -> o.CivId) |> Option.defaultValue 0
          CivName = assignedRegion |> Option.map (fun o -> o.Name) |> Option.defaultValue ""
          RegionName = ar.Name
          Assigned = assignedRegion |> Option.isSome
          Color = assignedRegion |> Option.map (fun o -> o.Color) |> Option.defaultValue ""
          Code = ar.Code
          Coords = ar.Coords })
    { GameId = civGame.Id
      Name = civGame.Name
      Code = civGame.Code
      Regions = regionsViewModel }
 
let getGameRegions gameId : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! gameRegionsAssignedToCivs = getGameRegions gameId
            match gameRegionsAssignedToCivs with
            | None -> return! RequestErrors.NOT_FOUND "Nothing here" next ctx
            | Some r ->
                let viewModel = mergeRegionsAssignedToCivs r
                return! json viewModel next ctx
        }