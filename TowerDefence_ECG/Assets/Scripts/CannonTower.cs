using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public enum TurretState
{
    WithOutRotation,
    WithRotation
}

public class CannonTower : MonoBehaviour 
{
	[SerializeField] private float m_shootInterval = 0.5f;
    [SerializeField] private float m_range = 4f;
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private Transform m_shootPoint;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private TurretState m_state;

    private float m_projectileSpeed;
    private float timeToShot;
    private GameObject m_monster;

    private void Start()
    {
        CreateDetectSphere();
        m_projectileSpeed = m_projectilePrefab.GetComponent<CannonProjectile>().m_speed;
    }

    private void FixedUpdate () 
	{
        timeToShot += Time.deltaTime;
        if (m_monster != null)
        {
            float speed = m_monster.GetComponent<Monster>().m_speed;
            float distance = Vector3.Distance(m_monster.transform.position, transform.position);
            float time = distance / m_projectileSpeed;
            Vector3 futPos = new(m_monster.transform.position.x + speed * 60,
                                 0f,
                                 m_monster.transform.position.z + speed * 60);
            switch (m_state)
            {
                case TurretState.WithOutRotation:
                    transform.LookAt(futPos);
                    break;
                case TurretState.WithRotation:
                    var direction = futPos - transform.position;
                    direction.y = futPos.y;
                    var rot = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(
                                                     transform.rotation,
                                                     rot,
                                                     RotationSpeed * Time.deltaTime);
                    break;
            }
            

            Debug.Log(m_monster.transform.TransformDirection(transform.forward));
            if (timeToShot > m_shootInterval)
            {
                Shot();
                timeToShot = 0;
            }
        }
	}
    private void CreateDetectSphere()
    {
        this.gameObject.AddComponent<SphereCollider>();
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<SphereCollider>().radius = m_range;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Monster>() && !m_monster)
        {
            m_monster = FindClosestEnemy();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Monster>() && !m_monster)
        {
            m_monster = FindClosestEnemy();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Monster>() && !m_monster)
        {
            m_monster = FindClosestEnemy();
        }
    }
    public GameObject FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_range);
        List<GameObject> enemys = new();
        GameObject closestEnemy = null;
        for (int j = 0; j < hitColliders.Length; j++)
        {
            if (hitColliders[j].GetComponent<Monster>())
            {
                enemys.Add(hitColliders[j].gameObject);
            }
        }
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (var enemy in enemys)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestEnemy = enemy;
                distance = curDistance;
            }
        }
        return closestEnemy;
    }
    private void Shot()
    {
		GameObject projectile = Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * m_projectileSpeed);
    }
}
