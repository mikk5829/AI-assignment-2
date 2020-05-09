import unittest

from lexyacc import parser
from tables import entails


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


if __name__ == '__main__':
    unittest.main()
