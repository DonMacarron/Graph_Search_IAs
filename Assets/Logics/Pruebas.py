import osmnx as ox
import networkx as nx

# Configurar el entorno de la ciudad
place_name = "Valencia, Spain"
graph = ox.graph_from_place(place_name, network_type="all")

# Iterar sobre los nodos y sus atributos
for node, data in graph.nodes(data=True):
    print(f"Atributos del nodo {node}: {data}")
