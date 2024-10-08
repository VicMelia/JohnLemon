using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    int state = -1;
    int selLevel = -1;
    GameObject[] levels;
    int liveChickens = 0;
    bool caught = false;
    bool victory = false;

    void Start() {}

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
        selLevel = 0;
        levels[selLevel].SetActive(true);
    }

    void runGame() {}

    void endGame() {
        levels[selLevel].SetActive(false);

        if (selLevel < levels.Length - 1 && victory) {
            selLevel++;
            victory = false;
            levels[selLevel].SetActive(true);
            state = 1;
        }
        else {
            victory = false;
            state = -1;
        }
    }
}