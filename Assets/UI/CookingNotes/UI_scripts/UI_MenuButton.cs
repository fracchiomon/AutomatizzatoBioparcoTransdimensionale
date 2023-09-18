using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    public void OnClick()
    {
        this.GetComponent<Button>().interactable = false;
    }

}
