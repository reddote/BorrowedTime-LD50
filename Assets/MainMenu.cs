using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        //Time.timeScale = 0;
        //gamePanel.SetActive(false);
        GameStateManager.Instance.StartScene();
        GameTickManager.Instance.StopTick();
    }

    public void StartGame()
    {
        GameStateManager.Instance.GameScene();

        //Time.timeScale = 1;
        //gamePanel.SetActive(true);
        //gameObject.SetActive(false);
        GameTickManager.Instance.StartTick();
    }
}
