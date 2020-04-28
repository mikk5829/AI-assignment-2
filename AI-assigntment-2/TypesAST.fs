// This file implements a module where we define a data type "expr"
// to store represent arithmetic expressions
module TypesAST
    
type a =
    | Num of float
    | True of string
    | Not of a
    | Array of (a * a)
    | Addition of (a * a)
    | Subtract of (a * a)
    | Multiply of (a * a)
    | Divide of (a * a)
    | Power of (a * a)
    | Implication of (a * a)
    | BiImplication of (a * a)
    | And of (a * a)
    | Or of (a * a)

and Predicate =
    | False
    | ShortAnd of (Predicate * Predicate)
    | ShortOr of (Predicate * Predicate)
    | Negate of Predicate
    | Equal of (a * a)
    | NotEqual of (a * a)
    | GreaterThen of (a * a)
    | GreaterEqual of (a * a)
    | LessThen of (a * a)
    | LessEqual of (a * a)

type C =
    | BelievesSet of (string * D) // Ex. B = {p, q, p → ¬q}
    | InitialArraySequence of (a * C)
    | InitialValues of (C * C) // C,C
    | InitialAssign of (a * a) // a = a - Only works when called from InitialValues sequence
    | InitialArray of (a * D) // a := [a,...,a]
    | Assign of (a * a) // x := a
    | AssignArray of (a * a) // A[a] := a
    | Skip // skip
    | Sequence of (C * C) // C; C
    | IfFi of GC // if GC fi
    | DoOd of GC // do GC od

and GC =
    | Condition of (Predicate * Predicate) // b -> C
    | Else of (GC * GC) // GC [] GC

and D =
    | Belief of a
    | BelievesSetSequence of (D * D)