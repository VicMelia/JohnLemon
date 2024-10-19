using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

	//coste g: distancia de ese nodo al nodo de inicio
	//coste h: distancia de ese nodo al nodo final
	//coste f: coste g + coste h
	//lista open: lista de nodos cuyo coste f ya se ha calculado (los descubiertos)
	//lista closed: lista de nodos ya explorados


	//empezamos comprobando el coste f de todos los vecinos del nodo inicial y nos quedamos con el más bajo (en caso de empate, desempatamos con el coste h más bajo)
	//entonces, el nodo elegido se añade a la lista de nodos cerrados y ya se repite el proceso hasta llegar al final.
	//importante, no se comprueban solo los 8 nodos de alrededor del nodo actual, sino que al pasar a un nodo nuevo, sus 8 vecinos se "descubren", y se comprueba el 
	//nodo más barato de todos los descubiertos hasta el momento, no solo de los vecinos del actual
	//en el caso de que los costes f y h sean iguales, da igual cuál elegir
	//también hay que tener en cuenta que los costes no son siempre iguales, si se descubre una ruta más optima a un nodo que suponga una reducción de su coste g o h,
	//éste se verá reducido



	//COSAS POR HACER:
	//poder obtener los nodos inicial y final
	//poder calcular el coste f de un nodo (para eso necesitamos saber la distancia entre dos nodos)
	//poder saber los vecinos de un nodo
	public Transform target;
	public AIGrid grid;

	public Vector3[] vectorPath;


	public void FindPath(Vector3 startPos, Vector3 targetPos) {
		//Hace falta una forma de obtener los nodos inicial y final sabiendo únicamente los vec3 de esas posiciones.
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		List<Node> openSet = new List<Node>();
		//HashSet es una colección de elementos únicos sin ordenar
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);


		while (openSet.Count > 0) {
			//comprueba que nodo de los descubiertos es el que tiene el coste más bajo y lo mueve a closed
			Node node = openSet[0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			//comprueba si el nodo actual es el objetivo al que queremos llegar
			if (node == targetNode) {
				RetracePath(startNode,targetNode);
				return;
			}
			//Ahora se procede a descubrir los vecinos
			foreach (Node neighbour in grid.GetNeighbours(node)) {
				//si el vecino no es cruzable o si ya está en la lista de closed lo ignora
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}
				//si resulta que esta ruta al vecino es más corta o si el vecino no se había descubierto antes
				//se actualiza su coste f, se pone al nodo actual como el padre de ese vecino
				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;
					
					//En caso de que lo acabemos de descubrir, lo añadimos a la lista open
					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		//Debug.Log("AAAAAAAAAAAAAA: "+ path[path.Count-1].worldPosition);
		GetVectorPath(path);
		return;

	}

	int GetDistance(Node nodeA, Node nodeB) {
		//Hay que hacer una función que devuelva la distancia entre 2 nodos

		
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
		
	}

	void GetVectorPath(List<Node> nodeList){
		vectorPath = new Vector3[nodeList.Count-1];
		for(int i = 0; i<nodeList.Count-1;i++){
			vectorPath[i] = nodeList[i].worldPosition;
		}

		return;
	 }

	//El del video pone esta función dentro del script del grid
	/*public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
	*/

	
}