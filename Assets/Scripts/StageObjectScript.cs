using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObjectScript : MonoBehaviour
{
    public bool _isMoving = true;

    public Vector3 CreatePosition = new Vector3(0f, 0f, 0f);
    
    [SerializeField] Vector3 MoveDirection = new Vector3(0f, 0f, 1f);
    public float MoveSpeed = -4f;

    [SerializeField] Vector3 DestroyPosition = new Vector3(0f, 0f, -3f);

    [SerializeField] bool MoveSuddenly = false;
    [SerializeField] float MoveStartPosZ = 0f;
    public Vector3 MoveSuddenlyDirection = new Vector3(1f, 0f, 0f);
    [SerializeField] float MoveSuddenlySpeed = 2f;

    public GameObject ParentObject;

    [SerializeField] bool isFixedObstacle = true;

    [SerializeField] bool itemAppear = false;
    [SerializeField] List<Vector2> itemPosition = new List<Vector2>();

    public bool isMachine = false;
    public bool isSmall = false;
    public bool isMedium = false;
    public bool isBig = false;

    GameObject Item = null;
    GameObject MoveObstacle = null;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag != "PointItem" && this.gameObject.tag != "ElectricItem") {
            var _pos = this.transform.position;
            var _posMoved = new Vector3(CreatePosition.x, CreatePosition.y, _pos.z);
            if (!isFixedObstacle) {
                _posMoved = new Vector3(Random.Range(-3.5f, 3.5f), CreatePosition.y, _pos.z);
            }
            if (isMachine) {
                if (Random.value <= 0.5f) {
                    _posMoved = new Vector3(1.5f, CreatePosition.y, -5f);
                    transform.rotation = Quaternion.Euler(0f, 180f, 90f); 
                    MoveSpeed = 2f;
                }
                else {
                    _posMoved = new Vector3(-1.5f, CreatePosition.y, _pos.z);
                    transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                    MoveSpeed = -6f;
                }
            }
            if (isSmall) {
                if (Random.value <= 0.5f) {
                    _posMoved = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-1f, 4f), -5f);
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f); 
                    MoveSpeed = 2f;
                }
                else {
                    _posMoved = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-1f, 4f), _pos.z);
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
                    MoveSpeed = -6f;
                }
            }
            if (isMedium) {
                if (Random.value <= 0.5f) {
                    _posMoved = new Vector3(Random.Range(-2f, 2f), Random.Range(-1.2f, 3f), -5f);
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f); 
                    MoveSpeed = 2f;
                }
                else {
                    _posMoved = new Vector3(Random.Range(-2f, 2f), Random.Range(-1.2f, 3f), _pos.z);
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
                    MoveSpeed = -6f;
                }
            }
            if (isBig) {
                if (Random.value <= 0.5f) {
                    _posMoved = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 2.3f), -5f);
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f); 
                    MoveSpeed = 2f;
                }
                else {
                    _posMoved = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 2.3f), _pos.z);
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
                    MoveSpeed = -6f;
                }
            }
            this.transform.position = _posMoved;
        }

        if (itemAppear) {
            if (Random.Range(0f, 1f) <= 0.8f) {
                var _pointItem = ParentObject.GetComponent<StageScript>().AppearItems[0];
                if (Random.value <= ParentObject.GetComponent<StageScript>().BatteryApperRate) {
                    _pointItem = ParentObject.GetComponent<StageScript>().AppearItems[1];
                }
                var _randomPos = itemPosition[Random.Range(0, itemPosition.Count)];
                var _createPos = new Vector3(_randomPos.x, _randomPos.y, this.transform.position.z);
                Item = Instantiate (_pointItem, _createPos, Quaternion.Euler(_pointItem.transform.eulerAngles));
                Item.transform.localScale = _pointItem.transform.localScale;
            }

            if (Random.Range(0f, 1f) <= 0.95f) {
                if (!ParentObject.GetComponent<StageScript>().NotMoveObstacle) {
                    var _obstacle = ParentObject.GetComponent<StageScript>().MoveObstacles[Random.Range(0, ParentObject.GetComponent<StageScript>().MoveObstacles.Length)];
                    var _createPos = new Vector3(0f, 0f, this.transform.position.z);
                    MoveObstacle = Instantiate (_obstacle, _createPos, Quaternion.Euler(_obstacle.transform.eulerAngles));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Item != null) {
            Item.GetComponent<StageObjectScript>()._isMoving = _isMoving;
        }
        if (MoveObstacle != null) {
            MoveObstacle.GetComponent<StageObjectScript>()._isMoving = _isMoving;
        }

        if (_isMoving) {
            var _pos = this.transform.position;
            var _movePos = MoveDirection * MoveSpeed * Time.deltaTime;

            if (MoveSuddenly) {
                if (this.transform.position.z <= MoveStartPosZ) {
                    _movePos += MoveSuddenlyDirection * MoveSuddenlySpeed * Time.deltaTime;
                }
            }

            this.transform.position = _pos + _movePos;
        }

        if (this.transform.position.z <= DestroyPosition.z) {
            if (this.gameObject.tag == "Stage") {
                var _rightObj = this.transform.GetChild(0).gameObject;
                ParentObject.GetComponent<StageScript>().DeleteRightObject(_rightObj);
            }
            Destroy(this.gameObject);
        }

        if (this.transform.position.z > 500f) {
            Destroy(this.gameObject);
        }
    }
}
