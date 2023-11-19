using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance = new GridManager();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    int[] PowersOfTwo = { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };
    int[] AssignableValues = { 2, 4, 8 };

    public class GameSlot // ----------------- GameSlot Class
    {
        int number;
        bool occupied;

        public GameSlot(bool occupied)
        {
            if (occupied)
            {
                int index = UnityEngine.Random.Range(0, 10);
                number = (int)Mathf.Pow(2, index);
            }
            else
            {
                number = 0;
            }
        }

        public bool Occupied() { return occupied; }
        public void SetNumber(int _number) { number = _number; }
        public int GetNumber() { return number; }
    }

    List<List<GameSlot>> SlotMatrix = new List<List<GameSlot>>();
    GameObject MATRIX_GO;

    void Start() // -------------------- Start Method
    {
        MATRIX_GO = GameObject.FindGameObjectWithTag("matrix");

        SlotMatrix.Add(new List<GameSlot>() { new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false) });
        SlotMatrix.Add(new List<GameSlot>() { new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false) });
        SlotMatrix.Add(new List<GameSlot>() { new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false) });
        SlotMatrix.Add(new List<GameSlot>() { new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false) });
        SlotMatrix.Add(new List<GameSlot>() { new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false), new GameSlot(false) });

        GenerateRandomMap();
        AssignSlots();
    }

    void Update() // ------------------- Update Method
    {

    }

    public GameSlot GetSlot(int row,int col)
    {
        //TO DO: Exception throw...
        return SlotMatrix[row][col];
    }

    public void SetSlot(int row, int col, GameSlot slot)
    {
        if (row <= 5 && col <= 5)
            SlotMatrix[row][col] = slot;
        else
        {
            throw new InvalidOperationException("OUTSIDE OF MATRIX");
        }
    }

    public void GenerateRandomMap()
    {
        for (int i = 0; i < 10; i++)// 10 is preset number for slots to fill
        {
            int index = UnityEngine.Random.Range(0, 3); // random value for powers of 2 ===> just (2,4,8,16)
            int rand_row = UnityEngine.Random.Range(0, 4);
            int rand_col = UnityEngine.Random.Range(0, 4);
            if (!SlotMatrix[rand_row][rand_col].Occupied()) // Arrange numbers of respective slots * if they're not occupied *
            {
                SlotMatrix[rand_row][rand_col].SetNumber(PowersOfTwo[index]);
            }
            else
            {
                continue;
            }
        }
    }

    public void AssignSlots()
    {
        int counter = 0;

        for (int i = 0; i < SlotMatrix.Count; i++)
        {
            for (int j = 0; j < SlotMatrix[i].Count; j++)
            {
                if (SlotMatrix[i][j].GetNumber() == 0)
                {
                    MATRIX_GO.transform.GetChild(counter).GetComponent<Slot>().SetValues(false, SlotMatrix[i][j].GetNumber());
                    counter++;
                }
                else
                {
                    MATRIX_GO.transform.GetChild(counter).GetComponent<Slot>().SetValues(true, SlotMatrix[i][j].GetNumber());
                    MATRIX_GO.transform.GetChild(counter).GetComponent<Slot>().ChangePNG(SlotMatrix[i][j].GetNumber());
                    counter++;
                }
                
            }
        }
    }
}
