﻿module Civ.Game.Handlers.GameRegions

open Civ
open Civ.Models.Game
open Repository.SearchForGame
open Repository.GetGameRegions
open Repository.Regions
open Microsoft.AspNetCore.Http
open Giraffe

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
    
let allRegionsUnassigned (game: Game) =
    let regions =
        allRegions
        |> List.map (fun r ->
            { CivId = 0
              CivName = ""
              RegionName = r.Name
              Assigned = false
              Color = ""
              Code = r.Code
              Coords = r.Coords })
    { GameId = game.Id
      Name = game.Name
      Code = game.Code
      Regions = regions }
 
let getGameRegions (gameId: int) : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! game = searchForGame (string gameId)
            match game with
            | None ->
                return! RequestErrors.NOT_FOUND "Nothing here" next ctx
            | Some g ->
                let! gameRegionsAssignedToCivs = getGameRegions g.Id
                match gameRegionsAssignedToCivs with
                | None ->
                    let viewModel = allRegionsUnassigned g
                    return! json viewModel next ctx
                | Some r ->
                    let viewModel = mergeRegionsAssignedToCivs r
                    return! json viewModel next ctx
        }