using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class DataReveiver : MonoBehaviour
{
    void Start()
    {
        // Ruta al archivo JSON
        string filePath = Path.Combine(Application.dataPath, "Logics/data.json");

        if (File.Exists(filePath))
        {
            // Leer el contenido del archivo JSON
            string jsonData = File.ReadAllText(filePath);

            // Deserializar JSON a objetos en Unity
            MapData mapData = JsonUtility.FromJson<MapData>(jsonData);

            // Acceder a la información
            Debug.Log("Número de nodos: " + mapData.nodos.Count);
            Debug.Log("Número de aristas: " + mapData.aristas.Count);
        }
        else
        {
            Debug.LogError("El archivo data.json no existe.");
        }
    }

    // Clases para deserializar el JSON
    [System.Serializable]
    public class MapData
    {
        public List<NodeInfo> nodos;
        public List<EdgeInfo> aristas;
    }

    [System.Serializable]
    public class NodeInfo
    {
        public int id;
        public float x;
        public float y;
        
    }

    [System.Serializable]
    public class EdgeInfo
    {
        public int nodoInicio;
        public int nodoFin;
        public double distance;
        public string name; 
                            
    }

}
