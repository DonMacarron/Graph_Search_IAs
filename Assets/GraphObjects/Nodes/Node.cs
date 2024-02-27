using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int ID { get; set; }
    public double CoordinateX { get; set; }
    public double CoordinateY { get; set; }

    public LinkedList<Edge> edges = new LinkedList<Edge>();
    public Color color {  get; set; }
}
