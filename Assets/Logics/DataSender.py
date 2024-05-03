# -*- coding: utf-8 -*-
from tkinter import Place
import osmnx as ox
import json
import sys  
import os
from shapely.geometry import mapping
import time

def addSpaceToCity(a):
    partes = a.split(',', 1)
    return partes[0] + ', ' + partes[1]

cantidad_argumentos = len(sys.argv)

print("estamos")
for aaaa in sys.argv:
    print(type(aaaa))

#la ciudad se pasa como argumentos. Por defecto = Valencia, Spain
if cantidad_argumentos > 1:
    print(f"Se proporcionaron {cantidad_argumentos} argumentos.")

    if(cantidad_argumentos == 2):
        place_name = sys.argv[1]
        graph = ox.graph_from_place(place_name, network_type="all")

        # Obtener información de nodos y aristas
        nodes_info = {node: data for node, data in graph.nodes(data=True)}
        edges_info = {str((u, v)): data['length'] for u, v, data in graph.edges(data=True)}

        # Guardar información en un archivo JSON
        data = {'nodos': nodes_info, 'aristas': edges_info}
        
        print("A escribir data!!")  
        
        # Obtener el directorio del script en tiempo de ejecución
        directorio_Logics = os.path.dirname(os.path.realpath(__file__))
        # Cambiar el directorio de trabajo al directorio Logics
        os.chdir(directorio_Logics)

        # Guardar información en un archivo JSON
        with open('data.json', 'w') as json_file:
            json.dump(data, json_file, default=str)
    
    else:
        print("Sobran argumentos")

else:
    print("No se proporcionaron argumentos.")
    place_name = "Valencia, Spain"

