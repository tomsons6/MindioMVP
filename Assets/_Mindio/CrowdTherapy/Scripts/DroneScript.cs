using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using System.Linq;
using UnityEngine.UI;
public class DroneScript : MonoBehaviour
{
    [SerializeField]
    GameObject InfoPanel;
    [SerializeField]
    GameObject CountPanel;
    [SerializeField]
    float CanvasOpenSpeed = .2f;
    [SerializeField]
    Text TexTBox;
    [SerializeField]
    Text CountTextBox;

    bool Grabbed = false;

    [SerializeField]
    GameObject RootObj;
    [SerializeField]
    GameObject Player;

     void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //Look At player
        Vector3 lTargetDir = Player.transform.position - RootObj.transform.position;
        lTargetDir.y = 0.0f;
        RootObj.transform.rotation = Quaternion.RotateTowards(RootObj.transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!Grabbed)
        {
            if (other.gameObject.GetComponent<Grabber>() != null)
            {
                Grabbed = true;
                RedDroneController.Instance.hasSpawned = false;
                RedDroneController.Instance.SpawnDrone();
                if(RedDroneController.Instance.RedDroneTexts.Any(x => x.hasShowned == false))
                {
                    foreach (RedDroneTexts RDTexts in RedDroneController.Instance.RedDroneTexts)
                    {
                        if (!RDTexts.hasShowned)
                        {
                            StartCoroutine(HelpingText(RDTexts));
                            break;
                        }
                    }
                }
                else
                {
                    Destroy(this.transform.parent.gameObject);
                }
            }
        }

    }
    IEnumerator HelpingText(RedDroneTexts DroneText)
    {
        DroneText.hasShowned = true;
        TexTBox.text = DroneText.HelpingText;
        int currentCount = ((int)RedDroneController.Instance.CurrentSpawnCount) - 1;
        CountTextBox.text = currentCount + " no " + RedDroneController.Instance.SpawnCount.ToString();
        Vector3 CanvasStartScale = InfoPanel.transform.localScale;
        Vector3 CanvasEndScale = new Vector3(InfoPanel.transform.localScale.x, 1f, InfoPanel.transform.localScale.z);
        Vector3 CountCanvasStartScale = CountPanel.transform.localScale;
        Vector3 CountCanvasEndScale = new Vector3(1f, CountPanel.transform.localScale.y, CountPanel.transform.localScale.z);

        float t = 0f;
        float t1 = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / CanvasOpenSpeed;
            Vector3 CurrentScale = Vector3.Lerp(CanvasStartScale, CanvasEndScale, t);
            Vector3 CurrentCountPanelScale = Vector3.Lerp(CountCanvasStartScale, CountCanvasEndScale, t);
            CountPanel.transform.localScale = CurrentCountPanelScale;
            InfoPanel.transform.localScale = CurrentScale;
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        while (t1 <= 1f)
        {
            t1 += Time.deltaTime / CanvasOpenSpeed;
            Vector3 CurrentScale = Vector3.Lerp(CanvasEndScale, CanvasStartScale, t1);
            Vector3 CurrentCountPanelScale = Vector3.Lerp(CountCanvasEndScale, CountCanvasStartScale, t1);
            InfoPanel.transform.localScale = CurrentScale;
            CountPanel.transform.localScale = CurrentCountPanelScale;
            yield return null;
        }
        yield return new WaitForSeconds(.5f);

        Destroy(this.transform.parent.gameObject);
    }
}
