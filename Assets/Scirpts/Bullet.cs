using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float power = 0f; //ÃÑ¾Ë µ©Áö
    public bool isRotate;
    private void Update()
    {
        if (isRotate)
        {
            transform.Rotate(Vector3.forward * 10);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
          //  Destroy(gameObject);
          gameObject.SetActive(false);

        }
    }
}
