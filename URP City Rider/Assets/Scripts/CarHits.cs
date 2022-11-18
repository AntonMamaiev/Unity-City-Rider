using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CarHits : MonoBehaviour
{
    public static bool ObstR = false;
    public static bool ObstL = false;
    public static bool ObstF = false;
    public GameObject CoinFX;
    public GameObject SmokeFX;
    public GameObject ImpactFX;

    private void Start()
    {
        ObstR = false; ObstL = false; ObstF = false;
    }
    // Врезание в обст
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obst")
        {
            ObstF = true;
            GameObject Impact = Instantiate(ImpactFX, new Vector3(transform.position.x, 0.03f, transform.position.z), transform.rotation);
            PlayerManager.GameOver = true;
        }
        if (other.gameObject.tag == "ObstR") 
        {
            ObstR = true;
            GameObject Impact = Instantiate(ImpactFX, new Vector3(transform.position.x, 0.03f, transform.position.z), transform.rotation);
            PlayerManager.GameOver = true;
        }
        if (other.gameObject.tag == "ObstL")
        {
            ObstL = true;
            GameObject Impact = Instantiate(ImpactFX, new Vector3(transform.position.x, 0.03f, transform.position.z), transform.rotation);
            PlayerManager.GameOver = true;
        }
        if (other.gameObject.tag == "CarObst")
        {
            ObstF = true;
            GameObject Impact = Instantiate(ImpactFX, new Vector3(transform.position.x, 0.03f, transform.position.z), transform.rotation);
            PlayerManager.GameOver = true;
        }
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            GameObject Effect = Instantiate(CoinFX, other.transform.position, other.transform.rotation);
            PlayerManager.Coins++;
        }


        // Анимация + ефект

        // Окно ГеймОвер


    }
}
