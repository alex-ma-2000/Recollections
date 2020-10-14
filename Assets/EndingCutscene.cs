using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // AkSoundEngine.SetState("");
        StartCoroutine(FinalCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FinalCutscene()
    {
        AkSoundEngine.PostEvent("Set_State_Hospital", gameObject);
        yield return new WaitForSeconds(1);
        GameObject.Find("FadeToBlack").SetActive(true);
        //Play Sound Son_Line_5
    }
}
