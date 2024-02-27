import osmnx as ox
import networkx as nx
import matplotlib.pyplot as plt

# Configurar el entorno de la ciudad
place_name = "Valencia, Spain"
graph = ox.graph_from_place(place_name, network_type="all")

# Seleccionar dos nodos de inicio y destino
start_node = list(graph.nodes())[0]
end_node = list(graph.nodes())[-1]

# Realizar el algoritmo de búsqueda (A*)
path = nx.astar_path(graph, start_node, end_node)

# Plotear nodos y aristas
fig, ax = ox.plot_graph(graph, node_size=0, show=False, close=False)

# Resaltar nodos de inicio y destino
ox.pcolot_graph_route(graph, path, route_color='r', route_linewidth=6, node_size=0, ax=ax, close=False)

# Actualizar la visualización en cada iteración del algoritmo
for i in range(len(path) - 1):
    partial_path = path[:i + 2]
    ox.plot_graph_route(graph, partial_path, route_color='b', route_linewidth=2, node_size=0, ax=ax, close=False)
    plt.pause(1)  # P
