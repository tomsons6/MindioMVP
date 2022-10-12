using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public bool m_Active = true;
    private Transform m_TargetTransform;
    public Vector3 m_PositionOffset = Vector3.zero;

    public float m_SmoothMoveStrength = 1f;
    public float m_MoveThreshold = 0.25f;
    public float m_StopDistance = 0.01f;

    private bool m_CanMove = false;

    // Start is called before the first frame update
    void Start()
    {
        m_TargetTransform = FindObjectOfType<BNG.BNGPlayerController>().gameObject.transform;

        if (m_TargetTransform == null)
        {
            Debug.LogError($"[FollowTarget] The target object is not assigned on {gameObject.name}");
        }

        if (m_TargetTransform != null)
        {
            SetInitPosRot();
        }
    }

    private void SetInitPosRot()
    {
        transform.position = m_TargetTransform.position + m_TargetTransform.forward + m_PositionOffset;

        //transform.rotation = Quaternion.LookRotation(transform.position - m_TargetTransform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (m_TargetTransform == null)
        {
            return;
        }
        if (m_Active == true && m_TargetTransform != null)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Vector3 initPos = m_TargetTransform.position + m_TargetTransform.forward + m_PositionOffset;
        // initPos.y = m_PositionOffset.y;

        float dist = Mathf.Abs(Vector3.Distance(transform.position, initPos));

        m_CanMove = dist >= m_MoveThreshold ? true : false;


        //transform.rotation = Quaternion.LookRotation(transform.position - m_TargetTransform.position);
        if (m_CanMove == true)
        {
            if (dist >= m_StopDistance)
            {
                Vector3 desiredPosition = m_TargetTransform.position + m_TargetTransform.forward + m_PositionOffset;
                // desiredPosition.y = m_PositionOffset.y;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * m_SmoothMoveStrength);
                transform.position = smoothedPosition;
            }
        }

    }


    public void Activate()
    {
        m_Active = true;
        SetInitPosRot();
    }

    public void Deactivate()
    {
        m_Active = false;
    }
}