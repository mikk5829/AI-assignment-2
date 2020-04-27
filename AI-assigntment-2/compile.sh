#!/bin/bash

mono FsLexYacc.10.0.0/build/fslex/net46/fslex.exe Lexer.fsl --unicode
mono FsLexYacc.10.0.0/build/fslex/net46/fslex.exe SignAnalysisLexer.fsl --unicode
mono FsLexYacc.10.0.0/build/fslex/net46/fslex.exe SecurityAnalysisLexer.fsl --unicode

mono FsLexYacc.10.0.0/build/fsyacc/net46/fsyacc.exe Parser.fsp --module Parser
mono FsLexYacc.10.0.0/build/fsyacc/net46/fsyacc.exe SignAnalysisParser.fsp --module SignAnalysisParser
mono FsLexYacc.10.0.0/build/fsyacc/net46/fsyacc.exe SecurityAnalysisParser.fsp --module SecurityAnalysisParser