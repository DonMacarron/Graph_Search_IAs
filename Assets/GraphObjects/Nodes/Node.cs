using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float CoordinateX { get; set; }
    public float CoordinateY { get; set; }

    public LinkedList<Edge> edges;
    public Node(float coordinateX, float coordinateY)
    {
        CoordinateX = coordinateX;
        CoordinateY = coordinateY;
        edges = new LinkedList<Edge>();
    }
    public void addConection(Edge ed) 
    {
        edges.AddLast(ed);
    }
    public void InitializeNode(float x, float y) {
        CoordinateX = x;
        CoordinateY = y;
        edges = new LinkedList<Edge>();
    }
}
