using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroPatrullandoState : GranjeroBaseState
{
    private GameObject[] routePoints;
    private bool searching;
    

    public override void EnterState(GranjeroStateManager granjero){
        routePoints = GameObject.FindGameObjectsWithTag(granjero.routeTag);
        granjero.GoTo(GetRandomRoutePoint(granjero));
    }
    public override void UpdateState(GranjeroStateManager granjero){
        
        if (granjero.distance <= 5f)
        {
            //granjero.StopCoroutine("FollowPath");
            granjero.GoTo(GetRandomRoutePoint(granjero));
        }

        if (granjero.vision.playerDetected)
        {
            Debug.Log("GWEIAYFGAWIFUWGEAIYWWAYGI");
            CallFarmers(999,granjero);
            granjero.SwitchState(granjero.PersiguiendoState);
        }

       

    }

    private void CallFarmers(float radius,GranjeroStateManager granjero)
    {
        Collider[] hitColliders = Physics.OverlapSphere(granjero.transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag=="Granjero")
            {
                hitCollider.GetComponent<GranjeroStateManager>().RecieveCall();
            }
        }
    }
    

    private Transform GetRandomRoutePoint(GranjeroStateManager granjero)
    {
        
        return routePoints[Random.Range(0, routePoints.Length)].transform;
    }

}
