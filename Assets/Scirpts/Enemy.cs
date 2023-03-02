using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public float health;
    public int enemyscore;

    GameObject objectManagerObj;
    ObjectManager objectManager;
    

    public float curBulletDelay = 0f;
    public float maxBulletDelay = 1f;
    public GameObject[] bulletPrefabs;
    
    public string[] bulletString = {"BulletEnemyA","BulletEnemyB"};
    public Sprite[] sprites;

    public GameObject playerObj;
    public GameObject[] items;

    public string[] itemstring = { "itemCoin", "itemPower","itemBoom" };

    
    SpriteRenderer spriteRender;
   public bool isEnemyDie;

    Animator anim;

    Rigidbody2D rd;

    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        
        playerObj = GetComponent<GameObject>();
        
        spriteRender = GetComponent<SpriteRenderer>();

        if(enemyName == "Boss")
        {
            anim = GetComponent<Animator>();
        }
        objectManagerObj = GameObject.Find("ObjectManager");
        objectManager = objectManagerObj.GetComponent<ObjectManager>();



        
    }
    void OnEnable()
    {
        switch (enemyName)
        {
            case "L":
                health = 10;
                break;
            case "M":
                health = 8;
                break;
            case "S":
                health = 5;
                break;

        }
        isEnemyDie = false; // ���� ����
    }
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        
        spriteRender = GetComponent<SpriteRenderer>();

        if (enemyName == "Boss")
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        


        Fire();
        ReloadBullet();
    }

    void BossPower()
    {
        for (int i = 0; i < 2; i++)
        {
           
            GameObject bulletObj = objectManager.MakeObj(bulletString[i]);
            bulletObj.transform.position = transform.position;
            bulletObj.transform.rotation = Quaternion.identity;

                                   //Instantiate(bulletPrefabs[i], transform.position, Quaternion.identity);
            Rigidbody2D bulletrd = bulletObj.GetComponent<Rigidbody2D>();  // �Ѿ��� ������ٵ�
            Vector3 dirVec = playerObj.transform.position - transform.position; //�÷��̾��- ������ ��ġ �������忡��

            bulletrd.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        }
    }

    void Power()
    {

        GameObject bulletObj = objectManager.MakeObj(bulletString[0]);
        bulletObj.transform.position = transform.position;
        bulletObj.transform.rotation = Quaternion.identity;

            //Instantiate(bulletPrefabs[0], transform.position, Quaternion.identity);
        Rigidbody2D rd = bulletObj.GetComponent<Rigidbody2D>();  // �Ѿ��� ������ٵ�
        
        Vector3 dirVec = playerObj.transform.position - transform.position; //�÷��̾��- ������ ��ġ �������忡��
        rd.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);


    }

    void ReloadBullet()
    {
    
        curBulletDelay += Time.deltaTime;

    }
    void Fire()
    {

      

        if (curBulletDelay > maxBulletDelay)
        {
            if (playerObj.GetComponent<Player>().Respawning == true)
            {
                return;
            }
            if (enemyName == "Boss")
            {
                BossPower();
            }
            else
            {
                Power();

            }
            
            
            curBulletDelay = 0f;  //�ٽ� 0�� �����������
        }


    }
    public void Move(int nPoint)
    {
        float randangz = Random.Range(0.1f, 0.5f);
        if (nPoint == 3 || nPoint == 4) //�������� ���� �迭 �ε���
        {
            rd.velocity = new Vector2(speed * (-randangz),-1);
        }
        else if (nPoint == 5 || nPoint == 6)
        {
            rd.velocity = new Vector2(speed * (randangz), -1);
        }
        else
        {
            rd.velocity = Vector2.down * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border" && enemyName !="Boss")
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);

        }

        if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            
            OnHit(bullet.power);

            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
    public void OnHit(float Bulletpower)
    {
        health -= Bulletpower;

        if (enemyName == "Boss")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRender.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);
        }


        if (health < 0 && isEnemyDie==false) //�� ����
        {
            GameManager.gamemanager.kill_Enemy++;
            GameManager.gamemanager.boss--;

            isEnemyDie = true;  //�κ�ũ ������ �ð��������� ������2��������� �÷��׷� ���� �̰ų� 
            Player playLogic = playerObj.GetComponent<Player>();
            playLogic.score += enemyscore;

            //�����۵��                                                                                                                                                                                                                              
            if (enemyName == "Boss")
            {
                Debug.Log("��������");


                GameObject item = objectManager.MakeObj(itemstring[0]);
                item.transform.position = transform.position; 
                item.transform.rotation = Quaternion.identity;
                    //Instantiate(items[0], transform.position, Quaternion.identity);
                item.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1f;
                GameObject item2 = objectManager.MakeObj(itemstring[1]);
                item2.transform.position = transform.position; item2.transform.rotation = Quaternion.identity;
                    //Instantiate(items[1], transform.position + Vector3.left, Quaternion.identity);
                item2.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1f;
                GameObject item3 = objectManager.MakeObj(itemstring[2]);
                item3.transform.position = transform.position;
                item3.transform.rotation = Quaternion.identity;
                    //Instantiate(items[2], transform.position+ Vector3.right, Quaternion.identity); //  ����
                item3.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1f;
                //Destroy(gameObject);
                gameObject.SetActive(false);


                GameManager.gamemanager.Bossdie_Gameclear();
                Time.timeScale = 0.1f;
            }


            else
            {
                int rand = Random.Range(0, 3);
                Debug.Log(rand);


                GameObject item = objectManager.MakeObj(itemstring[rand]);
                item.transform.position = transform.position;
                item.transform.rotation = Quaternion.identity;

                //Instantiate(items[rand], transform.position, Quaternion.identity);

                item.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1f;




                

            }
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }
    void ReturnSprite()
    {
        spriteRender.sprite = sprites[0];
    }
}
