using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Animator gameStateMachine;
    public IK ikMotor;

    private AK.Wwise.Event logEvent;

    void Awake()
    {
        GameManager.instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        //this.StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
