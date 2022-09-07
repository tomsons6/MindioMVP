using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BNG;
public class HelperDroneStartFlight : MonoBehaviour
{
    public GameObject[] DroneWayPoints;
    int destinationPoint;
    private NavMeshAgent agent;
    [SerializeField]
    GameObject Player;
    bool startText = false;
    bool startTextAferBreath = false;
    [SerializeField]
    TextController ScrollingText;
    DroneAnimation _DroneAnimation;
    [SerializeField]
    bool startpressed = false;
    [SerializeField]
    GameObject UI;
    [SerializeField]
    GameObject DroneController;
    [SerializeField]
    public bool startFrequenceFInished = false;

    public bool helpCalled;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        //FlyToStart();
        _DroneAnimation = GetComponent<DroneAnimation>();
        agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartSequence();
        if (InputBridge.Instance.AButtonDown || InputBridge.Instance.YButtonDown || InputBridge.Instance.BButtonDown || InputBridge.Instance.XButtonDown)
        {
            if (startFrequenceFInished)
            {
                //agent.destination = Player.transform.position + Player.transform.forward * 2f;
                agent.SetDestination(Player.transform.position + Player.transform.forward * 2f);
                //if (agent.remainingDistance < 0.2f)
                //{
                //    if (_DroneAnimation.ToogleBreathing == false)
                //    {
                //        _DroneAnimation.ToogleBreathing = true;
                //        StartCoroutine(_DroneAnimation.Breathing());
                //        Debug.Log("BreathAnimation if");
                //        //_DroneAnimation.ToogleBreathing = false;
                //    }
                //}
                Debug.Log(agent.destination);
                helpCalled = true;
                StartCoroutine(CallHelp());

            }
        }
    }

    void StartSequence()
    {
        if (startpressed)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f && !startFrequenceFInished)
            {
                GoToNextPoint(1);
            }
            if (agent.velocity.magnitude < 0.25f)
            {
                //transform.LookAt(Player.transform.position);
                Vector3 lTargetDir = Player.transform.position - transform.position;
                lTargetDir.y = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time);
            }
            if (Vector3.Distance(agent.destination, DroneWayPoints[1].transform.position) <= 2f && agent.velocity.magnitude < .25f)
            {
                if (!startText)
                {
                    startText = true;
                    StartCoroutine(ScrollingText.TextTyping(ScrollingText.m_Text.ToCharArray()));
                }
            }
            if (ScrollingText.TextFinished && _DroneAnimation.enabled == false)
            {
                _DroneAnimation.enabled = true;
                _DroneAnimation.ToogleBreathing = true;
            }
            if (_DroneAnimation.Breathingfinished)
            {
                if (!startTextAferBreath)
                {
                    ScrollingText.TextFinished = false;
                    startTextAferBreath = true;
                    StartCoroutine(ScrollingText.TextTyping(ScrollingText.m_AfterBreathing.ToCharArray()));
                }
                if (ScrollingText.TextFinished && !startFrequenceFInished)
                {
                    GoToNextPoint(2);
                    DroneController.SetActive(true);
                    startFrequenceFInished = true;
                }
            }
            if (startFrequenceFInished)
            {
               DroneController.SetActive(true);
            }
        }
    }
    IEnumerator CallHelp()
    {
        while (helpCalled)
        {
            if(agent.remainingDistance < 0.2f && agent.remainingDistance != 0f)
            {
                break;
            }
            //Debug.Log(agent.remainingDistance);
            yield return null;
        }
        yield return new WaitForSeconds( 1f);
        if (_DroneAnimation.ToogleBreathing == false )
        {
            _DroneAnimation.ToogleBreathing = true;
            StartCoroutine(_DroneAnimation.Breathing());
            Debug.Log("BreathAnimation if");
            //_DroneAnimation.ToogleBreathing = false;
        }

    }
    public void FlyToStart()
    {
        UI.SetActive(false);
        startpressed = true;
        agent.isStopped = false;
        agent.SetDestination(DroneWayPoints[0].transform.position);


        //agent.isStopped = true;
    }
    public void GoToNextPoint(int PointID)
    {
        agent.destination = DroneWayPoints[PointID].transform.position;
        agent.autoBraking = true;
    }
}
