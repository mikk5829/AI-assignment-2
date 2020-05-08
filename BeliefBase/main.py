from lexyacc import *
from tables import entails, getSymbols, valid_truth_table, contract
import cli
import sys


def new_belief_loop():
    cli.clear()
    menu = cli.Menu("Add new belief",
                    "Enter new beliefs here, e.g. 'p||(q<->!s)'.\n Type DONE to return to main menu.", {})
    print('{}\n{}\n=====================\n'.format(menu.title.upper(), menu.subtitle))

    while True:
        s = input('belief > ')
        if s.upper() == 'DONE':
            main_loop()
            break

        try:
            belief = parser.parse(s)
            add_belief(belief)

        # TODO: Check for contradictions etc here

        except TypeError:
            print("Error parsing belief")


def belief_base_loop():
    cli.clear()
    menu = cli.Menu("Current beliefs", "Select a belief to delete it", {})

    while True:
        cli.clear()
        i = 0
        menu.options = {}
        while i < len(beliefs):
            menu.options[i] = (str(beliefs[i]), lambda j: remove_belief(j))
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
    menu = cli.Menu("Logical entailment THIS IS WIP!", "Select a belief to delete it", {})

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


def contraction_loop():
    cli.clear()
    menu = cli.Menu("Contraction", "Enter a belief to contract, eg 'p'\nType DONE to return to main menu", {})

    while True:
        s = input('belief to contract > ')
        p = parser.parse(s)
        print(contract(beliefs, p))
        if s.upper() == 'DONE':
            main_loop()
            break


def main_loop():
    cli.clear()
    opts = {1: ("Add new beliefs", lambda: new_belief_loop()),
            2: ("View current beliefs", lambda: belief_base_loop()),
            3: ("Check logical entailment", lambda: entailment_loop()),
            4: ("Contract beliefs", lambda: contraction_loop()),
            5: ("Save and exit", lambda: arrivederci())}
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

def remove_belief(belief):
    del beliefs[belief]

# conclusions = [parser.parse("b")]
# beliefs = [parser.parse("!a && b"), parser.parse("c && b"), parser.parse("d || a || b")]
# CTT = valid_truth_table(conclusions, getSymbols(beliefs + conclusions))
# BTT = valid_truth_table(beliefs, getSymbols(beliefs + conclusions))
# print(entails(BTT, CTT))

def load():
    f = open("belief_base.txt", "r")
    for b in f:
        add_belief(parser.parse(b))


def arrivederci():
    print(beliefs)
    f = open("belief_base.txt", "w")
    lines = [b.input_format() + '\n' for b in beliefs]
    print(lines)
    f.writelines([b.input_format() + '\n' for b in beliefs])
    f.close()

    sys.exit()


if __name__ == "__main__":
    load()  # Load beliefs from file
    main_loop()
