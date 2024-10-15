using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroPatrullandoState : GranjeroBaseState
{
    private GameObject[] routePoints;
    private bool searching;

    public override void EnterState(GranjeroStateManager granjero){
        routePoints = GameObject.FindGameObjectsWithTag("Route");
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
        for (int i = 0; i < Random.Range(0, routePoints.Length); i++)
        {
            return routePoints[i].transform;
        }
        return routePoints[0].transform;
    }

}
