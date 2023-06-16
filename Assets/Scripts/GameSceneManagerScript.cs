using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
未実装：ポイント飛距離のデータ、障害物のランダム生成、ＵＩ処理
*/

public class GameSceneManagerScript : MonoBehaviour
{
    [SerializeField] GameObject DimensionView;
    [SerializeField] Sprite ModeTwoSprite;
    [SerializeField] Sprite ModeThreeSprite;
    Image _dimentionViewImage;

    public bool _gameStart = false;
    public bool _gameOver = false;
    private float _point = 0.0f;//ポイント
    private float _distance = 0.0f;//飛行距離
    [Header("移動距離増加パラメタ")] public float increaseDistance;//飛行距離の増加量
    [Header("ポイント増加パラメタ")] public float increasePoint;//時間によるポイントの増加
    //電気量はリザルトシーンへ行かないため、インスペクターで巨大な数に設定中
    [Header("電気量")] public float electricPower;
    [System.NonSerialized] public float _electricPower = 1f;
    [SerializeField] float IncreaseElectricPowerRatio = 0.2f;
    [SerializeField] float DecreaseElectricPowerRatio = 0.0001f;
    [SerializeField] float DecreaseElectricPowerRatioByDamage = 0.2f;
    

    [Header("飛行距離")] public Text Distance;
    [Header("ポイント")] public Text Point;
    //表示に使うよう＆リザルトからの参照用
    public static int distanceInt;
    public static int pointInt;
    //タイマー
    [Header("カウントダウン")] public Text CountText;
    float countdown = 6f;
    int count;

    //視点切り替え
    bool _is2dView = false;//trueなら2D表示
    //[Header("3Dカメラ")] public GameObject camera3D;  
    [Header("2Dカメラ")] public GameObject camera2D;

    [Header("オブジェクト移動速度(基本速度)")] public float DefaultObjectMoveSpeed = -1.0f;
    [Header("開始ステージオブジェクト")] [SerializeField] GameObject StartStage;
    [SerializeField] GameObject LoopStartStage;
    
    public List<GameObject> _rightObjects = new List<GameObject>();
    public List<StageObjectScript> startCreateObstacles = new List<StageObjectScript>();

    public void IncreaseElectricPower() {
        _electricPower += IncreaseElectricPowerRatio;
        if (_electricPower > 1f) {
            _electricPower = 1f;
        }
    }

    public void DecreaseElectricPowerByDamage() {
        _electricPower -= DecreaseElectricPowerRatioByDamage;
    }

    public void UpdatePointByItem(float itemPoint) {
        _point += itemPoint;
        Point.text = ((int)_point).ToString() + " pt";
    }

    public void UpdatePoint()
    {
        _point += increasePoint * Time.deltaTime;
        //アイテムとったら増加させる
        pointInt = (int)_point;
        Point.text = pointInt.ToString() + " pt";
    }


    void Start()
    {
        _dimentionViewImage = DimensionView.GetComponent<Image>();

        _rightObjects.Add(StartStage.transform.GetChild(0).gameObject);
        _rightObjects.Add(LoopStartStage.transform.GetChild(0).gameObject);
    }

    
    void Update()
    {
        Countdown();
        ChangeCamera();
        if(_gameStart)
        {     
            UpdateDistance();
            UpdatePoint();

            //常に電気量減少
            //electricPower--;
            DecreaseElectricPower();
        
            //ゲームオーバーになる条件
            if(_electricPower <= 0.0f){
                _gameOver = true;   
            }
            
            if(_gameOver){
                ChangeResultScene();   
            }
        }       
    }

    void Countdown()
    {
        if(countdown >= 1f)
        {
            countdown -= Time.deltaTime;
            count = (int)countdown;
            CountText.text = count.ToString();
        }
        if(countdown < 1f)
        {
            if (!_gameStart) {
                StartStage.GetComponent<StageObjectScript>()._isMoving = true;
                LoopStartStage.GetComponent<StageObjectScript>()._isMoving = true;
                foreach(var script in startCreateObstacles) {
                    script._isMoving = true;
                }
            }
            CountText.text = "";
            _gameStart = true;
        }  
    }

    void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _is2dView = !_is2dView;
            camera2D.SetActive(_is2dView);

            if (_is2dView) {
                foreach(var obj in _rightObjects) {
                    obj.SetActive(false);
                }
                _dimentionViewImage.sprite = ModeTwoSprite;
            }
            else {
                foreach(var obj in _rightObjects) {
                    obj.SetActive(true);
                }
                _dimentionViewImage.sprite = ModeThreeSprite;
            }
        }        
    }
      
    void ChangeResultScene()
    {
        DataScript.Distance = (int)_distance;
        DataScript.Point = (int)_point;
        SceneManager.LoadScene("ResultScene");
    }

    void UpdateDistance()
    {
        _distance += increaseDistance * Time.deltaTime;
        distanceInt = (int)_distance;
        Distance.text = distanceInt.ToString() + " m";
    }

    void DecreaseElectricPower() {
        _electricPower -= DecreaseElectricPowerRatio;
    }

    
}
