using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;
    public Image portraitImage;
    public GameObject talkPanel;
    public GameObject scanObject;
    public DialogueManager dialogueManager;

    public bool isAction;
    public int dialIndex;

    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = dialogueManager.GetDialogue(id, dialIndex);
        if (talkData == null)
        {
            isAction = false;
            dialIndex = 0;
            return;
        }
        if (isNpc)
        {
            string[] talkParts = talkData.Split(":");
            talkText.text = talkParts[0];

            portraitImage.sprite = dialogueManager.GetPortrait(id, int.Parse(talkParts[1]));
            portraitImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImage.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        dialIndex++;
    }
}
