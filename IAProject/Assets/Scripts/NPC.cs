using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Node currentNode;
    public Pathfinding pathfinding;

    private void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
    }



}
