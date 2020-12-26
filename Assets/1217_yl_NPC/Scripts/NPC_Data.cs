using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC/Create New NPC_Data")]
public class NPC_Data : ScriptableObject
{
    public string villagerName;
    public Color villagerColor;
    public Color villagerNameColor;
    public Dialogue_Data dialogue;
}
