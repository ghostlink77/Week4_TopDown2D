using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    string targetMsg;
    TextMeshProUGUI msgText;
    AudioSource audioSource;
    int index;
    float interval;

    public bool inAnim;
    public int CharPerSec;
    public GameObject cursor;


    void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (inAnim)
        {
            CancelInvoke();
            msgText.text = targetMsg;
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
        
    }

    void EffectStart()
    {

        msgText.text = "";
        index = 0;
        inAnim = true;
        cursor.SetActive(false);

        interval = 1.0f / CharPerSec;
        Invoke("Effecting", interval);
    }
    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        if (targetMsg[index] != ' ' || msgText.text != ".")
        {
            audioSource.Play();
        }
        index++;

        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        inAnim = false;
        cursor.SetActive(true);
    }
}
