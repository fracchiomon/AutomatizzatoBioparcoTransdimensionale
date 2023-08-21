using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private MoleController[] moles;
    public float gameTime;
    public Text gameText;
    public int index = 0;
    [SerializeField] private float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        this.moles[index].transform.position = this.moles[index].Points[moles[index]._indexPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        if (gameTime < 0)
        {
            gameTime = 0;
        }
        gameText.text = gameTime.ToString();
        this.moles[index].Move();
    }


}
