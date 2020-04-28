class Symbol:
    def __init__(self, name, val=None):
        self.name = name
        self.val = val

    def __repr__(self):
        #return str((self.name, self.val))
        return self.name

    def eval(self):
        return self.val


class Negation:
    def __init__(self, p):
        self.p = p
        self.truth_table = {False: True, True: False}

    def __repr__(self):
        return "¬{0}".format(self.p)

    def eval(self):
        return self.truth_table[self.p.eval()]


class Conjunction:
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): False, (False, True): False,
                            (True, False): False, (True, True): True}

    def __repr__(self):
        return "{0} Λ {1}".format(self.p, self.q)

    def eval(self):
        return self.truth_table[(self.p.eval(), self.q.eval())]


class Disjunction:
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): False, (False, True): True,
                            (True, False): True, (True, True): True}

    def __repr__(self):
        return "{0} V {1}".format(self.p, self.q)

    def eval(self):
        return self.truth_table[(self.p.eval(), self.q.eval())]


class Implication:
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): True, (False, True): True,
                            (True, False): False, (True, True): True}

    def __repr__(self):
        return "{0} ⟶ {1}".format(self.p, self.q)

    def eval(self):
        return self.truth_table[(self.p.eval(), self.q.eval())]


class Biconditional:
    def __init__(self, p, q):
        self.p = p
        self.q = q
        self.truth_table = {(False, False): True, (False, True): False,
                            (True, False): False, (True, True): True}

    def __repr__(self):
        return "{0} ⟷ {1}".format(self.p, self.q)

    def eval(self):
        return self.truth_table[(self.p.eval(), self.q.eval())]
