using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public int health = 100;
    public int val = 50;

    public GameObject deatheffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private Transform target;
    private int wavePointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
    }

    public void TakeDamage(int amount)
    {

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.Money += val;

        GameObject effect = (GameObject)Instantiate(deatheffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Direction();
    }

    void Direction()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
