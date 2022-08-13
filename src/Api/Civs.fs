module Civ.Api.Civs

open Giraffe
open Civ.Repository.Civs

let getCivs (gameId: int) : HttpHandler =
    fun next ctx ->
        task {
            let! civs = getAllCivsForGame gameId
            return! json civs next ctx 
        }
        
        
