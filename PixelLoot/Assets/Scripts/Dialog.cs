using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public static Dialog instance;
    public TextMeshProUGUI uiSentence;
    public TextMeshProUGUI speakerName;
    public float typingSpeed;
    public Animator animator;

    private void Awake()
    {
        instance = this;
    }

    IEnumerator TypeSentence(string sentence)
    {
        yield return new WaitForSeconds(1.2f);

        foreach (char letter in sentence)
        {
            uiSentence.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(2f);
        SkipDialog();
    }
    IEnumerator TypeSentence(string sentence, float delayEnd)
    {
        yield return new WaitForSeconds(1.2f);

        foreach (char letter in sentence)
        {
            uiSentence.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(delayEnd);
        SkipDialog();
    }

    public void SkipDialog()
    {
        animator.SetBool("isOpen", false);
        uiSentence.text = " ";
        StopAllCoroutines();
    }   

    public void OpenDialog(string speaker, string sentence)
    {
        uiSentence.text = "";
        speakerName.text = speaker;
        StartCoroutine(TypeSentence(sentence));
        animator.SetTrigger("triggerDialog");
        animator.SetBool("isOpen", true);

    }
    public void OpenDialog(string speaker, string sentence, float customDelay)
    {
        uiSentence.text = "";
        speakerName.text = speaker;
        StartCoroutine(TypeSentence(sentence, customDelay));
        animator.SetTrigger("triggerDialog");
        animator.SetBool("isOpen", true);

    }
}
