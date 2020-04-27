// This file implements a module where we define a data type "expr"
// to store represent arithmetic expressions
module TypesAST
    
type a =
    | Num of float
    | Var of string
    | Array of (a * a)
    | Addition of (a * a)
    | Subtract of (a * a)
    | Multiply of (a * a)
    | Divide of (a * a)
    | Power of (a * a)

type b =
    | True
    | False
    | ShortAnd of (b * b)
    | ShortOr of (b * b)
    | And of (b * b)
    | Or of (b * b)
    | Negate of b
    | Equal of (a * a)
    | NotEqual of (a * a)
    | GreaterThen of (a * a)
    | GreaterEqual of (a * a)
    | LessThen of (a * a)
    | LessEqual of (a * a)

type C =
    | InitialArraySequence of (a * C)
    | InitialValues of (C * C) // C,C
    | InitialAssign of (a * a) // a = a - Only works when called from InitialValues sequence
    | InitialArray of (a * D) // a := [a,...,a]
    | Assign of (a * a) // x := a
    | AssignSet of (a * char Set) // Ex. A = {+, -, 0}
    | AssignArray of (a * a) // A[a] := a
    | Skip // skip
    | Sequence of (C * C) // C; C
    | IfFi of GC // if GC fi
    | DoOd of GC // do GC od

and GC =
    | Condition of (b * C) // b -> C
    | Else of (GC * GC) // GC [] GC

and D =
    | ArrayNum of a 
    | InitialArraySequence of (D * D)

// Types for Sign Analysis
type SignC =
    | AssignSign of (string * SignA)
    | AssignSignSet of (string * SignA)
    | SignSequence of (SignC * SignC)
and SignA =
  | SignNum of float
  | SignVar of string
  | SetSequence of (SignA * SignA)
  | PositiveSign
  | NegativeSign
  | ZeroSign
  | SignAdd of (SignA * SignA)
  | SignSub of (SignA * SignA)
  | SignMul of (SignA * SignA)
  | SignDiv of (SignA * SignA)
//and SignB =
//  | 

type InitSignC =
    | InitSign of (string * InitSignA)
    | InitSignSet of (string * InitSignA)
    | InitSequence of (InitSignC * InitSignC)
and InitSignA =
  | InitNumSign of float
  | InitSignSequence of (InitSignA * InitSignA)
  | InitPositiveSign
  | InitNegativeSign
  | InitZeroSign

type InitSecC =
    | InitSec of (string * string)
    | InitSecSequence of (InitSecC * InitSecC)