using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 position;
    public bool accesible;

    public Node(Vector3 pos, bool access)
    {
        position = pos;
        accesible = access;
    }

}
