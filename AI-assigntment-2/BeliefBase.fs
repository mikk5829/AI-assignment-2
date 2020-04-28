module BeliefBase

open System.IO
open TypesAST

let rec treeC c =
    match c with
    | BelievesSet (var, values) -> var + (string values)

let rec toString p = match p with
    | True(v) -> v
    | Not(p) -> "!(" + toString p+")"
    | Implication(p1, p2) ->  toString p1 + " -> " + toString p2
    | BiImplication(p1, p2) -> "(" + toString p1 + ") <-> (" + toString p2 + ")"
    | And(p1, p2) -> "(" + toString p1 + ") ^ (" + toString p2 + ")"
    | Or(p1, p2) -> "(" + toString p1 + ") v (" + toString p2 + ")"

let rec contradict p = match p with
    | True(v) -> [Not(True(v))]
    | Not(p) -> [p]
    | Implication(p1, p2) -> [And(p1, Not(p2))] //TODO: Tilføj alle contradict p2 -> x til And(p1, x)
    | BiImplication(p1, p2) -> [And(Not(p1), p2); And(Not(p2), p1)] @ contradict p1 @ contradict p2
    | And(p1, p2) -> [Not(p1); Not(p2)] @ contradict p1 @ contradict p2
    | Or(p1, p2) -> [And(Not(p1), Not(p2))]

let rec generateBeliefBase parsed =
    try
        let c = treeC parsed
        File.WriteAllText(__SOURCE_DIRECTORY__ + "/generated/c_BeliefBase_output.txt", c)
        printfn "%s" c
    with err ->
        failwithf "Error.. BeliefBase file not generated \n Error: %A" err
