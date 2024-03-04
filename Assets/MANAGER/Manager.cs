using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{
    public Dictionary<string, Node> GraphNodes;

    public GameObject nodePrefab;
    public GameObject edgePrefab;

    [SerializeField]
    public float scaleFactor;

    [SerializeField]
    public float boundX;

    [SerializeField]
    public float boundY;

    [SerializeField]
    public float cameraScaler;

    public new Camera camera;

    public float minDistanceForEdge = 0.001f;

    private float maxNodeX;
    private float maxNodeY;
    private float minNodeX;
    private float minNodeY;


    private void Start()
    {
        GraphNodes = new Dictionary<string, Node>();

        maxNodeX = -10e10f;
        maxNodeY = -10e10f;
        minNodeX = 10e10f;
        minNodeY = 10e10f;

    }
    public void addNode(string id, NodeInfo inf)
    {
        GameObject nodeObject = Instantiate(nodePrefab);
        Node newNode = nodeObject.AddComponent<Node>();
        nodeObject.name = id;

        // Set properties or perform initialization as needed
        newNode.InitializeNode(inf.x, inf.y);
        Color ccc = Random.ColorHSV();
        SpriteRenderer spriteRenderer = nodeObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = ccc;

        // Add the node to the dictionary
        GraphNodes.Add(id, newNode);

        setNodeTransform(nodeObject);

        // is it an extreme of the map?
        if (inf.x > maxNodeX) maxNodeX = inf.x;
        if (inf.x < minNodeX) minNodeX = inf.x;
        if (inf.y > maxNodeY) maxNodeY = inf.y;
        if (inf.y < minNodeY) minNodeY = inf.y;


    }
    public void addEdge(string originId, string destineId, float distance)
    {

        if (GraphNodes.ContainsKey(originId) && GraphNodes.ContainsKey(destineId))
        {
            Node origin = GraphNodes[originId];
            Node dest = GraphNodes[destineId];

            // Is long enough
            Debug.Log(longEnoughEdge(origin, dest));
            if (longEnoughEdge(origin, dest)) {
                GameObject edgeObject = Instantiate(edgePrefab);
                edgeObject.name = "(" + originId + "," + destineId + ")";

                Edge newEdge = edgeObject.AddComponent<Edge>();
                newEdge.OriginNode = origin;
                newEdge.DestineNode = dest;
                newEdge.Distance = distance;


                origin.addConection(newEdge);
                dest.addConection(newEdge);


                setEdgePosition(edgeObject, newEdge); 
            }
        }
    }
    public void setNodeTransform(GameObject node)
    {
        Node nnn = node.GetComponent<Node>();
        float x = nnn.CoordinateX;
        float y = nnn.CoordinateY;

        float newX = (x + boundX) * scaleFactor;
        float newY = (y + boundY) * scaleFactor;
        node.transform.localPosition = new Vector3(newX, newY, 1f);
    }

    public void setEdgePosition(GameObject edgeObject, Edge edge)
    {
        //edge = edgeObject.GetComponent<Edge>();


        Vector3 orPos = new Vector3((edge.OriginNode.CoordinateX + boundX) * scaleFactor, (edge.OriginNode.CoordinateY + boundY) * scaleFactor, 2f);
        Vector3 desPos = new Vector3((edge.DestineNode.CoordinateX + boundX) * scaleFactor, (edge.DestineNode.CoordinateY + boundY) * scaleFactor, 2f);

        //calcular punto medio
        Vector3 pMid = (orPos + desPos) / 2;

        //calcular angulo
        Vector3 direction = orPos - desPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float length = direction.magnitude;

        edgeObject.transform.localRotation = Quaternion.Euler(0, 0, angle);

        edgeObject.transform.localPosition = pMid;
        edgeObject.transform.localScale = new Vector3(length, 0.2f, 1f);
    }

    public void cameraCenterKnowingNodes() {
        Vector3 camPos = new Vector3((((minNodeX + maxNodeX) / 2)+boundX) * scaleFactor, (((minNodeY + maxNodeY) / 2) + boundY) * scaleFactor, 0f);
        camera.transform.localPosition = camPos;

        float dX = Mathf.Abs(maxNodeX - minNodeX);
        float dY = Mathf.Abs(maxNodeY - minNodeY);

        float maxDiference =  dX>dY  ? dX : dY;
        camera.orthographicSize = maxDiference * scaleFactor * cameraScaler;
    }
    public void setBoundX(float x)
    {
        boundX = x;
    }
    public void setBoundy(float y)
    {
        boundY = y;
    }



    public void eliminateNodesWithNoConections()
    {
        // Create a list to store keys of nodes without connections
        List<string> nodesToRemove = new List<string>();

        // Iterate through the keys of the dictionary
        foreach (string nodeKey in GraphNodes.Keys)
        {
            // Check if the current node has no connections
            if (!GraphNodes[nodeKey].existsConection())
            {
                // Add the key to the list of nodes to be removed
                nodesToRemove.Add(nodeKey);

                // Destroy the corresponding GameObject
                Destroy(GraphNodes[nodeKey].gameObject);
            }
        }

        // Remove nodes without connections from the dictionary
        foreach (string keyToRemove in nodesToRemove)
        {
            GraphNodes.Remove(keyToRemove);
        }
    }

    private bool longEnoughEdge(Node or, Node dest)
    {
        if (Mathf.Abs(or.CoordinateX - dest.CoordinateX) > minDistanceForEdge ||
            Mathf.Abs(or.CoordinateY - dest.CoordinateY) > minDistanceForEdge)
        {
            return true;
        }
        else {  return false; }
    }
}
