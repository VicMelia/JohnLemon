using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallinaPululandoState : GallinaBaseState {

    public override void EnterState (GallinaStateManager gallina) {}
    public override void UpdateState (GallinaStateManager gallina) {
        if (gallina.vision.playerDetected) {

            gallina.SwitchState(gallina.HuyendoState);
        }
    }
}
