module BeliefBase

open System.IO
open TypesAST

let rec treeC c =
    match c with
    | BelievesSet(var, values) -> printfn "%s = %A" var values



let rec generateBeliefBase parsed =
    try
        let c = treeC parsed
        printfn "Generated Belief Base"
    with err ->
        printfn "Error.. Tex file not generated \n Error: %A" err