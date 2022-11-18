using UnityEngine;
using TMPro;

public class FpsMeter : MonoBehaviour
{
    public TextMeshProUGUI FpsText;
    private float poollingTime = 1f;
    private float time;
    private int frameCount;

    public void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if(time >= poollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount/time);
            FpsText.text = frameRate.ToString() + " FPS";

            time -= poollingTime;
            frameCount = 0;
        }

    }
}
