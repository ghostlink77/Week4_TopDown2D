using UnityEngine;

public class QuestData
{
    public string questName;
    public int[] npcIds;

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcIds = npc;
    }
}
