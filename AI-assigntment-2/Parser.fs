// Implementation file for parser generated by fsyacc
module Parser
#nowarn "64";; // turn off warnings that type variables used in production annotations are instantiated to concrete type
open FSharp.Text.Lexing
open FSharp.Text.Parsing.ParseHelpers
# 2 "Parser.fsp"

open TypesAST

# 10 "Parser.fs"
// This type is the type of tokens accepted by the parser
type token = 
  | NOT
  | BIIMPLICATION
  | IMPLICATION
  | COMMA
  | TRUE
  | FALSE
  | AND
  | OR
  | EQUAL
  | LPAR
  | RPAR
  | LCUR
  | RCUR
  | EOF
  | NUM of (float)
  | VAR of (string)
// This type is used to give symbolic names to token indexes, useful for error messages
type tokenId = 
    | TOKEN_NOT
    | TOKEN_BIIMPLICATION
    | TOKEN_IMPLICATION
    | TOKEN_COMMA
    | TOKEN_TRUE
    | TOKEN_FALSE
    | TOKEN_AND
    | TOKEN_OR
    | TOKEN_EQUAL
    | TOKEN_LPAR
    | TOKEN_RPAR
    | TOKEN_LCUR
    | TOKEN_RCUR
    | TOKEN_EOF
    | TOKEN_NUM
    | TOKEN_VAR
    | TOKEN_end_of_input
    | TOKEN_error
// This type is used to give symbolic names to token indexes, useful for error messages
type nonTerminalId = 
    | NONTERM__startcExpression
    | NONTERM_dExpression
    | NONTERM_cExpression
    | NONTERM_aExpression
    | NONTERM_bExpression
    | NONTERM_array_values
    | NONTERM_rev_values

// This function maps tokens to integer indexes
let tagOfToken (t:token) = 
  match t with
  | NOT  -> 0 
  | BIIMPLICATION  -> 1 
  | IMPLICATION  -> 2 
  | COMMA  -> 3 
  | TRUE  -> 4 
  | FALSE  -> 5 
  | AND  -> 6 
  | OR  -> 7 
  | EQUAL  -> 8 
  | LPAR  -> 9 
  | RPAR  -> 10 
  | LCUR  -> 11 
  | RCUR  -> 12 
  | EOF  -> 13 
  | NUM _ -> 14 
  | VAR _ -> 15 

// This function maps integer indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_NOT 
  | 1 -> TOKEN_BIIMPLICATION 
  | 2 -> TOKEN_IMPLICATION 
  | 3 -> TOKEN_COMMA 
  | 4 -> TOKEN_TRUE 
  | 5 -> TOKEN_FALSE 
  | 6 -> TOKEN_AND 
  | 7 -> TOKEN_OR 
  | 8 -> TOKEN_EQUAL 
  | 9 -> TOKEN_LPAR 
  | 10 -> TOKEN_RPAR 
  | 11 -> TOKEN_LCUR 
  | 12 -> TOKEN_RCUR 
  | 13 -> TOKEN_EOF 
  | 14 -> TOKEN_NUM 
  | 15 -> TOKEN_VAR 
  | 18 -> TOKEN_end_of_input
  | 16 -> TOKEN_error
  | _ -> failwith "tokenTagToTokenId: bad token"

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
let prodIdxToNonTerminal (prodIdx:int) = 
  match prodIdx with
    | 0 -> NONTERM__startcExpression 
    | 1 -> NONTERM_dExpression 
    | 2 -> NONTERM_dExpression 
    | 3 -> NONTERM_cExpression 
    | 4 -> NONTERM_cExpression 
    | 5 -> NONTERM_aExpression 
    | 6 -> NONTERM_aExpression 
    | 7 -> NONTERM_aExpression 
    | 8 -> NONTERM_aExpression 
    | 9 -> NONTERM_aExpression 
    | 10 -> NONTERM_aExpression 
    | 11 -> NONTERM_aExpression 
    | 12 -> NONTERM_aExpression 
    | 13 -> NONTERM_bExpression 
    | 14 -> NONTERM_bExpression 
    | 15 -> NONTERM_bExpression 
    | 16 -> NONTERM_array_values 
    | 17 -> NONTERM_array_values 
    | 18 -> NONTERM_rev_values 
    | 19 -> NONTERM_rev_values 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 18 
let _fsyacc_tagOfErrorTerminal = 16

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | NOT  -> "NOT" 
  | BIIMPLICATION  -> "BIIMPLICATION" 
  | IMPLICATION  -> "IMPLICATION" 
  | COMMA  -> "COMMA" 
  | TRUE  -> "TRUE" 
  | FALSE  -> "FALSE" 
  | AND  -> "AND" 
  | OR  -> "OR" 
  | EQUAL  -> "EQUAL" 
  | LPAR  -> "LPAR" 
  | RPAR  -> "RPAR" 
  | LCUR  -> "LCUR" 
  | RCUR  -> "RCUR" 
  | EOF  -> "EOF" 
  | NUM _ -> "NUM" 
  | VAR _ -> "VAR" 

