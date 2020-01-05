using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public float speed;
    bool done = false;
    bool fadeOut = false;
    bool fadeOnAttack = false;
    float speedLerp = 2f;

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
            FindObjectOfType<SpawnerEnemy>().listEnemy.Remove(gameObject);
            Destroy(gameObject);
        }

        if (transform.position.x < -3.9 && !done)
        {
            done = true;
            GetComponent<Animator>().SetTrigger("attack");
        }
        if(fadeOut)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, new Color(0, 0, 0, 0), Time.deltaTime * speedLerp);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Attack>().isAttacking)
        { 
            if(fadeOnAttack)
            {
                GetComponent<SpriteRenderer>().sortingOrder = 8;
                fadeOut = true;
            }
            //other.gameObject.GetComponent<ScoreManager>().upScore();
        }
        else
        {
            other.gameObject.GetComponent<Attack>().alive = false;
            other.gameObject.GetComponent<ScoreManager>().gameOver(0.1f);
        }
    }
}
