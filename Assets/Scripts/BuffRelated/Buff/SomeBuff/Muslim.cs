using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muslim : MonoBehaviour,IBuff
{

    public int damage = 4;
    // Start is called before the first frame update

    public void BuffEffect()
    {
        List<Enemy> enemies = new List<Enemy>(EnemyManager.Instance._enemyList);
        foreach(Enemy enemy in enemies)
        {
            //�����ʵ�˺���������playerȥ�����������ˣ���ʵ�˺����񲻻ᱻ������
            enemy.OnDamaged(damage);

        }
    }
}
