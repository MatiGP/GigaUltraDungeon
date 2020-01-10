using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ArchitectDialog : MonoBehaviour
{
    [TextArea(1,4)]
    [SerializeField] string[] sentences;
    [SerializeField] LevelFader fader;
    private Enemy enemy;
    private bool firstPhase = true;
    private bool secondPhase = false;
    private bool thirdPhase = false;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.GetIsTaunted()) {
            if (enemy.GetHealthPercentage() == 1 && firstPhase)
            {
                
                firstPhase = false;
                Dialog.instance.OpenDialog("Architekt", sentences[0], 1f);
                StartCoroutine(GetComponent<ArchitectSpecialAttacks>().PhaseOne());
                secondPhase = true;
            } else if (enemy.GetHealthPercentage() <= 0.5 && secondPhase)
            {
                secondPhase = false;
                Dialog.instance.OpenDialog("Architekt", sentences[1], 1f);
                GetComponent<ArchitectSpecialAttacks>().PhaseTwo();
                thirdPhase = true;
            }
            else if (enemy.GetHealthPercentage() <= 0.25 && thirdPhase)
            {
                thirdPhase = false;
                Dialog.instance.OpenDialog("Architekt", sentences[3], 1f);
                GetComponent<ArchitectSpecialAttacks>().PhaseThree();
            }
        }
    }

    private void OnDestroy()
    {
        fader.FadeOut(5);
    }

}
