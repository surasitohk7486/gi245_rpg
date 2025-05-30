using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    [SerializeField]
    private string toMapName;

    [SerializeField]
    private int enterPointId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Hero")
        {
            Debug.Log("Player enters Warp");
            MapManager.instance.GoToMap(toMapName,enterPointId);
        }
    }
}
