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
        dialData.Add(1000, new string[] { "안녕하세요.:0", "여긴 참 평화롭죠?:1" });
        dialData.Add(2000, new string[] { "안녕.:0", "이 호수 어때? 꽤나 아름답지?:1" });
        dialData.Add(100, new string[] { "나무 상자다." });
        dialData.Add(200, new string[] { "나무 책상이다." });

        dialData.Add(10 + 1000, new string[] { "어서와. :0",
                                               "이 마을에 놀라운 전설이 있다는데 알고 있었니? :0",
                                               "오른쪽 호수의 루도가 알려줄 거야 :2" });
        dialData.Add(11 + 2000, new string[] { "안녕. :0",
                                               "호수의 전설을 들으러 온거야? :0",
                                               "그럼 나를 좀 도와줬으면 좋겠는데.. :2",
                                               "내 지갑좀 찾아줄래?\n어디 나무 상자나 책상 밑에 떨어뜨린거 같아 :2"});
        dialData.Add(20 + 500, new string[] { "나무 상자다.", "...", "잘 보니 옆에 지갑 하나가 떨어져 있다.", 
                                              "루도의 지갑을 주웠다." });
        dialData.Add(21 + 2000, new string[] { "엇, 내 지갑을 찾아줬구나. :1", "고마워. :1" });
        

        // Portrait Data
        AddPortraitData(1000, 4, 0);
        AddPortraitData(2000, 4, 4);
    }

    public string GetDialogue(int id, int index)
    {
        if (!dialData.ContainsKey(id))
        {
            if (!dialData.ContainsKey(id-id%10))
            {
                return GetDialogue(id - id % 100, index);
            }
            else 
            {
                return GetDialogue(id - id % 10, index);
            }
        }

        return (index == dialData[id].Length) ? null : dialData[id][index];
    }

    public Sprite GetPortrait(int id, int index)
    {
        return portraitData[id + index];
    }

    void AddPortraitData(int id, int numPortrait, int startIndex)
    {
        for (int i = 0; i < numPortrait; i++)
        {
            portraitData.Add(id + i, portrats[startIndex+i]);
        }
    }
}
