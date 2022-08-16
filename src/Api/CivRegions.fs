module Civ.Api.CivRegions

open Civ.Models.Game
open Giraffe
open Civ.Repository.SetCivRegion

let setRegion (gameId: int) : HttpHandler =
    fun next ctx ->
        task {
            let! civRegion = ctx.BindModelAsync<SetRegionToCiv>()
            do! setCivRegion gameId civRegion
            return! Successful.ok (json ()) next ctx   
        }
        
let deleteRegion (gameId: int): HttpHandler =
    fun next ctx ->
        task {
            let! civRegion = ctx.BindModelAsync<SetRegionToCiv>()
            do! deleteCivRegion gameId civRegion
            return! Successful.ok (json ()) next ctx
        }