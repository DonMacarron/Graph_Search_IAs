# -*- coding: utf-8 -*-
import osmnx as ox
import json
from shapely.geometry import mapping

# Configurar el entorno de la ciudad
place_name = "Valencia, Spain"
graph = ox.graph_from_place(place_name, network_type="all")

# Obtener información de nodos y aristas
nodes_info = {node: data for node, data in graph.nodes(data=True)}
edges_info = {str((u, v)): data['length'] for u, v, data in graph.edges(data=True)}

# Guardar información en un archivo JSON
data = {'nodos': nodes_info, 'aristas': edges_info}

with open('data.json', 'w') as json_file:
    json.dump(data, json_file, default=str)  # Use default=str para manejar tipos no serializables directamente
