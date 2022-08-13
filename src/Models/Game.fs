module Civ.Models.Game

type CreateNewCivGame =
    { gameName: string
      password: string
      civCount: int
      civs: {| name: string; color: string |} list }
    
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