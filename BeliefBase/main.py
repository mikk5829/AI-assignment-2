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
    for i in range(0, 2**n):
        vals = str("{0:b}".format(i)).rjust(n, "0")
        d = {}
        for s in range(0, n):
            d[symbols[s]] = vals[s]
        dicts.append(d.copy());
    return dicts;

def pl_true(knowledge_base, model):
    if knowledge_base.truth_table in (True, False):
        return knowledge_base
    for k in knowledge_base:
        print(k, ":", k.__class__)
    operator, args = knowledge_base.__class__, knowledge_base.args
    print(model)
    return True


def check_all(knowledge_base, propositional_logic, symbols, model):
    if not symbols:
        if pl_true(propositional_logic, model): # TODO skift med knowledge_base
            return pl_true(propositional_logic, model)
        else:
            return True
    else:
        p, *rest = symbols
        return check_all(knowledge_base, propositional_logic, rest, eval('{**model, p: True}')) and check_all(
            knowledge_base, propositional_logic, rest, eval('{**model, p: False}'))


def entail(knowledge_base, propositional_logic):
    symbols = knowledge_base + propositional_logic
    return check_all(knowledge_base, propositional_logic, symbols, {})


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
beliefs = [parser.parse("!a && b"), parser.parse("c && b"), parser.parse("d || a || b")]

print(beliefs)
TT = []
for t in TruthTable(beliefs):
    valid = True
    for b in beliefs:
        if not b.cnf().eval(t):
            valid = False
    if valid:
        TT.append(t)

for t in TT:
    print(t)


# CNF https://math.stackexchange.com/questions/214338/how-to-convert-to-conjunctive-normal-form
