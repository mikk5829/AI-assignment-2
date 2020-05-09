import platform
import os
import main


class Menu:
    def __init__(self, title, subtitle, options):
        self.title = title
        self.subtitle = subtitle
        self.options = options

    def __repr__(self):
        """String repr. of a header and a list of menu options"""
        option_list = '\n'.join([str(option) + '. ' + self.options[option][0] for option in self.options])
        label_string = '{}\n{}\n=====================\n'.format(self.title.upper(), self.subtitle)
        return label_string + option_list + '\n......................\n'


# Clears the console
def clear():
    if platform.system() == "Windows":
        os.system('cls')

    else:  # Linux and Mac
        os.system('clear')
    return
