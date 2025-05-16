using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TypeEffect talk;
    public Image portraitImage;
    public Animator portraitAnim;
    public Animator talkPanel;
    public GameObject scanObject;
    public GameObject menuSet;
    public GameObject player;
    public TextMeshProUGUI questText;
    public DialogueManager dialogueManager;
    public QuestManager questManager;
    public Sprite prevPortrait;

    public bool isAction;
    public int dialIndex;


    private void Start()
    {
        GameLoad();
        Debug.Log(questManager.CheckQuest());
        questText.text = questManager.CheckQuest();
    }
    private void Update()
    {
        // sub Menu
        if (Input.GetButtonDown("Cancel"))
        {
            menuSet.SetActive(!menuSet.activeSelf);
        }
    }
    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;

        string talkData = "";

        if (talk.inAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestDialogIndex(id);
            talkData = dialogueManager.GetDialogue(id + questTalkIndex, dialIndex);
        }

        if (talkData == null)
        {
            isAction = false;
            dialIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            questText.text = questManager.CheckQuest(id);
            return;
        }
        if (isNpc)
        {
            string[] talkParts = talkData.Split(":");
            talk.SetMsg(talkParts[0]);

            portraitImage.sprite = dialogueManager.GetPortrait(id, int.Parse(talkParts[1]));
            portraitImage.color = new Color(1, 1, 1, 1);
            if(prevPortrait != portraitImage.sprite)
            {
                prevPortrait = portraitImage.sprite;
                portraitAnim.SetTrigger("doEffect");
            }
        }
        else
        {
            talk.SetMsg(talkData);
            portraitImage.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        dialIndex++;
    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestID", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerPosX"))
        {
            return;
        }

        float x = PlayerPrefs.GetFloat("PlayerPosX");
        float y = PlayerPrefs.GetFloat("PlayerPosY");
        int questId = PlayerPrefs.GetInt("QuestID");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }
    public void GaemExit()
    {
        Application.Quit();
    }
}
