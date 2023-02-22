using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int life=3;

    public int score = 0;
    public float power = 0f; //파워에따른 불릿타입결정

    public float limitPower = 3f;
    public bool isTouchTop = false;
    public bool isTouchBottom = false;
    public bool isTouchRight = false;
    public bool isTouchLeft = false;
    public bool isHit = false;

    public bool Respawning = false;
    Animator ani;

    public GameObject[] bulletPrefabs; //총알 프리펩 오브젝트 
   
    public float curBulletDelay = 0f;
    public float maxBulletDelay = 1f;
    
    SpriteRenderer spriteRender;

    public Sprite[] sprites;
    private void Start()
    {
        ani = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        ReloadBullet();
    }

    private void FixedUpdate()
    {
        if (GameManager.gamemanager.playerdie) // 죽었을때 깜빡 깜빡
        {
            float val = Mathf.Sin(Time.time * 500); //깜박임 빈도 
            //Debug.LogWarning(val);
            if (val > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;

            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;
           
        }
    }
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
        {
            h = 0;

        }
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
        {
            v = 0;

        }
        if (isHit)
        {
            h = 0;
            v = 0;
        }

        Vector3 curPos = transform.position;
        
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        if (Respawning)
        {
            Debug.Log("부활중");
            transform.position = curPos + (nextPos*0.2f);
        }
        else { transform.position = curPos + nextPos; }
        

        
        ani.SetInteger("Input", (int)h);
    }


    void ReloadBullet()
    {
        curBulletDelay += Time.deltaTime;
    }

    void Power() 
    {

        switch (power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletPrefabs[0], transform.position, Quaternion.identity);
                Rigidbody2D rd = bullet.GetComponent<Rigidbody2D>();
                rd.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2: //2발 발사 발사위치 Vector3.right*0.1f,+ Vector3.left * 0.1f으로 발사위치 조정
                GameObject bulletR = Instantiate(bulletPrefabs[0], transform.position+Vector3.right*0.1f, Quaternion.identity);
                Rigidbody2D rdR = bulletR.GetComponent<Rigidbody2D>();
                rdR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                GameObject bulletL = Instantiate(bulletPrefabs[0], transform.position + Vector3.left * 0.1f, Quaternion.identity);
                Rigidbody2D rdL = bulletL.GetComponent<Rigidbody2D>();
                rdL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bullet2 = Instantiate(bulletPrefabs[1], transform.position, Quaternion.identity);
                Rigidbody2D rd2 = bullet2.GetComponent<Rigidbody2D>();
                rd2.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2R = Instantiate(bulletPrefabs[0], transform.position + Vector3.right * 0.25f, Quaternion.identity);
                Rigidbody2D rd2R = bullet2R.GetComponent<Rigidbody2D>();
                rd2R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2L = Instantiate(bulletPrefabs[0], transform.position + Vector3.left * 0.25f, Quaternion.identity);
                Rigidbody2D rd2L = bullet2L.GetComponent<Rigidbody2D>();
                rd2L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject bullet21 = Instantiate(bulletPrefabs[1], transform.position, Quaternion.identity);
                Rigidbody2D rd21 = bullet21.GetComponent<Rigidbody2D>();
                rd21.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2R1 = Instantiate(bulletPrefabs[0], transform.position + Vector3.right * 0.75f, Quaternion.identity);
                Rigidbody2D rd2R1 = bullet2R1.GetComponent<Rigidbody2D>();
                rd2R1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2L1 = Instantiate(bulletPrefabs[0], transform.position + Vector3.left * 0.75f, Quaternion.identity);
                Rigidbody2D rd2L1 = bullet2L1.GetComponent<Rigidbody2D>();
                rd2L1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet211 = Instantiate(bulletPrefabs[1], transform.position+Vector3.up*0.25f, Quaternion.identity);
                Rigidbody2D rd211 = bullet211.GetComponent<Rigidbody2D>();
                rd211.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2R11 = Instantiate(bulletPrefabs[0], transform.position + Vector3.right * 0.25f, Quaternion.identity);
                Rigidbody2D rd2R11 = bullet2R11.GetComponent<Rigidbody2D>();
                rd2R11.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2L11 = Instantiate(bulletPrefabs[0], transform.position + Vector3.left * 0.25f, Quaternion.identity);
                Rigidbody2D rd2L11 = bullet2L11.GetComponent<Rigidbody2D>();
                rd2L11.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 5:
                GameObject bullet212 = Instantiate(bulletPrefabs[1], transform.position, Quaternion.identity);
                Rigidbody2D rd212 = bullet212.GetComponent<Rigidbody2D>();
                rd212.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2R12 = Instantiate(bulletPrefabs[0], transform.position + Vector3.right * 0.75f, Quaternion.identity);
                Rigidbody2D rd2R12 = bullet2R12.GetComponent<Rigidbody2D>();
                rd2R12.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2L12 = Instantiate(bulletPrefabs[0], transform.position + Vector3.left * 0.75f, Quaternion.identity);
                Rigidbody2D rd2L12 = bullet2L12.GetComponent<Rigidbody2D>();
                rd2L12.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2112 = Instantiate(bulletPrefabs[1], transform.position + Vector3.up * 0.5f, Quaternion.identity);
                Rigidbody2D rd2112 = bullet2112.GetComponent<Rigidbody2D>();
                rd2112.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2R112 = Instantiate(bulletPrefabs[0], transform.position + Vector3.right * 0.25f, Quaternion.identity);
                Rigidbody2D rd2R112 = bullet2R112.GetComponent<Rigidbody2D>();
                rd2R112.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2L112 = Instantiate(bulletPrefabs[0], transform.position + Vector3.left * 0.25f, Quaternion.identity);
                Rigidbody2D rd2L112 = bullet2L112.GetComponent<Rigidbody2D>();
                rd2L112.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                GameObject bullet2122 = Instantiate(bulletPrefabs[1], transform.position + Vector3.up*1, Quaternion.identity);
                Rigidbody2D rd2122 = bullet2122.GetComponent<Rigidbody2D>();
                rd2122.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2R122 = Instantiate(bulletPrefabs[0], transform.position + Vector3.right * 1.75f, Quaternion.identity);
                Rigidbody2D rd2R122 = bullet2R122.GetComponent<Rigidbody2D>();
                rd2R122.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2L122 = Instantiate(bulletPrefabs[0], transform.position + Vector3.left * 1.75f, Quaternion.identity);
                Rigidbody2D rd2L122 = bullet2L122.GetComponent<Rigidbody2D>();
                rd2L122.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet21122 = Instantiate(bulletPrefabs[1], transform.position + Vector3.up * 2f, Quaternion.identity);
                Rigidbody2D rd21122 = bullet21122.GetComponent<Rigidbody2D>();
                rd21122.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2R1122 = Instantiate(bulletPrefabs[0], transform.position + Vector3.right * 1.25f, Quaternion.identity);
                Rigidbody2D rd2R1122 = bullet2R1122.GetComponent<Rigidbody2D>();
                rd2R1122.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bullet2L1122 = Instantiate(bulletPrefabs[0], transform.position + Vector3.left * 1.25f, Quaternion.identity);
                Rigidbody2D rd2L1122 = bullet2L1122.GetComponent<Rigidbody2D>();
                rd2L1122.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }
        
    }
    void Fire()
    {
        if (!Input.GetButton("Fire1"))  // 안눌렷다면  발사안함
            return;


        if (curBulletDelay < maxBulletDelay) // max불릿딜레이이상시간지나야만 발사가능
            return;
        //발사 프리펩 Instantiate 사용
        if (isHit) return;

        if (Respawning) return;

        Power();
        
        curBulletDelay = 0f;  //다시 0초 설정해줘야함
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBorder")
        {
            Debug.Log("플레이어보더");
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;

            }

        }
        if(collision.gameObject.tag == "EnemyBullet")
        {


            //ui 컨트롤
            life--;  //체력 까고
            GameManager.gamemanager.UpdateLifeIcon(life); //ui에 반영


            //폭팔 애니메이션 보여주고
            ani.SetBool("die", true);
            gameObject.GetComponent<PolygonCollider2D>().enabled = false; // 맞으면 플레이어의 콜라이더 해제 애니메이션여러번 방지

            //Invoke("DamageEnd", 1f);
            if (isHit)
                return;

            isHit = true;
            Respawning = true;
            Invoke(nameof(delay), 0.1f);  //0.1초후 애니메이션의die해제 
            
            
        }
        if (collision.gameObject.tag == "Powerup")
        {
            if (power == limitPower)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                power++;
                Destroy(collision.gameObject);
            }
            
        }
        if (collision.gameObject.tag == "Coin")
        {
            if (life == 3)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                life++;
                GameManager.gamemanager.UpdateLifeIcon(life);
                Destroy(collision.gameObject);
            }
            }

    }
    void delay()
    {
        ani.SetBool("die",false);
        Invoke(nameof(ReallyDie), 1f); //1초뒤에 죽기 
    }
    void ReallyDie()
    {
        
        GameManager.gamemanager.Playerdie_delebullet(); //  총알삭제
        

        if (life == 0)
        {
            GameManager.gamemanager.GameOver();
        }
        else
        {
            GameManager.gamemanager.RespawnPlayer(); //재소환;
        }
        gameObject.SetActive(false); 

    }
    
    //void DamageEnd()
    //{
    //    GameManager.gamemanager.playerdie = false;
    //    gameObject.GetComponent<SpriteRenderer>().enabled = true;
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBorder")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                    
            }       
        }           
    }
}
