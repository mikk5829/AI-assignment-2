from lexyacc import *
from tables import entails, getSymbols, valid_truth_table

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
conclusions = [parser.parse("b")]
# beliefs = [parser.parse("p")]
# conclusions = [parser.parse("p && q")]

print("Belief:     ", beliefs)
print("Conclusion: ", conclusions)

print(getSymbols(beliefs + conclusions))
BTT = valid_truth_table(beliefs, getSymbols(beliefs + conclusions))


for t in BTT:
    print(t)
print("---")
CTT = valid_truth_table(conclusions, getSymbols(beliefs + conclusions))
for t in CTT:
    print(t)
print(entails(BTT, CTT))

# CNF https://math.stackexchange.com/questions/214338/how-to-convert-to-conjunctive-normal-form
