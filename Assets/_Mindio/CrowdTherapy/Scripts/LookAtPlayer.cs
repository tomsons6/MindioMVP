using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    Animator _Animator;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    [Range(0.0f,1f)]
    public float HeadWeight;
    [SerializeField]
    [Range(0.0f, 1f)]
    public float ClampWeight;
    [SerializeField]
    [Range(0.0f, 1f)]
    float EyeWeight;



    // Start is called before the first frame update
    void Awake()
    {
        _Animator = this.gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("MainCamera");

    }

    void OnAnimatorIK()
    {
        _Animator.SetLookAtWeight(1f, 0f, HeadWeight, EyeWeight, ClampWeight);
        if (Player != null)
        {
            _Animator.SetLookAtPosition(Player.transform.position);
        }
        //_Animator.SetLookAtPosition(Player.transform.position);
    }
}
