using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public enum Level {Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10 };
    public Level Levels;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
