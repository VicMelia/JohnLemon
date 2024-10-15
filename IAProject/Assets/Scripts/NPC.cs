using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Node currentNode;
    float speed;
    public Pathfinding pathfinding;

    Vector3[] path;
    [SerializeField] protected Transform target;
    int targetIndex;

    private void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
    }

    protected void GoToTarget()
    {
        pathfinding.FindPath(this.transform.position, target.transform.position);
        OnPathFound(pathfinding.vectorPath,true);
    }

    protected void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            //Debug.Log("pathsuccesful");
            path = newPath;
            //Debug.Log(path[0]);
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

}
