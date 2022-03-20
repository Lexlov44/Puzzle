using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickElement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0))
        {
            string newName = GameObject.Find("StartPuzzle").GetComponent<MainScript>().nameElement;
            if (newName == "")
            {
                GameObject.Find("StartPuzzle").GetComponent<MainScript>().nameElement = gameObject.name;
            }
            else
            {
                Vector2 timePosition = gameObject.transform.position;
                gameObject.transform.position = GameObject.Find(newName).GetComponent<Transform>().position;
                GameObject.Find(newName).GetComponent<Transform>().position = timePosition;
                GameObject.Find("StartPuzzle").GetComponent<MainScript>().nameElement = "";
            }
            GameObject.Find("StartPuzzle").GetComponent<MainScript>().CheckWin();
        }
    }
}
