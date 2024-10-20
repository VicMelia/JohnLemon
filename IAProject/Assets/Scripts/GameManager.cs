using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    int state = -1;
    int liveChickens = 0;
    bool caught = false;
    bool victory = false;

    void Start() {
        time.timescale = 1;
    }

    void Update() {

        switch (state) {
            case 0: startGame(); break;
            case 1: runGame(); break;
            case 2: endGame(); break;
        }
    }

    public void statCheck() {

        if (caught) {
            state = 2;
        }
        else if (liveChickens == 0) {
            victory = true;
            state = 2;
        }
    }

    void startGame() {
    }

    void runGame() {
        
    }

    void endGame() {
        Time.timescale = 0;
        if (victory) {
            //mostrar cartel de victoria
            
            /*victory = false;
            state = 1;*/
        }
        else {
            //mostrar cartel de derrota
            /*victory = false;
            state = -1;*/
        }
    }
}