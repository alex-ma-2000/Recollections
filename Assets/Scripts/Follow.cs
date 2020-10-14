using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    public float offset = 1.5f;
    public float lerpSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, new Vector2(target.transform.position.x, target.transform.position.y + offset), lerpSpeed * Time.deltaTime);
        transform.position = newPos;
    }
}
