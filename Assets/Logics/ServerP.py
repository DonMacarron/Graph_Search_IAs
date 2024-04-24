import socket

HOST = '127.0.0.1'  # Server IP
PORT = 8888         # Server Port

print("HEchoofasfdaf")

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen()
    conn, addr = s.accept() 
    with conn:
        print('Conectado a:', addr)

        # Recibir datos del cliente
        data = conn.recv(1024)
        print('Mensaje recibido desde Unity:', data.decode())
    