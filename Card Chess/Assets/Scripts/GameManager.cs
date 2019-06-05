using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardmanager;
    public PlayerManager player1;
    public PoolScript gamePool;
    public static GameManager instance = null;
    public List<GameObject> chessmenPrefabs;
    private List<GameObject> activeChessmen;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        boardmanager = GameObject.FindGameObjectWithTag("BoardManagerTag").GetComponent<BoardManager>();
        while (gamePool == null)
        {
            gamePool = GetComponent<PoolScript>();
        }
        //player1 = GetComponent<PlayerManager>();


        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        boardmanager.SetupScene();
        gamePool.InitCharPool();
        gamePool.RefreshPool();
        //player1.StartUp();

    }

    public void buyMinion(UnitManager unit)
    {
        boardmanager.buyMinion(unit);
    }



    //Update is called every frame.
    void Update()
    {
        boardmanager.UpdateSelection();
    }
}
