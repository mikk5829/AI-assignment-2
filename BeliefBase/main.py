from operators import *
import lexyacc
from lexyacc import *


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

print(beliefs)
for b in beliefs:
    print(b.cnf().neg())

# CNF https://math.stackexchange.com/questions/214338/how-to-convert-to-conjunctive-normal-form
