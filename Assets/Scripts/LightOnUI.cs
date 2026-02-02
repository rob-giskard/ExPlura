using UnityEngine;
using UnityEngine.UI;

public class LightOnUI : MonoBehaviour
{
    
    public Image bulbImage;
    public Sprite offSprite;
    public Sprite onSprite;

    public void SetState(bool isOn)
    {
        bulbImage.sprite = isOn ? onSprite : offSprite; //:D
        bulbImage.color = isOn ? Color.white : Color.black;
    }
}

