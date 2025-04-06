using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;
    public GameObject talkPanel;
    public GameObject scanObject;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = $"이것의 이름은 {scanObj.name} 이라고 한다."; 
        }
        talkPanel.SetActive(isAction);
    }
}
