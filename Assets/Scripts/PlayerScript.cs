using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//未実装：衝突、上下方向への移動（モーションは終わってて移動だけまだ）

public class PlayerScript : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;

    [SerializeField] GameObject GameSceneManager;
    [SerializeField] GameObject BarManager;
    GameSceneManagerScript gsm;
    PowerBarScript bar;

    [SerializeField] GameObject BirdMesh;

    //鳥の各パラメタ
    //[SerializeField][Header("鳥が進むはやさ")] float Speed;
    [SerializeField][Header("BarValueに対するX方向進行量")] float MoveWidthAmount;
    [SerializeField][Header("BarValueに対するY方向進行量")] float MoveHeightAmount;
    [SerializeField][Header("Straight/(Left,Right)アニメーション切り替え速度")] float CanStraight;
    [SerializeField][Header("Flight/Glideアニメーション切り替え速度")] float CanFlight;
    //[SerializeField][Header("鳥がGlideに自然に移り変わるための時間")] float ChangeAnimationTime;

    //フレーム間で保存するための変数で初期値は何もバー入力をしてないのと同じ0
    //float beforeRightPower = 0.0f;
    //float beforeLeftPower = 0.0f;
    
    float _powerOfDifference;//左右の差
    float _powerOfAverage;//左右の平均
    //private float _currentTime;//BarScriptと変数名ダブってる！！
    bool _isFirstTime = true;//スタート時はすぐにGlideモーションになすように

    bool _animFlag = false;
    float _animCount = 1f;
    bool _animFlight = false;
    float _animStraight = 0f;

    // 無敵フラグ
    bool _isInvincible = false;

    // ダメージ時点滅用変数
    bool _birdMeshActive = true;
    bool _isFlickering = false;
    int FlickerNumber = 10;
    int _flickerCount = 0;
    float FlickerIntervalTime = 0.075f;
    float _flickerIntervalCount = 0f;


    public void BirdMove(float leftValue, float rightValue) {
        var _moveX = leftValue - rightValue;
        var _moveY = (leftValue + rightValue) / 2;
        var _moveVec = new Vector3(_moveX * MoveWidthAmount, _moveY * MoveHeightAmount, 0f);
        
        m_Rigidbody.AddForce(_moveVec);
        _animCount = 0.1f;
        _animFlag = true;
        _animStraight += _moveVec.x;
        if (_moveVec.y >= CanFlight) {
            _animFlight = true;
        }
    }

   
    void Start()
    {
        gsm = GameSceneManager.GetComponent<GameSceneManagerScript>();
        bar = BarManager.GetComponent<PowerBarScript>();

        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
    }

    void Update()
    {
        if(gsm._gameStart)
        {
            // 初回処理
            if(_isFirstTime)
            {
                m_Rigidbody.isKinematic = false;
                m_Animator.SetBool ("GameStart", gsm._gameStart);
                BirdAnimationReset();
                _isFirstTime = false;
            }

            if (_animFlag) {
                if (_animCount < 0f) {
                    BirdAnimationX();
                    BirdAnimationY();
                    _animFlag = false;
                    _animCount = 0.8f;
                }
                _animCount -= Time.deltaTime;
            }
            else {
                if (_animCount < 0f) {
                    BirdAnimationReset();
                }
                _animCount -= Time.deltaTime;
            }
            

            // 点滅処理
            DamageFlicker();
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Obstacle") {
            if (!_isInvincible) {
                gsm.DecreaseElectricPowerByDamage();
                DamageFlickerStart();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Obstacle") {
            if (!_isInvincible) {
                gsm.DecreaseElectricPowerByDamage();
                DamageFlickerStart();
            }
        }

        if (other.gameObject.tag == "ElectricItem") {
            gsm.IncreaseElectricPower();
        }

        if (other.gameObject.tag == "PointItem") {
            var point = other.gameObject.GetComponent<PointItemScript>().Point;
            gsm.UpdatePointByItem(point);
        }
    }

    void BirdAnimationY() {
        if (_animFlight) {
            m_Animator.SetBool ("Glide", false);
            _animFlight = false;
        }
    }

    void BirdAnimationReset() {
        m_Animator.SetBool ("Left", false);
        m_Animator.SetBool ("Right", false);
        m_Animator.SetBool ("Glide", true);
    }

    void BirdAnimationX() {

        if (_animStraight < (-1)*CanStraight) {
            m_Animator.SetBool ("Left", true);
            m_Animator.SetBool ("Right", false);
        }
        if (_animStraight > CanStraight) {
            m_Animator.SetBool ("Left", false);
            m_Animator.SetBool ("Right", true);
        }
        
        _animStraight = 0f;
        
    }

    void DamageFlickerStart() {
        _isFlickering = true;
        _isInvincible = true;
        _flickerCount = 0;
        _flickerIntervalCount = 0;
    }

    void DamageFlicker() {
        if (_isFlickering) {
            if (_flickerIntervalCount >= FlickerIntervalTime) {
                _birdMeshActive = !_birdMeshActive;
                BirdMesh.SetActive(_birdMeshActive);
                _flickerIntervalCount = 0f;
                _flickerCount += 1;

                if (_flickerCount == FlickerNumber) {
                    _isFlickering = false;
                    _isInvincible = false;
                }
            }
            _flickerIntervalCount += Time.deltaTime;
        }
    }
    
}
