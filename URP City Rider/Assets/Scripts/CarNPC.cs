using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPC : MonoBehaviour
{
    Vector3 direction;
    Vector3 MovePosition = Vector3.zero;
    private int speed;
    private float sidespeed;
    private float rotation;
    private int randomTurn;
    private float randomTime = 0f;
    private Transform playerTransform;
    private bool crashed;
    private void Start()
    {
        crashed = false;
        sidespeed = 3f;
        speed = Random.Range(4,8);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (!PlayerManager.GameOver)
        {                
            if ((this.transform.eulerAngles.y <= 100f && this.transform.eulerAngles.y >= 0f) || (this.transform.eulerAngles.y <= 360f && this.transform.eulerAngles.y >= 260f))
            {
                direction.z = speed;
                if (transform.position.x == 1.15f || transform.position.x == 3.15f) 
                    transform.eulerAngles = new Vector3(0,0,0);    
                if (!crashed)
                    transform.Rotate(0, rotation * Time.deltaTime, 0);

            }
            else if (this.transform.eulerAngles.y <= 250f && this.transform.eulerAngles.y >= 150f)
            {
                direction.z = -speed;
                if (transform.position.x == -1.15f || transform.position.x == -3.15f)
                    transform.eulerAngles = new Vector3(0, 180, 0);
                if (!crashed)
                    transform.Rotate(0, rotation * Time.deltaTime, 0);
            }
            //Rotation

            this.transform.position += new Vector3(MovePosition.x * Time.deltaTime, 0, direction.z * Time.deltaTime);

            if (playerTransform.position.z - 24f > transform.position.z)
               SelfDestroyer();

            if ((transform.position.x > -1.05f || transform.position.x < -3.25f) && (this.transform.eulerAngles.y <= 250f && this.transform.eulerAngles.y >= 150f))
            {
                crashed = true;
                speed = 0;
                sidespeed = 0;
                MovePosition.x = 0;
            }
            else if ((transform.position.x > 3.25f || transform.position.x < 1.05f) && 
                ((this.transform.eulerAngles.y <= 100f && this.transform.eulerAngles.y >= 0f) || (this.transform.eulerAngles.y <= 360f && this.transform.eulerAngles.y >= 260f)))
            {
                crashed = true;
                speed = 0;
                sidespeed = 0;
                MovePosition.x = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TurnCar")
        {
            if (transform.position.x == 3.15f)
            {
                rotation = -40f;
                MovePosition.x = -sidespeed;
            }
            else if (transform.position.x == 1.15f)
            {
                rotation = 40f;
                MovePosition.x = sidespeed;
            }
            else if (transform.position.x == -1.15f)
            {
                rotation += 40f;
                MovePosition.x = -sidespeed;
            }
            else if (transform.position.x == -3.15f)
            {
                rotation += -40f;
                MovePosition.x = sidespeed;
            }
        }
        else if (other.gameObject.tag == "Obst" || other.gameObject.tag == "ObstR" || other.gameObject.tag == "ObstL" || other.gameObject.tag == "CarObst")
        {
            crashed = true;
            speed = 0;
            sidespeed = 0;
            MovePosition.x = 0;
        }
        else if (other.gameObject.tag == "INSIDE")
        {
            SelfDestroyer();
        }
    }
    //private void RandomTurn()
    //{
    //    if (this.transform.rotation.x == 3.15f) {
    //        if (transform.position.x == 3)
    //        {
    //            MovePosition.x = -sidespeed;
    //            if (transform.position.x <= 1.15f)
    //            {
    //                MovePosition.x = 0;
    //                transform.position = new Vector3(1.15f, transform.position.y, transform.position.z);
    //            }                
    //        }
    //        else if (this.transform.rotation.x == 1.15f)
    //        {
    //            MovePosition.x = sidespeed;
    //            if (transform.position.x >= 3.15f)
    //            {
    //                MovePosition.x = 0;
    //                transform.position = new Vector3(3.15f, transform.position.y, transform.position.z);}
    //            }
    //    }
    //    else {
    //        if (this.transform.rotation.x == -1.15f)
    //        {
    //            MovePosition.x = -sidespeed;
    //            if (transform.position.x <= -3.15f)
    //            {
    //                MovePosition.x = 0;
    //                transform.position = new Vector3(-3.15f, transform.position.y, transform.position.z); 
    //            }                        
    //        }
    //        else if ((this.transform.rotation.x == -3.15f))
    //        {
    //            MovePosition.x = sidespeed;
    //            if (transform.position.x >= -1.15f)
    //            {
    //                MovePosition.x = 0;
    //                transform.position = new Vector3(-1.15f, transform.position.y, transform.position.z);
    //            }                
    //        }
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TurnCar")
        {
            if (transform.position.x < 3.15 && MovePosition.x == -sidespeed &&
                ((this.transform.eulerAngles.y <= 100f && this.transform.eulerAngles.y >= 0f) || (this.transform.eulerAngles.y <= 360f && this.transform.eulerAngles.y >= 260f)) && !crashed)
            {
                MovePosition.x = 0f;
                transform.position = new Vector3(1.15f, transform.position.y, transform.position.z);
                rotation = 0f;
            }
            else if (transform.position.x > 1.15 && MovePosition.x == sidespeed &&
                ((this.transform.eulerAngles.y <= 100f && this.transform.eulerAngles.y >= 0f) || (this.transform.eulerAngles.y <= 360f && this.transform.eulerAngles.y >= 260f)) && !crashed)
            {
                MovePosition.x = 0f;
                transform.position = new Vector3(3.15f, transform.position.y, transform.position.z);
                rotation = 0f;
            }
            else if (transform.position.x > -3.15 && MovePosition.x == sidespeed && (this.transform.eulerAngles.y <= 250f && this.transform.eulerAngles.y >= 150f) && !crashed)
            {
                MovePosition.x = 0f;
                transform.position = new Vector3(-1.15f, transform.position.y, transform.position.z);
                rotation = 0f;
            }
            else if (transform.position.x < -1.15 && MovePosition.x == -sidespeed && (this.transform.eulerAngles.y <= 250f && this.transform.eulerAngles.y >= 150f) && !crashed)
            {
                MovePosition.x = 0f;
                transform.position = new Vector3(-3.15f, transform.position.y, transform.position.z);
                rotation = 0f;
            }
        }
        

    }
    private void SelfDestroyer()
    {
        Destroy(this.gameObject);
    }
}
