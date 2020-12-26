using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue Data")]
public class Dialogue_Data : ScriptableObject
{
    [TextArea(10, 10)]
    public List<string> conversationBlock;
}
