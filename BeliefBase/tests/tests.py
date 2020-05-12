import unittest

from lexyacc import *
from tables import contract, entails


class BeliefTests(unittest.TestCase):
    def test_one(self):
        beliefs = []
        test, answer = "!p&&v", "¬p Λ v"
        symbols = parser.parse(test)
        beliefs.append(symbols)
        str1 = ''.join(str(e) for e in beliefs)
        self.assertEqual(str1, answer)

    def test_two(self):
        beliefs = []
        test, answer = "!p&&v||r->!p", "¬p Λ v V r ⟶ ¬p"
        symbols = parser.parse(test)
        beliefs.append(symbols)
        str1 = ''.join(str(e) for e in beliefs)
        self.assertEqual(str1, answer)

    def test_three(self):
        beliefs = []
        test, answer = ["p", "p&&q", "p||q", "p<->q"], "p,p Λ q,p V q,p ⟷ q"
        for b in test:
            symbols1 = parser.parse(b)
            beliefs.append(symbols1)
        str1 = ','.join(str(e) for e in beliefs)
        self.assertEqual(str1, answer)


class ContractionTest(unittest.TestCase):
    def test_one(self):
        beliefs = []
        belief, contain = ["p", "p&&q", "p||q", "p<->q"], "p"
        for b in belief:
            symbols1 = parser.parse(b)
            beliefs.append(symbols1)
        contains = parser.parse(contain)
        contract_list = contract(beliefs, contains)
        str1 = ','.join(str(e) for e in contract_list)
        self.assertEqual(str1, "p ⟷ q")


class LogicalEntailmentTest(unittest.TestCase):
    def test_one(self):
        beliefs = []
        conclusions = []
        belief, conclusion = "p", "p||q"
        symbols1 = parser.parse(belief)
        beliefs.append(symbols1)
        symbols2 = parser.parse(conclusion)
        conclusions.append(symbols2)
        self.assertTrue(entails(beliefs, conclusions))

    def test_two(self):
        beliefs = []
        conclusions = []
        belief, conclusion = ["q", "p"], "p&&q"
        for b in belief:
            symbols1 = parser.parse(b)
            beliefs.append(symbols1)
        symbols2 = parser.parse(conclusion)
        conclusions.append(symbols2)
        self.assertTrue(entails(beliefs, conclusions))

    def test_three(self):
        beliefs = []
        conclusions = []
        belief, conclusion = ["q", "p"], "p<->q"
        for b in belief:
            symbols1 = parser.parse(b)
            beliefs.append(symbols1)
        symbols2 = parser.parse(conclusion)
        conclusions.append(symbols2)
        self.assertTrue(entails(beliefs, conclusions))

    def test_four(self):
        beliefs = []
        conclusions = []
        belief, conclusion = ["q", "p", "r"], "p && q -> !r"
        for b in belief:
            symbols1 = parser.parse(b)
            beliefs.append(symbols1)
        symbols2 = parser.parse(conclusion)
        conclusions.append(symbols2)
        self.assertFalse(entails(beliefs, conclusions))

    def test_five(self):
        """paradox test"""
        beliefs = []
        conclusions = []
        belief, conclusion = "p&&!p", "q"
        symbols1 = parser.parse(belief)
        beliefs.append(symbols1)
        symbols2 = parser.parse(conclusion)
        conclusions.append(symbols2)
        self.assertFalse(entails(beliefs, conclusions))


if __name__ == '__main__':
    unittest.main()
