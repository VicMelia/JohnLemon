using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class AIGrid : MonoBehaviour
{
    public int gridResolution = 1;
    Mesh mesh;
    public Vector3[] meshVertex;
    public List<Vector3> gridNodes = new List<Vector3>();


    void Start()
    {

        GetMeshVertex();
    }

    void GetMeshVertex()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        // Obtén los vértices y triángulos de la malla
        meshVertex = mesh.vertices;
        transform.TransformPoints(meshVertex,meshVertex);
        CreateNavegationGrid();
    }

    private void CreateNavegationGrid()
    {
        float vertexDistance = Vector3.Distance(meshVertex[0], meshVertex[1]) / gridResolution;
        Node firstNode = new Node(meshVertex[0]);
    }


    void OnDrawGizmos()
    {
        if (meshVertex != null) {
            foreach (Vector3 v in meshVertex) {
                Gizmos.DrawSphere(v, 0.1f);
            }
        }

        /*
        if (nodes != null)
        {
            foreach (TriangleNode node in nodes)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(node.position, 0.1f);

                foreach (TriangleNode neighbor in node.neighbors)
                {
                    Gizmos.DrawLine(node.position, neighbor.position)
                }
            }
        }
        */
    }
    


}
