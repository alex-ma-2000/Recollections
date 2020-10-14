using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour
{
    public bool activated;
    public int id;
    public float lerpSpeed;
    public float lightRange;
    private Light l;
    private bool lightUp;
    private bool curLevel;

    ProgressManager pm;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        l = (Light)GetComponent("Light");
        l.enabled = false;
        lightUp = false;
        pm = ProgressManager.instance;
        gameObject.GetComponent<Follow>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (pm.getCurrentMemory().getID() == gameObject.GetComponentInParent<Memory>().getID())
        {
            curLevel = true;
        } 
        else
        {
            curLevel = false;
        }
        if (curLevel == true) {
            AkSoundEngine.PostEvent("Trigger_Memory_Segment_" +
                gameObject.GetComponentInParent<Transform>().GetComponentInParent<Memory>().getID() + "_" + id, gameObject);
            Debug.Log("PlayedSound" + " Trigger_Memory_Segment_" +
                gameObject.GetComponentInParent<Transform>().GetComponentInParent<Memory>().getID() + "_" + id);
        }
        if (lightUp == true)
        {
            l.range = Mathf.Lerp(l.range, lightRange, lerpSpeed);
        }
    }

    public void ActivateNeuron()
    {
        Debug.Log("Activate neuron");
        activated = true;
        LightUpNeuron();
        if (transform.GetChild(0))
        {
            StartCoroutine(FlashMemSprite());
        }
    }

    IEnumerator FlashMemSprite()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += 0.1f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(0.05f);
        }
        while (alpha > 0)
        {
            alpha -= 0.1f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(0.05f);
        }
        Vector2 newSize = transform.localScale / 3;
        while (transform.localScale.magnitude > newSize.magnitude)
        {
            transform.localScale = transform.localScale * 0.8f;
            yield return null;
        }
        gameObject.GetComponent<Follow>().enabled = true;
        yield return null;
    }

    void LightUpNeuron()
    {
        l.enabled = true;
        lightUp = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            GameObject camera = GameObject.FindWithTag("camera");
            camera.GetComponent<CameraZoom>().ZoomToNeuron(gameObject);
        }
    }
}
