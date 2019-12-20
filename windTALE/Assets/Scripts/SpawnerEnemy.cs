using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject spikePrefab;
    public List<GameObject> listEnemy;
    public List<GameObject> listSpike;
    float timeBeforeSpawn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeSpawn -= Time.deltaTime;

        if(timeBeforeSpawn < 0f)
        {
            GameObject go;
            if (Random.Range(0, 2) == 0)
            {
                go = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                listEnemy.Add(go);
            }
            else
            {
                go = Instantiate(spikePrefab, new Vector3(transform.position.x, -3.11f, 0), Quaternion.identity);
                if (Random.Range(0, 2) == 0) go.GetComponent<SpriteRenderer>().flipX = false;
                listSpike.Add(go);
            }
            
            go.transform.parent = gameObject.transform;
            timeBeforeSpawn = 0.8f + Random.Range(0, 1f);
        }
    }
}
