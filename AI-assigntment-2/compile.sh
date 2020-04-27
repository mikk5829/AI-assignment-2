#!/bin/bash

mono FsLexYacc.10.0.0/build/fslex/net46/fslex.exe Lexer.fsl --unicode

mono FsLexYacc.10.0.0/build/fsyacc/net46/fsyacc.exe Parser.fsp --module Parser