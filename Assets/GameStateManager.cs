using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    [SerializeField]
    Transform[] EndSceneList;
    [SerializeField]
    Transform[] InGameList;
    [SerializeField]
    Transform[] StartGameList;
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    void DisableObjects(Transform[] list)
    {
        foreach (var item in list)
        {
            item.gameObject.SetActive(false);
        }
    }
    void EnableObjects(Transform[] list)
    {
        foreach (var item in list)
        {
            item.gameObject.SetActive(true);
        }
    }
    public void EndScene()
    {
        EnableObjects(EndSceneList);
        DisableObjects(InGameList);
        DisableObjects(StartGameList);
    }
    public void GameScene()
    {
        EnableObjects(InGameList);
        //EnableObjects(EndSceneList);
        DisableObjects(EndSceneList);
        DisableObjects(StartGameList);
    }
    public void StartScene()
    {
        EnableObjects(StartGameList);
        //EnableObjects(InGameList);
        DisableObjects(EndSceneList);
        DisableObjects(InGameList);
    }

}
