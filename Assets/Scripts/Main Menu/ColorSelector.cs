using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviour
{
    
    public void select()
    {
        MainMenu.selected = gameObject.GetComponent<Material>().color;
    }

}
