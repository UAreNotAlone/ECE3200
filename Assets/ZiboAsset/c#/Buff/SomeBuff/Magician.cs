using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : MonoBehaviour,IBuff

{
    public Character character
    {
        set { _character = value; }
        get { return _character; }
    }

    public Character _character = new Character();//ʹ��buffʱ����ʵ����buff���ٴ���setCharacter,�������buff����
    [Header("�ֶ����ò���")]
    public float initialIncreaseShield = 0;
    public BattleGameMaster battleGameMaster;
    public void Start()
    {
        battleGameMaster = GameObject.Find("BattleGameMaster").GetComponent<BattleGameMaster>();
    }
    //
    // Start is called before the first frame update

    public void BuffEffect()
    {
        character.shield += initialIncreaseShield + battleGameMaster.currentTurn;
    }
}
