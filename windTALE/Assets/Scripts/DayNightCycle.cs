using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float speedRotate;
    public float lerpSpeed;
    public SpriteRenderer sky;
    public SpriteRenderer bg1;
    public SpriteRenderer bg2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * speedRotate);
        if (transform.rotation.eulerAngles.z > 290 || transform.rotation.eulerAngles.z < 60)
        {
            sky.GetComponent<SpriteRenderer>().color = Color.Lerp(sky.GetComponent<SpriteRenderer>().color, Color.white, Time.deltaTime * lerpSpeed * 8);
            bg1.GetComponent<SpriteRenderer>().color = Color.Lerp(bg1.GetComponent<SpriteRenderer>().color, Color.white, Time.deltaTime * lerpSpeed * 8);
            bg2.GetComponent<SpriteRenderer>().color = Color.Lerp(bg2.GetComponent<SpriteRenderer>().color, Color.white, Time.deltaTime * lerpSpeed * 8);
        }
        else
        {
            sky.GetComponent<SpriteRenderer>().color = Color.Lerp(sky.GetComponent<SpriteRenderer>().color, Color.black, Time.deltaTime * lerpSpeed);
            bg1.GetComponent<SpriteRenderer>().color = Color.Lerp(bg1.GetComponent<SpriteRenderer>().color, Color.gray, Time.deltaTime * lerpSpeed);
            bg2.GetComponent<SpriteRenderer>().color = Color.Lerp(bg2.GetComponent<SpriteRenderer>().color, Color.gray, Time.deltaTime * lerpSpeed);
        }
    }
}
