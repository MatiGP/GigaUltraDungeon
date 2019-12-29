using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ArchitectDialog : MonoBehaviour
{
    [TextArea(1,4)]
    public string[] sentences;
    public Animator dialogAnimator;
    private Enemy enemy;
    private bool firstPhase = true;
    public bool secondPhase = false;
    public bool thirdPhase = false;
    public Button skipButton;
    public float typingSpeed;
    public TextMeshProUGUI uiSentence;
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
                OpenDialog(0);
                StartCoroutine(GetComponent<ArchitectSpecialAttacks>().PhaseOne());
                secondPhase = true;
            } else if (enemy.GetHealthPercentage() <= 0.5 && secondPhase)
            {
                secondPhase = false;
                OpenDialog(1);
                GetComponent<ArchitectSpecialAttacks>().PhaseTwo();
                thirdPhase = true;
            }
            else if (enemy.GetHealthPercentage() <= 0.25 && thirdPhase)
            {
                thirdPhase = false;
                OpenDialog(2);
                GetComponent<ArchitectSpecialAttacks>().PhaseThree();
            }
        }
    }

    void OpenDialog(int index)
    {
        skipButton.onClick.AddListener(SkipDialog);
        dialogAnimator.SetTrigger("hasEnteredTheTrigger");
        dialogAnimator.SetBool("isOpen", true);

        StartCoroutine(TypeSentence(index));

    }

    IEnumerator TypeSentence(int index)
    {
        yield return new WaitForSeconds(1.2f);

        foreach (char letter in sentences[index])
        {
            uiSentence.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(1f);
        SkipDialog();
    }

    void SkipDialog()
    {
        dialogAnimator.SetBool("isOpen", false);
        uiSentence.text = " ";
        skipButton.onClick.RemoveListener(SkipDialog);
        StopAllCoroutines();
    }

}
