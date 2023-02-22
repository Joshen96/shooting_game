using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //���� ��ġ �����ǿ� ������ �� ������Ʈ
    public Transform[] spawnPoints;
    public GameObject[] EnemyPrefabs;

   

    // �� ���������� ���� ��ӻ�������
    public float curEnemySpawnDelay;
    public float nextEnemySpawnDelay;
    
    public GameObject player;

    public bool playerdie = false;

    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverset;

    bool isReplay = false;

    // Start is called before the first frame update
    public static GameManager gamemanager = null;
    private void Awake()
    {
        if (gamemanager == null)
        {
            gamemanager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isReplay)
           curEnemySpawnDelay += Time.deltaTime;
        if (curEnemySpawnDelay > nextEnemySpawnDelay) //�����ð��Ǹ� ����
        {
            SpawnEnemy();


            nextEnemySpawnDelay = Random.Range(1f, 2.0f); //��� �������� �����̼�������
            curEnemySpawnDelay = 0f;
                        
        }

        Player playLogic = player.GetComponent<Player>();
        scoreText.text=string.Format("{0:n0}",playLogic.score); // 000,000���� fotmat���
    }  
    void SpawnEnemy()
    {
        int ranType = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 7);
        GameObject goEnemy = Instantiate(EnemyPrefabs[ranType], spawnPoints[ranPoint].position, Quaternion.identity);
        Enemy enemyLogic = goEnemy.GetComponent<Enemy>();
        enemyLogic.playerObj = player;
        enemyLogic.Move(ranPoint);
    }

    

    public void GameOver()
    {
        gameOverset.SetActive(true); ;//���ӿ��� UI Ȱ��ȭ
    }
    public void RespawnPlayer() //���ȯ
    {
        
        Invoke("AilvePlayer", 1.0f);
        
    }
    
    void AilvePlayer()  //��Ȱ�ϸ鼭 �÷��̾� �����ð�
    {
        player.transform.position = Vector3.down * 4.2f; //���ӿ�����Ʈ��ġ -y 4.2��ġ�� �÷��̾� ��ȯ
        player.SetActive(true);
        Player playrLogic = player.GetComponent<Player>();
        //playrLogic.Respawning = true;
        playrLogic.power = 1;
        playrLogic.isHit = false;
        
        player.GetComponent<PolygonCollider2D>().enabled = false;
        Invoke("DamageEnd", 2f); //�����ð� 2�� �� �÷��̾��� �������� �ݶ��̴� ��������
    }
    void DamageEnd()
    {
        playerdie = false;
        Player playrLogic = player.GetComponent<Player>();
        playrLogic.Respawning = false;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PolygonCollider2D>().enabled = true;
    }
    public void Playerdie_delebullet()// �̶� �ֳʹ� �Ѿ� ����
    {
        playerdie = true;
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach(GameObject temp in temps)
        {
            Destroy(temp);
        }
        
    }
    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < lifeImage.Length; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }

    }
    public void Replay()
    {
        /*
        player.gameObject.SetActive(true);
        isReplay = true;
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject temp in temps)
        {
            Destroy(temp);
        }
        */
        isReplay = true;
        GameObject[] temps;
        temps = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject temp in temps)
        {
            Destroy(temp);
        }
        RespawnPlayer();

        Player playLogic = player.GetComponent<Player>();
        playLogic.life = 3;
        UpdateLifeIcon(playLogic.life);
        playLogic.score = 0;
        gameOverset.SetActive(false);
        Invoke(nameof(DelayEnemySpawn), 1f);

    }
    
    void DelayEnemySpawn()
    {
        isReplay = false;
    }
}
