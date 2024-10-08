using System.Collections;
using System.Collections.Generic;
using System.Text;
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
   
    private int gridSideSize = 0;
    
    public float sphereRadius = 1;
    public LayerMask block;

    void Start()
    {
        GetMeshVertex();
        GetMeshCorners();
    }

    void GetMeshVertex()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        // Obt�n los v�rtices y tri�ngulos de la malla
        meshVertex = mesh.vertices;
        gridSideSize= (int) Mathf.Sqrt(meshVertex.Length);
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
        

        for (int i = 0; i < gridSideSize; i++) {
            for (int j = 0; j < gridSideSize; j++)
            {
                grid[j, i] = new Node(meshVertex[(i* gridSideSize) +j], Physics.CheckSphere(meshVertex[(i * gridSideSize) + j], sphereRadius, block));
            }
        }

        ShowGridInConsole();

    }

    private void ShowGridInConsole()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < grid.GetLength(1); i++)
        {
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                if(grid[i, j].accesible)
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append("0");
                }
                
                sb.Append(' ');
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }

    [ExecuteInEditMode]
    void OnDrawGizmos()
    {
        if (meshVertex != null) {
            for(int i =0; i<meshVertex.Length; i++) {
                Gizmos.DrawSphere(meshVertex[i], sphereRadius);
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
