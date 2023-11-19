using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Slot : MonoBehaviour
{
    int number, last_number_val;
    bool occupied = false;

    [SerializeField] List<GameObject> NumberMeshes = new List<GameObject>();
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void SetValues(bool occ, int val)
    {
        if (occ == true)
        {
            occupied = true;
            number = val;
        }
        else
        {
            number = 0;
        }
    }

    public int GetNumber()
    {
        return number;
    }

    public void ChangePNG(int number)
    {
        foreach (GameObject item in NumberMeshes)
        {
            if (item.activeInHierarchy)
            {
                item.transform.DOScale(0.01f, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    item.SetActive(false);
                });

            }

        }
        switch (number)
        {
            case 2:
                NumberMeshes[0].SetActive(true);
                NumberMeshes[0].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 4:
                NumberMeshes[1].SetActive(true);
                NumberMeshes[1].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 8:
                NumberMeshes[2].SetActive(true);
                NumberMeshes[2].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 16:
                NumberMeshes[3].SetActive(true);
                NumberMeshes[3].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 32:
                NumberMeshes[4].SetActive(true);
                NumberMeshes[4].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 64:
                NumberMeshes[5].SetActive(true);
                NumberMeshes[5].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 128:
                NumberMeshes[6].SetActive(true);
                NumberMeshes[6].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 256:
                NumberMeshes[7].SetActive(true);
                NumberMeshes[7].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 512:
                NumberMeshes[8].SetActive(true);
                NumberMeshes[8].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 1024:
                NumberMeshes[9].SetActive(true);
                NumberMeshes[9].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            case 2048:
                NumberMeshes[10].SetActive(true);
                NumberMeshes[10].transform.DOScale(0.08f, 0.2f).SetEase(Ease.Linear);
                break;

            default:
                break;
        }
    }

    public void ClearSlot()
    {
        last_number_val = number;
        occupied = false;
        foreach (GameObject item in NumberMeshes)
        {
            if (item.activeInHierarchy)
            {
                item.transform.DOScale(0.01f, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    item.SetActive(false);
                });
                
            }
            
        }
    }

    public void RefillSlot()
    {
        number = last_number_val;
        occupied = true;
        ChangePNG(number);
    }

    public bool isOccupied()
    {
        return occupied;
    }
}
