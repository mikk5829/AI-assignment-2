module computer_science_modelling_mandatory.Tasks.BeliefBase

open System.IO
open TypesAST
open computer_science_modelling_mandatory.Tasks

let rec treeC c =
    match c with
    | BelievesSet(var, values) -> printfn "%A %A" var values



let rec generateBeliefBase parsed =
    try
        let c = treeC parsed
        printfn "Generated Belief Base"
    with err ->
        printfn "Error.. Tex file not generated \n Error: %A" err