from functools import reduce


def get_variables(B):
    variables = []
    for b in B:
        variables.extend(b.TT())
    variables = list(dict.fromkeys(variables))
    return variables


def truth_table(variables):
    """
    Returns a truth table, from given variables
    """
    n = len(variables)
    dicts = []
    for i in range(0, 2 ** n):
        vals = str("{0:b}".format(i)).rjust(n, "0")
        d = {}
        for s in range(0, n):
            d[variables[s]] = (vals[s] == "1")
        dicts.append(d.copy())
    return dicts


def valid_truth_table(belief_base, variables):
    """
    Returns a valid truth table with given variables, useful for logical entailment
    """
    TT = []
    for t in truth_table(variables):
        valid = True
        for b in belief_base:
            if not b.cnf().eval(t):
                valid = False
        if valid:
            TT.append(t)
    return TT


def entails(B1, B2):
    """Checks if B1 entails B2"""
    variables = get_variables(B1 + B2)
    tt = valid_truth_table(B1, variables)
    new_belief_truth_table = valid_truth_table(B2, variables)
    if len(tt) == 0 or len(new_belief_truth_table) == 0:
        print("paradox found")
        return False

    is_entailing = False
    for premise in tt:
        if premise in new_belief_truth_table:
            new_belief_truth_table.remove(premise)
            is_entailing = True
        else:
            return False
    return is_entailing


def contract(B, p):
    def subsets(s):
        return reduce(lambda P, x: P + [subset | {x} for subset in P], s, [set()])[1::]

    bs = subsets(B)
    new_B = {}
    for b in bs:
        if not entails(list(b), [p]):
            new_B = b

    return list(new_B)
