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
    // カットフルーツの添字対応表。この添字を元に以下を参照する
    Dictionary<string, int> cutFruitsIdTable;

    //フルーツの個数を格納する配列
    float [] fruitsNumList = new float[6];
    //cut系のフルーツの個数を格納する配列
    float[] cutFruitsNumList = new float[4];

    //FruitsType
    // 使う画像の配列
    public Sprite[] fruitsSpriteImages;
    // 表示名の配列
    string[] fruitsNameList = { "キウイ", "マスカット", "ぶどう", "バナナ", "みかん", "いちご" };
    // 表示用のフルーツオブジェクト配列
    public GameObject[] dispalyFruits;

    //FruitsCut
    // 使う画像の配列
    public Sprite[] cutFruitsSpriteImages;
    // 表示用のフルーツオブジェクト配列
    public GameObject[] dispalyCutFruits;

    public GameObject FruitsNumGroupes;
    public GameObject FruitsCutGroupes;

    public GameObject buttonNext1;
    public GameObject buttonNext2;
    public GameObject buttonNext3;
    public GameObject buttonCream;
    public GameObject bg;

    public GameObject cream;
    public GameObject pans;


    // Start is called before the first frame update
    void Start()
    {
        creamObj = GameObject.Find("Cream");
        FruitsData = creamObj.GetComponent<Cream>().fruitsData;

        fruitsIdTable = new Dictionary<string, int>();
        fruitsIdTable.Add("kiwi", 0);
        fruitsIdTable.Add("muscat", 1);
        fruitsIdTable.Add("grape", 2);
        fruitsIdTable.Add("banana", 3);
        fruitsIdTable.Add("orange", 4);
        fruitsIdTable.Add("straw", 5);

        cutFruitsIdTable = new Dictionary<string, int>();
        cutFruitsIdTable.Add("kiwi", 0);
        cutFruitsIdTable.Add("muscat", 1);
        cutFruitsIdTable.Add("grape", 2);
        cutFruitsIdTable.Add("banana", 3);

        fruitsType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //用意するフルーツの説明
    void fruitsType()
    {
        foreach (KeyValuePair<string, List<double>> item in FruitsData)
        {
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
                cutFruitsNumList[fruitsIdTable["banana"]]++;
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

    //カット方法の説明
    public void frutisCut()
    {
        //fruitsTypeで表示したUIを見えなくする
        FruitsNumGroupes.SetActive(false);
        buttonNext1.SetActive(false);
        buttonNext2.SetActive(true);

        int displayCutFruitsIndex = 0; // 必要なフルーツオブジェクトを参照するindex

        //配列cutFruitsIdTableすべて回す
        foreach (KeyValuePair<string, int> fruitsId in cutFruitsIdTable)
        {
            //カットフルーツが一つ以上あったら
            if (cutFruitsNumList[fruitsId.Value] > 0)
            {
                // 0個でなければ、dispalyCutFruitsのsetActiveをtrueにし、画像、名前を差し替え、個数表示
                dispalyCutFruits[displayCutFruitsIndex].SetActive(true);

                Text nameText = dispalyCutFruits[displayCutFruitsIndex].transform.GetChild(0).GetComponent<Text>(); // 0番目は名前

                //個数が1個以上の時、個数も名前の横につける 例：キウイ（２個分）
                if (cutFruitsNumList[fruitsId.Value] > 1)
                {
                    nameText.text = fruitsNameList[fruitsId.Value] + "(" + cutFruitsNumList[fruitsId.Value] + "個分)";

                }else{
                    nameText.text = fruitsNameList[fruitsId.Value];
                }

                GameObject image = dispalyCutFruits[displayCutFruitsIndex].transform.GetChild(1).gameObject; // 1番目は画像
                image.GetComponent<Image>().sprite = cutFruitsSpriteImages[fruitsId.Value];

                Text introText = dispalyCutFruits[displayCutFruitsIndex].transform.GetChild(2).GetComponent<Text>(); // 2番目は説明

                Text arrowText = dispalyCutFruits[displayCutFruitsIndex].transform.GetChild(3).GetComponent<Text>(); // 3番目は矢印

                GameObject image2 = dispalyCutFruits[displayCutFruitsIndex].transform.GetChild(4).gameObject; // 4番目は画像
                Text introText2 = dispalyCutFruits[displayCutFruitsIndex].transform.GetChild(5).GetComponent<Text>(); // 5番目は説明

                if (fruitsIdTable[fruitsId.Key] == 0){
                    //キウイの時
                    introText.text = "皮をむきます";
                    image2.GetComponent<Image>().sprite = cutFruitsSpriteImages[4];
                    introText2.text = "5mm幅に切ります";
                }else if(fruitsIdTable[fruitsId.Key] == 3){
                    //バナナの時
                    introText.text = "皮をむきます";
                    image2.GetComponent<Image>().sprite = cutFruitsSpriteImages[5];
                    introText2.text = "３等分します";
                }else{
                    //マスカット・ぶどうの時
                    introText.text = "半分に切ります";
                    arrowText.enabled = false;
                    image2.SetActive(false);
                    introText2.enabled = false;
                }

                displayCutFruitsIndex++;
            }
        }
    }

    //GameManagerへつなげるボタン
    public void buttonFastner(){
        bg.SetActive(false);
        FruitsCutGroupes.SetActive(false);
        buttonNext2.SetActive(false);
        buttonNext3.SetActive(true);
        buttonCream.SetActive(true);
        cream.SetActive(true);
        pans.SetActive(true);
    }
}
