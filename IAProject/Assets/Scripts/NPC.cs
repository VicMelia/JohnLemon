using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Node currentNode;
    [SerializeField] protected float speed;
    public Pathfinding pathfinding;
    public float distance;
    public GameObject player;
    public float height;
    Vector3[] path;
    public Transform target;
    int targetIndex;

    private Vector3 targetDirectionforRotation;
    public float rSpeed;
    private float step;
    private Vector3 rotation;


    private void Start()
    {
        
        pathfinding = GetComponent<Pathfinding>();
    }

    public void GoTo(Transform place)
    {
        target = place;
        GoToTarget();
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
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        height = transform.position.y;
        Vector3 currentWaypoint = path[0];
        currentWaypoint.y = height;
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
            distance = Vector3.Distance (this.transform.position, target.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            
            targetDirectionforRotation = target.transform.position - transform.position;
            step = rSpeed * Time.deltaTime;
            rotation = Vector3.RotateTowards(transform.forward, targetDirectionforRotation, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(rotation);
            yield return null;

        }
    }

}
