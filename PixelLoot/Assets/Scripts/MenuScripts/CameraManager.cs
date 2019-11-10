using UnityEngine.EventSystems;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject charSelectCam;
    public GameObject mageCam;
    public GameObject archerCam;
    public GameObject knightCam;
    public GameObject characterStatPanel;
    public GameObject titleScreenCam;
    public GameObject titleScreenPanel;
    RaycastHit2D hitInfo;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

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
            case 1:               
                mageCam.SetActive(true);
                charSelectCam.SetActive(false);
                archerCam.SetActive(false);
                knightCam.SetActive(false);
                break;
            case 2:
                mageCam.SetActive(false);
                charSelectCam.SetActive(false);
                archerCam.SetActive(true);
                knightCam.SetActive(false);
                break;
            case 3:
                mageCam.SetActive(false);
                charSelectCam.SetActive(false);
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
        charSelectCam.SetActive(true);
        archerCam.SetActive(false);
        knightCam.SetActive(false);
        characterStatPanel.SetActive(false);
    }

    public void ChooseChar()
    {
        titleScreenCam.SetActive(false);
        titleScreenPanel.SetActive(false);
        charSelectCam.SetActive(true);

    }
}
