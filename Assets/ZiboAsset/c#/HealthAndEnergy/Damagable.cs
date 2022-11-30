using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Damagable : MonoBehaviour, IDamagable
{

    public Transform can;
    public GameObject healthText;
    public BattleGameMaster gameMaster;

    public float _health = 3f;
    public GameObject playHealthBar;
    public GameObject enemyHealthBar;

    public GameObject thisHealthBar;
    public Transform Bar;

    void Start()
    {
        gameMaster = GameObject.Find("BattleGameMaster").GetComponent<BattleGameMaster>();
        can = GameObject.Find("Canvas").transform;
        healthText = Resources.Load<GameObject>("Text");
        playHealthBar = Resources.Load<GameObject>("PlayerHealthBar");
        enemyHealthBar = Resources.Load<GameObject>("EnemyHealthBar");
        
        if(gameObject.tag == "Player")
        {
            Transform trans = GameObject.Find("PlayerHealthBarTransform").transform;
            thisHealthBar = (GameObject)Instantiate(playHealthBar, trans.position, trans.rotation);
            thisHealthBar.transform.SetParent(trans);
            _health = this.gameObject.GetComponent<Character>().maxHealth;
            Bar = thisHealthBar.transform.GetChild(1);

        }
        else if(gameObject.tag == "Enemy")
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            thisHealthBar = (GameObject)Instantiate(enemyHealthBar,
                enemy.transform.position + new Vector3(-0.5f*enemy.enemySize.x, enemy.enemySize.y + 1, 0), transform.rotation);//
            thisHealthBar.transform.SetParent(enemy.transform);
            _health = this.gameObject.GetComponent<Character>().maxHealth;
            Bar = thisHealthBar.transform.GetChild(1);
        }
        

    }
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            if(value <= 0)
            {
                _health = 0;
            }
            else
            {
                _health = value;
            };//ע��value�Ǹ�ֵ�����ұߵĽ��
            //Debug.Log(_health);
            if (_health > 0)
            {
                Hurt();//Hurt������
            }
            if (_health <= 0)
            {
                Death();
            }
        }
    }

    public void Death()//������Ҫ����Enemy��player��death����
    {
        if(gameObject.tag == "Player") 
        {
            GetComponent<Player>().Death();
        }
        else if(gameObject.tag == "Enemy")
        {
            gameMaster.enemies.Remove(this.gameObject.GetComponent<Enemy>());
            GetComponent<Enemy>().DeathEvent();//�ȵ��ö������ڶ�������ʱʹ�ö����¼�
        }
        //��Ϊ����Ѫ��ʱ��
        //Destroy(this.gameObject);
    }

    public void Hurt()
    {
        

    }

   
    public void OnHit(float damage)
    {
        
        RectTransform rec = Instantiate(healthText).GetComponent<RectTransform>();
        rec.position = Camera.main.WorldToScreenPoint(transform.position);
        rec.SetParent(can);

        //Debug.Log(transform.position);
        rec.gameObject.GetComponent<Text>().text.text = damage.ToString();
        //Debug.Log(rec.gameObject.name);
        rec.SetParent(can);
        Health -= damage;
        //Debug.Log(damage);
        Bar.localScale = new Vector3(Health/gameObject.GetComponent<Character>().maxHealth,
            Bar.localScale.y, Bar.localScale.z);    //Ѫ������
    }

    #region Callback
    // Start is called before the first frame update
   
    #endregion
    
    // Update is called once per frame

}

