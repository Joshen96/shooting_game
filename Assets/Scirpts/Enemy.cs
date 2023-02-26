using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public float health;
    public int enemyscore;

    public float curBulletDelay = 0f;
    public float maxBulletDelay = 1f;
    public GameObject[] bulletPrefabs;

    public Sprite[] sprites;

    public GameObject playerObj;
    public GameObject[] items;
    
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
            GameObject bulletObj = Instantiate(bulletPrefabs[i], transform.position, Quaternion.identity);
            Rigidbody2D bulletrd = bulletObj.GetComponent<Rigidbody2D>();  // �Ѿ��� ������ٵ�
            Vector3 dirVec = playerObj.transform.position - transform.position; //�÷��̾��- ������ ��ġ �������忡��

            bulletrd.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        }
    }

    void Power()
    {

        GameObject bulletObj = Instantiate(bulletPrefabs[0], transform.position, Quaternion.identity);
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
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            
            OnHit(bullet.power);

            Destroy(collision.gameObject);
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

                
                GameObject item = Instantiate(items[0], transform.position, Quaternion.identity);
                item.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1f;
                GameObject item2 = Instantiate(items[1], transform.position + Vector3.left, Quaternion.identity);
                item2.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1f;
                GameObject item3 = Instantiate(items[2], transform.position+ Vector3.right, Quaternion.identity); //  ����
                item3.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1f;
                Destroy(gameObject);
                


                GameManager.gamemanager.Bossdie_Gameclear();
                Time.timeScale = 0.1f;
            }


            else
            {
                int rand = Random.Range(0, 3);
                Debug.Log(rand);


                GameObject item = Instantiate(items[rand], transform.position, Quaternion.identity);

                item.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1f;




                

            }
            Destroy(gameObject);

        }

    }
    void ReturnSprite()
    {
        spriteRender.sprite = sprites[0];
    }
}
