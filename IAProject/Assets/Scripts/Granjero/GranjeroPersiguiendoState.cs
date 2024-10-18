using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroPersiguiendoState : GranjeroBaseState
{
    private float margin;
    private bool lost;
    private PlayerMovement pMv;

    public override void EnterState(GranjeroStateManager granjero){
        //Aquí tienen que pasar cosas
        Debug.Log("patata");
        granjero.GoTo(granjero.target);
        //distance = Vector3.Distance (this.transform.position, granjero.target.transform.position);
        margin = 2;
        lost = false;
        pMv = granjero.player.GetComponent<PlayerMovement>();

    }
    public override void UpdateState(GranjeroStateManager granjero){
        //El granjero persigue al zorro de forma constante. Si deja de tener línea de visión con él, no se reactiva el GoTo.
        //Si al llegar al final del GoTo sigue sin tener visión del zorro, pasa a un estado en el que rota en el sitio en direcciones al azar durante
        //x segundos, y después vuelve al estado de patrullar
        //si en algún momento ve al zorro, retoma su GoTo. Si en algún momento se acerca lo suficiente, el jugador pierde.

        
        if (granjero.distance < margin) {
            //Jugador pierde
            granjero.StopCoroutine("FollowPath");
        }
        else if (lost == true){

        }

        else if (granjero.distance >= margin  && lost == false){
            granjero.GoTo(granjero.target);
        }
    }
}
