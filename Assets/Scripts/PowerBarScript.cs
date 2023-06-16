using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//未実装：PQのキーでの受け取り
public class PowerBarScript : MonoBehaviour
{
    // PowerBarのGameObject
    [SerializeField] GameObject PowerBarWingLeft;
    [SerializeField] GameObject PowerBarWingRight;

    // GameSceneManager
    [SerializeField] GameObject GameSceneManager;
    GameSceneManagerScript gameSceneManager;

    // Bird
    [SerializeField] GameObject Player;
    PlayerScript player;

    //他からの参照不可。バーの値(確認用にシリアライズフィールドにしてます)
    [SerializeField] float _right = 0.0f;
    [SerializeField] float _left = 0.0f;
    //他からの参照可能でこれがバーからの返り値、初期値はバー入力無しと等しい0
    public float _rightPower = 0.0f;
    public float _leftPower = 0.0f;
    // ゲージ増減量の正負
    float _rightPowerSign = 1.0f;
    float _leftPowerSign = 1.0f;
    
    [Header("押した時間とキーの強さの間のパラメタ")] public float KeyValue;
    [Header("キーの強さが自然に減少する時のパラメタ")] public float KeyDownPower;
    
    //フレーム間でのバーの返り値の数値の保存をするためのものたち
    //private float _currentTime = 0.0f;
    private float _rightLastTime = 0.0f;
    private float _leftLastTime = 0.0f;
    [Header("バー入力何秒されないと破棄されるか")] public float BarLimitTime;
    //BarLimitTimeは結構長いほうがいいかも

    // PowerBarの位置下限
    [SerializeField] float PowerBarWingLowerPosY = -210f;
    // PowerBarの位置上限
    [SerializeField] float PowerBarWingUpperPosY = 175f;
    // PowerBarの長さ
    float powerBarWingLength;
    

    
    void Start()
    {
        powerBarWingLength = PowerBarWingUpperPosY - PowerBarWingLowerPosY;

        gameSceneManager = GameSceneManager.GetComponent<GameSceneManagerScript>();
        player = Player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        //バー入力保持のための現在の時間計測
        //_currentTime += Time.deltaTime;
        
        /*
        //基本は徐々に左右の力が下がっていく（キーを押してる間も）
        if(_right > 0)
        {
            _right -= KeyDownPower * Time.deltaTime;
        }
        if(_left > 0)
        {
            _left -= KeyDownPower * Time.deltaTime;
        }
        */
        
        if (gameSceneManager._gameStart) {

            // Barの位置リセット
            BarReset();

            //キー入力されているとき
            if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.P))
            {
                _right += _rightPowerSign * KeyValue * Time.deltaTime;
                // バーの端に到達時に増減方向を逆転
                if((_right <= 0f && _rightPowerSign < 0f) || (_right >= 1f && _rightPowerSign > 0f))
                {
                    //_right = 0.0f;
                    _rightPowerSign *= -1f;
                }
            }
            if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
            {
                _left += _leftPowerSign * KeyValue * Time.deltaTime;
                if((_left <= 0f && _leftPowerSign < 0f) || (_left >= 1f && _leftPowerSign > 0f))
                {
                    //_left = 0.0f;
                    _leftPowerSign *= -1f;
                }
            }
            
            //キーが離された瞬間、その時の値をpublicのものに代入
            if (Input.GetKeyUp (KeyCode.RightArrow))
            {
                //まだ左を押してからBarLimitTime経過してない
                //if(_leftLastTime < BarLimitTime)
                //{
                    //_leftPower = _left;
                    _rightPower = _right;
                //}
                /*
                //左は前回押した時から全然押されてない
                else
                {
                    _leftPower = 0.0f;
                    _rightPower = _right;

                    _right = 0f;
                }
                //リセット
                //_currentTime = 0.0f;
                */

                player.BirdMove(0f, _rightPower);
                _rightPower = 0f;

            }

            if (Input.GetKeyUp (KeyCode.LeftArrow))
            {
                //まだ右を押してからBarLimitTime経過してない
                //if(_rightLastTime + BarLimitTime > _currentTime)
                //{
                    _leftPower = _left;
                    //_rightPower = _right;
                //}
                /*
                //右は前回押した時から全然押されてない
                else
                {
                    _leftPower = _left;
                    _rightPower = 0.0f;

                    _right = 0.0f;
                }
                //リセット
                //_currentTime = 0.0f;
                */

                player.BirdMove(_leftPower, 0f);
                _leftPower = 0f;
            }

            // PowerBarWingの位置変更
            ChangeBarPosition();
        }
    }

    // Barの位置リセット
    void BarReset() {
        // 初期化前のキー入力開始で値送信/値初期化
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (_left != 0f) {
                _leftPower = 0f;
                _left = 0f;
                _leftLastTime = 0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (_right != 0f) { 
                //_rightPower = 0f;
                _right = 0f;
                _rightLastTime = 0f;
            }
        }

        // BarWing上昇後, キー入力がない場合にBarLimitTime経過後にWing位置リセット
        if (!Input.GetKey(KeyCode.LeftArrow)) {
            if (_left != 0f) {
                _leftLastTime += Time.deltaTime;

                if (_leftLastTime > BarLimitTime) {
                    //player.BirdMove(_leftPower, _rightPower);
                    //_leftPower = 0f;
                    _left = 0f;
                    _leftLastTime = 0f;
                }
            }
        }
        if (!Input.GetKey(KeyCode.RightArrow)) {
            if (_right != 0f) {
                _rightLastTime += Time.deltaTime;

                if (_rightLastTime > BarLimitTime) {
                    //player.BirdMove(_leftPower, _rightPower);
                    _rightPower = 0f;
                    _right = 0f;
                    _rightLastTime = 0f;
                }
            }
        }
    }

   // PowerBarWingの位置変更
   void ChangeBarPosition() {
        var _leftPos = PowerBarWingLeft.transform.localPosition;
        var _rightPos = PowerBarWingRight.transform.localPosition;
        _leftPos.y = PowerBarWingLowerPosY + powerBarWingLength * _left;
        _rightPos.y = PowerBarWingLowerPosY + powerBarWingLength * _right;
        PowerBarWingLeft.transform.localPosition = _leftPos;
        PowerBarWingRight.transform.localPosition = _rightPos;
   }
}
