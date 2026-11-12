//using System.Collections;
//using TMPro;
//using UnityEngine;

//[RequireComponent(typeof(TMP_Text))]
//public class TextController : MonoBehaviour
//{
//    private TMP_Text text;
//    [SerializeField] private TextObject[] languages;
//    private TranslateSystem translateSystem;

//    [SerializeField] private bool UsesTextObject = true;

//    [SerializeField] private string RegularTextBox;
//    private void Awake()
//    {
//        translateSystem = GameManager.instance.TranslateSystem;


//        if (translateSystem == null)
//            Debug.LogError("TranslateSystem NÃO encontrado!");
//        text = GetComponent<TMP_Text>();
//        UpdateText((Language)PlayerPrefs.GetInt("language", 0));
//    }

//    private void OnEnable()
//    {
//       StartCoroutine(UpdateTextOnEnable());
//    }

//    private IEnumerator UpdateTextOnEnable()
//    {
//        yield return null;
//       if( UsesTextObject)
//        {
//            translateSystem.OnSwitchLanguage += UpdateText;

//            UpdateText(translateSystem.Language);
//        }
//       else if(!UsesTextObject)
//        {
//            text.text = GameManager.instance.DynamicControlIcons.ProcessMessage(RegularTextBox);
//        }
//    }
//    private void OnDisable() => translateSystem.OnSwitchLanguage -= UpdateText;
    
//    void UpdateText(Language language)
//    {
//        foreach (TextObject textObject in languages)
//        {
//            if (textObject.language == language)
//            {
//                text.text = textObject.text;
//                break;
//            }
//        }
//    }
//}
