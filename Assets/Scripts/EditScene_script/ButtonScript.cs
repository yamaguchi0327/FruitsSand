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
    GameObject textWarning;

    // Start is called before the first frame update
    void Start()
    {

        this.GetComponent<Button>().onClick.AddListener(cream.GetComponent<Cream>().CreateFruitsList);
        textWarning = GameObject.Find("textWarninng");
        textWarning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //フルーツが半透明（重なってる）の時、警告文を出して、ボタンを押せなく（半透明に）する
        if(fruitsOverlap){
            textWarning.SetActive(true);
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 160.0f / 255.0f);

        }
        else if(!fruitsOverlap){
            textWarning.SetActive(false);
            GetComponent<Button>().interactable = true;
            GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        }
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