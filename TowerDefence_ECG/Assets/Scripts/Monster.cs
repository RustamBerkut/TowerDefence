using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Monster : MonoBehaviour 
{
    const float m_reachDistance = 0.3f;

    public GameObject m_moveTarget;
	public float m_speed = 0.01f;
	public int m_maxHP = 30;
	public int m_hp;

	private void Start() 
	{
		m_hp = m_maxHP;
	}

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
