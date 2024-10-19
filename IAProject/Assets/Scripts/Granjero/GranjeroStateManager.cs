using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroStateManager : NPC
{
    public GranjeroBaseState currentState;
    public GranjeroPatrullandoState PatrullandoState = new GranjeroPatrullandoState();
    public GranjeroPersiguiendoState PersiguiendoState = new GranjeroPersiguiendoState();
    public string routeTag = "";
    public ConoVision vision;
    public int tiempoPerdido;


    // Start is called before the first frame update
    void Start()
    {
        currentState = PatrullandoState;
        currentState.EnterState(this);
        vision= transform.GetChild(0).GetComponent<ConoVision>();
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

    public void RecieveCall()
    {
        SwitchState(PersiguiendoState);
    }

}
