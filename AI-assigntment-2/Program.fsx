open System.IO

// We need to import a couple of modules, including the generated lexer and parser
#r "FsLexYacc.Runtime.10.0.0/lib/net46/FsLexYacc.Runtime.dll"

open FSharp.Text.Lexing
open System

#load "TypesAST.fs"

open TypesAST

#load "Parser.fs"

open Parser

#load "Lexer.fs"

open Lexer

// Load tasks as modules <- Needed to define namespaces!
#load "BeliefBase.fs"
#load "LogicalEntailment.fs"


// 1) Function for parsing GCL code
let parse input =
    // translate string into a buffer of characters
    let lexbuf = LexBuffer<char>.FromString input
    // translate the buffer into a stream of tokens and parse them
    let res = Parser.cExpression Lexer.tokenize lexbuf
    // return the result of parsing (i.e. value of type "expr")
    res

type ExecuteTasks(task: int, tries: int, ?gcl_code0: string) =

    let mainProg input =
        match input with
        | Some code -> code
        | None ->
            printfn "Please enter GCL code: "
            Console.ReadLine()

    do
        try
            if task = 0 then
                let e = parse (mainProg gcl_code0)
                BeliefBase.generateBeliefBase e
            elif task = 1 then
                failwith "Task 5 not implemented yet"
            elif task = 2 then
                failwith "Task 5 not implemented yet"
            elif task = 3 then
                failwith "Task 5 not implemented yet"
            elif task = 4 then
                failwith "Task 5 not implemented yet"
            elif task = 5 then
                failwith "Task 5 not implemented yet"
            elif task = 6 then
                failwith "Task 6 not implemented yet"
            elif task > 6 then
                failwith "Not a valid task number"
            with err ->
            printfn "Execution failed %A" err

/////////////////////////////////////////////////////////////////
///  Run the program
///  Params:
///  - task  <- # of task to run or 0 to run all tasks
///  - tries <- # of times the program should try to execute
///  OptionalParams:
///  - ?initial_values <- provide code describing initial values to be used in computation
///  - ?gcl_code <- provide code programatically or leave empty if console.input() should be used
/////////////////////////////////////////////////////////////////
//ExecuteTasks(task = 4, tries = 5, initial_values0 = "x:=+,y:=-", gcl_code0 = "do x>0 -> y:=x*y;x:=x-1 od") // x factorial
//ExecuteTasks(task = 3, tries = 10, initial_values0 = "i:=0,j:=0,n:=4,t:=0,A:=[9,4,3,7]", gcl_code0 = "i:=1; do i<n -> j:=i; do (j>0)&&(A[j-1]>A[j]) -> t:=A[j]; A[j]:=A[j-1]; A[j-1]:=t; j:=j-1 od; i:=i+1 od") // Insertion sort
//ExecuteTasks(task = 3, tries = 5, initial_values0 = "x:=3,i:=0,j:=0", gcl_code0 = "y:=1; do x>0 -> y:=x*y; x:=x-1 od") // Insertion sort
//ExecuteTasks(task = 4, tries = 3, initial_values0 = "y=-,A={+,-,+,-,0}", gcl_code0 = "y:=+; z:=-; x:=y+z")

/// <key,(gcl,initial)>
let programTestGcl =
    Map.ofList
        [ "varOnly", ("B={p, q, z, x}", "")
          "predicate", ("B = {p,q→w,q↔¬z}", "")
          "complexPredicate", ("B = {p,q→w,q↔¬z,e∨d,c∧¬r}", "") ]

let code = programTestGcl.["complexPredicate"]

ExecuteTasks(task = 0, tries = 1, gcl_code0 = fst code)

let readLines (filePath: string) =
    seq {
        use sr = new StreamReader(filePath)
        while not sr.EndOfStream do
            yield sr.ReadLine()
    }
