﻿#r @".\packages\FSharp.Data.2.0.0-alpha3\lib\net40\FSharp.Data.dll"

//#r @".\bin\Release\FSVindinium.dll"
// or, if you want to experiment with the source
#load "FSVindinium.fs"
#load "Tools.fs"
#load "Keys.fs"

open System 
open FSharp.Data
open FSVindinium

// Put your API key here!
// If you don't have one register at: http://vindinium.org


let key = Keys.primaryKey             // Proheme key
let secondaryKey = Keys.secondaryKey    // SuperTill key

// Helpers
let rnd = new Random()
let getRandomMove () =     
    match rnd.Next(4) with
    | 0 -> Stay
    | 1 -> North
    | 2 -> South                            
    | 3 -> East
    | 4 -> West
    | i -> failwithf "Unexpected Random Number: %i" i

//Parser.

// Define AI
let startAI (game: VindiniumGame) =
    let rec aiLoop (state: Parser.Entity) = 
        // If finished return the final game state
        if state.Game.Finished then 
            printfn "Finished"
            state                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
        else
            // Otherwise do a random move and give it another go
            let nextMove = getRandomMove()
            printfn "Move for round %i of %i: %s" (state.Game.Turn) (state.Game.MaxTurns) (nextMove.ToString())
            Tools.makeMap state |> ignore
            let newState = game.Move (nextMove)
            aiLoop newState
    aiLoop game.StartingState

// Run in Training Mode with Default Params
let trainInstance = startTraining key (Some(300)) None
startAI trainInstance


// Run in Arena Mode
//let arenaInstance = startArena key


// First bot

VindiniumGame()