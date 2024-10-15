using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroPersiguiendoState : GranjeroBaseState
{
    private float margin;

    public override void EnterState(GranjeroStateManager granjero){
        //Aquí tienen que pasar cosas
        Debug.Log("patata");
        granjero.GoTo(granjero.target);
        //distance = Vector3.Distance (this.transform.position, granjero.target.transform.position);
        margin = 2;

    }
    public override void UpdateState(GranjeroStateManager granjero){
        //Aquí tienen que pasar cosas
        if (granjero.distance >= margin){
            granjero.GoTo(granjero.target);
        }
        else {
            granjero.StopCoroutine("FollowPath");
        }
    }
}
