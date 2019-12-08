using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI uiSentence;
    public GameObject boundedArchitect;
    public ParticleSystem disappearArchitectParticleEffect;
    public GameObject playerUI;
    public PlayerController playerController;
    public Button skipButton;
    public float typingSpeed;
    [TextArea(3,10)]
    public string sentence;

    public Animator animator;

    IEnumerator TypeSentence()
    {
        yield return new WaitForSeconds(1.2f);

        foreach (char letter in sentence)
        {
            uiSentence.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        playerController.canMove = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenDialog();

        }
    }

    private void OpenDialog()
    {
        skipButton.onClick.AddListener(SkipDialog);
        playerController.canMove = false;
        playerController.canAttack = false;
        playerController.canCastSpells = false;
        playerUI.SetActive(false);
        animator.SetTrigger("hasEnteredTheTrigger");
        animator.SetBool("isOpen", true);

        StartCoroutine(TypeSentence());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SkipDialog();
        }
    }

    public void SkipDialog()
    {
        animator.SetBool("isOpen", false);
        uiSentence.text = " ";
        Instantiate(disappearArchitectParticleEffect, boundedArchitect.transform.position, Quaternion.identity);
        Destroy(boundedArchitect);
        Destroy(gameObject);       
        playerUI.SetActive(true);
        skipButton.onClick.RemoveListener(SkipDialog);
        playerController.canMove = true;
        playerController.canAttack = true;
        playerController.canCastSpells = true;
    }
}
