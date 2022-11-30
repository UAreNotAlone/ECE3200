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

    public int enemyAnimationNumber = 0;
    public Animator enemyAnimator;

    public enum EnemyStage
    { 
        MouseAround,
        notMouseAround,
        ShowAttackPart,
        attacked,
        attack
    }
    public EnemyStage enemyStage = EnemyStage.notMouseAround;

    [Header("��Ҫ���õı���")]
    public Vector2 enemySize = new Vector2(7,6);//
    public void AttackedWhichPart()
    {
        
        
        enemyStage = Enemy.EnemyStage.ShowAttackPart;
    }
    public void Attacked(float realDamage)
    {
        if (isCannotHurtOnce)
        {
            realDamage = 0;
            isCannotHurtOnce = false;
        }
        Damagable enemyDamagable = gameObject.GetComponent<Damagable>();
        if (enemyDamagable == null)
        {
            Debug.Log(gameObject.name + "enemyDamageable does not exist");
        }
        enemyDamagable.OnHit(realDamage);//�˺�����
        enemyStage = EnemyStage.attacked;//�����������
        enemyAnimationNumber += 1;
        enemyAnimator.SetTrigger("Attacked");
        Debug.Log("enemy is under attacked");//��������
    }
    // Start is called before the first frame update
    public void Attack(Player player, float damage)//damage�ɵ���AIѡ��Ĺ�����ʽ����//��attack��attacked��������˺�������ж�
    {
        if (player.isReflectHarmOnce)
        {
            Attacked(player.reflectProportion*damage);
            player.isReflectHarmOnce = false;
        }

        enemyStage = EnemyStage.attack;
        float realDamage = damage;
        player.Attacked(realDamage);
        
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
    public void Start()
    {
        enemyAnimationNumber = 0;
        gameObject.tag = "Enemy";
        LiveEnemy.Add(GetComponent<Enemy>());
        AddDamageableScript();
        enemyAnimator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            enemyStage = EnemyStage.notMouseAround;
        }
        
    }


}
