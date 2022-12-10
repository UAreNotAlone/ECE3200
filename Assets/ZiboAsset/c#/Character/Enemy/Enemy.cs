using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    public static bool AllEnemyFinishAniamtion
    {
        set { }
        get {
            foreach (Enemy enemy in LiveEnemy)
            {
                if (enemy.enemyFinishAnimation == false)
                {
                    return false;
                }
            }
            return true;
        }
     //����һ�����Ե�������ʱִ�к���
    }
    [Header("�Զ����ñ���")]
    public static List<Enemy> LiveEnemy = new List<Enemy>();

    public bool enemyFinishAnimation
    {
        get { 
            if(enemyAnimationNumber <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        ; }
    }
    [Header("�Զ����ñ���")]
    public int enemyAnimationNumber = 0;
    public Animator enemyAnimator;

    public enum EnemyStage
    { 
        MouseAround,
        notMouseAround,
        ShowAttackPart,
        attackedPartChosen,
        attacked,
        attack
    }
    public EnemyStage enemyStage = EnemyStage.notMouseAround;

    public bool ActiveCyberborg = false;
    public Cyberborg chosenCyberBorg;
    public List<Transform> listCy = new List<Transform>();
    
    public Vector2 enemySize;//
    
    public void Start()
    {
        enemyAnimationNumber = 0;
        gameObject.tag = "Enemy";
        LiveEnemy.Add(GetComponent<Enemy>());
        AddDamageableScript();
        enemyAnimator = GetComponent<Animator>();
        enemySize = characterSize;
        //ע����˵����������ɿ���������
        base.Start();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            enemyStage = EnemyStage.notMouseAround;//��ȡ������ѡ��ʱ�ǵð����enemy��״̬��������
        }
        if(enemyStage == EnemyStage.ShowAttackPart)//�ڱ������׶�չʾ���壬����ر�����
        {
            if (!ActiveCyberborg)
            {
                foreach (Cyberborg.CyberborgType cyType in cyberborgs.Keys)
                {
                    if (cyberborgs[cyType] != null)
                    {
                        foreach (Transform trans in cyberborgs[cyType].transform.GetComponentsInChildren<Transform>())
                        {
                            trans.gameObject.layer = 0;
                        }
                    }
                }
                ActiveCyberborg = true;
            }
            CheckChoosingAttackPart();

        }
        else
        {
            if (ActiveCyberborg)
            {
                foreach (Cyberborg.CyberborgType cyType in cyberborgs.Keys)
                {
                    if (cyberborgs[cyType] != null)
                    {
                        foreach (Transform trans in cyberborgs[cyType].transform.GetComponentsInChildren<Transform>())
                        {
                            trans.gameObject.layer = 7;
                        }
                    }
                }
                ActiveCyberborg = false;
            }
        }
        

    }
    public void AttackedWhichPart()
    {
        enemyStage = Enemy.EnemyStage.ShowAttackPart;
    }
    public bool isClosedToCyberCriterion(Vector3 v1,Vector3 v2)
    {

        if (Vector2.Distance(v1, v2) < 10)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
    public void CheckChoosingAttackPart()//ʵ������һ����Ҳ������Cyberborg����ɣ�
    {
        //��ȡ�ǿ�����\
        listCy = new List<Transform>();
        foreach (Cyberborg.CyberborgType cyType in cyberborgs.Keys)
        {
            if (cyberborgs[cyType]!= null)
            {
                listCy.Add(cyberborgs[cyType].transform);
            }
        }
        if(listCy.Count == 0)
        {
            Debug.Log("an enemy live without cyberborg");
            enemyStage = EnemyStage.attackedPartChosen;//û������ֱ�ӽ�����һ�׶�
        }

        Transform CyberborgAroundMouse = IsMouseOnTransform.IsMouseAroundSomeTransform(listCy, isClosedToCyberCriterion);
        if (CyberborgAroundMouse != null && Input.GetMouseButton(0)&& enemyStage == EnemyStage.ShowAttackPart)
        {
            chosenCyberBorg = CyberborgAroundMouse.GetComponent<Cyberborg>();//ѡ������
            //������һ�׶�
            enemyStage = EnemyStage.attackedPartChosen;

        }
    }
    public void Attacked(float realDamage,Cyberborg cyberborg)
    {
        //Debug.Log(realDamage);
        realDamage *= (1 - defenceAbility);
        if (isCannotHurtOnce)
        {
            realDamage = 0;
            isCannotHurtOnce = false;
        }

        if (cyberborg == null)
        {
            Damagable enemyDamagable = gameObject.GetComponent<Damagable>();
            if (enemyDamagable == null)
            {
                Debug.Log(gameObject.name + "enemyDamageable does not exist");
            }
           // Debug.Log(realDamage);
            enemyDamagable.OnHit(realDamage);//�˺�����
        }
        else
        {
            realDamage *= (1 - cyberborg.defense);
            //Debug.Log(realDamage);
            cyberborg.OnHit(realDamage);//�ܵ�cyber defenceӰ��
        }
        enemyStage = EnemyStage.attacked;//�����������
        enemyAnimationNumber += 1;
        enemyAnimator.SetTrigger("Attacked");
        Debug.Log("enemy is under attacked");//��������
    }
    // Start is called before the first frame update
    public void Attack(Player player, float originDamage,Cyberborg attackedCyberborg,Cyberborg usedCyberborg)//damage�ɵ���AIѡ��Ĺ�����ʽ����//��attack��attacked��������˺�������ж�
    {
        float damage = attackForce + originDamage;
        if (usedCyberborg != null)
        {
            damage += usedCyberborg.attack;
        }//�������˺�

        if (damage > player.shield)
        {
            damage -= player.shield;
            player.shield = 0;
        }
        else
        {
            player.shield -= damage;
            damage = 0;
        }//���㻤�ܵ����˺�
        if (player.isReflectHarmOnce)
        {
            Attacked(player.reflectProportion*damage, usedCyberborg);
            player.isReflectHarmOnce = false;
        }

        enemyStage = EnemyStage.attack;
        float realDamage = damage;
        player.Attacked(realDamage, attackedCyberborg);
        
        enemyAnimator.SetTrigger("Attack");//
    }
    public void AttackAI()
    {
        //д���˹���AI
        SendMessage("AttackAction");
    }
    
    public void BeginToMove()
    {
        enemyAnimator.SetBool("Move", true);
        enemyAnimationNumber += 1;
    }

    public void EndMove()
    {
        enemyAnimator.SetBool("Move", false);//ԭ�Ȱ�player���߼������û�иĵ����
        enemyAnimationNumber -= 1;
    }
    public void EndAttack()
    {
        enemyStage = EnemyStage.notMouseAround;
    }
   public void EndAttacked()
    {
        enemyStage = EnemyStage.notMouseAround;
    }
    
    public void DeathEvent()
    {
        
        enemyAnimator.SetTrigger("Death");//��Death���������destroy event;�Լ�finishThisAnimation
        LiveEnemy.Remove(this.GetComponent<Enemy>());
        
    }
    public void DestroyThisEnemy()
    {
        Debug.Log("enemy is destroyed");
        Destroy(gameObject);
    }
    public void FinishThisAnimation()
    {
        Debug.Log("finish animation");
        enemyAnimationNumber -= 1;
    }
    


}
