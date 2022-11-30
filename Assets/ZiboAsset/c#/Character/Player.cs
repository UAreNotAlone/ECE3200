using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character//�ڸ�����ɫ����ʵ�ֶ����ĵ����Լ���ִ�����������ж�player�Ƿ��ڶ���״̬����ʵ��player��
{
    // Start is called before the first frame update
    public bool IsPlayerFinishAnimation = true;
    public GameMasterPlayerTurn MasterplayerTurn;
    public Animator animator;
    //Ϊ��ʵ��ǿ��Ч��
    
    
    void Start()//���Ҫ��ĳ������ְҵ�̳�player����Ļ���ע���start��update�������ȥ
    {
        currentEnergy = MaxEnergy;
        animator = GetComponent<Animator>();
        MasterplayerTurn = GameObject.Find("PlayerTurn").GetComponent<GameMasterPlayerTurn>();
        gameObject.tag = "Player";
        AddDamageableScript(); 
    }
    public void BeginToMove()
    {
        animator.SetBool("bool_IsPlayerMove", true);
        IsPlayerFinishAnimation = false;
    }

    public void EndMove()
    {
        animator.SetBool("bool_IsPlayerMove", false);
        IsPlayerFinishAnimation = true;
    }
    public void Attack(Enemy enemy, float damage)//damage�ɿ��ƾ���
    {
        if (enemy.isReflectHarmOnce)
        {
            Attacked(enemy.reflectProportion*damage);//������˷��ˣ����ڹ���ʱ����Լ�����˺�
            enemy.isReflectHarmOnce = false;
        }
        float Realdamage = damage;
        enemy.Attacked(Realdamage);
        //To do, generate a formula for damage and real harm
        //To do, finish the cause damage by a card parameter, then use this function in Attack card
        IsPlayerFinishAnimation = false;
        animator.SetTrigger("Attack");
    }
    public void Attacked(float realDamage)
    {
        if (isCannotHurtOnce)
        {
            realDamage = 0;
            isCannotHurtOnce = false;
        }
        Damagable playerDamagable = gameObject.GetComponent<Damagable>();
        if (playerDamagable == null)
        {
            Debug.Log(gameObject.name + "enemyDamageable does not exist");
        }
        playerDamagable.OnHit(realDamage);//�˺�����
        //To do,finish the damage part when deal with the enemy AI(let enmey use this function by a int parameter  or a Enemy parameter)
        IsPlayerFinishAnimation = false;//important in  change the state of the whole battle
        animator.SetTrigger("Attacked");
    }
    public bool TryConsumeEnergy(int energy)
    {
        if(energy > currentEnergy)
        {
            Debug.Log("Player do not have enough energy");
            return false;
        }
        else
        {
            currentEnergy -= energy;
            return true;
        }
    }
    public void RecoverEnergy()
    {
        currentEnergy += RecoverEnergyATurn;
        if(currentEnergy > MaxEnergy)
        {
            currentEnergy = MaxEnergy;
        }
       //����һ�����⣬���һ�ֿ��ƣ�ʹ������ܲ���ת��Ѫ����������Ϊʹ�ÿ��Ƶ�������

    }
    public void Death()
    {
        IsPlayerFinishAnimation = false;
        Destroy(gameObject);
    }
    public void PlayerAnimationFinished()
    {
        Debug.Log("player finished animation");
        IsPlayerFinishAnimation = true; 
    }


    
    // Update is called once per frame
    void Update()
    {
        
    }
}
