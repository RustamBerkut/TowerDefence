using UnityEngine;
using System.Collections;

public class CannonProjectile : MonoBehaviour 
{
	public float m_speed = 0.2f;
	public int m_damage = 10;

	private void OnTriggerEnter(Collider other) 
	{
		if (!other.gameObject.TryGetComponent<Monster>(out var monster))
			return;

		monster.m_hp -= m_damage;
		if (monster.m_hp <= 0) {
			Destroy (monster.gameObject);
		}
		Destroy (gameObject);
	}
}
