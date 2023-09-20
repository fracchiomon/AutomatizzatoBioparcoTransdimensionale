using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cronometro : MonoBehaviour
{
    private float gameTime;
    // Start is called before the first frame update

    public void setGameTime(float gameTime)
    {
        this.gameTime = gameTime;
    }

    public float getGameTime()
    {
        return this.gameTime;
    }

    public void timeRunsOut()
    {
        this.gameTime -= Time.deltaTime;
    }

}
