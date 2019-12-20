using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isDodging = false;

    float timeAttack = 0f;
    float timeDodge = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

        if (Input.GetMouseButtonDown(0) && timeAttack <= 0 && timeDodge <= 0 && Time.timeScale != 0)
        {
            isAttacking = true;
            timeAttack = 0.4f;
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
            //gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        if (Input.GetMouseButtonDown(1) && timeAttack <= 0 && timeDodge <= 0 && Time.timeScale != 0)
        {
            isDodging = true;
            timeDodge = 0.4f;
            gameObject.GetComponent<Animator>().SetTrigger("Dodge");
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
