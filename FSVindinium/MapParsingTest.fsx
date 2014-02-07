#r @".\packages\FSharp.Data.2.0.0-alpha3\lib\net40\FSharp.Data.dll"
#load "Maps.fs"
#load "FSVindinium.fs"

open Maps

let mapList = [m1; m2; m3; m4; m5; m6]

mapList |> List.map (fun i -> (double i.Length)/2.)