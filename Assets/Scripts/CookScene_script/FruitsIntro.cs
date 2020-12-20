using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FruitsIntro : MonoBehaviour
{
    GameObject creamObj;
    Dictionary<string, List<double>> FruitsData; //持ってきてるやつ

    // フルーツの添字対応表。この添字を元に以下を参照する
    Dictionary<string, int> fruitsIdTable;
    //フルーツの個数を格納する配列
    float [] fruitsNumList = new float[6];
    //cut系のフルーツの個数を格納する配列
    float[] cutFruitsNumList = new float[6];
    // 使う画像の配列
    public Sprite[] fruitsSpriteImages;
    // 表示名の配列
    string [] fruitsNameList = { "キウイ", "みかん", "マスカット", "ぶどう", "いちご", "バナナ" };
    // 表示用のフルーツオブジェクト配列
    public GameObject[] dispalyFruits;

    // Start is called before the first frame update
    void Start()
    {
        creamObj = GameObject.Find("Cream");
        FruitsData = creamObj.GetComponent<Cream>().fruitsData;

        fruitsIdTable = new Dictionary<string, int>();
        fruitsIdTable.Add("kiwi", 0);
        fruitsIdTable.Add("orange", 1);
        fruitsIdTable.Add("muscat", 2);
        fruitsIdTable.Add("grape", 3);
        fruitsIdTable.Add("straw", 4);
        fruitsIdTable.Add("banana", 5);

        //cutFruitsNumList = new Dictionary<string, int>();
        //fruitsNumList.Add("kiwi", 0);
        //fruitsNumList.Add("muscat", 0);
        //fruitsNumList.Add("grape", 0);
        //fruitsNumList.Add("banana", 0);


        for (int i = 0; i < fruitsNumList.Length; i++){
            fruitsNumList[i] = 0f;
            cutFruitsNumList[i] = 0f;
        }

        fruitsType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fruitsType()
    {
        foreach (KeyValuePair<string, List<double>> item in FruitsData)
        {
            Debug.Log("unti");
            if (item.Key.Contains("Fruits_orange"))
            {
                fruitsNumList[fruitsIdTable["orange"]]++;
            }
            else if (item.Key.Contains("Fruits_kiwi"))
            {
                fruitsNumList[fruitsIdTable["kiwi"]]++;
            }
            else if (item.Key.Contains("Fruits_cutkiwi"))
            {
                cutFruitsNumList[fruitsIdTable["kiwi"]]++;
                fruitsNumList[fruitsIdTable["kiwi"]] += 0.33f;
            }
            else if (item.Key.Contains("Fruits_straw"))
            {
                fruitsNumList[fruitsIdTable["straw"]] ++;
            }
            else if (item.Key.Contains("Fruits_grapefull"))
            {
                fruitsNumList[fruitsIdTable["grape"]]++;
            }
            else if (item.Key.Contains("Fruits_graperight") || item.Key.Contains("Fruits_grapeleft"))
            {
                cutFruitsNumList[fruitsIdTable["grape"]]++;
                fruitsNumList[fruitsIdTable["grape"]] +=0.5f;
            }
            else if (item.Key.Contains("Fruits_muscatfull"))
            {
                fruitsNumList[fruitsIdTable["muscat"]]++;
            }
            else if (item.Key.Contains("Fruits_muscatright") || item.Key.Contains("Fruits_muscatleft"))
            {
                fruitsNumList[fruitsIdTable["muscat"]] +=0.5f;
                cutFruitsNumList[fruitsIdTable["muscat"]]++;
            }
            else if (item.Key.Contains("Fruits_banana"))
            {
                fruitsNumList[fruitsIdTable["banana"]] += 0.33f;
            }
        }

        int displayFruitsIndex = 0; // 必要なフルーツオブジェクトを参照するindex
        foreach (KeyValuePair<string, int> fruitsId in fruitsIdTable)
        {
            Debug.Log(fruitsId.Key + fruitsNumList[fruitsId.Value]+"個");

            if(fruitsNumList[fruitsId.Value] > 0){
                // 0個でなければ、setActiveをtrueにし、画像、名前を差し替え、個数表示
                dispalyFruits[displayFruitsIndex].SetActive(true);
                GameObject image = dispalyFruits[displayFruitsIndex].transform.GetChild(0).gameObject; // 0番目は画像
                image.GetComponent<Image>().sprite = fruitsSpriteImages[fruitsId.Value];

                Text nameText = dispalyFruits[displayFruitsIndex].transform.GetChild(1).GetComponent<Text>(); // 1番目は名前
                nameText.text = fruitsNameList[fruitsId.Value];

                Text numText = dispalyFruits[displayFruitsIndex].transform.GetChild(2).GetComponent<Text>(); // 2番目は個数
                float num = (int)Mathf.Ceil(fruitsNumList[fruitsId.Value]);
                numText.text = "× " + num.ToString();

                displayFruitsIndex++;
            }
        }

    }
}
