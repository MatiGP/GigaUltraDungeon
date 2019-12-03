using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI uiSentence;
    public GameObject boundedArchitect;
    public ParticleSystem disappearArchitectParticleEffect;
    public GameObject playerUI;
    public PlayerController playerController;
    public float typingSpeed;
    [TextArea(3,10)]
    public string sentece;

    public Animator animator;

    IEnumerator TypeSentence()
    {
        yield return new WaitForSeconds(1.2f);

        foreach (char letter in sentece)
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
            playerController.canMove = false;
            playerUI.SetActive(false);
            animator.SetTrigger("hasEnteredTheTrigger");
            animator.SetBool("isOpen", true);

            StartCoroutine(TypeSentence());

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isOpen", false);
            uiSentence.text = "";
            Instantiate(disappearArchitectParticleEffect, boundedArchitect.transform.position, Quaternion.identity);
            Destroy(boundedArchitect);
            Destroy(gameObject);
            playerUI.SetActive(true);
        }
    }
}
