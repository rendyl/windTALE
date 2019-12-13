using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatonMove : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1,0,0) * speed * Time.deltaTime;
        if (transform.position.x < -8) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Attack>().isAttacking)
        {
            Debug.Log("hello");
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            GetComponent<Animator>().SetTrigger("burnout");
            other.gameObject.GetComponent<ScoreManager>().upScore();
        }
        else
        {
            other.gameObject.GetComponent<ScoreManager>().gameOver();
        }
    }
}
