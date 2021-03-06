class Var:
    """
    Var is a true predicate variable
    """
    def __init__(self, name, val=None):
        self.name = name
        self.val = val
        self.truth_table = {False: False, True: True}

    def __repr__(self):
        return self.name

    def input_format(self):
        return self.name

    def eval(self, tt):
        return tt[self.name]

    def cnf(self):
        return self

    def TT(self):
        return [self.name]


class Negation:
    """
    Negation is a false predicate variable
    """
    def __init__(self, p):
        self.p = p
        self.truth_table = {False: True, True: False}

    def __repr__(self):
        return "¬{0}".format(self.p)

    def input_format(self):
        return "!({0})".format(self.p.input_format())

    def eval(self, tt):
        return self.truth_table[self.p.eval(tt)]

    def cnf(self):
        return Negation(self.p.cnf())

    def TT(self):
        return self.p.TT()


class Conjunction:
    """
    Conjunction is a compound statement formed by joining two statements with the connector AND
    """
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): False, (False, True): False,
                            (True, False): False, (True, True): True}

    def __repr__(self):
        return "{0} Λ {1}".format(self.p, self.q)

    def input_format(self):
        return "({0})&&({1})".format(self.p.input_format(), self.q.input_format())

    def eval(self, tt):
        return self.truth_table[(self.p.eval(tt), self.q.eval(tt))]

    def cnf(self):
        return Conjunction(self.p.cnf(), self.q.cnf())

    def TT(self):
        variables = []
        variables.extend(self.p.TT())
        variables.extend(self.q.TT())
        return variables


class Disjunction:
    """
    Disjunction is a compound statement formed by joining two statements with the connector OR
    """
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): False, (False, True): True,
                            (True, False): True, (True, True): True}

    def __repr__(self):
        return "{0} V {1}".format(self.p, self.q)

    def input_format(self):
        return "({0})||({1})".format(self.p.input_format(), self.q.input_format())

    def eval(self, tt):
        return self.truth_table[(self.p.eval(tt), self.q.eval(tt))]

    def cnf(self):
        return Disjunction(self.p.cnf(), self.q.cnf())

    def TT(self):
        variables = []
        variables.extend(self.p.TT())
        variables.extend(self.q.TT())
        return variables


class Implication:
    """
    Implication is a compound statement formed by joining two statements with the connector IMPLICATION
    """
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): True, (False, True): True,
                            (True, False): False, (True, True): True}

    def __repr__(self):
        return "{0} ⟶  {1}".format(self.p, self.q)

    def input_format(self):
        return "({0})->({1})".format(self.p.input_format(), self.q.input_format())

    def eval(self, tt):
        return self.truth_table[(self.p.eval(tt), self.q.eval(tt))]

    def cnf(self):
        return Disjunction(Negation(self.p.cnf()), self.q.cnf())

    def TT(self):
        variables = []
        variables.extend(self.p.TT())
        variables.extend(self.q.TT())
        return variables


class Biconditional:
    """
    Biconditional is a compound statement formed by joining two statements with the connector BIIMPLICATION
    """
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): True, (False, True): False,
                            (True, False): False, (True, True): True}

    def __repr__(self):
        return "{0} ⟷  {1}".format(self.p, self.q)

    def input_format(self):
        return "({0})<->({1})".format(self.p.input_format(), self.q.input_format())

    def eval(self, tt):
        return self.truth_table[(self.p.eval(tt), self.q.eval(tt))]

    def cnf(self):
        return Conjunction(Implication(self.p.cnf(), self.q.cnf()).cnf(), Implication(self.q.cnf(), self.p.cnf()).cnf())

    def TT(self):
        symbols = []
        symbols.extend(self.p.TT())
        symbols.extend(self.q.TT())
        return symbols
