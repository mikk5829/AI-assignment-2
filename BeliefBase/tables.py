def getSymbols(B):
    symbols = []
    print(B)
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


def entails(truth_table, new_belief_truth_table):
    is_entailing = False
    for premise in truth_table:
        if premise in new_belief_truth_table:
            new_belief_truth_table.remove(premise)
            is_entailing = True
        else:
            return False
    return is_entailing