using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granjero : NPC
{
    private float distance;
    [SerializeField] private float margin;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("patata");
        GoToTarget();
        distance = Vector3.Distance (this.transform.position, target.transform.position);
        margin = 2;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance (this.transform.position, target.transform.position);
        if (distance >= margin){
            GoToTarget();
        }
        else {
            StopCoroutine("FollowPath");
        }
        
    }
}
