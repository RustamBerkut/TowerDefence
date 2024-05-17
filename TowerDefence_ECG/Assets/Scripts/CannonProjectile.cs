using UnityEngine;
using System.Collections;

public class CannonProjectile : MonoBehaviour 
{
	public float m_speed = 0.2f;

    [SerializeField] private int m_damage = 10;

    private void OnTriggerEnter(Collider other) 
	{
		if (!other.gameObject.TryGetComponent<MonsterHealthSystem>(out var monster))
			return;

		monster.TakeDamage(m_damage);
		Destroy (gameObject);
	}
}
