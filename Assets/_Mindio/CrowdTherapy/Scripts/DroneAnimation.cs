using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimation : MonoBehaviour
{
    [SerializeField]
    float BreathIn;

    [SerializeField]
    float BreathOut;
    [SerializeField]
    float pause;
    [SerializeField]
    public bool ToogleBreathing;


    public bool Breathingfinished = false;

    HelperDroneStartFlight HelpDroneSc;

    [SerializeField]
    GameObject DroneModel;

    void Start()
    {
        StartCoroutine(Breathing());
        HelpDroneSc = GetComponent<HelperDroneStartFlight>();
    }
    public IEnumerator Breathing()
    {
        int BreathCounts = 0;
        Breathingfinished = false;
        ToogleBreathing = true;
        Vector3 StartScale = DroneModel.transform.localScale;
        Vector3 EndScale = new Vector3(2f, 2f, 2f);
        while (ToogleBreathing)
        {
            BreathCounts++;
            if(BreathCounts == 4)
            {
                Breathingfinished = true;
                break;
            }
            float t = 0f;
            float t1 = 0f;
            while (t <= 1f)
            {
                t += Time.deltaTime / BreathIn;
                Vector3 CurrentScaleIn = Vector3.Slerp(StartScale, EndScale, t);
                DroneModel.transform.localScale = CurrentScaleIn;
                yield return null;
            }
            yield return new WaitForSeconds(pause);
            while (t1 <= 1f)
            {
                t1 += Time.deltaTime / BreathOut;
                Vector3 CurrentScaleOut = Vector3.Slerp(EndScale, StartScale, t1);
                DroneModel.transform.localScale = CurrentScaleOut;
                yield return null;
            }
            yield return new WaitForSeconds(pause);
        }
        if (HelpDroneSc.startFrequenceFInished)
        {
            HelpDroneSc.GoToNextPoint(2);
        }
        ToogleBreathing = false;
        HelpDroneSc.helpCalled = false;

        yield return null;
    }
    //void Update()
    //{
    //    transform.localScale = cubeBezier3(new Vector3(0f,0,0f),new Vector3(0.2f,0.2f,0.2f),new Vector3(0.8f,0.8f,0.8f),new Vector3(1f,1f,1f),5f);
    //}
    //public static Vector3 cubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    //{
    //    return (((-p0 + 3 * (p1 - p2) + p3) * t + (3 * (p0 + p2) - 6 * p1)) * t + 3 * (p1 - p0)) * t + p0;
    //}
}

