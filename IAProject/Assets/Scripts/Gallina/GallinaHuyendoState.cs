using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallinaHuyendoState : GallinaBaseState {

    private Transform destination;
    public override void EnterState(GallinaStateManager gallina) {

        ScreamAndFlee(3, gallina);
    }

    public override void UpdateState(GallinaStateManager gallina) {

        if (gallina.transform == destination) { gallina.SwitchState(gallina.PululandoState); }
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

        Debug.Log("Voy a huir!");

        GameObject fox = GameObject.FindGameObjectWithTag("Player");
        gallina.GoTo(GetSafePlace(gallina, fox.transform));
    }

    private Transform GetSafePlace(GallinaStateManager gallina, Transform foxPlace) {

        Vector3 directionToFox = foxPlace.position - gallina.transform.position;
        Vector3 fleeDirection = -directionToFox.normalized;
        Vector3 safePosition = gallina.transform.position + fleeDirection * 6;

        GameObject safePlace = new GameObject();
        safePlace.transform.position = safePosition;

        Debug.Log("Voy a huir a " + safePlace.transform.position);

        destination = safePlace.transform;
        return destination;
    }
}
