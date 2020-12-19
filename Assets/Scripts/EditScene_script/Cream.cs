using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Cream : MonoBehaviour
{
    //最終的に次のシーンへ持ち込む辞書型のフルーツリスト
    public Dictionary<string, List<double>> fruitsData;

    // クリームに乗っているフルーツを入れるリスト
    List<GameObject> fruitsOnCream;

    //クリームの原点（右下に変更する）
    double creamPivot_x;
    double creamPivot_y;

    public IOrderedEnumerable<KeyValuePair<string, List<double>>> sortedFruits;

    //スライダー
    public Slider creamSlider; //クリームのサイズ変更用スライダー
    float creamMaxSize = 5.2f;
    float creamMinSize = 2.6f;

    //クリームの左右のパン
    public GameObject rightPan;
    public GameObject leftPan;
    //クリームの右端、左端のx座標をいれる
    double creamRight;
    double creamLeft;
    //クリームの最初のサイズ
    Vector3 creamSize = new Vector3(5.2f, 12.3598f, 0.8f);

    // 確定ボタン（オブジェクトとUIで分かれてるっぽい）
    //public GameObject buttonObj;
    //Button button;

    private void Awake()
    {
        DontDestroyOnLoad(this.transform.root.gameObject);
    }

    void Start()
    {
        // リストの初期化
        fruitsData = new Dictionary<string, List<double>>();
        fruitsOnCream = new List<GameObject>();

        //クリームの最大値・最小値・最初のスライダーの値をスライダーに代入
        creamSlider.maxValue = creamMaxSize;
        creamSlider.minValue = creamMinSize;
        creamSlider.value = creamMaxSize;

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //スライダーによる生クリームの幅変更
    public void CreamSlider()
    {
        //クリームのサイズをスライダーに合わせて変更
        creamSize.x = creamSlider.value;
        this.transform.localScale = creamSize;

        //クリームのサイズを変えた時の左右のx座標
        creamRight = this.transform.position.x + 0.5 * this.transform.localScale.x; //クリームのサイズを変えた時の右端のx座標
        creamLeft = this.transform.position.x - 0.5 * this.transform.localScale.x;　//クリームのサイズを変えた時の右端のy座標

        //パンをクリームのサイズに合わせて移動させる
        rightPan.gameObject.transform.position = new Vector3((float)creamRight + rightPan.gameObject.transform.localScale.x*0.5f , transform.position.y, transform.position.z);
        leftPan.gameObject.transform.position = new Vector3((float)creamLeft - leftPan.gameObject.transform.localScale.x*0.5f , transform.position.y, transform.position.z);
    }

    //クリームの上に居るかどうかの判定
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<DragFruits>().onCream = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.tag == "FruitsObj")
        //{
        collision.gameObject.GetComponent<DragFruits>().onCream = false;
        //}

    }

    //確定ボタンを押した時に実行される
    public void CreateFruitsList()
    {
        // クリームの原点を右下に
        creamPivot_x = this.transform.position.x + 0.5 * this.transform.localScale.x;
        creamPivot_y = this.transform.position.y - 0.5 * this.transform.localScale.y;

        //パンの上に乗っているオブジェクトをfruitsOnCreamリストに入れる
        // GameObject型の全てのオブジェクトを取得し要素分繰り返す
        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
        {
            // タグにFruitsを含まなければスキップ
            if (!obj.tag.Contains("Fruits")) continue;

            //DragFruitsスクリプトをもってなくてもスキップ
            if (!obj.GetComponent<DragFruits>()) continue;
            //Objの中でもクリームの上に乗っているObjを見つける
            if (obj.GetComponent<DragFruits>().onCream)
            {
                fruitsOnCream.Add(obj);
            }
        }

        // クリームの上にあるフルーツのリスト(fruitsOnCream)から、座標込みのDictionaryを生成
        foreach (GameObject obj in fruitsOnCream)
        {
            //サンドを右に倒した時のフルーツの座標を計算（xとyを入れ替えて代入）
            double cx = obj.transform.position.y - creamPivot_y;
            double cy = creamPivot_x - obj.transform.position.x - obj.transform.localScale.x/2;

            // キーの重複対策
            bool createdKey = false; // キーが生成されたかどう
            string defaultKey = obj.tag; // タグをキーとして格納(初期値。これは変えない　例:Fruits_Kiwi）
            string key = defaultKey; // 被っていた場合に作り直すキー
            int keyCount = 2; // キーが被った場合につける添字（2からスタート）
            while (!createdKey)
            {
                if (fruitsData.ContainsKey(key))
                {
                    string suffix = keyCount.ToString(); // 添字をキーにつけるためstringに
                    if(suffix.Length == 1){ //suffixが2~9(1桁)なら
                        suffix = '0' + suffix;
                    }
                    key = defaultKey + suffix; //例:Fruits_Kiwiが3個目なら key=Fruits_Kiwi+03
                    keyCount++;
                }
                else
                {
                    createdKey = true;
                }

            }
            // 辞書に登録
            fruitsData.Add(key, new List<double>() { cx, cy });
        }

        // y座標が小さいもの順に並び替えて再生成
        sortedFruits = fruitsData.OrderBy((fruit) => fruit.Value[1]);

        // デバッグ
        //Debug.Log(sortedFruits.ElementAt(0).Key + "x=" + sortedFruits.ElementAt(0).Value[0] + ",y=" + sortedFruits.ElementAt(0).Value[1]);
        //Debug.Log(sortedFruits.ElementAt(1).Key + "x=" + sortedFruits.ElementAt(1).Value[0] + ",y=" + sortedFruits.ElementAt(1).Value[1]);
        //Debug.Log(sortedFruits.ElementAt(2).Key + "x=" + sortedFruits.ElementAt(2).Value[0] + ",y=" + sortedFruits.ElementAt(2).Value[1]);
        //Debug.Log(sortedFruits.ElementAt(3).Key + "x=" + sortedFruits.ElementAt(3).Value[0] + ",y=" + sortedFruits.ElementAt(3).Value[1]);


        foreach (KeyValuePair<string, List<double>> item in sortedFruits)
        {
            Debug.Log(item.Key + "の座標は" + item.Value[0] + "," + item.Value[1]);
        }

        SceneManager.LoadScene("CookScene");
        // できればこうしたい
        //fruitsData = sortedFruits;
    }
}