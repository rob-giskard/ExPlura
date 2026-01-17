using UnityEngine;
using TMPro;

public class UIInteractWith : MonoBehaviour
{
    public GameObject promptUI;
    public GameObject interactible;
    private bool playerInRange = false;
    


    void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        if (interactible == null)
        {
            Debug.Log("Gonna destroy the text...");
            Destroy(promptUI);
        }
    }

    public void DestroyText()
    {
        if (promptUI != null)
        {
            Destroy(promptUI);
            promptUI = null;
        }
    }

    void Interact()
    {
        Debug.Log("Player interacted!");
        DestroyText();

    }
}
