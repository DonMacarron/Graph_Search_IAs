using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    public Node OriginNode { get; set; }
    public Node DestineNode { get; set; }
    public float Distance {  get; set; }
    public Edge() { }
    public Edge(Node destine, Node origin, float distance)
    {
        OriginNode = origin;
        DestineNode = destine;
        Distance = distance;
    }
    public void OnServerInitialized(Node destine, Node origin, float distance)
    {
        OriginNode = origin;
        DestineNode = destine;
        Distance = distance;
    }
}
