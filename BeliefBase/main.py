from operators import *
import lexyacc
from lexyacc import *


def TruthTable(B):
    symbols = []
    for b in B:
        symbols.extend(b.TT())
    symbols = list(dict.fromkeys(symbols))
    n = len(symbols)
    dicts = []
    for i in range(0, 2 ** n):
        vals = str("{0:b}".format(i)).rjust(n, "0")
        d = {}
        for s in range(0, n):
            d[symbols[s]] = (vals[s] == "1")
        dicts.append(d.copy())
    return dicts


def valid_truth_table(belief_base):
    TT = []
    for t in TruthTable(belief_base):
        valid = True
        for b in belief_base:
            if not b.cnf().eval(t):
                valid = False
        if valid:
            TT.append(t)
    return TT


def entails(premise_truth_table, conclusion_truth_table):
    symbols = []
    for b in premise_truth_table+conclusion_truth_table:
        symbols.extend(b)
    symbols = list(dict.fromkeys(symbols))
    is_entailing = False
    for premise in premise_truth_table:
        if premise in conclusion_truth_table:
            conclusion_truth_table.remove(premise)
            is_entailing = True
        else:
            return not is_entailing
    return is_entailing

"""
while True:
    # try:
    #     s = input('belief > ')
    #     if s == 'DONE':
    #         break
    # except EOFError:
    #     break

    kb = "!p&&v"
    pl = "p"
    kb_symbol = parser.parse(kb)
    kb_symbols.append(kb_symbol)
    pl_symbol = parser.parse(pl)
    pl_symbols.append(pl_symbol)
    break

    # TODO: Check for contradictions etc here
    #if belief.__class__ is Symbol:
    #    belief.val = True
    #elif belief.__class__ is Negation and belief.p.__class__ is Symbol:
    #    belief.p.val = False
"""
# beliefs = [parser.parse("!a && b"), parser.parse("c && b"), parser.parse("d || a || b")]
# conclusions = [parser.parse("!a && b"), parser.parse("c && b"), parser.parse("d || a || b")]
beliefs = [parser.parse("q || p")]
conclusions = [parser.parse("p && q")]

print("Belief:     ", beliefs)
print("Conclusion: ", conclusions)

BTT = valid_truth_table(beliefs)
for t in BTT:
    print(t)
CTT = valid_truth_table(conclusions)
for t in CTT:
    print(t)
print(entails(BTT, CTT))

# CNF https://math.stackexchange.com/questions/214338/how-to-convert-to-conjunctive-normal-form
