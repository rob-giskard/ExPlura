using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    public static InteractionPromptUI Instance;

    public TextMeshProUGUI TooltipText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Hide();
    }

    public void Show(string text)
    {
        TooltipText.text = text;
        TooltipText.enabled = true;
    }

    public void Hide()
    {
        TooltipText.enabled = false;
    }
}
