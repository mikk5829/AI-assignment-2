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

"""
while True:
    try:
        s = input('belief > ')
        if s == 'DONE':
            break
    except EOFError:
        break

    belief = parser.parse(s)
    beliefs.append(belief)

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
