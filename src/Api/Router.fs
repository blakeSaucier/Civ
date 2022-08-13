module Civ.Game.Router

open Giraffe
open Handlers.New
open Handlers.GetGame
open Handlers.GameRegions
open Civ.Api.Civs

let gameRouter: HttpHandler =
    choose [
        GET >=> route "/api/game/search" >=> searchForGame
        GET >=> routef "/api/game/%i/regions" getGameRegions
        GET >=> routef "/api/game/%i/civs" getCivs
        POST >=> choose [
            route "/api/game" >=> submitGame
        ]
    ]
