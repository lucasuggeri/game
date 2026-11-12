using TMPro;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour
{
    [SerializeField] private GameObject textObject;

 
    public void HidePrompt()
    {
        textObject.SetActive(false);
    }

    public void ShowPrompt()
    {
        textObject.SetActive(true);
    }
}
