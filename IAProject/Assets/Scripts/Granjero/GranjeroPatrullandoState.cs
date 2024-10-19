using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroPatrullandoState : GranjeroBaseState
{
    private GameObject[] routePoints;
    private bool searching;
    

    public override void EnterState(GranjeroStateManager granjero){
        granjero.GoTo(GetRandomRoutePoint(granjero));
    }
    public override void UpdateState(GranjeroStateManager granjero){
        
        if (granjero.distance <= 3f)
        {
            //granjero.StopCoroutine("FollowPath");
            granjero.GoTo(GetRandomRoutePoint(granjero));
        }
        

       

    }

    private Transform GetRandomRoutePoint(GranjeroStateManager granjero)
    {
        routePoints = GameObject.FindGameObjectsWithTag(granjero.routeTag);
        return routePoints[Random.Range(0, routePoints.Length)].transform;
    }

}
