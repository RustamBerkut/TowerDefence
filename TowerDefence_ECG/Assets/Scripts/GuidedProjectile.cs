using UnityEngine;
using System.Collections;

public class GuidedProjectile : MonoBehaviour 
{
	public GameObject m_target;
	public float m_speed = 0.2f;

	[SerializeField] private int m_damage = 10;

	private void Update () 
	{
		if (m_target == null) {
			Destroy (gameObject);
			return;
		}

		var translation = m_target.transform.position - transform.position;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}
		transform.Translate (translation);
	}

	private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.TryGetComponent<MonsterHealthSystem>(out var monster))
            return;

        monster.TakeDamage(m_damage);
        Destroy(gameObject);
    }
}
