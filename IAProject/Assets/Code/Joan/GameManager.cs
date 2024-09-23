using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int state = -1
    int selLevel = -1
    GameObject[] levels;
    int liveChickens = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
        switch (state) {
            case 0: StartGame(); break;
            case 1: RunGame(); break;
            case 2: EndGame(); break;
        }
    }
}
