using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using System.Linq;
using UnityEngine.UI;
public class DroneScript : MonoBehaviour
{
    [SerializeField]
    GameObject CanvasPanel;
    [SerializeField]
    float CanvasOpenSpeed = .2f;
    [SerializeField]
    Text TexTBox;

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
                foreach (RedDroneTexts RDTexts in RedDroneController.Instance.RedDroneTexts)
                {
                    if (!RDTexts.hasShowned)
                    {
                        StartCoroutine(HelpingText(RDTexts));
                        break;
                    }
                }

            }
        }

    }
    IEnumerator HelpingText(RedDroneTexts DroneText)
    {
        DroneText.hasShowned = true;
        TexTBox.text = DroneText.HelpingText;
        Vector3 CanvasStartScale = CanvasPanel.transform.localScale;
        Vector3 CanvasEndScale = new Vector3(CanvasPanel.transform.localScale.x, 1f, CanvasPanel.transform.localScale.z);
        float t = 0f;
        float t1 = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / CanvasOpenSpeed;
            Vector3 CurrentScale = Vector3.Lerp(CanvasStartScale, CanvasEndScale, t);
            CanvasPanel.transform.localScale = CurrentScale;
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        while (t1 <= 1f)
        {
            t1 += Time.deltaTime / CanvasOpenSpeed;
            Vector3 CurrentScale = Vector3.Lerp(CanvasEndScale, CanvasStartScale, t1);
            CanvasPanel.transform.localScale = CurrentScale;
            yield return null;
        }
        yield return new WaitForSeconds(.5f);
        Destroy(this.transform.parent.gameObject);
    }
}
