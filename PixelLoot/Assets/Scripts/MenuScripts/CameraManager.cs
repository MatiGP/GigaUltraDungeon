using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.LWRP;

public class CameraManager : MonoBehaviour
{
    public GameObject characterStatPanel;
    public GameObject titleScreenCam;
    public GameObject titleScreenPanel;
    public GameObject mageVCam;
    public Light2D mageLight;
    public GameObject knightVCam;
    public Light2D knightLight;
    public GameObject archerVCam;
    public Light2D archerLight;

    public GameObject buttons;
    public GameObject blackOverlay;
    public SetupCharacterStatPanel setupCharacter;  

    private void Awake()
    {
        
        
    }

    private void Start()
    {
        Time.timeScale = 1f;      

    }
    public void ResetCamera()
    {       
        characterStatPanel.SetActive(false);
        mageLight.enabled = false;
        knightLight.enabled = false;
        archerLight.enabled = false;
        mageVCam.SetActive(false);
        knightVCam.SetActive(false);
        archerVCam.SetActive(false);
        titleScreenCam.SetActive(true);
        StartCoroutine(LoadButtons());
    }

    public void ChooseChar()
    {
        titleScreenPanel.SetActive(false);
        blackOverlay.SetActive(false);
    }

    public void SelectMage()
    {
        mageVCam.SetActive(true);
        knightVCam.SetActive(false);
        archerVCam.SetActive(false);
        characterStatPanel.SetActive(true);
        buttons.SetActive(false);
        mageLight.enabled = true;
        knightLight.enabled = false;
        archerLight.enabled = false;
        setupCharacter.setupWindow(1);
    }
    public void SelectKnight()
    {
        mageVCam.SetActive(false);
        knightVCam.SetActive(true);
        archerVCam.SetActive(false);
        characterStatPanel.SetActive(true);
        buttons.SetActive(false);
        mageLight.enabled = false;
        knightLight.enabled = true;
        archerLight.enabled = false;
        setupCharacter.setupWindow(3);
    }
    public void SelectArcher()
    {
        mageVCam.SetActive(false);
        knightVCam.SetActive(false);
        archerVCam.SetActive(true);
        characterStatPanel.SetActive(true);
        buttons.SetActive(false);
        mageLight.enabled = false;
        knightLight.enabled = false;
        archerLight.enabled = true;
        setupCharacter.setupWindow(2);
    }

    IEnumerator LoadButtons()
    {
        yield return new WaitForSeconds(1);
        buttons.SetActive(true);
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
