# -*- coding: utf-8 -*-
"""
Created on Mon Oct 21 13:26:03 2019

@author: Lenovo
"""

file = open("../QAPLIB - Processed/esc16a.txt", "r")
file_write = open("../QAPLIB - Processed/esc16a_processed.txt", "+w")
lines = file.readlines()
cnt = 0
cnt2 = 0
done = True
for line in lines:
    if done:
        begin_of_line_1 = True
        if cnt == 0 :
            cnt = int(line)+3
            maxi = cnt - 1
            file_write.write("MatrixF = new List<List<int>>(){")
        elif cnt > 1:
            if line.strip():
                if len(line) > 2: #zmienic gdy wymiar macierzy ma wiecej niz 2 cyfry  
                    i = 0
                    while i < len(line):
                        if line[i] == ' ':
                            if not begin_of_line_1:
                                file_write.write(", ")
                            while(line[i+1] == ' '):
                                i = i + 1
                        elif line[i] == '\n':
                            if cnt > 2:
                                file_write.write("},")
                            else:
                                file_write.write("}")
                        else:
                            file_write.write(line[i])
                            begin_of_line_1 = False
                        i = i+1                 
            if cnt > 2:
                file_write.write("\n\t" + "new List<int>(){")
            else:
                file_write.write("\n\t};\n\n")
                done = False
        cnt = cnt - 1
    else:
        begin_of_line = True
        if cnt2 == 0 :
            cnt2 = maxi
            file_write.write("MatrixD = new List<List<int>>(){"+ "\n\t" + "new List<int>(){")
        elif cnt2 > 1:
            if line.strip():
                if len(line) > 2: #jak cos to tu   
                    i = 0
                    while i < len(line):
                        if line[i] == ' ':
                            if not begin_of_line:
                                file_write.write(", ")
                            while(line[i+1] == ' '):
                                i = i + 1
                        elif line[i] == '\n':
                            if cnt2 > 2:
                                file_write.write("},")
                            else:
                                file_write.write("}")
                        else:
                            file_write.write(line[i])
                            begin_of_line = False
                        i = i+1                 
            if cnt2 > 2:
                file_write.write("\n\t" + "new List<int>(){")
            else:
                file_write.write("}\n\t};\n\n")
                done = False
        cnt2 = cnt2 - 1
file_write.close()
file.close()
