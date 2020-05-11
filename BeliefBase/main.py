from lexyacc import beliefs, parser
from tables import entails, get_variables, valid_truth_table, contract
from operators import Negation
import cli
import sys


def new_belief_loop():
    cli.clear()
    menu = cli.Menu("Add new belief",
                    "Enter new beliefs here, e.g. 'p||(q<->!s)'.\nYou can write comma-separated beliefs on the same line.\nType DONE to return to main menu.",
                    {})
    print('{}\n{}\n=====================\n'.format(menu.title.upper(), menu.subtitle))

    while True:
        input_str = input('belief > ').split(',')

        for b in input_str:
            if b.casefold() == 'done':
                return
            try:
                belief = parser.parse(b)
                contract_belief(Negation(belief))
                add_belief(belief)
                # TODO: Check for contradictions etc here
            except TypeError:
                print("Error parsing belief", b)


def belief_base_loop():
    cli.clear()
    menu = cli.Menu("Current beliefs", "Select a belief to contract it", {})
    while True:
        cli.clear()
        i = 1
        menu.options = {}
        while i <= len(beliefs):
            menu.options[i] = (str(beliefs[i - 1]), lambda j: contract_belief(beliefs[j - 1]))
            i += 1
        menu.options[i] = ("Back", lambda j: main_loop())
        print(menu)

        try:
            choice = int(input('> '))
            if choice in menu.options:
                menu.options[choice][1](choice)
        except ValueError:
            continue


def entailment_loop():
    cli.clear()
    menu = cli.Menu("Logical entailment", "Type a belief to check if it is a logical consequence of the belief base",
                    {})

    while True:
        s = input('belief > ')
        if s.upper() == 'DONE':
            main_loop()
            break

        try:
            belief = parser.parse(s)
            print(entails(beliefs, [belief]))

        except TypeError:
            print("Error parsing belief")


def contract_loop():
    cli.clear()
    menu = cli.Menu("Contraction", "Please enter a sentence to contract from belief base", {})
    global beliefs

    while True:
        s = input("belief > ")
        if s.upper() == "DONE":
            main_loop()
            break

        try:
            belief = parser.parse(s)
            beliefs = contract(beliefs, belief)
            print(beliefs)
        except:
            print("Error contracting belief")

def TT():
    cli.clear();
    symbs = get_variables(beliefs)
    rows = valid_truth_table(beliefs, symbs)
    for r in rows:
        print(r)

def main_loop():
    cli.clear()
    opts = {1: ("Add new beliefs", lambda: new_belief_loop()),
            2: ("View current beliefs", lambda: belief_base_loop()),
            3: ("Check logical entailment", lambda: entailment_loop()),
            4: ("Contract a new belief", lambda: contract_loop()),
            5: ("TruthTable", lambda: TT()),
            6: ("Save and exit", lambda: pozegnanie())}
    menu = cli.Menu("Belief Base", "Welcome!", opts)

    while True:
        cli.clear()
        print(menu)

        try:
            choice = int(input('> '))
            if choice in menu.options:
                selected_menu = menu.options[int(choice)]
                selected_menu[1]()
        except ValueError:
            continue


def add_belief(belief):
    beliefs.append(belief)


def contract_belief(belief):
    global beliefs
    beliefs = contract(beliefs, belief)


def load():
    f = open("belief_base.txt", "r+")
    for b in f:
        add_belief(parser.parse(b))


def pozegnanie():
    """Saves the belief base and exits the program"""
    f = open("belief_base.txt", "w")
    f.writelines([b.input_format() + '\n' for b in beliefs])
    f.close()
    sys.exit()


if __name__ == "__main__":
    load()  # Load beliefs from file
    main_loop()
