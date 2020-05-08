from lexyacc import *
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
        menu.options[i + 1] = ("Back", lambda j: main_loop())
        print(menu)

        try:
            choice = int(input('> '))
            if choice in menu.options:
                menu.options[choice][1](choice)
        except ValueError:
            continue


def main_loop():
    cli.clear()
    opts = {1: ("Add new beliefs", lambda: new_belief_loop()),
            2: ("View current beliefs", lambda: belief_base_loop()),
            3: ("Exit", lambda: sys.exit())}
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


if __name__ == "__main__":
    # Load beliefs from file
    f = open("belief_base.txt", "r")
    for b in f:
        add_belief(parser.parse(b))
    main_loop()
    print(beliefs)
    for b in beliefs:
        print(b.cnf().neg())
