using System;
using System.Collections;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform[] spawnPoint;
    private float spawnInterval = 2f;
    private float minimunSpawnInterval =1f;
    private float intervalDecrese = 0.1f;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            Transform localSpwnPoint = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)];
            // Here we try catch here, as we may want to know if we've failed to attach a spawnpoint or enemy to the spawner.
            try
                {Instantiate(objectToSpawn, new Vector3(UnityEngine.Random.Range((float)(localSpwnPoint.position.x - 1.3), (float)(localSpwnPoint.position.x + 1.3)), localSpwnPoint.position.y, localSpwnPoint.position.z), localSpwnPoint.rotation);}
            catch(Exception error)
                {Debug.LogError(error);}
            yield return new WaitForSeconds(spawnInterval);
            spawnInterval = MathF.Max(minimunSpawnInterval, spawnInterval = intervalDecrese);
        }
    }
}
