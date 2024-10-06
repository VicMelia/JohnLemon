using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class AIGrid : MonoBehaviour
{
    public Node[,] grid;
    //public int gridResolution = 1;
    Mesh mesh;
    public Vector3[] meshVertex;
    public Vector3[] meshCorners;
    public List<Vector3> gridNodes = new List<Vector3>();

    private float gridSideSize = 0;


    void Start()
    {
        GetMeshVertex();
        GetMeshCorners();
    }

    void GetMeshVertex()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        // Obtén los vértices y triángulos de la malla
        meshVertex = mesh.vertices;
        gridSideSize= Mathf.Sqrt(meshVertex.Length);
        transform.TransformPoints(meshVertex,meshVertex); //Obtener puntos de la malla ya escalados
        CreateNavegationGrid();
    }

    void GetMeshCorners()
    {

    }

    private void CreateNavegationGrid()
    {
        grid = new Node[gridSideSize, gridSideSize];
        //float vertexDistance = Vector3.Distance(meshVertex[0], meshVertex[1]) / gridResolution;
        grid[0][0] = new Node(meshVertex[0]);

        for (int i = 1; i < meshVertex.Length; i++) {
            if ()
            {

            }
        }

    }

    [ExecuteInEditMode]
    void OnDrawGizmos()
    {
        if (meshVertex != null) {
            for(int i =0; i<meshVertex.Length; i++) {
                Gizmos.DrawSphere(meshVertex[i], 0.4f);
                if (i == meshVertex.Length - 1) { break; }
                Gizmos.DrawLine(meshVertex[i], meshVertex[i+1]);
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
