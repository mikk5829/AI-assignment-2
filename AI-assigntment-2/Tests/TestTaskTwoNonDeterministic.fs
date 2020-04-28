module TestLexer

open NUnit.Framework

let readLines path = Program.readLines path
let testGcl = Program.programTestGcl
let testTask = 0

[<SetUp>]
let Setup () =
    ()

[<Test>]
let TestFactorial () =
    let mutable test1 = Seq.empty
    let mutable test2 = Seq.empty
    let code = testGcl.["complexPredicate"]
    printfn "%A" (fst code)
    Program.ExecuteTasks(task = testTask, tries = 1, gcl_code0 = fst code)
    test1 <- readLines ("../../../generated/c_BeliefBase_output.txt")
    printfn "%A" (snd code)
    Program.ExecuteTasks(task = testTask, tries = 1, gcl_code0 = snd code)
    test2 <- readLines ("../../../generated/c_BeliefBase_output.txt")
    Assert.AreEqual(test1, test2)

//[<Test>]
//let TestMaximum () =
//    let testName = "maximum"
//    Program.ExecuteTasks(task = testTask, tries = 1, gcl_code0 = fst testGcl.[testName])
//    let correct = readLines ("../../../generated/formalmethods/non_d_" + testName + ".gv")
//    Assert.AreEqual(generated, correct)
