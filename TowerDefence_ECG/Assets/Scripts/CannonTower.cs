using UnityEngine;

public class CannonTower : MonoBehaviour 
{
	public float m_shootInterval = 0.5f;
	public float m_range = 4f;
	public GameObject m_projectilePrefab;
	public Transform m_shootPoint;
    
    private float m_projectileSpeed;
    private GameObject m_monster;
    private float timeToShot;
    private Vector3 point;

    private void Start()
    {
        CreateDetectSphere();
        m_projectileSpeed = m_projectilePrefab.GetComponent<CannonProjectile>().m_speed;
    }

    private void Update () 
	{
        timeToShot += Time.deltaTime;
        
        if (m_monster != null)
        {
            float speed = m_monster.GetComponent<Monster>().m_speed;
            float time = (m_monster.transform.position.x - transform.position.x) / 
                (m_projectileSpeed - speed) + 
                (m_monster.transform.position.z - transform.position.z) /
                (m_projectileSpeed - speed);

            point.x = m_monster.transform.position.x + speed * time;
            point.z = m_monster.transform.position.z + speed * time;

            transform.LookAt(point);

            if (timeToShot > m_shootInterval)
            {
                Shot();
                timeToShot = 0;
            }

        }
        if (m_monster == null) 
        {

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
            m_monster = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Monster>())
        {
            m_monster = null;
            Debug.Log("find new enemy");
        }
    }
    private void FindNewEnemy()
    {

    }
    private void Shot()
    {
		GameObject projectile = Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * m_projectileSpeed);
    }
}
