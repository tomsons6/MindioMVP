using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class MeditationController : MonoBehaviour
{
    public GameObject FlyingObejct;
    Rigidbody FlyingObjectRB;

    public float BreathIn;
    float CurrentHoldBreahtIn;
    bool breathInDone;

    public float BreathInPause;
    bool breathinPauseDone;

    public float BreathOut;
    bool breathOutDone;

    public float BreathOutPause;
    bool BreathOutPauseDone;

    [SerializeField]
    float startAltitude;

    //Animation Timers
    float t = 0f;


    // Start is called before the first frame update
    void Start()
    {
        FlyingObjectRB = FlyingObejct.GetComponent<Rigidbody>();
        FlyingObjectRB.useGravity = false;
        startAltitude = FlyingObejct.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        MeditationCycle();
    }

    void MeditationCycle()
    {
        if (InputBridge.Instance.RightTrigger > .7f)
        {
            breathIn();
        }
        
        if (InputBridge.Instance.LeftTrigger > .7f)
        {
            breathOut();
        }

    }
    public void breathIn()
    {
        if(FlyingObejct.transform.position.y != startAltitude)
        {
            FlyingObjectRB.useGravity = false;
            FlyingObjectRB.isKinematic = true;

            while (t <= 1f)
            {
                t += Time.deltaTime/2;
                Vector3 currentPos = Vector3.Lerp(FlyingObejct.transform.position, new Vector3(FlyingObejct.transform.position.x, startAltitude, FlyingObejct.transform.position.z), t);
                FlyingObejct.transform.position = currentPos;
                Debug.Log("should flyup");
            }
        }


    }
    public void breathPauseIn()
    {

    }
    public void breathOut()
    {

    }
    public void breathPauseOut()
    {

    }
}
