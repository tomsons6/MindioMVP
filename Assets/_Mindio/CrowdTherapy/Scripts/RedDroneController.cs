using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDroneController : MonoBehaviour
{
    [SerializeField]
    GameObject DronePrefab;
    [SerializeField]
    float SpawnCount;
    public float CurrentSpawnCount = 0;
    public float SpawnCounterStep;
    [SerializeField]
    GameObject[] SpawnPoints;

    public GameObject[] ListOfRobots;

    public bool hasSpawned = false;

    int PreviousSpawnPoint;

    public GameObject UI;
    public GameObject UIStartPanel;
    public GameObject UIEnPanel;

    [SerializeField]
    bool DifferentstartLevel;

    //NPcPublic variables
    public bool TurnToPlayer;
    public bool WalkToPlayer;
    //should never be less than 3
    public float WaitTimeToGoToPlayer = 10f;
    //should always be bigger than 1
    public float NpcToPlayerDistance = 4f;

    [SerializeField]
    public List<RedDroneTexts> RedDroneTexts;

    private static RedDroneController _instance;
    public static RedDroneController Instance { get { return _instance; } }


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
        if (GameManager.Instance.Levels != GameManager.Level.Level1)
        {
            DifferentstartLevel = true;
        }
        else
        {
            DifferentstartLevel = false;
        }
    }
    void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("DroneSpawnPoint");
        SpawnCounterStep = 1;
        TurnToPlayer = false;
        SpawnDrone();
    }
    public void SpawnDrone()
    {
        if (SpawnCount >= CurrentSpawnCount && !hasSpawned)
        {
            CurrentSpawnCount += SpawnCounterStep;
            hasSpawned = true;

            int SpawnPoint = Random.Range(0, SpawnPoints.Length);
            if (PreviousSpawnPoint == SpawnPoint)
            {
                SpawnPoint = Random.Range(0, SpawnPoints.Length);
                PreviousSpawnPoint = SpawnPoint;
            }
            else
            {
                PreviousSpawnPoint = SpawnPoint;
            }
            Instantiate(DronePrefab, SpawnPoints[SpawnPoint].transform.position, SpawnPoints[SpawnPoint].transform.rotation);
            ChangeLevel();
        }

    }
    public void ChangeLevel()
    {
        //change lvl
        if (GameManager.Instance.Levels != GameManager.Level.Level1 && DifferentstartLevel)
        {
            DifferentstartLevel = false;
        }
        else
        {
            switch (CurrentSpawnCount)
            {
                case 1:
                    GameManager.Instance.Levels = GameManager.Level.Level1;
                    break;
                case 2:
                    GameManager.Instance.Levels = GameManager.Level.Level2;
                    break;
                case 3:
                    GameManager.Instance.Levels = GameManager.Level.Level3;
                    break;
                case 4:
                    GameManager.Instance.Levels = GameManager.Level.Level4;
                    break;
                case 5:
                    GameManager.Instance.Levels = GameManager.Level.Level5;
                    break;
                case 6:
                    GameManager.Instance.Levels = GameManager.Level.Level6;
                    break;
                case 7:
                    GameManager.Instance.Levels = GameManager.Level.Level7;
                    break;
                case 8:
                    GameManager.Instance.Levels = GameManager.Level.Level8;
                    break;
                case 9:
                    GameManager.Instance.Levels = GameManager.Level.Level9;
                    break;
                case 10:
                    GameManager.Instance.Levels = GameManager.Level.Level10;
                    break;
            }
        }
        //what happens when the level is changed
        switch (GameManager.Instance.Levels)
        {
            case GameManager.Level.Level1:
                Debug.Log("Level1");
                if (CurrentSpawnCount != 1) CurrentSpawnCount = 1;
                if (NpcController.Instance.SpawnedNpcCount <= 1)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        NpcController.Instance.SpawnNpc(0f, 1f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                }
                break;
            case GameManager.Level.Level2:
                Debug.Log("Level2");
                if (CurrentSpawnCount != 2) CurrentSpawnCount = 2;
                if (NpcController.Instance.SpawnedNpcCount <= 2)
                {
                    int CurrentNpcCount = NpcController.Instance.SpawnedGirls.Count;
                    for (int i = 0; i < 2 - CurrentNpcCount; i++)
                    {
                        NpcController.Instance.SpawnNpc(.1f, .9f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                    foreach (SpawnedGirl Girl in NpcController.Instance.SpawnedGirls)
                    {
                        Girl.LookAtScript.HeadWeight = .1f;
                        Girl.LookAtScript.ClampWeight = .9f;
                    }
                }
                break;
            case GameManager.Level.Level3:
                Debug.Log("Level3");
                if (CurrentSpawnCount != 3) CurrentSpawnCount = 3;
                TurnToPlayer = true;
                if (NpcController.Instance.SpawnedNpcCount <= 3)
                {
                    int CurrentNpcCount = NpcController.Instance.SpawnedGirls.Count;
                    for (int i = 0; i < 3 - CurrentNpcCount; i++)
                    {
                        NpcController.Instance.SpawnNpc(.2f, .9f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                    foreach (SpawnedGirl Girl in NpcController.Instance.SpawnedGirls)
                    {
                        Girl.LookAtScript.HeadWeight = .1f;
                        Girl.LookAtScript.ClampWeight = .9f;
                    }
                }
                break;
            case GameManager.Level.Level4:
                Debug.Log("Level4");
                if (CurrentSpawnCount != 4) CurrentSpawnCount = 4;
                WalkToPlayer = true;
                TurnToPlayer = true;
                if (NpcController.Instance.SpawnedNpcCount <= 4)
                {
                    int CurrentNpcCount = NpcController.Instance.SpawnedGirls.Count;
                    for (int i = 0; i < 4 - CurrentNpcCount; i++)
                    {
                        NpcController.Instance.SpawnNpc(.2f, .9f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                    foreach (SpawnedGirl Girl in NpcController.Instance.SpawnedGirls)
                    {
                        Girl.LookAtScript.HeadWeight = .2f;
                        Girl.LookAtScript.ClampWeight = .9f;
                    }
                }
                break;
            case GameManager.Level.Level5:
                Debug.Log("Level5");
                if (CurrentSpawnCount != 5) CurrentSpawnCount = 5;
                WalkToPlayer = true;
                TurnToPlayer = true;
                if (NpcController.Instance.SpawnedNpcCount <= 5)
                {
                    int CurrentNpcCount = NpcController.Instance.SpawnedGirls.Count;
                    for (int i = 0; i < 5 - CurrentNpcCount; i++)
                    {
                        NpcController.Instance.SpawnNpc(.3f, .8f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                    foreach (SpawnedGirl Girl in NpcController.Instance.SpawnedGirls)
                    {
                        Girl.LookAtScript.HeadWeight = .3f;
                        Girl.LookAtScript.ClampWeight = .8f;
                    }
                }
                break;
            case GameManager.Level.Level6:
                Debug.Log("Level6");
                if (CurrentSpawnCount != 6) CurrentSpawnCount = 6;
                WalkToPlayer = true;
                TurnToPlayer = true;
                WaitTimeToGoToPlayer = 8f;
                NpcToPlayerDistance = 3.5f;
                if (NpcController.Instance.SpawnedNpcCount <= 6)
                {
                    int CurrentNpcCount = NpcController.Instance.SpawnedGirls.Count;
                    for (int i = 0; i < 6 - CurrentNpcCount; i++)
                    {
                        NpcController.Instance.SpawnNpc(.3f, .8f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                    foreach (SpawnedGirl Girl in NpcController.Instance.SpawnedGirls)
                    {
                        Girl.LookAtScript.HeadWeight = .3f;
                        Girl.LookAtScript.ClampWeight = .8f;
                    }
                }
                break;
            case GameManager.Level.Level7:
                Debug.Log("Level7");
                if (CurrentSpawnCount != 7) CurrentSpawnCount = 7;
                WalkToPlayer = true;
                TurnToPlayer = true;
                WaitTimeToGoToPlayer = 7f;
                if (NpcController.Instance.SpawnedNpcCount <= 7)
                {
                    int CurrentNpcCount = NpcController.Instance.SpawnedGirls.Count;
                    for (int i = 0; i < 7 - CurrentNpcCount; i++)
                    {
                        NpcController.Instance.SpawnNpc(.5f, .6f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                    foreach (SpawnedGirl Girl in NpcController.Instance.SpawnedGirls)
                    {
                        Girl.LookAtScript.HeadWeight = .5f;
                        Girl.LookAtScript.ClampWeight = .6f;
                    }
                }
                break;
            case GameManager.Level.Level8:
                Debug.Log("Level8");
                if (CurrentSpawnCount != 8) CurrentSpawnCount = 8;
                WalkToPlayer = true;
                TurnToPlayer = true;
                WaitTimeToGoToPlayer = 7f;
                NpcToPlayerDistance = 3f;
                if (NpcController.Instance.SpawnedNpcCount <= 7)
                {
                    int CurrentNpcCount = NpcController.Instance.SpawnedGirls.Count;
                    for (int i = 0; i < 7 - CurrentNpcCount; i++)
                    {
                        NpcController.Instance.SpawnNpc(.8f, .6f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                    foreach (SpawnedGirl Girl in NpcController.Instance.SpawnedGirls)
                    {
                        Girl.LookAtScript.HeadWeight = .8f;
                        Girl.LookAtScript.ClampWeight = .6f;
                    }
                }
                break;
            case GameManager.Level.Level9:
                Debug.Log("Level9");
                if (CurrentSpawnCount != 9) CurrentSpawnCount = 9;
                WalkToPlayer = true;
                TurnToPlayer = true;
                NpcToPlayerDistance = 2.5f;
                if (NpcController.Instance.SpawnedNpcCount <= 8)
                {
                    int CurrentNpcCount = NpcController.Instance.SpawnedGirls.Count;
                    for (int i = 0; i < 8 - CurrentNpcCount; i++)
                    {
                        NpcController.Instance.SpawnNpc(.8f, .6f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                    foreach (SpawnedGirl Girl in NpcController.Instance.SpawnedGirls)
                    {
                        Girl.LookAtScript.HeadWeight = .8f;
                        Girl.LookAtScript.ClampWeight = .6f;
                    }
                }
                break;
            case GameManager.Level.Level10:
                Debug.Log("Level10");
                WaitTimeToGoToPlayer = 6f;
                if (CurrentSpawnCount != 10) CurrentSpawnCount = 10;
                WalkToPlayer = true;
                TurnToPlayer = true;
                if (NpcController.Instance.SpawnedNpcCount <= 8)
                {
                    int CurrentNpcCount = NpcController.Instance.SpawnedGirls.Count;
                    for (int i = 0; i < 8 - CurrentNpcCount; i++)
                    {
                        NpcController.Instance.SpawnNpc(1f, .4f);
                        NpcController.Instance.SpawnedNpcCount++;
                    }
                    foreach (SpawnedGirl Girl in NpcController.Instance.SpawnedGirls)
                    {
                        Girl.LookAtScript.HeadWeight = 1f;
                        Girl.LookAtScript.ClampWeight = .4f;
                    }
                }
                StartCoroutine(WaitBeforeENDPanel());
                break;
        }
    }
    public IEnumerator WaitBeforeENDPanel()
    {
        yield return new WaitForSecondsRealtime(40f);
        UI.SetActive(true);
        UIStartPanel.SetActive(false);
        UIEnPanel.SetActive(true);
    }
}
[System.Serializable]
public class RedDroneTexts
{
    public string HelpingText;
    public bool hasShowned;

    public RedDroneTexts(string text, bool displayed)
    {
        this.HelpingText = text;
        this.hasShowned = displayed;
    }
}
