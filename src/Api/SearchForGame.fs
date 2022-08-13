module Civ.Game.Handlers.GetGame

open Civ.Repository.SearchForGame
open Microsoft.AspNetCore.Http
open Giraffe

let searchForGame : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let query = ctx.TryGetQueryStringValue "query"
            let! res = searchForGame (Option.defaultValue "" query)
            match res with
            | None -> return! RequestErrors.NOT_FOUND "Nothing here" next ctx
            | Some r -> return! json r next ctx
        }