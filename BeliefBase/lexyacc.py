from operators import *
import ply.lex as lex
import ply.yacc as yacc

symbols = {}
beliefs = []

# List of token names.   This is always required
tokens = (
    'NAME',
    'NEGATION',
    'CONJUNCTION',
    'DISJUNCTION',
    'IMPLICATION',
    'BICONDITIONAL',
    'COMMA',
    'LPAREN',
    'RPAREN',
    'LBRACKET',
    'RBRACKET',
)

# Regular expression rules for simple tokens
t_NEGATION = r'!'
t_CONJUNCTION = r'&{2}'
t_DISJUNCTION = r'(\|{2})'
t_IMPLICATION = r'(->)'
t_BICONDITIONAL = r'(<->)'
t_COMMA = r','
t_LPAREN = r'\('
t_RPAREN = r'\)'
t_LBRACKET = r'\{'
t_RBRACKET = r'\}'
t_ignore = ' \t\n'


def t_NAME(t):
    r'[a-zA-Z_]+'
    if t.value not in symbols:
        symbols[t.value] = Symbol(t.value)
    return t


# Error handling rule
def t_error(t):
    raise TypeError("Illegal character: {0}".format(t.value[0]))


# Build the lexer
lexer = lex.lex()


# PARSING RULES
precedence = (
    ('left', 'IMPLICATION', 'BICONDITIONAL'),
    ('left', 'CONJUNCTION', 'DISJUNCTION'),
    ('left', 'NEGATION'),
)


def p_expression_binop(p):
    """
    expression  : NEGATION expression
                | expression CONJUNCTION expression
                | expression DISJUNCTION expression
                | expression IMPLICATION expression
                | expression BICONDITIONAL expression
    """

    if p[1] == '!':
        p[0] = Negation(p[2])
    elif p[2] == '&&':
        p[0] = Conjunction(p[1], p[3])
    elif p[2] == '||':
        p[0] = Disjunction(p[1], p[3])
    elif p[2] == '->':
        p[0] = Implication(p[1], p[3])
    elif p[2] == '<->':
        p[0] = Biconditional(p[1], p[3])


def p_expression_group(p):
    'expression : LPAREN expression RPAREN'
    p[0] = p[2]


def p_expression_name(p):
    'expression : NAME'
    p[0] = symbols[p[1]]


def p_error(p):
    raise TypeError("Syntax error: {0}".format(p.value))


# Build the parser
parser = yacc.yacc()
