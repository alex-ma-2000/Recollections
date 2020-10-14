using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    // Use this for initialization
    private bool dead = false;
    public bool stunned;
    private Rigidbody2D rb2d;

	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        if(stunned)
        {
            if(rb2d.drag < 100)
            {
                rb2d.drag = Mathf.Lerp(rb2d.drag, 100, 0.5f * Time.deltaTime);
            }
        }
        else
        {
            rb2d.drag = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("obstacle"))
        {
            //StartCoroutine(StunSpinPlayer());
        }
    }

    IEnumerator StunSpinPlayer()
    {
        //Debug.Log("Spinning");
        yield return new WaitForSeconds(0.2f);
        stunned = true;
        Vector3 rot = transform.GetChild(0).transform.rotation.eulerAngles;
        Color defaultColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
        float duration = 1.5f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            rot = transform.GetChild(0).transform.rotation.eulerAngles;
            rot.z += 5f;
            transform.GetChild(0).transform.rotation = Quaternion.Euler(rot);
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = defaultColor;
        stunned = false;

        yield return null;
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        //if (coll.gameObject.tag == "barrier")
        //{
        //    this.destroyEntireTrail();
        //    // Camera.main.GetComponent<Shake>().shakeDuration = 0.5f;
        //    dead = true;

        //    IEnumerator coroutine = WaitAndRestart(1.0f);

        //    StartCoroutine(coroutine);
        //}
        //if (coll.gameObject.CompareTag("softline"))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward);
        //    if (hit && hit.collider)
        //    {
        //        //List<Vector2> points = coll.gameObject.GetComponent<Line>().GetPoints();
        //        Vector2 pointPos = hit.point;
        //        Line line = coll.gameObject.GetComponent<Line>();
        //        Debug.Log(line.GetPoints());
        //        if (line.GetPoints().IndexOf(pointPos) < (line.GetPoints().Count - 10))
        //        {
        //            Debug.Log("Circle Drawn!");
        //        }
        //    }
        //}
    }

    private IEnumerator WaitAndRestart(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GameManager.instance.Restart();
        

        }
    }

    private void applyImpact(float knockback, Vector2 direction) {
        Rigidbody2D rb = transform.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockback, ForceMode2D.Impulse);
    }

    public void destroyEntireTrail() {
        GameObject[] rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject rootObject in rootObjects) {
            if (rootObject.tag == "line") {
                Destroy(rootObject);
            }
        }
    }

}
 