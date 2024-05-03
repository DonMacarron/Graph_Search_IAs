import socket
from collections import deque

HOST = '127.0.0.1'  # Server IP
PORT = 8888         # Server Port

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    msgRcv = ""
    while (msgRcv != "EXIT"):
        if (msgRcv == "BFS" || msgRcv == "DFS" || msgRcv == "ASTAR"):
            searchAlgorithm(msgRcv)            

        s.listen()
        conn, addr = s.accept() 
        with conn:
            print('Conectado a:', addr)

            # Recibir datos del cliente
            data = conn.recv(1024)
            msgRcv = data.decode();
            print('Mensaje recibido desde Unity:', msgRcv)

def searchAlgorithm(alg):
    return{
        "BFS":BFS(),
        "DFS":DFS(),
        "ASTAR":ASTAR(),
        }.get(alg,None)
        

def askForNodeAndAdjacents():
    pass

def BFS(graph, initial):
    visited = set()
    queue = deque([initial])

    while queue:
        vertice = queue.popleft()
        if vertice not in visited:
            print(vertice)
            visited.add(vertice)
            queue.extend(graph[vertice] - visited)

def DFS():
    pass

def ASTAR():
    pass
    