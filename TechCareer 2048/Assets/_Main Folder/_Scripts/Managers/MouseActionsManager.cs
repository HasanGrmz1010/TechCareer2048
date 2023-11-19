using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class MouseActionsManager : MonoBehaviour
{
    #region Singleton
    public static MouseActionsManager instance = new MouseActionsManager();
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
    #endregion

    public GameObject mouse_debug;
    [SerializeField] GameObject mouse_collider_debug;
    [SerializeField] List<GameObject> NumberCubes = new List<GameObject>();

    GameObject CurrentCube, LastRayHit, LastRayCube;

    public bool isDragging = false;
    public bool isHoldingCube = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clicking event of mouse
        {
            isDragging = true;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 9999f))
            {
                if (hit.collider.tag == "slot_cube") // When raycasting a slot cube
                {
                    mouse_debug.transform.position = hit.point;
                    mouse_collider_debug.transform.position = hit.point;
                    if (hit.transform.GetComponent<Slot>().isOccupied())
                    {
                        LastRayCube = hit.transform.gameObject;
                        foreach (GameObject item in NumberCubes)
                        {
                            if (item.name == hit.transform.GetComponent<Slot>().GetNumber().ToString()) // Finding the number that slot is holding
                            {
                                GameObject cube = Instantiate(item, hit.point, Quaternion.identity);
                                cube.GetComponent<NumberCube>().SetNumber(hit.transform.GetComponent<Slot>().GetNumber());
                                
                                CurrentCube = cube;
                                
                                cube.transform.DOScale(1f, 0.25f).SetEase(Ease.OutCirc);
                                cube.transform.DOMoveY(transform.position.y + 2f, 0.25f).SetEase(Ease.OutElastic).OnComplete(() =>
                                {

                                });
                                isHoldingCube = true;
                                hit.transform.GetComponent<Slot>().ClearSlot();
                                break;
                            }
                        }
                    }
                }
            }
        }

        else if (Input.GetMouseButton(0)) // Mouse held down
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "slot_cube" || hit.collider.tag == "floor")
                {
                    mouse_debug.transform.position = new Vector3(hit.point.x, hit.point.y + 2f, hit.point.z);
                    mouse_collider_debug.transform.position = hit.point;
                }
                LastRayHit = hit.transform.gameObject;
            }
        }

        else if (Input.GetMouseButtonUp(0)) // Releasing event of mouse
        {
            if (LastRayHit.tag == "slot_cube" && isHoldingCube)
            {
                int current_number = CurrentCube.GetComponent<NumberCube>().GetNumber();
                int release_number = LastRayHit.transform.GetComponent<Slot>().GetNumber();
                if (CurrentCube != null && current_number == release_number) // Numbers match
                {
                    Slot slot_sc = LastRayHit.transform.GetComponent<Slot>();
                    int new_number = slot_sc.GetNumber() * 2;

                    CurrentCube.transform.DOScale(.1f, .3f).SetEase(Ease.OutCirc);
                    CurrentCube.transform.DOMoveY(CurrentCube.transform.position.y - 2f, 0.3f).SetEase(Ease.OutElastic).OnComplete(() =>
                    {
                        Destroy(CurrentCube);
                        slot_sc.ChangePNG((slot_sc.GetNumber() * 2));
                        slot_sc.SetValues(true, new_number);
                    });
                }
                else // Numbers don't match
                {
                    Sequence seq = DOTween.Sequence();
                    seq.Append(CurrentCube.transform.DOPunchRotation(new Vector3(transform.rotation.x, 180, transform.rotation.z), 0.2f, 10, .1f))
                    .Append(CurrentCube.transform.DOScale(0.1f, 0.25f).SetEase(Ease.InSine))
                    .OnComplete(() =>
                    {
                        Destroy(CurrentCube);
                        LastRayCube.transform.GetComponent<Slot>().RefillSlot();

                    });
                    seq.Play();


                }
            }

            else if (LastRayHit.tag == "floor" && isHoldingCube) // Released on floor
            {
                Sequence seq = DOTween.Sequence();
                seq.Append(CurrentCube.transform.DOPunchRotation(new Vector3(transform.rotation.x, 180, transform.rotation.z), 0.2f, 10, .1f))
                .Append(CurrentCube.transform.DOScale(0.1f, 0.25f).SetEase(Ease.InSine))
                .OnComplete(() =>
                {
                    Destroy(CurrentCube);
                    LastRayCube.transform.GetComponent<Slot>().RefillSlot();
                });
                seq.Play();
            }
            isDragging = false;
            isHoldingCube = false;



        }
    }

}
