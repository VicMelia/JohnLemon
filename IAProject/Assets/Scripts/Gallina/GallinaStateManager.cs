using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallinaStateManager : NPC {

    public GallinaBaseState currentState;
    public GallinaPululandoState PululandoState = new GallinaPululandoState();
    public GallinaHuyendoState HuyendoState = new GallinaHuyendoState();
    public ConoVision vision;

    void Start() {

        vision = transform.GetChild(0).GetComponent<ConoVision>();
    }

    void Update() {

        currentState.UpdateState(this);
    }

    public void SwitchState(GallinaBaseState state) {

        currentState = state;
        state.EnterState(this);
    }

    /* En el caso de que las gallinas reaccionen a las alertas de otras gallinas

    public void ReceiveCall() {

        SwitchState(HuyendoState);
    } */
}