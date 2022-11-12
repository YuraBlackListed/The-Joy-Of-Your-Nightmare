using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    public GameObject TestObject1;
    public GameObject TestObject2;
    public GameObject TestObject3;
    public GameObject TestObject4;
    public void Select()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        TestObject1.GetComponent<Image>().color = Color.red;
        TestObject2.GetComponent<Image>().color = Color.red;
        TestObject3.GetComponent<Image>().color = Color.red;
        TestObject4.GetComponent<Image>().color = Color.red;
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material.color = Color.gray;
        TestObject1.GetComponent<Image>().color = Color.white;
        TestObject2.GetComponent<Image>().color = Color.white;
        TestObject3.GetComponent<Image>().color = Color.white;
        TestObject4.GetComponent<Image>().color = Color.white;
    }
}
