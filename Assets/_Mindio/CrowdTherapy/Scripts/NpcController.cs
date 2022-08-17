using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [SerializeField]
    GameObject NpcPrefab;
    [SerializeField]
    GameObject[] NpcSpawnPoints;
    public int SpawnedNpcCount;
    [SerializeField]
    public int MaxNpcCount;
    [SerializeField]
    public List<SpawnedGirl> SpawnedGirls;
    [SerializeField]
    public List<WayPoints> NpcWayPoints;

    private static NpcController _instance;
    public static NpcController Instance { get { return _instance; } }
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
    void Start()
    {
        NpcSpawnPoints = GameObject.FindGameObjectsWithTag("NpcSpawnPoint");

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("GirlDrone").Length; i++)
        {
            SpawnedGirls.Add(new SpawnedGirl(GameObject.FindGameObjectsWithTag("GirlDrone")[i].gameObject, GameObject.FindGameObjectsWithTag("GirlDrone")[i].gameObject.GetComponent<NpcPatroling>(),GameObject.FindGameObjectsWithTag("GirlDrone")[i].gameObject.GetComponent<LookAtPlayer>()));
        }
        for(int i = 0; i < GameObject.FindGameObjectsWithTag("NpcNavPoint").Length; i++)
        {
            NpcWayPoints.Add(new WayPoints(GameObject.FindGameObjectsWithTag("NpcNavPoint")[i].transform, false));
        }

    }
    public void SpawnNpc(float HeadWeight, float ClampWeight)
    {
        if (SpawnedNpcCount <= MaxNpcCount)
        {
            int SpawnPoint = Random.Range(0, NpcSpawnPoints.Length);
            GameObject TempObj = Instantiate(NpcPrefab, NpcSpawnPoints[SpawnPoint].transform.position, NpcSpawnPoints[SpawnPoint].transform.rotation);
            TempObj.GetComponent<LookAtPlayer>().HeadWeight = HeadWeight;
            TempObj.GetComponent<LookAtPlayer>().ClampWeight = ClampWeight;
            SpawnedGirls.Add(new SpawnedGirl(TempObj.gameObject,TempObj.gameObject.GetComponent<NpcPatroling>(),TempObj.gameObject.GetComponent<LookAtPlayer>()));
        }
        
    }

}
[System.Serializable]
public class SpawnedGirl
{
    public GameObject SpanwedGirl;
    public NpcPatroling PatrolScript;
    public LookAtPlayer LookAtScript;


    public SpawnedGirl(GameObject gameObject, NpcPatroling Patrolsc,LookAtPlayer LookSc)
    {
        this.SpanwedGirl = gameObject;
        this.PatrolScript = Patrolsc;
        this.LookAtScript = LookSc;
    }
}
[System.Serializable]
public class WayPoints
{
    public Transform WayPoint;
    public bool Taken;

    public WayPoints(Transform transform, bool taken)
    {
        this.WayPoint = transform;
        this.Taken = taken;
    }
}