using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : MonoBehaviour,IBuff

{
    

    //ʹ��buffʱ����ʵ����buff���������buff����
    [Header("�ֶ����ò���")]
    public int initialIncreaseShield = 0;
    
    
    //
    // Start is called before the first frame update

    public void BuffEffect()
    {
        AudioManager.Instance.PlayEffectByName("Effect/healspell");
        //  Enhance the Shield.
        FightManager.Instance.player_defenseValue += initialIncreaseShield + FightManager.Instance.currentTurn;
        UIManager.Instance.GetUIScript<FightUI>("FightUI").UpdateDefenseTxt();
    }
}
