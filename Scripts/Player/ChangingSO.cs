using UnityEngine;

using UnityEditor;
[System.Serializable]
public enum CharackterType
{
     Burun,
     Goz,
     Agiz,
     Kulak,
     Kas,
     Dudak,
     Cene

}

[System.Serializable]
public enum IndexMove
{
     Left,
     Right,
     Top,
     Down
}

[CreateAssetMenu(fileName = "ChangingSO", menuName = "Golden Ratio 3D/ChangingSO", order = 0)]
public class ChangingSO : ScriptableObject
{
     public CharackterSO so;
     public Levels[] levels;
}




[System.Serializable]

public class Levels
{

     public string indexName;
     public int indexLevel;
     public Elements[] elements;
}


[System.Serializable]
public class Elements
{
     public Element[] elements;
}

[System.Serializable]
public class Element
{
     public string name;
     public CharackterType charackterType;
     public IndexMove indexMoveType;
     public IntractableType intractableType = IntractableType.UNDEFINED;
     [Range(0, 100)]
     public float value;
}