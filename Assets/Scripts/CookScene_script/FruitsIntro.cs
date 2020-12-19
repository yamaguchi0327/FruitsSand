using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FruitsIntro : MonoBehaviour
{
    GameObject creamObj;
    Dictionary<string, List<double>> FruitsData;

    //フルーツの個数を格納する変数
    float kiwiNum;
    float orangeNum;
    float muscatNum;
    float grapeNum;
    float strawNum;
    float bananaNum;

    //cut系のフルーツの個数を格納する変数
    float cutKiwiNum;
    float cutMuscatNum;
    float cutGrapeNum;
    float cutBananaNum;

    // Start is called before the first frame update
    void Start()
    {
        creamObj = GameObject.Find("Cream");
        FruitsData = creamObj.GetComponent<Cream>().fruitsData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fruitsType()
    {
        foreach (KeyValuePair<string, List<double>> item in FruitsData)
        {
            if (FruitsData.ContainsKey("Fruits_orange"))
            {
                orangeNum++;
            }
            else if (FruitsData.ContainsKey("Fruits_kiwi"))
            {
                kiwiNum++;
            }
            else if (FruitsData.ContainsKey("Fruits_cutkiwi"))
            {
                cutKiwiNum++;
                kiwiNum += 0.33f;
            }
            else if (FruitsData.ContainsKey("Fruits_straw"))
            {
                strawNum++;
            }
            else if (FruitsData.ContainsKey("Fruits_grapefull"))
            {
                grapeNum++;
            }
            else if (FruitsData.ContainsKey("Fruits_graperight")|| FruitsData.ContainsKey("Fruits_grapeleft"))
            {
                cutGrapeNum++;
                grapeNum += 0.5f;
            }
            else if (FruitsData.ContainsKey("Fruits_muscatfull"))
            {
                muscatNum++;
            }
            else if (FruitsData.ContainsKey("Fruits_muscatright")|| FruitsData.ContainsKey("Fruits_muscatleft"))
            {
                cutMuscatNum++;
                muscatNum += 0.5f;
            }
            else if (FruitsData.ContainsKey("Fruits_banana"))
            {
                bananaNum += 0.33f;
            }
        }


    }
}
