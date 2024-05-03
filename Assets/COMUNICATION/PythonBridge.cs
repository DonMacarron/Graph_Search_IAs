using System;
using System.Diagnostics;
using UnityEngine;
using System.Threading;
using System.Net.Sockets;
using System.Text;

public class PythonBridge : MonoBehaviour
{
    Manager manager;

    [SerializeField]
    public string destino;

    [SerializeField]
    public DataReader reader;


    public string host = "127.0.0.1";
    public int port = 8888;
    public TcpClient client;

    void Awake()
    {
        string rutaScriptPython = "";
        string argsPython = "";

        destino = PlayerPrefs.GetString("City");
        UnityEngine.Debug.Log("Destino:  "+destino);
        if (destino != null && destino != "" && destino != "Valencia, Spain")
        {
            rutaScriptPython = "Assets\\Logics\\Datasender.py ";
            argsPython = destino.Replace(" ", "");
            UnityEngine.Debug.Log("Destino:  " + destino);

            reader.isVlc = false;
            executePythonCityIni(rutaScriptPython, argsPython);
        }
        else
        {
            if (destino == "Valencia, Spain") reader.isVlc = true;
            else reader.isVlc = false;
        }
        rutaScriptPython = "Assets\\Logics\\serverP.py ";
        argsPython = "";
        Thread serverThread = new Thread(() => { pythonServerIni(rutaScriptPython, argsPython); });
        serverThread.Start();
    }

    void executePythonCityIni(string rutaScriptPython, string argumentos)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";  
        startInfo.Arguments = $"{rutaScriptPython} {argumentos}";
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        //startInfo.CreateNoWindow = true;

        using (Process process = new Process())
        {
            process.StartInfo = startInfo;

            try
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                UnityEngine.Debug.Log("Output: " + output);
                UnityEngine.Debug.Log("Error: " + error);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log("Error al ejecutar Python: " + ex.Message);
            }
        }
    }
    void pythonServerIni(string rutaScriptPython, string argumentos)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";
        startInfo.Arguments = $"{rutaScriptPython} {argumentos}";
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        //startInfo.CreateNoWindow = true;

        using (Process process = new Process())
        {
            process.StartInfo = startInfo;

            try
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                UnityEngine.Debug.Log("Output: " + output);
                UnityEngine.Debug.Log("Error: " + error);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log("Error al ejecutar Python: " + ex.Message);
            }
        }
    }
    public void connectToPythonServer() {
        try
        {

            client = new TcpClient(host, port);

            sendMessageToServer("Hello Python!");
            unityPetitionManagerLaunch();
        }
        catch (SocketException e)
        {
            UnityEngine.Debug.LogError("Error de conexión: " + e.Message);
        }
    }
    public void sendMessageToServer(string ms)
    {
        

        try
        {
            // Convert messsage to bytes
            byte[] data = Encoding.UTF8.GetBytes(ms);

            // Send data
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);

            UnityEngine.Debug.Log("Mensaje enviado a Python: " + ms);
        }
        catch (SocketException e)
        {
            UnityEngine.Debug.LogError("Error de conexión: " + e.Message);
        }
    }
    public void unityPetitionManagerLaunch()
    {
        try
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string msgRcv = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                UnityEngine.Debug.Log("Mensaje recibido de Python: " + msgRcv);

                if (msgRcv == "EXIT")
                {
                    break; // Salir del bucle si recibimos "EXIT"
                }
                switch (msgRcv)
                {
                    case "OBJECTIVE_NODES":
                        manager.sendObjectiveNodes();
                        break;

                    case string s1 when s1.StartsWith("NODEANDADJACENTS"):
                        // s1 = Node id
                        s1 = s1.Split("_")[1];
                        manager.sendNodeAndAdjacents(manager.GraphNodes[s1]);
                        break;

                    default: 
                        break;

                }
            }
        }
        catch (SocketException e)
        {
            UnityEngine.Debug.LogError("Error de conexión: " + e.Message);
        }
    }
}
