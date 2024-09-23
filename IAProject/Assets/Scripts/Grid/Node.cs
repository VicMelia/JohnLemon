using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 position;
    public List<Node> neighborNodes;

    public Node(Vector3 pos)
    {
        position = pos;
        neighborNodes = new List<Node>();
    }

    public void IsCloseNodeAccesible()
    {
    }
}
