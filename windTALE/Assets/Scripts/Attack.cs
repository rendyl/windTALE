using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isDodging = false;
    public bool alive = true;
    public float score = 0;

    public float[] distances = new float[2];
    public NeuralNetwork network;

    float timeAttack = 0f;
    float timeDodge = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Attackz()
    {
        isAttacking = true;
        timeAttack = 0.4f;
        gameObject.GetComponent<Animator>().SetTrigger("Attack");
        //gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void Dodge()
    {
        isDodging = true;
        timeDodge = 0.4f;
        gameObject.GetComponent<Animator>().SetTrigger("Dodge");
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(alive)
        {
            score += Time.deltaTime;

            if (timeAttack > 0)
            {
                timeAttack -= Time.deltaTime;
                if (timeAttack <= 0.2f)
                {
                    isAttacking = false;
                    //gameObject.GetComponent<SpriteRenderer>().color = Color.white;     
                }
            }

            if (timeDodge > 0)
            {
                timeDodge -= Time.deltaTime;
                if (timeDodge <= 0.2f)
                {
                    isDodging = false;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
                }
            }       
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0f);
        }
        /*
        if (Input.GetMouseButtonDown(0) && timeAttack <= 0 && timeDodge <= 0 && alive)
        {
            Attackz();
        }

        if (Input.GetMouseButtonDown(1) && timeAttack <= 0 && timeDodge <= 0 && alive)
        {
            Dodge();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        */
    }

    public IEnumerator GameLoop()
    {
        while (alive)
        {
            UpdateDistances();
            Decision();
            yield return new WaitForSeconds(0.05f);
        }
    }

    void UpdateDistances()
    {
        if (FindObjectOfType<SpawnerEnemy>().listEnemy.Count > 0)
        {
            if(FindObjectOfType<SpawnerEnemy>().listEnemy[0].transform.position.x <= transform.position.x)
            {
                distances[0] = -1f;
            }
            else
            {
                distances[0] = (FindObjectOfType<SpawnerEnemy>().listEnemy[0].transform.position - transform.position).magnitude;
            }
            
        }
        else
        {
            distances[0] = -1f;
        }

        if (FindObjectOfType<SpawnerEnemy>().listSpike.Count > 0)
        {
            if (FindObjectOfType<SpawnerEnemy>().listSpike[0].transform.position.x <= transform.position.x)
            {
                distances[1] = -1f;
            }
            else
            {
                distances[1] = (FindObjectOfType<SpawnerEnemy>().listSpike[0].transform.position - transform.position).magnitude;
            }
        }
        else
        {
            distances[1] = -1f;
        }
    }

    void Decision()
    {
        if (alive)
        {   
            float[] decision = network.Compute(distances);

            if (decision[0] < 0.333f && timeAttack <= 0 && timeDodge <= 0 && alive)
            {
                Attackz();
            }
            else if (decision[0] > 0.666f && timeAttack <= 0 && timeDodge <= 0 && alive)
            {
                Dodge();
            }
        }
    }
}
