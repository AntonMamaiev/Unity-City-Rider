using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMove : MonoBehaviour
{
    private Vector3 moveController = Vector3.zero;
    private Vector3 direction;
    public static Vector3 pos;
    private bool afterGS;

    public int line = 2;
    public int targetline = 2;
    public float sideSpeedR;
    public float sideSpeedL;
    public float forwardSpeed;
    public float maxSpeed;
    public bool canmove;
    public Animator anim2;
    public Animator anim;


    void Start()
    {
        maxSpeed = 25f;
        sideSpeedR = 0f; sideSpeedL = 0f;
        canmove = false;
        forwardSpeed = 0;
        afterGS = false;
        anim.SetTrigger("StartTrigger");
        anim2.SetTrigger("ControllerStartTrigger");
        Invoke("StartAnimationEnd", 5f);
    }
    public void StartAnimationEnd()
    {
        sideSpeedR = 8f; sideSpeedL = -8f;
        canmove = true;
        anim2.applyRootMotion = true;
        this.transform.position = new Vector3(1.15f, 0, 0);
        forwardSpeed = 12f;
        afterGS = true;
    }
    void Update()
    {
        if (!PlayerManager.GameOver)
        {
            if (forwardSpeed < maxSpeed)
                forwardSpeed += 0.0065f * Time.deltaTime;

            pos = transform.position;

            if (!line.Equals(targetline))
            {
                if (targetline == 0 && pos.x <= -3.15f)
                {
                    this.transform.position = new Vector3(-3.15f, pos.y, pos.z);
                    line = targetline;
                    canmove = true;
                    moveController.x = 0;
                }
                else if (targetline == 1 && (pos.x >= -1.15f || pos.x < -1.15f))
                {
                    if (line == 0 && pos.x >= -1.15f)
                    {
                        this.transform.position = new Vector3(-1.15f, pos.y, pos.z);
                        line = targetline;
                        canmove = true;
                        moveController.x = 0;
                    }
                    else if (line == 2 && pos.x < -1.15f)
                    {
                        this.transform.position = new Vector3(-1.15f, pos.y, pos.z);
                        line = targetline;
                        canmove = true;
                        moveController.x = 0;
                    }
                }
                else if (targetline == 2 && (pos.x >= 1.15f || pos.x <= 1.15f))
                {
                    if (line == 1 && pos.x >= 1.15f)
                    {
                        this.transform.position = new Vector3(1.15f, pos.y, pos.z);
                        line = targetline;
                        canmove = true;
                        moveController.x = 0;
                    }
                    else if (line == 3 && pos.x <= 1.15f)
                    {
                        this.transform.position = new Vector3(1.15f, pos.y, pos.z);
                        line = targetline;
                        canmove = true;
                        moveController.x = 0;
                    }
                }
                else if (targetline == 3 && pos.x >= 3.15)
                {
                    this.transform.position = new Vector3(3.15f, pos.y, pos.z);
                    line = targetline;
                    canmove = true;
                    moveController.x = 0;
                }
            }

            checkInputs();

            direction.z = forwardSpeed;

            this.transform.position += new Vector3(moveController.x * Time.deltaTime, 0, 0);
        }
        else if (PlayerManager.GameOver && CarHits.ObstF)
        {            
            canmove = false;
            anim.SetTrigger("GameOverF");
            CarHits.ObstF = false;
        }
        else if (PlayerManager.GameOver && CarHits.ObstR)
        {
            canmove = false;
            anim.SetTrigger("GameOverR");
            CarHits.ObstR = false;
        }
        else if (PlayerManager.GameOver && CarHits.ObstL)
        {
            canmove = false;
            anim.SetTrigger("GameOverL");
            CarHits.ObstL = false;
        }
    }

    void checkInputs()
    {
        if((Input.GetKeyDown(KeyCode.A) || SwipeManager.swipeLeft) && canmove && line > 0 && afterGS)
        {           
            targetline--;
            canmove = false;
            moveController.x = sideSpeedL;
            anim.SetTrigger("LeftTrigger");            
        }
        if ((Input.GetKeyDown(KeyCode.D) || SwipeManager.swipeRight) && canmove && line < 3 && afterGS)
        {
            targetline++;
            canmove = false;
            moveController.x = sideSpeedR;
            anim.SetTrigger("RightTrigger");            
        }
    }
    private void FixedUpdate()
    {
        if(!PlayerManager.GameOver)
            if (afterGS)
                this.transform.position += new Vector3 (0 , 0, direction.z * Time.fixedDeltaTime);
    }
}
