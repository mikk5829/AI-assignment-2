module computer_science_modelling_mandatory.Tests.TestTaskTwoNonDeterministic

open NUnit.Framework

let readLines path = Program.readLines path
let testGcl = Program.programTestGcl
let generated = (readLines "../../../generated/non_deterministic_graph.dot")
let testTask = 2

[<SetUp>]
let Setup () =
    ()

[<Test>]
let TestFactorial () =
    let testName = "factorial"
    Program.ExecuteTasks(task = testTask, tries = 1, gcl_code0 = fst testGcl.[testName])
    let correct = readLines ("../../../generated/formalmethods/non_d_" + testName + ".gv")
    Assert.AreEqual(generated, correct)

[<Test>]
let TestMaximum () =
    let testName = "maximum"
    Program.ExecuteTasks(task = testTask, tries = 1, gcl_code0 = fst testGcl.[testName])
    let correct = readLines ("../../../generated/formalmethods/non_d_" + testName + ".gv")
    Assert.AreEqual(generated, correct)

[<Test>]
let TestInsertion () =
    let testName = "insertion"
    Program.ExecuteTasks(task = testTask, tries = 1, gcl_code0 = fst testGcl.[testName])
    let correct = readLines ("../../../generated/formalmethods/non_d_" + testName + ".gv")
    Assert.AreEqual(generated, correct)
