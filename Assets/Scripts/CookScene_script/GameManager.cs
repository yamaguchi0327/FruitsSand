using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    GameObject creamObj;
    IOrderedEnumerable<KeyValuePair<string, List<double>>> sortedFruits;
    // Start is called before the first frame update
    int ButtonCount = 0;//ボタンを押した回数

    public GameObject banana;
    public GameObject left_grape;
    public GameObject right_grape;
    public GameObject grape;
    public GameObject left_muscat;
    public GameObject right_muscat;
    public GameObject muscat;
    public GameObject cut_kiwi;
    public GameObject kiwi;
    public GameObject orange;
    public GameObject straw;
    public GameObject big_cream;
    public GameObject cream;
    public GameObject pan;

    void Start()
    {
        creamObj = GameObject.Find("Cream");
        Debug.Log(creamObj);
        sortedFruits = creamObj.GetComponent<Cream>().sortedFruits;
        //Debug.Log("シーン遷移した!");
        //foreach (KeyValuePair<string, List<double>> item in sortedFruits)
        //{
        //    Debug.Log(item.Key + "の座標は" + item.Value[0] + "," + item.Value[1]);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void putFruits()
    {
        string FruitsKey = sortedFruits.ElementAt(ButtonCount).Key;
        float x = (float)sortedFruits.ElementAt(ButtonCount).Value[0];
        float y = (float)sortedFruits.ElementAt(ButtonCount).Value[1];
        Debug.Log(sortedFruits.ElementAt(ButtonCount).Key + "をおいた");
        GameObject putFruit = null;

        if(FruitsKey.Contains("Fruits_orange"))
        {
            putFruit = orange;
        }
        else if(FruitsKey.Contains ("Fruits_kiwi"))
        {
            putFruit = kiwi;
        }
        else if (FruitsKey.Contains("Fruits_cutkiwi"))
        {
            putFruit = cut_kiwi;
        }
        else if (FruitsKey.Contains("Fruits_straw"))
        {
            putFruit = straw;
        }
        else if (FruitsKey.Contains("Fruits_grapefull"))
        {
            putFruit = grape;
        }
        else if (FruitsKey.Contains("Fruits_graperight"))
        {
            putFruit = right_grape;
        }
        else if (FruitsKey.Contains("Fruits_grapeleft"))
        {
            putFruit = left_grape;
        }
        else if (FruitsKey.Contains("Fruits_muscatfull"))
        {
            putFruit = muscat;
        }
        else if (FruitsKey.Contains("Fruits_muscatright"))
        {
            putFruit = right_muscat;
        }
        else if (FruitsKey.Contains("Fruits_muscatleft"))
        {
            putFruit = left_muscat;
        }
        else if (FruitsKey.Contains("Fruits_banana"))
        {
            putFruit = banana;
        }

        if(putFruit){
            Vector3 panSize = pan.GetComponent<MeshRenderer>().bounds.size;
            float z = panSize.z * (x / panSize.x);
            float rotateY = Mathf.Atan(panSize.z / panSize.x);
            Instantiate(putFruit, new Vector3(x, y + (float)0.5 * putFruit.GetComponent<MeshRenderer>().bounds.size.y, z), Quaternion.Euler(0f, Mathf.Rad2Deg * -rotateY,0f));
        }

        ButtonCount++;
    }
}