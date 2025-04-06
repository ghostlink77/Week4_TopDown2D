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
            talkText.text = $"�̰��� �̸��� {scanObj.name} �̶�� �Ѵ�."; 
        }
        talkPanel.SetActive(isAction);
    }
}
