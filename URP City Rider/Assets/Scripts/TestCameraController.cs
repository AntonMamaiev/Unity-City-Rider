using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    public Transform player;
    public float yOffSet = 9f;
    public float zOffSet = -9f;
    public float xOffSet = 2.5f;
    private bool afterGS = false;

    public Animator animCamera;
    void Start()
    {
        animCamera.SetTrigger("CameraTrigger");
        Invoke("StartAnimationEnd", 5f);
    }
    public void StartAnimationEnd()
    {
        player = GameObject.Find("Player").transform;
        afterGS = true;
    }
    void LateUpdate()
    {
        if (afterGS)
            transform.position = new Vector3(player.position.x + xOffSet, player.position.y + yOffSet, player.position.z + zOffSet);        
    }
}
