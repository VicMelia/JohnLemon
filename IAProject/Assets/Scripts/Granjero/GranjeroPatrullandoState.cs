using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroPatrullandoState : GranjeroBaseState
{
    private GameObject[] routePoints;
    private bool searching;

    public override void EnterState(GranjeroStateManager granjero){
        
    }
    public override void UpdateState(GranjeroStateManager granjero){
        if (!searching)
        {
            searching = true;
            granjero.GoTo(GetRandomRoutePoint());
        }

        if (granjero.stop)
        {
            searching=false;
        }

    }

    private Transform GetRandomRoutePoint()
    {
        routePoints= routePoints = GameObject.FindGameObjectsWithTag("Route");
        return routePoints[Random.Range(0, routePoints.Length)].transform;
    }

}
