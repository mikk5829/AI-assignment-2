import unittest

from lexyacc import *


class BeliefTests(unittest.TestCase):
    def test_simple(self):
        beliefs = []
        test, answer = "!p&&v", "¬p Λ v"
        symbols = parser.parse(test)
        beliefs.append(symbols)
        str1 = ''.join(str(e) for e in beliefs)
        self.assertEqual(str1, answer)

    def test_moderate(self):
        beliefs = []
        test, answer = "!p&&v||r->!p", "¬p Λ v V r ⟶ ¬p"
        symbols = parser.parse(test)
        beliefs.append(symbols)
        str1 = ''.join(str(e) for e in beliefs)
        self.assertEqual(str1, answer)

if __name__ == '__main__':
    unittest.main()
