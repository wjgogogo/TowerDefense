using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform startPoint;
    public float waveRate = 3;
    public static int countAlive;

    //public
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        foreach (var wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                Instantiate(wave.enemeyPrefab, startPoint.position, Quaternion.identity);
                countAlive++;
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }
            while (countAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
    }
}