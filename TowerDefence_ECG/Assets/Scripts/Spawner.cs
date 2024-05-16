using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	public float m_interval = 3;
	public GameObject m_moveTarget;
    public GameObject m_enemy;

    private float m_lastSpawn = -1;

	private void Update () {
		if (Time.time > m_lastSpawn + m_interval) 
		{
			EnemySpawn();
		}
	}

	private void EnemySpawn()
	{
        GameObject _enemy = Instantiate(m_enemy, transform.position, transform.rotation);
		_enemy.GetComponent<Monster>().m_moveTarget = m_moveTarget;
        m_lastSpawn = Time.time;
	}
}
