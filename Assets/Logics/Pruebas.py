# -*- coding: utf-8 -*-
from re import I
import sys  
from shapely.geometry import mapping


cantidad_argumentos = len(sys.argv)


if cantidad_argumentos > 1:
    print(f"Se proporcionaron {cantidad_argumentos} argumentos.")

    if(cantidad_argumentos == 2):
        for indice, argumento in enumerate(sys.argv[0:], start=0):
            print(f"Argumento {indice}: {argumento}")
    else:
        print("Sobran argumentos")
else:
    print("No se proporcionaron argumentos.")