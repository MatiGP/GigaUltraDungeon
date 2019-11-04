using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject globalCam;
    public GameObject mageCam;
    public GameObject archerCam;
    public GameObject knightCam;
    public GameObject characterStatPanel;
    RaycastHit2D hitInfo;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hitInfo.collider.CompareTag("Selectable"))
            {
                int num = hitInfo.collider.GetComponent<CharacterSelect>().numOfChar;                
                SwitchCamera(num);
            }
        }       
    }

    void SwitchCamera(int numOfChar)
    {
        switch (numOfChar)
        {
            case 0:               
                mageCam.SetActive(true);
                globalCam.SetActive(false);
                archerCam.SetActive(false);
                knightCam.SetActive(false);
                break;
            case 1:
                mageCam.SetActive(false);
                globalCam.SetActive(false);
                archerCam.SetActive(true);
                knightCam.SetActive(false);
                break;
            case 2:
                mageCam.SetActive(false);
                globalCam.SetActive(false);
                archerCam.SetActive(false);
                knightCam.SetActive(true);
                break;

        }
        characterStatPanel.SetActive(true);
        characterStatPanel.GetComponent<SetupCharacterStatPanel>().setupWindow(numOfChar);
    }
    public void ResetCamera()
    {
        mageCam.SetActive(false);
        globalCam.SetActive(true);
        archerCam.SetActive(false);
        knightCam.SetActive(false);
        characterStatPanel.SetActive(false);
    }
}
