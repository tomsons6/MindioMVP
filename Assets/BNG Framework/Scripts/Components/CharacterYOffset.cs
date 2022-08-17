using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNG
{
    public class CharacterYOffset : MonoBehaviour
    {
        public float m_offsetYAgain = 0f;
        // This is used to offset a FinalIK character by it's parent. This fixes some positional issues.
        void LateUpdate()
        {
            float yOffset = transform.parent.localPosition.y;
            // transform.localPosition = new Vector3(transform.localPosition.x, (-1f )+ m_offsetYAgain, transform.localPosition.z);
            transform.localPosition = new Vector3(transform.localPosition.x, (-1f ) - yOffset, transform.localPosition.z);

            // float yOffset = transform.parent.position.y;
            // transform.localPosition = new Vector3(transform.localPosition.x, yOffset, transform.localPosition.z);

            // transform.localPosition = new Vector3 (transform.localPosition.x, yOffset, transform.localPosition.z);
        }
    }
}