// This function gets the data carried by a token as an object
let _fsyacc_dataOfToken (t:token) = 
  match t with 
  | NOT  -> (null : System.Object) 
  | BIIMPLICATION  -> (null : System.Object) 
  | IMPLICATION  -> (null : System.Object) 
  | COMMA  -> (null : System.Object) 
  | TRUE  -> (null : System.Object) 
  | FALSE  -> (null : System.Object) 
  | AND  -> (null : System.Object) 
  | OR  -> (null : System.Object) 
  | EQUAL  -> (null : System.Object) 
  | LPAR  -> (null : System.Object) 
  | RPAR  -> (null : System.Object) 
  | LCUR  -> (null : System.Object) 
  | RCUR  -> (null : System.Object) 
  | EOF  -> (null : System.Object) 
  | NUM _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | VAR _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
let _fsyacc_gotos = [| 0us; 65535us; 0us; 65535us; 2us; 65535us; 0us; 1us; 6us; 5us; 8us; 65535us; 2us; 16us; 9us; 10us; 18us; 11us; 19us; 12us; 20us; 13us; 22us; 14us; 23us; 15us; 25us; 17us; 0us; 65535us; 1us; 65535us; 2us; 3us; 1us; 65535us; 2us; 24us; |]
let _fsyacc_sparseGotoTableRowOffsets = [|0us; 1us; 2us; 5us; 14us; 15us; 17us; |]
let _fsyacc_stateToProdIdxsTableElements = [| 1us; 0us; 2us; 0us; 4us; 1us; 3us; 1us; 3us; 1us; 3us; 2us; 4us; 4us; 1us; 4us; 1us; 5us; 1us; 6us; 1us; 7us; 5us; 7us; 8us; 9us; 11us; 12us; 5us; 8us; 8us; 9us; 11us; 12us; 5us; 8us; 9us; 9us; 11us; 12us; 5us; 8us; 9us; 10us; 11us; 12us; 5us; 8us; 9us; 11us; 11us; 12us; 5us; 8us; 9us; 11us; 12us; 12us; 5us; 8us; 9us; 11us; 12us; 18us; 5us; 8us; 9us; 11us; 12us; 19us; 1us; 8us; 1us; 9us; 1us; 10us; 1us; 10us; 1us; 11us; 1us; 12us; 2us; 17us; 19us; 1us; 19us; |]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us; 2us; 5us; 7us; 9us; 11us; 14us; 16us; 18us; 20us; 22us; 28us; 34us; 40us; 46us; 52us; 58us; 64us; 70us; 72us; 74us; 76us; 78us; 80us; 82us; 85us; |]
let _fsyacc_action_rows = 26
let _fsyacc_actionTableElements = [|1us; 32768us; 11us; 2us; 1us; 49152us; 3us; 6us; 4us; 16400us; 0us; 9us; 9us; 20us; 14us; 7us; 15us; 8us; 1us; 32768us; 12us; 4us; 0us; 16387us; 0us; 16388us; 1us; 32768us; 11us; 2us; 0us; 16389us; 0us; 16390us; 4us; 32768us; 0us; 9us; 9us; 20us; 14us; 7us; 15us; 8us; 0us; 16391us; 0us; 16392us; 0us; 16393us; 5us; 32768us; 1us; 23us; 2us; 22us; 6us; 18us; 7us; 19us; 10us; 21us; 2us; 16395us; 6us; 18us; 7us; 19us; 2us; 16396us; 6us; 18us; 7us; 19us; 4us; 16402us; 1us; 23us; 2us; 22us; 6us; 18us; 7us; 19us; 4us; 16403us; 1us; 23us; 2us; 22us; 6us; 18us; 7us; 19us; 4us; 32768us; 0us; 9us; 9us; 20us; 14us; 7us; 15us; 8us; 4us; 32768us; 0us; 9us; 9us; 20us; 14us; 7us; 15us; 8us; 4us; 32768us; 0us; 9us; 9us; 20us; 14us; 7us; 15us; 8us; 0us; 16394us; 4us; 32768us; 0us; 9us; 9us; 20us; 14us; 7us; 15us; 8us; 4us; 32768us; 0us; 9us; 9us; 20us; 14us; 7us; 15us; 8us; 1us; 16401us; 3us; 25us; 4us; 32768us; 0us; 9us; 9us; 20us; 14us; 7us; 15us; 8us; |]
let _fsyacc_actionTableRowOffsets = [|0us; 2us; 4us; 9us; 11us; 12us; 13us; 15us; 16us; 17us; 22us; 23us; 24us; 25us; 31us; 34us; 37us; 42us; 47us; 52us; 57us; 62us; 63us; 68us; 73us; 75us; |]
let _fsyacc_reductionSymbolCounts = [|1us; 1us; 3us; 3us; 3us; 1us; 1us; 2us; 3us; 3us; 3us; 3us; 3us; 1us; 3us; 3us; 0us; 1us; 1us; 3us; |]
let _fsyacc_productionToNonTerminalTable = [|0us; 1us; 1us; 2us; 2us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 4us; 4us; 4us; 5us; 5us; 6us; 6us; |]
let _fsyacc_immediateActions = [|65535us; 65535us; 65535us; 65535us; 16387us; 65535us; 65535us; 16389us; 16390us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16394us; 65535us; 65535us; 65535us; 65535us; |]
let _fsyacc_reductions ()  =    [| 
# 180 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : C)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : '_startcExpression));
# 189 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 47 "Parser.fsp"
                                       Belief(_1) 
                   )
