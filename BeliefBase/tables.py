from functools import reduce

def getSymbols(B):
    symbols = []
    for b in B:
        symbols.extend(b.TT())
    symbols = list(dict.fromkeys(symbols))
    return symbols


def TruthTable(symbols):
    n = len(symbols)
    dicts = []
    for i in range(0, 2 ** n):
        vals = str("{0:b}".format(i)).rjust(n, "0")
        d = {}
        for s in range(0, n):
            d[symbols[s]] = (vals[s] == "1")
        dicts.append(d.copy())
    return dicts


def valid_truth_table(belief_base, symbols):
    TT = []
    for t in TruthTable(symbols):
        valid = True
        for b in belief_base:
            if not b.cnf().eval(t):
                valid = False
        if valid:
            TT.append(t)
    return TT


def entails(B1, B2):
    symbols = getSymbols(B1+B2)
    truth_table = valid_truth_table(B1, symbols)
    new_belief_truth_table = valid_truth_table(B2, symbols)

    is_entailing = False
    for premise in truth_table:
        if premise in new_belief_truth_table:
            new_belief_truth_table.remove(premise)
            is_entailing = True
        else:
            return False
    return is_entailing


def contract(B, p):
    subsets = lambda s: reduce(lambda P, x: P + [subset | {x} for subset in P], s, [set()])[1::]
    bs = subsets(B)
    newB = {}
    for b in bs:
        if not entails(list(b), [p]):
            newB = b

    print("New B: ", newB)

