using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour
{
    [SerializeField] GameObject GameSceneManager;
    GameSceneManagerScript gameSceneManager;

    [SerializeField] Vector3 CreatePosition = new Vector3(0f, 0f, 0f);

    [SerializeField] GameObject NowStage;
    [SerializeField] Vector3 CreateStagePosition = new Vector3(0f, 0f, 0f);
    [SerializeField] GameObject StageLoop;
    [SerializeField] GameObject[] Obstacles;
    public GameObject[] MoveObstacles;
    public bool NotMoveObstacle = false;
    public GameObject[] AppearItems;

    [SerializeField] float CreateObstacleInterval = 3f;
    //[SerializeField] float CreatePointItemInterval = 3f;
    float _createObstacleTimer = 0f;
    //float _createPointItemTimer = 0f;
    float CreateCarInterval = 3.5f;
    float _createCarTimer = -2f;

    [SerializeField] bool _isTown = false;
    [SerializeField] GameObject[] Cars;

    public float BatteryApperRate = 0.5f;

    public void DeleteRightObject(GameObject rightObj) {
        gameSceneManager._rightObjects.Remove(rightObj);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameSceneManager = GameSceneManager.GetComponent<GameSceneManagerScript>();

        CreateStartObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (_createObstacleTimer >= CreateObstacleInterval) {
            CreateObstacle();
            _createObstacleTimer = 0f;
        }
        _createObstacleTimer += Time.deltaTime;

        //if (_createPointItemTimer >= CreatePointItemInterval) {
        //    CreatePointItem();
        //    _createPointItemTimer = 0f;
        //}

        if (_isTown) {
            if (_createCarTimer >= CreateCarInterval) {
                CreateCars();
                _createCarTimer = 0f;
            }
            _createCarTimer += Time.deltaTime;
        }

        if (NowStage.transform.position.z < CreateStagePosition.z) {
            NowStage = Instantiate (StageLoop, CreatePosition, Quaternion.identity);
            NowStage.GetComponent<StageObjectScript>().ParentObject = this.gameObject;
            var _rightObj = NowStage.transform.GetChild(0).gameObject;
            gameSceneManager._rightObjects.Add(_rightObj);
        }
    }

    void CreateObstacle() {
        var _obstacle = Obstacles[Random.Range(0, Obstacles.Length)];
        var obj = Instantiate (_obstacle, CreatePosition, Quaternion.Euler(_obstacle.transform.eulerAngles));
        obj.GetComponent<StageObjectScript>().ParentObject = this.gameObject;
    }

    void CreatePointItem() {
        var _pointItem = AppearItems[Random.Range(0, AppearItems.Length)];
        Instantiate (_pointItem, CreatePosition, Quaternion.Euler(_pointItem.transform.eulerAngles));
    }

    void CreateCars() {
        var _obstacle = Cars[Random.Range(0, Cars.Length)];
        var obj = Instantiate (_obstacle, CreatePosition, Quaternion.Euler(_obstacle.transform.eulerAngles));
        obj.GetComponent<StageObjectScript>().isMachine = true;
        obj.GetComponent<StageObjectScript>().ParentObject = this.gameObject;
    }

    void CreateStartObstacle() {
        for (float zPos = 23f; zPos < CreatePosition.z; zPos += 30f) {
            var _obstacle = Obstacles[Random.Range(0, Obstacles.Length)];
            var obj = Instantiate (_obstacle, new Vector3(CreatePosition.x, CreatePosition.y, zPos), Quaternion.Euler(_obstacle.transform.eulerAngles));
            obj.GetComponent<StageObjectScript>()._isMoving = false;
            obj.GetComponent<StageObjectScript>().ParentObject = this.gameObject;
            gameSceneManager.startCreateObstacles.Add(obj.GetComponent<StageObjectScript>());
        }
    }

}
