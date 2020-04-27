open System.IO
open computer_science_modelling_mandatory.Tasks

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

#load "SignAnalysisParser.fs"
open SignAnalysisParser
#load "SignAnalysisLexer.fs"
open SignAnalysisLexer

#load "SecurityAnalysisParser.fs"
open SecurityAnalysisParser
#load "SecurityAnalysisLexer.fs"
open SecurityAnalysisLexer

// Load tasks as modules <- Needed to define namespaces!
#load "Utility/Prettify.fs"
#load "Utility/SignAnalysis.fs"
#load "Tasks/TaskOne.fs"
#load "Tasks/TaskTwo.fs"
#load "Tasks/TaskThree.fs"
#load "Tasks/TaskFour.fs"

// 1) Function for parsing GCL code
let parse_gcl input =
    // translate string into a buffer of characters
    let lexbuf = LexBuffer<char>.FromString input
    // translate the buffer into a stream of tokens and parse them
    let res = Parser.cExpression Lexer.tokenize lexbuf
    // return the result of parsing (i.e. value of type "expr")
    res

// 2) Function for parsing Sign analysis configuration code aka. init
let parse_sign input =
    // translate string into a buffer of characters
    let lexbuf = LexBuffer<char>.FromString input
    // translate the buffer into a stream of tokens and parse them
    let res = SignAnalysisParser.init_Sign_C_Expression SignAnalysisLexer.tokenize lexbuf
    // return the result of parsing (i.e. value of type "expr")
    res

// 3) Function for parsing Security analysis configuration code aka. init
let parse_security input =
    // translate string into a buffer of characters
    let lexbuf = LexBuffer<char>.FromString input
    // translate the buffer into a stream of tokens and parse them
    let res = SecurityAnalysisParser.init_Sec_C_Expression SecurityAnalysisLexer.tokenize lexbuf
    // return the result of parsing (i.e. value of type "expr")
    res
    
type ExecuteTasks(task: int, tries: int, ?initial_values0: string, ?gcl_code0: string) =
    
    // Check if parameters are empty
    let initConfig input =
        match input with
        | Some code -> code
        | None ->
            printfn "Please enter initial configuration: "
            Console.ReadLine()
    let mainProg input =
        match input with
        | Some code -> code
        | None ->
            printfn "Please enter GCL code: "
            Console.ReadLine()

    do
        try
            if task = 0 then
                failwith "Only test cases should do this"
            elif task = 1 then
                let e = parse_gcl (mainProg gcl_code0)
                TaskOne.generateAbstractSyntax tries e
            elif task = 2 then
                let e = parse_gcl (mainProg gcl_code0)
                TaskTwo.computeEdges e true tries "q▷" "q◀" False
                TaskTwo.computeEdges e false tries "q▷" "q◀" False
            elif task = 3 then
                let i = parse_gcl (initConfig initial_values0)
                let e = parse_gcl (mainProg gcl_code0)
                TaskThree.interpreter i e "q▷" "q◀"
            elif task = 4 then
                failwith "Skipping for now"
                //TaskFour.interpreter i e "q▷" "q◀"
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

//ExecuteTasks
//    (task = 1, tries = 5, initial_values0 = "i := 0, n := 0, x := 0, y := 0, A := [0]",
//     gcl_code0 =
//         "i:=0; x:=0;y:=0;do (n>i)&&(A[i]>=0) -> x:=x+A[i]; y:=y+1; i:=i+1 [] (n>i)&&(0>A[i]) -> i:=i+1 od; x:=x/y") // Average array

/// <key,(gcl,initial)>
let programTestGcl =
    Map.ofList
        [ "factorial", ("y:=1;do x>0 -> y:=x*y;x:=x-1 od", "x:=3,y:=0")
          "maximum", ("if x>=y -> z:=x [] y>x -> z:=y fi", "x := 10, y := 2, z := 0")
          "insertion",
          ("i:=1; do i<n -> j:=i; do (j>0)&&(A[j-1]>A[j]) -> t:=A[j]; A[j]:=A[j-1]; A[j-1]:=t; j:=j-1 od; i:=i+1 od",
           "i := 0, j := 0, n := 4, t := 0, A := [9,4,1,2]")
          "fibonacci",
          ("a:=0;b:=1;n:=0; do c>1 -> n:=a+b; if b>a -> a:=n [] b<=a -> b:=n fi; c:=c-1 od",
           "a := 0, b := 0, c := 9, n := 0")
          "ntimesn",("y:=1; do x>0 -> y:=2*y; x:=x-1 od","x:=5,y:=0")]

let code = programTestGcl.["ntimesn"]

ExecuteTasks(task = 3, tries = 1, initial_values0 = snd code, gcl_code0 = fst code)

let readLines (filePath: string) =
    seq {
        use sr = new StreamReader(filePath)
        while not sr.EndOfStream do
            yield sr.ReadLine()
    }
