using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class BossDialog : MonoBehaviour
{
    public TextMeshProUGUI uiSentence;
    public GameObject playerUI;
    public PlayerController playerController;
    public float typingSpeed;
    [TextArea(3, 10)]
    public string[] sentences;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerStats.instance.gameObject.GetComponent<PlayerController>();   
    }

    IEnumerator TypeSentence(int index)
    {
        yield return new WaitForSeconds(1.2f);

        foreach (char letter in sentences[index])
        {
            uiSentence.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        playerController.enabled = true;

    }

    public void OpenDialog(int index)
    {
        playerController.enabled = false;
        playerUI.SetActive(false);
        animator.SetTrigger("hasEnteredTheTrigger");
        animator.SetBool("isOpen", true);

        StartCoroutine(TypeSentence(index));
    }
}
