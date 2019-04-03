using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylMenuTextController : MonoBehaviour
{

    public static readonly int MENU_SIZE = 16;
    public static readonly int MENU_HALF = 8;
    public static readonly int INITIAL_MENU_SELECT = 12;

    public int selection = 0;
    public GameObject textPrefab;

    List<GameObject> menuSelections = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MENU_SIZE; i++)
        {
            var go = Instantiate(textPrefab);
            go.transform.SetParent(transform);
            go.transform.rotation = transform.rotation;
            Vector3 up = Vector3.up;
            var rotation = Quaternion.Euler(0, 0, i * 180f / MENU_HALF);
            up = rotation * up;
            go.transform.localPosition = up;
            go.transform.localRotation = Quaternion.Euler(0, 0, 90 + i * 180f / MENU_HALF);
            menuSelections.Add(go);
        }
    }

    public void ScrollUp() {
        selection--;

        if (selection == -1)
        {
            selection = MENU_SIZE-1;
        }
    }

    public void ScrollDown() {
        selection++;
        if (selection == MENU_SIZE) {
            selection = 0;
        }
    }

    private void Update()
    {
        Quaternion targetRotation =  Quaternion.Euler(0, 0, -selection * 180f / MENU_HALF);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime*5);
        for (int i = 0; i < MENU_SIZE; i++) {
            int selected_indx = (INITIAL_MENU_SELECT + selection) % MENU_SIZE;
            if (selected_indx == i)
            {
                menuSelections[i].transform.localScale = new Vector3(2f, 2f, 2f);
            }
            else {
                menuSelections[i].transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
