using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BattleGameMaster : MonoBehaviour
{


    [SerializeField]
    public static bool IsPlayerTurn = true;
    public GameMasterPlayerTurn playerTurnController;
    public bool Attackacted = false;

    public Player player;
    public List<Enemy> enemies;

    public int currentTurn = 1; //ע�⣬һ���غϵĸ��������һغϺ��������һغ�֮��ĵ��˻غ�
    [Header("�������")]
    
    public Player[] playerPrefabs;
    public Transform playerTransform;

    public List<Enemy> EnemyPrefabs;
    public List<Transform> enemyTransforms;

    public List<CardClip> cardClips;//���ɿ��Ƶĵط�
    public int numberOfCardATurn;
    public List<CardClass> playerTotalCard;

    // Start is called before the first frame update
    void Awake()
    {
        currentTurn = 0;
        InitializeCharacter();
        //��ʼ��//ʵ����playerTotalCard�ĳ�ʼ������ͨ����ȡresources�ڵ�prefabsִ��
        //��������ÿ����ټ��س���ʱͬʱ���У��Ӷ�ʵ���ò�ͬ�Ŀ������ս������
        Attackacted = true;
        IsPlayerTurn = true;
        playerTurnController = GameObject.Find("PlayerTurn").GetComponent<GameMasterPlayerTurn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerTurn == true)
        {//ע�Ᵽ֤��ʼ�������Ҫ��ִ�У��������ԣ�
            if(Attackacted == true) //�������ֻ�ڻغϽ׶ε�ת����ִ�к���
            { 

                if (IsInvoking(nameof(IsPerformingAnimation)))
                {
                CancelInvoke(nameof(IsPerformingAnimation));
                }
                currentTurn++;//�����������һ�غ�
                Debug.Log("currentTurn" + currentTurn);
                playerTurnController.enabled = true;
                Attackacted = false;
            }
        }
        else
        {
            if (Attackacted == false)
            {
                
                playerTurnController.GetComponent<GameMasterPlayerTurn>().enabled = false;
                Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
                foreach (Enemy enemy in enemies)
                {
                    enemy.AttackAI();
                }
                InvokeRepeating(nameof(IsPerformingAnimation), 0.1f, 0.5f);
                Attackacted = true;
            }
        }

        CheckWin();
    }
    public void InitializeCharacter()
    {
        for (int i = 0; i < enemyTransforms.Count; i++)
        {

            enemies.Add(Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)], enemyTransforms[i].position, enemyTransforms[i].rotation));
            GameObject empty = new GameObject(); empty.AddComponent<InistantiateCahracter>();
            empty.GetComponent<InistantiateCahracter>().Init(enemies[i].gameObject, enemyTransforms[i]);
            enemies[i].transform.SetParent(enemyTransforms[i]);
            //Debug.Log("real enmey"+enemies[i].transform.position);

        }
        player = Instantiate(playerPrefabs[0], playerTransform.position, playerTransform.rotation).GetComponent<Player>();
        player.transform.SetParent(playerTransform);
        GameObject empty2 = new GameObject(); empty2.AddComponent<InistantiateCahracter>();
        empty2.GetComponent<InistantiateCahracter>().Init(player.gameObject, playerTransform);//initial position
    }
   
    public void CheckWin()
    {
        if (enemies.Count == 0)
        {
            Win();
        }
    }
    public void Win()
    {
        SceneManager.LoadScene("win");
    }
    public void EndPlayerTurn()
    {
        if(playerTurnController.stage == GameMasterPlayerTurn.Stage.NoChooseCard ||
            playerTurnController.stage == GameMasterPlayerTurn.Stage.ChosenCard)
        {
            IsPlayerTurn = false;
        }
        Debug.Log("click");
    }
    public void IsPerformingAnimation()
    {
        Debug.Log("Judging Animation in turn end");
        if(Enemy.AllEnemyFinishAniamtion && player.IsPlayerFinishAnimation)
        {
            if(IsPlayerTurn == false)
            {
                IsPlayerTurn = true;
            }
        }
    }

}
