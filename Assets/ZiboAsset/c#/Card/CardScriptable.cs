using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="����",menuName ="����")]
public class CardScriptable : ScriptableObject
{
    // Start is called before the first frame update
    public string cardName;
    public CardClass cardClass;

    public IEffect l;
    
}
