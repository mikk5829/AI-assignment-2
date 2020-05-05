from operators import *
import lexyacc
from lexyacc import *


def pl_true(knowledge_base, model):
    pass


def check_all(knowledge_base, propositional_logic, symbols, model):
    if len(symbols) == 0:
        if pl_true(knowledge_base, model):
            return pl_true(propositional_logic, model)
        else:
            return True
    else:
        p, *rest = symbols
        return check_all(knowledge_base, propositional_logic, rest, model.union(p=True))


def entail(knowledge_base, propositional_logic):
    symbols = knowledge_base + propositional_logic
    return check_all(knowledge_base, propositional_logic, symbols, set())


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
    # if belief.__class__ is Symbol:
    #    belief.val = True
    # elif belief.__class__ is Negation and belief.p.__class__ is Symbol:
    #    belief.p.val = False

print(beliefs)

# CNF https://math.stackexchange.com/questions/214338/how-to-convert-to-conjunctive-normal-form
