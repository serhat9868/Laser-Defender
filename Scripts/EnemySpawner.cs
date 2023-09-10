using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSo> waveConfigs;
    public WaveConfigSo currentWawe;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;
  
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }
    public WaveConfigSo GetCurrentWawe()
    {
        return currentWawe;
    }
  IEnumerator SpawnEnemyWaves()
    {
      while(isLooping)
        {

            foreach (WaveConfigSo wawe in waveConfigs)
            {
                currentWawe = wawe;
                for (int i = 0; i < currentWawe.GetEnemyCount(); i++)
                {
                    Instantiate(currentWawe.GetEnemyPrefabs(i),
                        currentWawe.GetStartingWaypoint().position,
                        Quaternion.Euler(0,0,180),
                        transform);
                    
                  
                    yield return new WaitForSeconds(currentWawe.GetRandomSpawnTime());
                }
                
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } 
        }
    }
    
    

