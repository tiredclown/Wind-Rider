using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    public GameObject tapPanel;
    public static bool gameStarted=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame()
    {
        tapPanel.SetActive(false);
        gameStarted = true;
    }
    public void Restart()
    {
        gameStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
