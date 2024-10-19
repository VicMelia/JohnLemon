using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroStateManager : NPC
{
    public GranjeroBaseState currentState;
    public GranjeroPatrullandoState PatrullandoState = new GranjeroPatrullandoState();
    public GranjeroPersiguiendoState PersiguiendoState = new GranjeroPersiguiendoState();
    public string routeTag = "";

    // Start is called before the first frame update
    void Start()
    {
        currentState = PatrullandoState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GranjeroBaseState state){
        currentState = state;
        state.EnterState(this);
    }
}
