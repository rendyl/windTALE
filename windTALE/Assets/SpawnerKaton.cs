using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerKaton : MonoBehaviour
{
    public GameObject katonPrefab;
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
            GameObject go = Instantiate(katonPrefab, transform.position, Quaternion.identity);
            go.transform.parent = gameObject.transform;
            timeBeforeSpawn = 1f;
        }
    }
}
