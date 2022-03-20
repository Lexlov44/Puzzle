using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MainScript : MonoBehaviour
{
    public GameObject partPuzzle;
    public GameObject EndPuzzle;
    public Sprite[] SpritesExample;

    public int countElements;
    public float rengeBElements;

    public bool isSwap = false;
    public string nameElement = "";
    public bool win;

    private GameObject[,] fullPuzzle;
    private List<Vector3> checkfullPuzzle;

    private float averagePos;


    void Start()
    {
        transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, transform.position.z);
        fullPuzzle = new GameObject[countElements/4, countElements/8];
        averagePos = partPuzzle.GetComponent<SpriteRenderer>().bounds.size.x;
        checkfullPuzzle = new List<Vector3>();
        CreatePuzzle();
    }
    private void CreatePuzzle()
    {
        int numberElemet = 0;
        Vector3 firstPosition = transform.position;
        for (int y = 0; y < countElements/8; y++)
        {
            for (int x = 0; x < countElements/4; x++)
            {
                fullPuzzle[x, y] = Instantiate(partPuzzle);
                //onePart.SetActive(false);
                fullPuzzle[x, y].transform.position = transform.position;
                fullPuzzle[x, y].GetComponent<SpriteRenderer>().sprite = SpritesExample[numberElemet];
                transform.position = new Vector3(transform.position.x + averagePos, transform.position.y, transform.position.z);
                fullPuzzle[x, y].name = fullPuzzle[x, y].name + numberElemet.ToString();
                checkfullPuzzle.Add(fullPuzzle[x, y].transform.position);
                numberElemet++;
            }
            transform.position = new Vector3(firstPosition.x, transform.position.y - averagePos, transform.position.z);
        }
        transform.position = firstPosition;

    }

    void Update()
    {
        if (isSwap == true)
        {
            SwapElements();
        }
    }

    public void CheckWin()
    {
        int countTransformPosition = 0;
        for (int y = 0; y < countElements / 8; y++)
        {
            for (int x = 0; x < countElements / 4; x++)
            {
                if (fullPuzzle[x, y].transform.position != checkfullPuzzle[countTransformPosition])
                {
                    win = false;
                    return;
                }
                countTransformPosition++;
            }
        }
        win = true;
    }

    private void SwapElements()
    {
        for (int y = 0; y < countElements / 8; y++)
        {
            for (int x = 0; x < countElements / 4; x++)
            {
                if (fullPuzzle[x, y].activeInHierarchy)
                {
                    fullPuzzle[x, y].SetActive(false);
                    fullPuzzle[x, y].transform.position = SwapOneElement(fullPuzzle[x, y].transform.position);
                }
            }            
        }

        SetActiveAll();
    }

    private void SetActiveAll()
    {
        for (int y = 0; y < countElements / 8; y++)
        {
            for (int x = 0; x < countElements / 4; x++)
            {
                fullPuzzle[x, y].SetActive(true);
                isSwap = false;
            }
        }
    }

    private Vector2 SwapOneElement(Vector2 newPosition)
    {
        List<int[]> timePuzzle = new List<int[]>();
        Vector2 timePosition = new Vector2();

        for (int y = 0; y < countElements / 8; y++)
        {
            for (int x = 0; x < countElements / 4; x++)
            {
                if (fullPuzzle[x, y].activeInHierarchy)
                {
                    timePuzzle.Add(new int[] { x, y });
                }
            }
        }
        if (timePuzzle.Count != 0)
        {
            int randomNumber = UnityEngine.Random.Range(0, timePuzzle.Count);
            int xNum = timePuzzle[randomNumber][0];
            int yNum = timePuzzle[randomNumber][1];

            fullPuzzle[xNum, yNum].SetActive(false);
            timePosition = fullPuzzle[xNum, yNum].transform.position;
            fullPuzzle[xNum, yNum].transform.position = newPosition;
        }
        return timePosition;
    }

    public void WinButt()
    {
        int countTransformPosition = 0;
        for (int y = 0; y < countElements / 8; y++)
        {
            for (int x = 0; x < countElements / 4; x++)
            {
                fullPuzzle[x, y].transform.position = checkfullPuzzle[countTransformPosition];
                countTransformPosition++;
            }
        }
    }
}
