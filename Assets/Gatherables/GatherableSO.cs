using UnityEngine;

public enum ResourceType
{
    Food,
    Wood,
    Stone,
    Gold
}

[CreateAssetMenu(menuName = "Resource/Gatherable")]
public class GatherableSO : ScriptableObject
{
    public ResourceType type;
    public int quantity;
    public int amountPerGather;
}