using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public Rigidbody2D PlayerA, PlayerB;

    private Rigidbody2D[] rbA, rbB;

    Vector2 p, start, end, force;

    private int turn,turnA,turnB;

    private bool isOdd,isInit;

    private Vector3 startA,startB;

    public enum State {
        playerTurnInit,
        playerTurnSet,
        playerTurnShoot,
        playerEndTurn,
        EndGame
    }

    public State state;

//    public State state;
//
//    IEnumerator playerATurn() {
//        Debug.Log("playerATurn: Enter");
//        while (state == State.playerATurn) {
//            yield return 0;
//        }
//        Debug.Log("playerATurn: Exit");
//        NextState();
//    }
//
//    IEnumerator WalkState () {
//        Debug.Log("playerBTurn: Enter");
//        while (state == State.playerBTurn) {
//            yield return 0;
//        }
//        Debug.Log("playerBTurn: Exit");
//        NextState();
//    }
//
//    void NextState(){
//        if(State
//    }

    void Start()
    {
        turn = turnA = turnB = 0;

        startA = new Vector3(0f, 9.8f, 0f);
        startB = new Vector3(0f, -9.8f, 0f);

        rbA = new Rigidbody2D[12];
        rbB = new Rigidbody2D[12];

        state = State.playerTurnInit;
        //StartCoroutine(GameLoop());
    }

    void Update()
    {
        p = new Vector2(0, 0);

        if (Input.GetMouseButtonDown(0) && state == State.playerTurnInit)
        {
            isInit = false;
          
            if (rbA[turnA] == null && !isOdd && turnA < 12)
            {
                rbA[turnA] = Instantiate(PlayerA);
                rbA[turnA].transform.position = startA;
            }
            else
            {
                Debug.Log("Player A out of turns:");
            }
               
            if (rbB[turnB] == null && isOdd && turnB < 12)
            {
                rbB[turnB] = Instantiate(PlayerB);
                rbB[turnB].transform.position = startB;
            }
            state = State.playerTurnSet;

        
        }

        if (state == State.playerTurnSet)
        {
            state = State.playerTurnShoot;
            StartCoroutine(Pause());
        }

        if (Input.GetMouseButtonDown(0) && state == State.playerTurnShoot)
        {
            p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            start = p;
            Debug.Log(p);
            
        }

        if (Input.GetMouseButtonUp(0) && state == State.playerTurnShoot && isInit)
        {
            if (!isOdd)
            {
                end = p;
                force = start - end;
                rbA[turnA].AddForce(force * 100);
                state = State.playerEndTurn;
                if(turnA<11)
                turnA++;
            }
            if (isOdd)
            {
                end = p;
                force = start - end;
                rbB[turnB].AddForce(force * 100);
                state = State.playerEndTurn;
                if(turnB<11)
                turnB++;
            }
        }

        if (state == State.playerEndTurn)
        {
            isOdd = !isOdd;
            state = State.playerTurnInit;
            Debug.Log("turnA " + turnA + " : turnB " + turnB);
        }


    }
        

    bool IsOdd(int value)
    {
        return value % 2 != 0;
    }

    private IEnumerator Pause() {
        yield return new WaitForSeconds(.1f);
        isInit = true;

    }

    void takeTurn(int turnNumber,Rigidbody2D player,Rigidbody2D[] peices,Vector3 paStart)
    {
        Debug.LogError("in takeTurn");
        peices[turnNumber] = Instantiate(player);
        peices[turnNumber].transform.position = paStart;
        StartCoroutine(TakeShot(turnNumber,peices));
    }

    private IEnumerator GameLoop() {

        Debug.LogError("in GameLoop");

        while (true)
        {
        
//            if (!IsOdd(turn))
//            {
//                takeTurn(turnA, PlayerA, rbA, startA);
//                turn++;
//                turnA++;
//            }
//            else
//            {
//                takeTurn(turnB, PlayerB, rbB, startB);
//                turn++;
//                turnB++;
//            }

            yield return null;
        }
    }

    private IEnumerator TakeShot(int turnNumber, Rigidbody2D[] peices) 
    {
        Vector2 p = new Vector2(0f,0f);
        while (true)
        {
//            WaitForMouse();
////            if (Input.GetMouseButtonDown(0))
//                p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//                start = p;
//            if (Input.GetMouseButtonUp(0))
//            {
//                end = p;
//                force = start - end;
//                peices[turnNumber].AddForce(force*100);
//            }
            yield return null;
        }
    }

//    IEnumerator WaitForMouseDown(System.Action<Vector2> callback)
//    {
//        while (!Input.GetMouseButtonDown(0))
//            yield return null;
//
//        callback(Input.GetMouseButtonDown(0));
//    }

//        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        //p.z = 0f;
//        //Debug.Log(p);
//        Debug.Log(rbA.Length);
//        //if (Vector3.Distance(rb[0].position,p) < 2f)
//        {
//            if (Input.GetMouseButtonDown(0))
//                start = p;
//            if (Input.GetMouseButtonUp(0))
//            {
//                end = p;
//                force = start - end;
//                rbA[0].AddForce(force*100);
//            }
//        }


    void FixedUpdate()
    {
        
    }


//IEnumerator Start()
//{
//    while (true)
//    {
//        // don't do anything when we're standing still
//        while (!changePlayer)
//            yield return null;
//
//        // we're now starting to move
//
//        // play the accelerate
//            TakeShot(); //OneShot
//
//        // wait for the sound to finish
//        yield return new WaitForSeconds(playSound_1.length));
//
//        // play the main engine loop
//        playSound_2; //Loop
//
//        // keep looping until we're stopping
//        while (changePlayer)
//            yield return null;
//
//        // play the decelerate
//        playSound_3; // OneShot
//
//        // the while true loop will now jump back
//        // to the start, and the coroutine will wait
//        // for the car to start moving again
//    }
//}



    
//{
//
//    private Vector3 screenPoint;
//    private Vector3 offset;
//
//    void OnMouseDown()
//    {
//        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
//
//        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
//
//    }
//
//    void OnMouseDrag()
//    {
//        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
//
//        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
//        transform.position = curPosition;
//
//    }
//
//}
}
