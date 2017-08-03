using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int maxLevel = 2;
    public int increaseCount = 5;
    public int maxCount = 100;

    public Wave[] waves;
    public Transform startPoint;

    public static int countAlive;

    private int waveRate = 2;
    private int level = 1;
    private Coroutine coroutine;

    private void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (level <= maxLevel)
        {
            CalculateEnemyCount();
            foreach (var wave in waves)
            {
                for (int i = 0; i < wave.count; i++)
                {
                    Instantiate(wave.enemeyPrefab, startPoint.position, Quaternion.identity);
                    countAlive++;
                    yield return new WaitForSeconds(wave.rate);
                }
            }
            while (countAlive > 0)
            {
                yield return 0;
            }
            if (++level < maxCount)
                yield return new WaitForSeconds(waveRate);
        }

        SendMessage("GameWin");
    }

    private void CalculateEnemyCount()
    {
        int count = Mathf.Min(maxCount, (level / 5 + 1) * 10);
        int enemy1Count = Random.Range(0, count + 1);
        int enemy2Count = Random.Range(0, count - enemy1Count + 1);
        int enemy3Count = count - enemy1Count - enemy2Count;
        waves[0].count = enemy1Count;
        waves[1].count = enemy2Count;
        waves[2].count = enemy3Count;
    }

    public void StopSpawn()
    {
        StopCoroutine(coroutine);
    }
}