using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public GameObject controller;
    public GameObject[] cutscenes; 
    public GameObject player;
    public CinemachineVirtualCamera camera;
    [SerializeField]
    private int flag;
    public static ProgressManager instance = null;
    private ControllerInputScript controls;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        flag = 0;
        player = GameObject.Find("Player");
        controls = controller.GetComponent<ControllerInputScript>();
    }

    public void updateFlag(int id)
    {
        flag = id;
    }

    public Memory getCurrentMemory()
    {
        Memory[] memories = FindObjectsOfType<Memory>();
        foreach (Memory memory in memories)
        {
            if (flag == memory.getID())
            {
                return memory;
            }
        }
        // If no memory of give id is found.
        return null;
    }

    /*public void playCutscene(int idx)
    {
        if (idx < cutscenes.Length) {
            //Instantiate(cutscenes[idx]);
            pause();
        }
    }*/

    public IEnumerator playCutscene(int idx)
    {
        if (idx <= cutscenes.Length)
        {
            StartCoroutine(FadeIn(cutscenes[idx - 1]));
            AkSoundEngine.PostEvent("Trigger_Memory_Full_" + flag, getCurrentMemory().gameObject);
            pause();
            yield return new WaitUntil(() => controls.getADown());
            StartCoroutine(FadeOut(cutscenes[idx - 1]));
            print("playing Cutscene");
        }
        yield return null;
        camera.GetComponent<CameraPan>().SetPlayCutscence(true);
        yield return new WaitForSeconds(7);
        camera.GetComponent<CameraPan>().SetPlayCutscence(false);
        yield return new WaitForSeconds(7);
        camera.GetComponent<CameraZoom>().turnOnOff(true);
        camera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        unpause();
        yield return null;
    }

    public void pause()
    {
        player.GetComponentInChildren<DrawController>().Pause(true);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void unpause()
    {
        player.GetComponentInChildren<DrawController>().Pause(false);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    IEnumerator FadeIn(GameObject prompt)
    {
        float alpha = 0;
        prompt.transform.GetChild(0).gameObject.SetActive(true);
        while (alpha < 1)
        {
            alpha += 0.1f;
            if (prompt.GetComponent<Image>())
            {
                prompt.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    IEnumerator FadeOut(GameObject prompt)
    {
        float alpha = 0;
        prompt.transform.GetChild(0).gameObject.SetActive(false);
        if (prompt.GetComponent<Image>())
        {
            alpha = prompt.GetComponent<Image>().color.a;
        }
        while (alpha > 0)
        {
            alpha -= 0.1f;
            if (prompt.GetComponent<Image>())
            {
                prompt.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
