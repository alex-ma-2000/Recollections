using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPan : MonoBehaviour
{
    public GameObject[] panLocations;

    ProgressManager pm;
    Memory curLevel;

    private bool playCutscene = false;

    // Start is called before the first frame update
    void Start()
    {
        pm = ProgressManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        curLevel = pm.getCurrentMemory();
        if (playCutscene)
        {
            GetComponent<CameraZoom>().turnOnOff(false);
            GetComponent<CinemachineVirtualCamera>().Follow = null;
            PanCamera(curLevel.getID());
        } else
        {
            PanCameraBack(curLevel.getID());
        }
    }

    public void SetPlayCutscence(bool play)
    {
        playCutscene = play;
    }

    void PanCamera(int lvlID)
    {
        if (lvlID == 1)
        {
            if (gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < 50)
            {
                gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 0.2f;
            }
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, panLocations[0].transform.position, 1.0f * Time.deltaTime);
        }
        else if (lvlID == 2)
        {
            if (gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < 50)
            {
                gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 0.2f;
            }
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, panLocations[1].transform.position, 1.0f * Time.deltaTime);
        } 
        else if (lvlID == 3)
        {

        }
    }
    void PanCameraBack(int lvlID)
    {
        if (lvlID == 1)
        {
            if (gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 25)
            {
                gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= 0.03f;
            }
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, curLevel.transform.position, 1.0f * Time.deltaTime);
        }
        else if (lvlID == 2)
        {
            if (gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 25)
            {
                gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= 0.03f;
            }
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, curLevel.transform.position, 1.0f * Time.deltaTime);
        }
        else if (lvlID == 3)
        {

        }
    }
}

