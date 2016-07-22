using UnityEngine;
using System.Collections.Generic;

public class ItemAttributeList : ScriptableObject
{
    [SerializeField]
    public List<ItemAttribute> itemAttributeList = new List<ItemAttribute>();

}
