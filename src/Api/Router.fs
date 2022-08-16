module Civ.Game.Router

open Giraffe
open Handlers.New
open Handlers.GetGame
open Handlers.GameRegions
open Civ.Api.Civs
open Civ.Api.CivRegions

let gameRouter: HttpHandler =
    choose [
        GET >=> route "/api/game/search" >=> searchForGame
        GET >=> routef "/api/game/%i/regions" getGameRegions
        GET >=> routef "/api/game/%i/civs" getCivs
        POST >=> choose [
            route "/api/game" >=> submitGame
            routef "/api/game/%i/region" setRegion
        ]
        DELETE >=> choose [
            routef "/api/game/%i/region" deleteRegion
        ]
    ]
