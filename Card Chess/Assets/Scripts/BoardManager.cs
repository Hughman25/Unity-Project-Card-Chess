using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = .5f;

    private int selectionX = -1;
    private int selectionY = -1;

    private int[] ini_pos = new int[2];

    public List<GameObject> chessmenPrefabs;
    private List<GameObject> activeChessmen;
    private List<Vector3> boardPositions = new List<Vector3>();
    private List<Vector3> reservePositions = new List<Vector3>();
    private List<UnitManager> reserveUnits = new List<UnitManager>();

    private bool isMovingUnit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        UpdateSelection();
        // DrawChessboard();
    }

    public void UpdateSelection()
    {
        if (!Camera.main) { return; }

        RaycastHit hit;

        if (isMovingUnit)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f, LayerMask.GetMask("ChessPlane")))
            {
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
            if (selectionX >= 0 && selectionY >= 0)
            {
                DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1), true);

                //Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                //              Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            }
        }
        /**
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f, LayerMask.GetMask("ChessPlane")))
            {
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;
                Instantiate(chessmenPrefabs[0], (new Vector3(0, .5f, 1 * selectionY + .5f) + new Vector3(1 *selectionX + .5f, .5f, 0)), Quaternion.identity);
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
        }*/
    }

    //if isSelecting //select unit if reserver/board has unit,
    //when input ended, move selected unit to that location if possible

    void InitalizePositions()
    {
        boardPositions.Clear();

        for (int i = ini_pos[0]; i <= ini_pos[0] + 8; i++)
        {
            for (int j = ini_pos[1]; j <= ini_pos[1] + 8; j++)
            {
                boardPositions.Add(new Vector3(j, 0, i));
            }
        }

        int[] ini_res_pos = { ini_pos[0] - 3, ini_pos[1] - 2 };

        for (int i = ini_res_pos[0] + 8; i > ini_res_pos[0]; i--)
        {
            reservePositions.Add(new Vector3(ini_res_pos[1]+.5f, .75f, i+2.5f));
            reserveUnits.Add(null);
        }
        Debug.Log(reservePositions.Count);
    }

    void BoardSetup()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = ini_pos[0]; i <= ini_pos[0] + 8; i++)
        {
            Vector3 start = new Vector3(ini_pos[1], 0, i);
            DrawLine(start, start + widthLine, false);
            for (int j = ini_pos[1]; j <= ini_pos[1] + 8; j++)
            {
                start = new Vector3(j, 0, ini_pos[0]);
                DrawLine(start, start + heightLine, false);
            }
        }
    }

    void ReserveBoardSetup()
    {
        Vector3 widthLine = Vector3.right;
        Vector3 heightLine = Vector3.forward * 8;

        Vector3 start = new Vector3(ini_pos[1] - 2, 0, ini_pos[0]);
        DrawLine(start, start + heightLine, false);
        start = new Vector3(ini_pos[1] - 1, 0, ini_pos[0]);
        DrawLine(start, start + heightLine, false);

        for (int i = 2; i <= 10; i++)
        {
            start = new Vector3(ini_pos[1] - 2, 0, i);
            DrawLine(start, start + widthLine, false);
        }
    }

    public void SetupScene()
    {
        ini_pos[0] = 2;
        ini_pos[1] = 2;
        BoardSetup();
        ReserveBoardSetup();
        InitalizePositions();
        setMovingUnit(); //for debug
    }

    void DrawLine(Vector3 start, Vector3 end, bool flag)
    {
        Vector3[] temp = { start, end };
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.startWidth = .1f;
        lr.endWidth = .1f;
        lr.SetPositions(temp);
        if (flag)
        {
            GameObject.Destroy(myLine, .02f);
        }
    }


    private void SpawnMinion(int index, Vector3 positon, int round)
    {
        //activeChessmen.Add(SpawnMinion);
    }

    public void setMovingUnit()
    {
        isMovingUnit = !isMovingUnit;
    }

    private Vector3 GetTileCenter(int x, int z)
    {
        Vector3 origin = new Vector3(ini_pos[1], 0, ini_pos[2]);
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * z) + TILE_OFFSET;
        return origin;
    }
    public void buyMinion(UnitManager unit)
    {
        int pos = getOpenReserveSpot();
        //Debug.Log(pos);
        if (pos != -1)
        {
            reserveUnits[pos] = unit;
            Instantiate(unit, (reservePositions[pos]), Quaternion.identity);
        }
    }

    public int getOpenReserveSpot()
    {
        Debug.Log(reservePositions.Count);
        for (int i = 0; i < reservePositions.Count; i++)
        {
            if (reserveUnits[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
