using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GallinaBaseState {

    public abstract void EnterState(GallinaStateManager gallina);
    public abstract void UpdateState(GallinaStateManager gallina);
}
