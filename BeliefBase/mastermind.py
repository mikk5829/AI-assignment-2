import itertools


def init_mastermind():
    f = open("mastermind.txt", "w")
    # colors = ["green", "yellow", "red", "blue"]
    # positions = ["one", "two", "three", "four"]
    colors = ["t", "s", "d"]
    positions = ["one", "one"]
    for p in positions:
        colors_positions = "".join([c + "_" + p + "||" for c in colors])
        f.writelines(colors_positions[:-2])
        f.writelines("\n")
    combinations = []
    for p in positions:
        combinations.append([c + "_" + p for c in colors])
    res = list(itertools.product(*combinations))
    print(res)

init_mastermind()