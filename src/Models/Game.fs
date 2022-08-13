module Civ.Models.Game

type Region =
    { CivId: int
      Name: string
      Color: string
      Code: string }

type CivGame =
    { Id: int
      Name: string
      Code: string
      Regions: Region list }
    
type Civ =
    { Id: int
      Name: string
      Color: string }

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