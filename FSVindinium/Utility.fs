module Tools

open FSVindinium

type Player = | Me | Enemy1 | Enemy2 | Enemy3
with
    static member FromInt(i) =
        match i with
        | 0 -> Me | 1 -> Enemy1 | 2 -> Enemy2 | 3 -> Enemy3 | x -> failwithf "Didn't recognise %i" x

//## Impassable wood
//@1 Hero number 1
//[] Tavern
//$- Gold mine (neutral)
//$1 Gold mine (belonging to hero 1)

type MapComponent =
    | Space
    | Wood
    | Hero of Player
    | Tavern
    | Mine of Player option

let makeMap (state: Parser.Entity) =
    let mapDim = state.Game.Board.Size
    
    // Need to know which hero is me
    let playerNo = state.Hero.Id

    let rec splitString (str:string) lst =
        match str with
        | "" -> lst |> List.rev
        | _ -> splitString (str.Substring(2)) ((str.Substring(0,2))::(lst))

    let map1D = splitString state.Game.Board.Tiles []

    Array2D.init mapDim mapDim (fun i j -> map1D.[i*mapDim + j%mapDim])
    |> Array2D.map (fun i ->
        match i with
        | "  " -> Space
        | "##" -> Wood
        | "[]" -> Tavern
        | "$-" -> Mine(None)
        | mine when mine.StartsWith("$") ->
            let index = int mine.[1]
            if (index = playerNo) then Mine(Some(Me)) else Mine(Some(Player.FromInt((playerNo + index)%4)))
        | hero when hero.StartsWith("@") ->
            let index = int hero.[1]
            if (index = playerNo) then Hero(Me) else Hero(Player.FromInt((playerNo + index)%4))
        | x -> failwithf "Didn't recognise map piece %s" x)


let m2 = """
########################
########        ########
####$-            $-####
####  @2        @4  ####
##    []  $-$-  []    ##
##    ##  ####  ##    ##
##    ##  ####  ##    ##
##    []  $-$-  []    ##
####  @1        @3  ####
####$-            $-####
########        ########
########################""".Replace("\n","")
