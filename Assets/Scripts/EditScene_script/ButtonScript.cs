using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    //true=重なってる　false=重なってない
    public bool fruitsOverlap = false;
    //true=ボタンが押された&重なっていない　//false=それ以外

    public GameObject cream;

    // Start is called before the first frame update
    void Start()
    {

        this.GetComponent<Button>().onClick.AddListener(cream.GetComponent<Cream>().CreateFruitsList);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (fruitsOverlap)
        {
            Debug.Log("重なってる! やり直し");
        }
        else if (!fruitsOverlap)
        {
            Debug.Log("重なってない!");
            //clickDecision = true;
        }

    }
}