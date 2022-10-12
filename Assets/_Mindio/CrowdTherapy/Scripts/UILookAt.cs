using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{

    Transform Player;
    [SerializeField]
    float ForawrdDistance;
    [SerializeField]
    float UIHeight;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<BNG.BNGPlayerController>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = new Quaternion(0f,Player.rotation.y,0f,0f);
        transform.position = Player.position + Player.forward + new Vector3(0f,UIHeight,ForawrdDistance);

    }
}
