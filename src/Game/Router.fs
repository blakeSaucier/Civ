module Civ.Game.Router

open Giraffe
open Handlers.New
open Handlers.GetGame
open Handlers.GameRegions

let gameRouter: HttpHandler =
    choose [
        GET >=> route "/api/game/search" >=> searchForGame
        GET >=> routef "/api/game/%i/regions" getGameRegions 
        POST >=> choose [
            route "/api/game" >=> submitGame
        ]
    ]
