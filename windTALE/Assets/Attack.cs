using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool isAttacking = false;
    float timeAttack = 0f;
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
            if (timeAttack <= 0)
            {
                isAttacking = false;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;     
            }
        }

        if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            timeAttack = 0.3f;
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
}
