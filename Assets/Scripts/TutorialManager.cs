using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject controller;
    public GameObject neuron;
    public GameObject player;
    public GameObject fader;
    private ControllerInputScript controls;
    public GameObject triggerPrompt;
    public GameObject analogPrompt;
    public GameObject neuronPrompt;
    public GameObject startPrompt;
    public float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controls = controller.GetComponent<ControllerInputScript>();
        StartCoroutine(displayTutorialButtons());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 neuronPos = neuron.transform.position;
        if (Vector2.Distance(neuronPos, (Vector2)player.transform.position) > 25f)
        {
            neuronPos = Vector2.Lerp(neuronPos, (Vector2)player.transform.position + new Vector2(10f, -10f), 3f * Time.deltaTime);
            neuron.transform.position = neuronPos;
        }
    }

    IEnumerator displayTutorialButtons()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOut(fader));
        yield return new WaitForSeconds(3);
        //Spawn in Trigger button prompt
        StartCoroutine(FadeIn(triggerPrompt));
        yield return new WaitUntil(() => controls.getRightTriggerDown());
        //Change color of trigger button prompt
        yield return new WaitForSeconds(4);
        //Spawn in Analog stick prompt
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeIn(analogPrompt));
        yield return new WaitUntil(() => controls.getRightTrigger() && (controls.getLeftStickX() > Mathf.Abs(0.5f) || controls.getLeftStickY() > Mathf.Abs(0.5f)));
        //Change color of analog stick prompt
        yield return new WaitForSeconds(10);
        StartCoroutine(FadeOut(triggerPrompt));
        StartCoroutine(FadeOut(analogPrompt));
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeIn(neuronPrompt));
        yield return new WaitForSeconds(3);
        neuron.SetActive(true);
        //Spawn in start button
        yield return new WaitUntil(() => neuron.GetComponent<Neuron>().activated);
        StartCoroutine(FadeOut(neuronPrompt));
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeIn(startPrompt));
        yield return new WaitUntil(() => controls.getADown());
        StartCoroutine(FadeIn(fader));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);




        yield return null;
    }

    IEnumerator FadeIn(GameObject prompt)
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += 0.1f;
            if (prompt.GetComponent<SpriteRenderer>())
            {
                prompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
            }
            else if (prompt.GetComponent<Image>())
            {
                prompt.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
            }
            else if (prompt.GetComponent<TextMeshProUGUI>())
            {
                prompt.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, alpha);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    IEnumerator FadeOut(GameObject prompt)
    {
        float alpha = 0;
        if (prompt.GetComponent<Image>())
        {
            alpha = prompt.GetComponent<Image>().color.a;
        }
        else if (prompt.GetComponent<SpriteRenderer>())
        {
            alpha = prompt.GetComponent<SpriteRenderer>().color.a;
        }
        while (alpha > 0)
        {
            alpha -= 0.1f;
            if (prompt.GetComponent<Image>())
            {
                prompt.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
            }
            else if (prompt.GetComponent<SpriteRenderer>())
            {
                prompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }


}
