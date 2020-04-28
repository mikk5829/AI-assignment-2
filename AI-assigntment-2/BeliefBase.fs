module BeliefBase

open System.IO
open TypesAST

let rec treeC c =
    match c with
    | BelievesSet (var, values) -> var + (string values)



let rec generateBeliefBase parsed =
    try
        let c = treeC parsed
        File.WriteAllText(__SOURCE_DIRECTORY__ + "/generated/c_BeliefBase_output.txt", c)
        printfn "%s" c
    with err ->
        failwithf "Error.. BeliefBase file not generated \n Error: %A" err
