using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallinaHuyendoState : GallinaBaseState {

    private bool fleeing = false;
    private float countDown = 3f;
    private ConoVision cv;
    private List<GameObject> availableEscapePoints = new List<GameObject>();
    private float distanceFoxChicken;
    GameObject fox;
    Transform foxPlace;

    public override void EnterState(GallinaStateManager gallina) {
        Debug.Log("entrando");
        countDown = 3f;
        ScreamAndFlee(3, gallina);
        cv = gallina.vision;
    }

    public override void UpdateState(GallinaStateManager gallina) {
        //Debug.Log (countDown);
        countDown -= Time.deltaTime;
        float distanceFoxChicken = Vector3.Distance (fox.transform.position, gallina.transform.position);        
        if (distanceFoxChicken > cv.VisionRange*2) gallina.vision.playerDetected = false;

        if (countDown <= 0) { fleeing = false; }

        if (fleeing) {
            availableEscapePoints.Clear();
            foreach(GameObject eP in gallina.escapePoints) {
                if (!Physics.CheckBox(eP.transform.position, eP.transform.localScale / 2, eP.transform.rotation, LayerMask.GetMask("Block"))) {
                    availableEscapePoints.Add(eP);
                }
            }
            gallina.GoTo(GetSafePlace(gallina, availableEscapePoints));
        }
        else { gallina.SwitchState(gallina.PululandoState); Debug.Log("Ya estoy a salvo"); }
    }

    private void ScreamAndFlee(float radius, GallinaStateManager gallina) {

        Collider[] hitColliders = Physics.OverlapSphere(gallina.transform.position, radius);
        foreach (var hitCollider in hitColliders) {

            if (hitCollider.gameObject.tag=="Granjero") {

                hitCollider.GetComponent<GranjeroStateManager>().RecieveCall();
            }

            /* Para el caso en que gallina avise a otras gallinas

            else if (hitCollider.gameObject.tag=="Gallina") {

                hitCollider.GetComponent<GallinaStateManager>().ReceiveCall(gallina.Transform);
            }*/
        }

        fox = GameObject.FindGameObjectWithTag("Player");
        foxPlace = fox.transform;

        fleeing = true;
        Debug.Log("Voy a huir!");
    }

    private Transform GetSafePlace(GallinaStateManager gallina, List<GameObject> availableEscapePoints) {

        Transform farthestEscapePoint = null;
        float maxDistance = 0f;

        foreach (GameObject escapePoint in availableEscapePoints) {

            float distance = Vector3.Distance(foxPlace.position, escapePoint.transform.position);
            if (distance > maxDistance) {

                maxDistance = distance;
                farthestEscapePoint = escapePoint.transform;
            }
        }

        return farthestEscapePoint;
    }
}
