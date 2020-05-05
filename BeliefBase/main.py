from operators import *
from lexyacc import *
from consolemenu import *
from consolemenu.format import *
from consolemenu.items import *


def add_belief(belief):
    beliefs.append(belief)


def remove_belief(belief,menu):
    print("deleting",belief)
    beliefs.remove(belief)
    menu.draw()

if __name__ == "__main__":
    # Load beliefs from file
    f = open("belief_base.txt", "r")
    for belief in f:
        add_belief(parser.parse(belief))

    print("Loaded belief base", beliefs)

    menu_format = MenuFormatBuilder().set_border_style_type(MenuBorderStyleType.HEAVY_BORDER) \
        .set_prompt("SELECT>") \
        .set_title_align('center') \
        .set_subtitle_align('center') \
        .set_left_margin(4) \
        .set_right_margin(4) \
        .show_header_bottom_border(True)

    menu = ConsoleMenu("Belief Base","Currently {} beliefs in memory".format(len(beliefs)),formatter=menu_format)
    function_item = FunctionItem("Add new belief", input, ["new belief > "])

    submenu = ConsoleMenu("Current beliefs",subtitle="Select a belief to delete it",formatter=menu_format)
    for belief in beliefs:
        submenu.append_item(FunctionItem(belief,remove_belief, [belief,menu]))

    submenu_item = SubmenuItem("View beliefs", submenu=submenu)
    submenu_item.set_menu(menu)
    menu.append_item(function_item)
    menu.append_item(submenu_item)
    menu.show()

    print("Type DONE to ")
    while True:
        try:
            s = input('belief > ')
            if s == 'DONE':
                break
        except EOFError:
            break

        belief = parser.parse(s)
        add_belief(belief)
        # TODO: Check for contradictions etc here

    print(beliefs)

# CNF https://math.stackexchange.com/questions/214338/how-to-convert-to-conjunctive-normal-form
