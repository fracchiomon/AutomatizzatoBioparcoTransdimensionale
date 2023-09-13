using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    private Animator anim;
    public float a = 5  ;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void GetAnim()
    {
        anim.SetTrigger("from idle to correct");
    }


}
