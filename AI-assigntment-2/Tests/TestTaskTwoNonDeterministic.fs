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
    let code = testGcl.["complexPredicate"]
    Program.ExecuteTasks(task = testTask, tries = 1, gcl_code0 = snd code)
    let test1 = readLines ("../../../generated/c_BeliefBase_output.txt")
    Program.ExecuteTasks(task = testTask, tries = 1, gcl_code0 = fst code)
    let test2 = readLines ("../../../generated/c_BeliefBase_output.txt")
    Assert.AreEqual(test1, test2)

//[<Test>]
//let TestMaximum () =
//    let testName = "maximum"
//    Program.ExecuteTasks(task = testTask, tries = 1, gcl_code0 = fst testGcl.[testName])
//    let correct = readLines ("../../../generated/formalmethods/non_d_" + testName + ".gv")
//    Assert.AreEqual(generated, correct)
