using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour
{

    public Transform enemyprefab;

    public Transform spawnPoint;

    public float TimeBtweenWave = 5f;
    public float CountDown = 2f;
    public Text WaveCountdowntimer;

    private int WaveIndex = 0;
    void Update()
    {
        if (CountDown <= 0f)
        {
            StartCoroutine(SpanWave());
            CountDown = TimeBtweenWave;
        }
        CountDown -= Time.deltaTime;

        CountDown = Mathf.Clamp(CountDown, 0f, Mathf.Infinity);
        WaveCountdowntimer.text = string.Format("{0:0.00}", CountDown);
    }

    IEnumerator SpanWave()
    {
        WaveIndex++;
        PlayerStats.Rounds++;

        for (int i = 0; i < WaveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

    }
    void SpawnEnemy()
    {
        Instantiate(enemyprefab, spawnPoint.position, spawnPoint.rotation);
    }

}
