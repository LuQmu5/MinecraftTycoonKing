using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StaticData/Tools", fileName = "Tool Data", order = 54)]
public class ToolData : ScriptableObject
{
    [field: SerializeField] public string Label { get; private set; }
    [field: SerializeField] public ToolTypes Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