# 47 "Parser.fsp"
                 : D));
# 200 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : D)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : D)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 48 "Parser.fsp"
                                                         BelievesSetSequence(_1,_3) 
                   )
# 48 "Parser.fsp"
                 : D));
# 212 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'array_values)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 50 "Parser.fsp"
                                                 BelievesSet(_2)
                   )
# 50 "Parser.fsp"
                 : C));
# 223 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : C)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : C)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 51 "Parser.fsp"
                                                         InitialValues(_1,_3) 
                   )
# 51 "Parser.fsp"
                 : C));
# 235 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : float)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 53 "Parser.fsp"
                                                             Num(_1) 
                   )
# 53 "Parser.fsp"
                 : a));
# 246 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 54 "Parser.fsp"
                                                             True(_1) 
                   )
# 54 "Parser.fsp"
                 : a));
# 257 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 55 "Parser.fsp"
                                                            Not(_2)
                   )
# 55 "Parser.fsp"
                 : a));
# 268 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 56 "Parser.fsp"
                                                             And(_1,_3) 
                   )
# 56 "Parser.fsp"
                 : a));
# 280 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 57 "Parser.fsp"
                                                             Or(_1,_3) 
                   )
# 57 "Parser.fsp"
                 : a));
# 292 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 58 "Parser.fsp"
                                                             _2 
                   )
# 58 "Parser.fsp"
                 : a));
# 303 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 59 "Parser.fsp"
                                                              Implication(_1,_3)
                   )
# 59 "Parser.fsp"
                 : a));
# 315 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 60 "Parser.fsp"
                                                                BiImplication(_1,_3)
                   )
# 60 "Parser.fsp"
                 : a));
# 327 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 62 "Parser.fsp"
                                                             False 
                   )
# 62 "Parser.fsp"
                 : Predicate));
# 337 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 63 "Parser.fsp"
                                                             Equal(_1,_3) 
                   )
# 63 "Parser.fsp"
                 : Predicate));
# 349 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Predicate)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 64 "Parser.fsp"
                                                             _2 
                   )
# 64 "Parser.fsp"
                 : Predicate));
# 360 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 67 "Parser.fsp"
                           [] 
                   )
# 67 "Parser.fsp"
                 : 'array_values));
# 370 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_values)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 68 "Parser.fsp"
                                      List.rev _1 
                   )
# 68 "Parser.fsp"
                 : 'array_values));
# 381 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 71 "Parser.fsp"
                                       [_1] 
                   )
# 71 "Parser.fsp"
                 : 'rev_values));
# 392 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_values)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : a)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 72 "Parser.fsp"
                                                        _3 :: _1 
                   )
# 72 "Parser.fsp"
                 : 'rev_values));
|]
# 405 "Parser.fs"
let tables () : FSharp.Text.Parsing.Tables<_> = 
  { reductions= _fsyacc_reductions ();
    endOfInputTag = _fsyacc_endOfInputTag;
    tagOfToken = tagOfToken;
    dataOfToken = _fsyacc_dataOfToken; 
    actionTableElements = _fsyacc_actionTableElements;
    actionTableRowOffsets = _fsyacc_actionTableRowOffsets;
    stateToProdIdxsTableElements = _fsyacc_stateToProdIdxsTableElements;
    stateToProdIdxsTableRowOffsets = _fsyacc_stateToProdIdxsTableRowOffsets;
    reductionSymbolCounts = _fsyacc_reductionSymbolCounts;
    immediateActions = _fsyacc_immediateActions;
    gotos = _fsyacc_gotos;
    sparseGotoTableRowOffsets = _fsyacc_sparseGotoTableRowOffsets;
    tagOfErrorTerminal = _fsyacc_tagOfErrorTerminal;
    parseError = (fun (ctxt:FSharp.Text.Parsing.ParseErrorContext<_>) -> 
                              match parse_error_rich with 
                              | Some f -> f ctxt
                              | None -> parse_error ctxt.Message);
    numTerminals = 19;
    productionToNonTerminalTable = _fsyacc_productionToNonTerminalTable  }
let engine lexer lexbuf startState = (tables ()).Interpret(lexer, lexbuf, startState)
let cExpression lexer lexbuf : C =
    Microsoft.FSharp.Core.Operators.unbox ((tables ()).Interpret(lexer, lexbuf, 0))
