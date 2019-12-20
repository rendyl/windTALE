using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMove : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        if (transform.position.x < -10)
        {
            FindObjectOfType<SpawnerEnemy>().listSpike.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Attack>().isDodging)
        {
            other.gameObject.GetComponent<ScoreManager>().upScore();
        }
        else
        {
            other.gameObject.GetComponent<ScoreManager>().gameOver(0.05f);
        }
    }
}
