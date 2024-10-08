using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GranjeroBaseState
{
   public abstract void EnterState(GranjeroStateManager granjero);
   public abstract void UpdateState(GranjeroStateManager granjero);
}
