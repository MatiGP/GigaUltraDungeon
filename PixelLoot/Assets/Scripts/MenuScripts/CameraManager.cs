using UnityEngine.EventSystems;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject characterStatPanel;
    public GameObject titleScreenCam;
    public GameObject titleScreenPanel;
    RaycastHit2D hitInfo;

    private void Start()
    {
        Time.timeScale = 1f;

        AudioListener[] myListeners = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];

        int totalListeners = 0;//find out how many listeners are actually active
        foreach (AudioListener thisListener in myListeners)
        {
            if (thisListener.enabled) { totalListeners++; }
        }

        if (totalListeners > 1)
        {
            //turn off my audioListener component
            AudioListener al = GetComponent<AudioListener>();
            al.enabled = false;
        }
        else
        {
            //turn on my audioListener component
            AudioListener al = GetComponent<AudioListener>();
            al.enabled = true;
            //print ("turn on audio "+name);
        }

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
        
        characterStatPanel.SetActive(true);
        characterStatPanel.GetComponent<SetupCharacterStatPanel>().setupWindow(numOfChar);
    }
    public void ResetCamera()
    {       
        characterStatPanel.SetActive(false);
    }

    public void ChooseChar()
    {
        titleScreenCam.SetActive(false);
        titleScreenPanel.SetActive(false);      
    }
}
