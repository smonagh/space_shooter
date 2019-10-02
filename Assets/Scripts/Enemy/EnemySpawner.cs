using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    private int startingWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnAllWaves(){
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++){
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig){

      for(int enemy = 0; enemy < waveConfig.GetNumberOfEnemies(); enemy++){
            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity);

            newEnemy.GetComponent<EnemyMovement>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
          }
    }
}
