using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Settings : MonoBehaviour
{

    /*private void Awake()
    {
        //gameObject.SetActive(false);

    }*/
    public void ChangeSettingsActive(bool visibility)
    {
        gameObject.SetActive(visibility);
    }

}
