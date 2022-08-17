using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BNG;
using System.Linq;


public class NpcPatroling : MonoBehaviour
{
    public GameObject[] points;
    [SerializeField]
    public int destPoint = 0;
    private NavMeshAgent agent;

    public Animator _Animator;
    public GameObject Player;

    public WayPoints CurrentWayPoint;

    RaycastHit hit;
    [SerializeField]
    float DefaultStoppingDistance = .5f;
    [SerializeField]
    bool WalkingToPlayer;
    void Start()
    {
        NavMesh.avoidancePredictionTime = 10f;
        _Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;
        points = GameObject.FindGameObjectsWithTag("NpcNavPoint");
        Player = GameObject.FindGameObjectWithTag("Player");
        WalkingToPlayer = false;
        //GotoNextPoint();
    }
    void Update()
    {
        //if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        //{
        //    Debug.Log("Path Complete");
        //}
        if (RedDroneController.Instance.WalkToPlayer && !WalkingToPlayer)
        {
            GoToPlayer(Player.transform.position,RedDroneController.Instance.WaitTimeToGoToPlayer, RedDroneController.Instance.NpcToPlayerDistance);
        }
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

    }
    //Rumble
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("player");
            InputBridge.Instance.VibrateController(.6f, .1f, .05f, ControllerHand.Left);
            InputBridge.Instance.VibrateController(.6f, .1f, .05f, ControllerHand.Right);
        }
    }
    void GotoNextPoint()
    {
        if (NpcController.Instance.NpcWayPoints.Count != 0)
        {
            destPoint = Random.Range(0, NpcController.Instance.NpcWayPoints.Count);
            while (NpcController.Instance.NpcWayPoints[destPoint].Taken)
            {
                destPoint = Random.Range(0, NpcController.Instance.NpcWayPoints.Count);
            }
            CurrentWayPoint.Taken = false;
            CurrentWayPoint = NpcController.Instance.NpcWayPoints[destPoint];
            CurrentWayPoint.Taken = true;
            agent.SetDestination(CurrentWayPoint.WayPoint.position);
        }
        StartCoroutine(WaitBeforNextPoint(Player.transform.position));
    }
    void GoToNextPointWithoutWait()
    {
        if (NpcController.Instance.NpcWayPoints.Count != 0)
        {
            destPoint = Random.Range(0, NpcController.Instance.NpcWayPoints.Count);
            while (NpcController.Instance.NpcWayPoints[destPoint].Taken)
            {
                destPoint = Random.Range(0, NpcController.Instance.NpcWayPoints.Count);
            }
            CurrentWayPoint.Taken = false;
            CurrentWayPoint = NpcController.Instance.NpcWayPoints[destPoint];
            CurrentWayPoint.Taken = true;
            agent.SetDestination(CurrentWayPoint.WayPoint.position);
        }
    }
    void GoToPlayer(Vector3 PlayerPos,float WaitTime, float Distance)
    {
        WalkingToPlayer = true;

        agent.SetDestination(PlayerPos);
        StartCoroutine(WaitForWalkingToPlayer(WaitTime, Distance));
        Debug.Log("Going to player");


    }
    IEnumerator WaitForWalkingToPlayer(float WaitTime, float Distance)
    {
        while (agent.remainingDistance > Distance)
        {
            agent.SetDestination(Player.transform.position);
            yield return null;
        }
        agent.ResetPath();
        GoToNextPointWithoutWait();
        yield return new WaitForSeconds(WaitTime);
        WalkingToPlayer = false;
    }

    IEnumerator WaitBeforNextPoint(Vector3 PlayerPos)
    {
        agent.isStopped = true;
        _Animator.SetBool("HasStopped", true);

        Vector3 DirToPlayer = (PlayerPos - transform.position).normalized;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        Physics.Raycast(transform.position + new Vector3(0f, .6f, 0f), fwd, out hit, 1000, 1 << 8);
        //Vector3.Dot(transform.forward, DirToPlayer) < .9f
        if (RedDroneController.Instance.TurnToPlayer)
        {
            while (hit.collider == null)
            {
                fwd = transform.TransformDirection(Vector3.forward) * 10;
                Physics.Raycast(transform.position + new Vector3(0f, .6f, 0f), fwd, out hit, 1000, 1 << 8);
                Debug.DrawRay(transform.position + new Vector3(0f, .6f, 0f), fwd, Color.green);

                //PlayerPos = Player.transform.position;
                DirToPlayer = (PlayerPos - transform.position).normalized;
                //Debug.Log(Vector3.Dot(transform.forward, DirToPlayer));

                // Direction 1 = right      -1 = left
                float Direction = AngleDir(transform.forward, DirToPlayer, transform.up);

                if (!_Animator.GetBool("TurnRight") && Direction == 1)
                {
                    _Animator.SetBool("TurnRight", true);
                }
                else
                {
                    _Animator.SetBool("TurnLeft", true);
                }
                yield return null;
            }
        }

        _Animator.SetBool("TurnRight", false);
        _Animator.SetBool("TurnLeft", false);

        yield return new WaitForSeconds(3f);
        _Animator.SetBool("HasStopped", false);
        agent.isStopped = false;

    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }


}
