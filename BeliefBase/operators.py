class Symbol:
    def __init__(self, name, val=None):
        self.name = name
        self.val = val

    def __repr__(self):
        #return str((self.name, self.val))
        return self.name

    def eval(self, tt):
        return tt[self.name] == "1"

    def cnf(self):
        return self

    def neg(self):
        return self

    def TT(self):
        return [self.name]


class Negation:
    def __init__(self, p):
        self.p = p
        self.truth_table = {False: True, True: False}

    def __repr__(self):
        return "¬{0}".format(self.p)

    def eval(self, tt):
        return self.truth_table[self.p.eval(tt)]

    def neg(self):
        if (isinstance(self.p, Negation)):
            return self.p.p.neg()
        elif isinstance(self.p, Conjunction):
            return Disjunction(Negation(self.p.p).neg(), Negation(self.p.q).neg())
        elif isinstance(self.p, Disjunction):
            return Conjunction(Negation(self.p.p).neg(), Negation(self.p.q).neg())
        elif isinstance(self.p, Symbol):
            return self
        return self

    def cnf(self):
        return Negation(self.p.cnf())

    def TT(self):
        return self.p.TT()


class Conjunction:
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): False, (False, True): False,
                            (True, False): False, (True, True): True}

    def __repr__(self):
        return "{0} Λ {1}".format(self.p, self.q)

    def eval(self, tt):
        return self.truth_table[(self.p.eval(tt), self.q.eval(tt))]

    def cnf(self):
        return Conjunction(self.p.cnf(), self.q.cnf())

    def neg(self):
        return Disjunction(self.p.neg(), self.q.neg())

    def TT(self):
        symbols = []
        symbols.extend(self.p.TT())
        symbols.extend(self.q.TT())
        return symbols


class Disjunction:
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): False, (False, True): True,
                            (True, False): True, (True, True): True}

    def __repr__(self):
        return "{0} V {1}".format(self.p, self.q)

    def eval(self, tt):
        return self.truth_table[(self.p.eval(tt), self.q.eval(tt))]

    def cnf(self):
        return Disjunction(self.p.cnf(), self.q.cnf())

    def neg(self):
        return Conjunction(self.p.neg(), self.q.neg())

    def TT(self):
        symbols = []
        symbols.extend(self.p.TT())
        symbols.extend(self.q.TT())
        return symbols


class Implication:
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): True, (False, True): True,
                            (True, False): False, (True, True): True}

    def __repr__(self):
        return "{0} ⟶ {1}".format(self.p, self.q)

    def eval(self, tt):
        return self.truth_table[(self.p.eval(tt), self.q.eval(tt))]

    def cnf(self):
        return Disjunction(Negation(self.p.cnf()), self.q.cnf())

    def neg(self):
        return Implication(self.p.neg(), self.q.neg())

    def TT(self):
        symbols = []
        symbols.extend(self.p.TT())
        symbols.extend(self.q.TT())
        return symbols


class Biconditional:
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): True, (False, True): False,
                            (True, False): False, (True, True): True}

    def __repr__(self):
        return "{0} ⟷ {1}".format(self.p, self.q)

    def eval(self, tt):
        return self.truth_table[(self.p.eval(tt), self.q.eval(tt))]

    def cnf(self):
        return Conjunction(Implication(self.p.cnf(), self.q.cnf()).cnf(), Implication(self.q.cnf(), self.p.cnf()).cnf())

    def neg(self):
        return Biconditional(self.p.neg(), self.q.neg())

    def TT(self):
        symbols = []
        symbols.extend(self.p.TT())
        symbols.extend(self.q.TT())
        return symbols

