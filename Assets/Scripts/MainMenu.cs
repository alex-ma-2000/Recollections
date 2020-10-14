using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject controller;
    public GameObject fader;
    private ControllerInputScript controls;

    // Start is called before the first frame update
    void Start()
    {
        controls = controller.GetComponent<ControllerInputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.getStartDown())
        {
            StartCoroutine(FadeIn(fader));
        }
    }

    IEnumerator FadeIn(GameObject prompt)
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += 0.1f;
            prompt.GetComponent<Image>().color = new Color(255f, 255f, 255f, alpha);
            yield return null;
        }
        SceneManager.LoadScene(1);
        yield return null;
    }

}
