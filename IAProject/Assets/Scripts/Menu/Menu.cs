using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject deadMenu;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        mainMenu.SetActive(false);
    }
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    public void YouDied()
    {
        deadMenu.SetActive(false);
    }
}
