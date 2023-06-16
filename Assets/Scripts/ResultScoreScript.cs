using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScoreScript : MonoBehaviour
{
    //[SerializeField] GameObject GameSceneManager;
    //GameSceneManagerScript gsm;
    [Header("飛行距離")] public Text Distance;
    [Header("ポイント")] public Text Point;
    int distance;
    int point;

    public bool _toTitleScene = false;

    void Start()
    {
        //gsm = GameSceneManager.GetComponent<GameSceneManagerScript>();
        distance = GameSceneManagerScript.distanceInt;
        point = GameSceneManagerScript.pointInt;
        Distance.text = distance.ToString();
        Point.text = point.ToString();
    }

    void Update()
    {
        ToTitleScene();
    }

    void ToTitleScene()
    {
        if(Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.Return))
        {
            _toTitleScene = true;
            SceneManager.LoadScene("TitleScene");
        }
    }
}
