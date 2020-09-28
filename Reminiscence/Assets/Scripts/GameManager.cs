using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	public int currentLevel = 0;
	public bool isLobby = false;

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
    [HideInInspector]
    public Vector3 playerLastKnownPos;

    void Awake()
    {
		if(GameManager.instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		}
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            this.FinishLevel();
        }
    }

    public void FinishLevel()
    {
		//this.gameStateMachine.SetTrigger("Lvl Finished");
		if (isLobby)
		{
			GoToNextLevel();
			isLobby = false;
		}else{
			GoToLobby();
		}
    }

    public void StartLevel()
    {
        //this.gameStateMachine.SetTrigger("Lvl Starting");
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

	public void GoToLobby(){
		isLobby = true;
		SceneManager.LoadScene(1);
		GameManager.instance.getLog().Post(GameManager.instance.gameObject, (uint)AkCallbackType.AK_EndOfEvent, CallbackFunction);
	}

	public void GoToNextLevel(){
		currentLevel++;
		SceneManager.LoadScene(currentLevel + 1);
		GameManager.instance.ikMotor.pointToReach.transform.position = GameManager.instance.PTRStartPos;
	}

	public void LoadScene(int id){
		SceneManager.LoadScene(id);
	}

	public void CallbackFunction(object in_cookie, AkCallbackType in_type, object in_info)
	{
		isLobby = false;
		GoToNextLevel();
		
	}
}
