module computer_science_modelling_mandatory.Tasks.TaskOne

open System.IO
open TypesAST
open computer_science_modelling_mandatory.Utility

// TASK 1 //
// Generate SyntaxTree in LaTeX Tikz aka. "abstract syntax"
let rec treeA a =
    match a with
    | Num n -> string n
    | Var v -> v
    | Addition (a1, a2) -> "{+} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | Subtract (a1, a2) -> "{-} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | Multiply (a1, a2) -> "{*} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | Divide (a1, a2) -> "{/} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | Power (a1, a2) -> "{^} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | Array (s, a1) ->
        let var = Prettify.aToString (a1)
        "{" + treeA s + "[" + var + "]}"

let rec treeB b =
    match b with
    | True -> "{true}"
    | False -> "{false}"
    | ShortAnd (b1, b2) -> "{&} [ " + treeB b1 + " ] [ " + treeB b2 + " ] "
    | ShortOr (b1, b2) -> "{|} [ " + treeB b1 + " ] [ " + treeB b2 + " ] "
    | And (b1, b2) -> "{&&} [ " + treeB b1 + " ] [ " + treeB b2 + " ] "
    | Or (b1, b2) -> "{||} [ " + treeB b1 + " ] [ " + treeB b2 + " ] "
    | Negate b -> "{!} [ " + string b + " ] "
    | Equal (a1, a2) -> "{=} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | NotEqual (a1, a2) -> "{!=} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | GreaterThen (a1, a2) -> "{<} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | GreaterEqual (a1, a2) -> "{<=} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | LessThen (a1, a2) -> "{>} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "
    | LessEqual (a1, a2) -> "{>=} [ " + treeA a1 + " ] [ " + treeA a2 + " ] "

let rec treeC c =
    match c with
    | InitialValues (_) -> failwith "Initial Values should not exist in GCL code!"
    | Assign (s, a) -> "{:=} [ " + treeA s + " ] [ " + treeA a + " ] "
    //| AssignArray (s, a) -> "{:=} [ " + treeA s + " ] [ " + treeA a + " ] "
    | Skip -> "{skip}"
    | Sequence (c1, c2) -> "{;} [ " + treeC c1 + " ] [ " + treeC c2 + "] "
    | IfFi gc -> "{IF...FI} [ " + treeGC gc + " ] "
    | DoOd gc -> "{DO...OD} [ " + treeGC gc + " ] "
    | _ -> failwith "not implemented"

and treeGC gc =
    match gc with
    | Condition (b, c) -> "{->} [ " + treeB b + " ] [ " + treeC c + " ] "
    | Else (gc1, gc2) -> "{[]} [ " + treeGC gc1 + " ] [ " + treeGC gc2 + " ] "

// Function that reads user input and generates syntax tree
let rec generateAbstractSyntax tries parsed =
    if tries = 0 then
        printfn "Process ended"
    else
        try
            let e = parsed
            let beginning = File.ReadAllText(__SOURCE_DIRECTORY__ + "/../generated/beginning.txt")
            let ending = File.ReadAllText(__SOURCE_DIRECTORY__ + "/../generated/ending.txt")
            let c = treeC e
            File.WriteAllText
                (__SOURCE_DIRECTORY__ + "/../generated/syntaxTree.tex", beginning + "\n[" + treeC e + "]\n" + ending)
            printfn "Generated Tex file"
        with err ->
            printfn "Error.. Tex file not generated \n Error: %A" err
            generateAbstractSyntax (tries - 1) parsed
