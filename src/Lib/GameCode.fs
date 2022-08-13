module Civ.GameCode

open System
let rand = Random()

let private randomLetter () = (rand.Next(26) + 97) |> char

let private randomNumber () = string (rand.Next(10)) |> char

let private join (chars: char[] ) = String.Join("", chars)

let generateCode () =
    [|
        randomLetter()
        randomNumber()
        randomLetter()
        randomNumber()
    |] |> join