using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    public Manager manager;
    

    void Start()
    {
        // Recuperar manager de la escena para agregar el contenido del JSON
        manager = GetComponent<Manager>();

        // Ruta al archivo JSON
        string filePath = Path.Combine(Application.dataPath, "Logics/data.json");

        if (File.Exists(filePath))
        {
            // Leer el contenido del archivo JSON
            string jsonData = File.ReadAllText(filePath);

            // Deserializar JSON a objetos en Unity
            Data data = JsonConvert.DeserializeObject<Data>(jsonData);




            float maxX = -10e10f;
            float minX = 10e10f;
            float maxY = -10e10f;
            float minY = 10e10f;
            // Crear nodos y aristas en Unity
            foreach (var kvp in data.nodos)
            {
                string nodeId = kvp.Key;
                NodeInfo nodeInfo = kvp.Value;
                manager.addNode(nodeId, nodeInfo);

                //minmax node values
                if (nodeInfo.x > maxX) { maxX = nodeInfo.x; }
                if (nodeInfo.x < minX) { minX = nodeInfo.x; }
                if (nodeInfo.y > maxY) { maxY = nodeInfo.y; }
                if (nodeInfo.y < minY) {  minY = nodeInfo.y; }
            }
            manager.cameraCenterKnowingNodes();


            foreach (var kvp in data.aristas)
            {
                string edgeKey = kvp.Key;
                float edgeLength = kvp.Value;

                // Descomponer la clave para obtener los nodos de inicio y fin
                string[] nodeIds = edgeKey.Replace(" ","").Trim('(', ')').Split(',');

                manager.addEdge(nodeIds[0], nodeIds[1], edgeLength);
            }

            // Acceder a la información
            Debug.Log("Número de nodos: " + data.nodos.Count);
            Debug.Log("Número de aristas: " + data.aristas.Count);
        }
        else
        {
            Debug.LogError("El archivo data.json no existe.");
        }
    }
}
[System.Serializable]
public class Data
{
    public Dictionary<string, NodeInfo> nodos;
    public Dictionary<string, float> aristas;
}

[System.Serializable]
public class NodeInfo
{
    public float y;
    public float x;
    public string highway;
    public int street_count;
}
