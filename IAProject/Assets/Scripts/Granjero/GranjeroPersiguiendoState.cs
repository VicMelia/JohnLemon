using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroPersiguiendoState : GranjeroBaseState
{
    private float margin;
    //private bool lost;
    private PlayerMovement pMv;
    private float timer;
    private GameManager gM; 
   private ConoVision cv;

    public override void EnterState(GranjeroStateManager granjero){
        granjero.tiempoPerdido = 5;
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Aquí tienen que pasar cosas
        Debug.Log("patata");
        granjero.GoTo(granjero.player.transform);
        //distance = Vector3.Distance (this.transform.position, granjero.target.transform.position);
        margin = 3f;
        pMv = granjero.player.GetComponent<PlayerMovement>();
        cv = granjero.vision;
        timer = 0;

    }
    public override void UpdateState(GranjeroStateManager granjero){
        //El granjero persigue al zorro de forma constante. Si deja de tener línea de visión con él, no se reactiva el GoTo.
        //Si al llegar al final del GoTo sigue sin tener visión del zorro, pasa a un estado en el que rota en el sitio en direcciones al azar durante
        //x segundos, y después vuelve al estado de patrullar
        //si en algún momento ve al zorro, retoma su GoTo. Si en algún momento se acerca lo suficiente, el jugador pierde.

        if (granjero.distance > cv.VisionRange*2) granjero.vision.playerDetected = false;
        if (granjero.distance < margin) {
            //Jugador pierde
            granjero.StopCoroutine("FollowPath");
            GameManager.Instance.caught = true;
            GameManager.Instance.statCheck();
        }
        else if (pMv.status == PlayerMovement.Status.Hidden){
            timer += Time.deltaTime;
            Debug.Log ("timer es: "+timer + " y el tiempoP es de: " + granjero.tiempoPerdido);
            granjero.StopCoroutine("FollowPath");
            if (timer > granjero.tiempoPerdido){
                granjero.vision.playerDetected = false;
                granjero.SwitchState(granjero.PatrullandoState);
            }

        }
        else if (!granjero.vision.playerDetected){
            timer += Time.deltaTime;
            if (timer > granjero.tiempoPerdido){
                granjero.SwitchState(granjero.PatrullandoState);
            }
        }

        else if (granjero.distance >= margin  && granjero.vision.playerDetected){
            timer = 0;
            granjero.GoTo(granjero.player.transform);
        }
    }
}
