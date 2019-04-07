using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylMenuTextController : MonoBehaviour
{

    public static readonly int MENU_SIZE = 16;
    public static readonly int MENU_HALF = 8;
    public static readonly int INITIAL_MENU_SELECT = 12;
    public static readonly int ROTATION_OFFSET = 12;
    public static readonly int FIXED_ROTATION_OFFSET = 0;

    public int rotation = 0;
    public int absoluteSelection = 0;
    public GameObject textPrefab;

    public GameObject vinyl;

    List<VinylMenuController.MenuEntry> entries = new List<VinylMenuController.MenuEntry>();
    List<VinylMenuText> menuSelections = new List<VinylMenuText>();

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < MENU_SIZE; i++)
        {
            var go = Instantiate(textPrefab);
            go.transform.SetParent(transform);
            go.transform.rotation = transform.rotation;
            Vector3 startVector = Vector3.up;
            var rotation = Quaternion.Euler(0, 0, i * 180f / MENU_HALF);
            startVector = rotation * startVector;
            go.transform.localPosition = startVector;
            go.transform.localRotation = Quaternion.Euler(0, 0, 90 + i * 180f / MENU_HALF);
            menuSelections.Add(go.GetComponent<VinylMenuText>());
        }
    }

    private void RefreshMenuItems() {
        for (int i = 0; i < menuSelections.Count; i++)
        {
            int rotatedIndex = (i + ROTATION_OFFSET) % MENU_SIZE;
            int fixedIndex = (MENU_SIZE + (rotatedIndex - rotation)) % MENU_SIZE;
            fixedIndex = (fixedIndex + ROTATION_OFFSET) % MENU_SIZE;
            int entryIndex = (fixedIndex - (INITIAL_MENU_SELECT + ROTATION_OFFSET) % MENU_SIZE + absoluteSelection) % entries.Count;
            entryIndex = entryIndex < 0 ? entries.Count + entryIndex : entryIndex;
            menuSelections[rotatedIndex].SetText(entries[entryIndex].menuText);
        }
    }

    public void SetMenuItems(List<VinylMenuController.MenuEntry> entries) {
        this.entries = entries;
        absoluteSelection = ((INITIAL_MENU_SELECT  + ROTATION_OFFSET) % MENU_SIZE) % entries.Count;
        rotation = absoluteSelection;
        absoluteSelection = 0;
        RefreshMenuItems();
    }

    public void ScrollDown() {
        rotation--;
        absoluteSelection--;

        if (absoluteSelection == -1) {
            absoluteSelection = entries.Count - 1;
        }

        if (rotation == -1)
        {
            rotation = MENU_SIZE-1;
        }
        RefreshMenuItems();
    }

    public void ScrollUp() {
        rotation++;
        absoluteSelection++;

        if (absoluteSelection == entries.Count) {
            absoluteSelection = 0;
        }
        if (rotation == MENU_SIZE) {
            rotation = 0;
        }
        RefreshMenuItems();
    }

    private void Update()
    {
        Quaternion targetRotation =  Quaternion.Euler(0, 0, -rotation * 180f / MENU_HALF);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 5);

        if (vinyl) {
            vinyl.transform.localRotation = Quaternion.Lerp(vinyl.transform.localRotation, targetRotation, Time.deltaTime * 5);
        }

        for (int i = 0; i < MENU_SIZE; i++) {
            int selected_indx = (INITIAL_MENU_SELECT + rotation) % MENU_SIZE;
            if (selected_indx == i)
            {
                menuSelections[i].transform.localScale = new Vector3(2f, 2f, 2f);
                menuSelections[i].text.color = Color.cyan;
            }
            else {
                menuSelections[i].transform.localScale = new Vector3(1, 1, 1);
                menuSelections[i].text.color = Color.white;
            }
        }
    }

}
