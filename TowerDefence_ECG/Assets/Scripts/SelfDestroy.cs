using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float m_timeToDestroy = 2f;
    private void Start()
    {
        Destroy(gameObject, m_timeToDestroy);
    }
}
