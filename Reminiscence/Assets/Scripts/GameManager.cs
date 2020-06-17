using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Animator gameStateMachine;
    public IK ikMotor;

    private AK.Wwise.Event logEvent;

    public Vector3 playerStartPos ;
    public Vector3 PTRStartPos;
    public GameObject player;
    [HideInInspector]
    public Vector3 playerPos;
    public MatrixTransposer newMatrix;
    public Vector3 sensitivity;
    public Vector3 chosenTranslate;

    void Awake()
    {
        GameManager.instance = this;
        DontDestroyOnLoad(this.gameObject);
        playerStartPos = new Vector3(0, 0, 17);

    }


    // Start is called before the first frame update
    void Start()
    {
       //this.StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && playerStartPos != null)
        {
            playerPos = player.transform.position;
            this.chosenTranslate = playerPos - playerStartPos;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.FinishLevel();
        }
    }

    public void FinishLevel()
    {
        this.gameStateMachine.SetTrigger("Lvl Finished");
    }

    public void StartLevel()
    {
        this.gameStateMachine.SetTrigger("Lvl Starting");
    }


    public void setLog(AK.Wwise.Event logEvent)
    {
        this.logEvent = logEvent;
    }
    public AK.Wwise.Event getLog()
    {
        return this.logEvent;
    }

    public void setNewMatrix(MatrixTransposer mat)
    {
        GameManager.instance.newMatrix = mat;
        GameManager.instance.ikMotor.pointToReach.transform.Translate(new Vector3(GameManager.instance.newMatrix.TranslatePosition(GameManager.instance.chosenTranslate).x * GameManager.instance.sensitivity.x,
                                                                                  GameManager.instance.newMatrix.TranslatePosition(GameManager.instance.chosenTranslate).y * GameManager.instance.sensitivity.y,
                                                                                  GameManager.instance.newMatrix.TranslatePosition(GameManager.instance.chosenTranslate).z * GameManager.instance.sensitivity.z));
        
    }
}
