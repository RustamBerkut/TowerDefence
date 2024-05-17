using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Monster : MonoBehaviour 
{
	const float m_reachDistance = 0.3f;

    public GameObject m_moveTarget;
	public float m_speed = 0.01f;
	
	private void Update () 
	{
		if (m_moveTarget == null)
			return;
		
		if (Vector3.Distance (transform.position, m_moveTarget.transform.position) <= m_reachDistance) 
		{
			Destroy (gameObject);
			return;
		}

        var translation = m_moveTarget.transform.position - transform.position;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}
		transform.Translate (translation);
	}
}
