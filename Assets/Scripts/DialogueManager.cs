using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    Dictionary<int, string[]> dialData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portrats;
    void Awake()
    {
        dialData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        dialData.Add(1000, new string[] { "�ȳ��ϼ���.:0", "���� �� ��ȭ����?:1" });
        dialData.Add(100, new string[] { "���� ���ڴ�." });
        dialData.Add(200, new string[] { "���� å���̴�." });

        for(int i = 0; i < portrats.Length; i++)
        {
            portraitData.Add(1000 + i, portrats[i]);
        }
    }

    public string GetDialogue(int id, int index)
    {
        return (index == dialData[id].Length) ? null : dialData[id][index];
    }

    public Sprite GetPortrait(int id, int index)
    {
        return portraitData[id + index];
    }
}
