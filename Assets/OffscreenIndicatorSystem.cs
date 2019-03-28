using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenIndicatorSystem : MonoBehaviour
{

    // private
    Dictionary<GameObject, GameObject> UIMarkers = new Dictionary<GameObject, GameObject>();
    // moves markers and updates them
    private void Update()
    {
        foreach (GameObject target in UIMarkers.Keys) {
           Vector3 position = Camera.main.WorldToScreenPoint(target.transform.position);


            if (position.x >= 0 && position.y >= 0 &&
                position.x <= Camera.main.pixelWidth &&
                position.y <= Camera.main.pixelHeight)
            { // inside screen
                UIMarkers[target].SetActive(false); // disable the object, not needed!
            }
            else { // outside screen
                UIMarkers[target].SetActive(target.activeInHierarchy);
                position.x = Mathf.Min(Mathf.Max(position.x, xPad), Camera.main.pixelWidth - xPad);
                position.y = Mathf.Min(Mathf.Max(position.y, yPad), Camera.main.pixelHeight - yPad);
                UIMarkers[target].transform.position = position;
            }
        }
    }

    // public
    public static OffscreenIndicatorSystem current;
    public GameObject markerPrefab;

    [Range(0,100)]
    public int xPad = 0;
    [Range(0, 100)]
    public int yPad = 0;

    private void Start()
    {
        current = this;
    }

    // adds marker for target object
    public void AddMarker(GameObject targetObject) {
        UIMarkers[targetObject] = GameObject.Instantiate(markerPrefab);
        UIMarkers[targetObject].transform.SetParent(this.transform);
    }

    // removes marker for target object
    public void RemoveMarker(GameObject targetObject)
    {
        GameObject todestroy = UIMarkers[targetObject];
        UIMarkers.Remove(targetObject); // hopefully its inside or this will throw
        Destroy(todestroy);
    }
}
