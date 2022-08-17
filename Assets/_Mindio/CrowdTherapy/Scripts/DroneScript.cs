using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class DroneScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Grabber>()!= null)
        {
            RedDroneController.Instance.hasSpawned = false;
            RedDroneController.Instance.SpawnDrone();
            Destroy(this.transform.parent.gameObject);
        }
    }
}
