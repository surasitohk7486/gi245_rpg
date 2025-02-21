using UnityEngine;

public class RightClick : MonoBehaviour
{
    private Camera cam;
    public LayerMask layerMask;

    private LeftClick leftClick;

    public static RightClick instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        leftClick = GetComponent<LeftClick>();
    }
    void Start()
    {
        instance = this;
        cam = Camera.main;
        layerMask = LayerMask.GetMask("Ground", "Character", "Building");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            TryCommand(Input.mousePosition);
        }
    }

    private void CommandToWalk(RaycastHit hit , Characters c)
    {
        if(c != null)
        {
            c.WalkToPosition(hit.point);

            CreateVFX(hit.point,VFXManager.instance.DoubleRingMarker);
        }
    }

    private void TryCommand(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,1000,layerMask))
        {
            switch (hit.collider.tag)
            {
                case "Ground":
                    CommandToWalk(hit,leftClick.CurChar);
                    break;
                case "Enemy":
                    CommandToAttack(hit,leftClick.CurChar);
                    break;
                    
            }
        }
    }

    private void CreateVFX(Vector3 pos, GameObject vfxPrefab)
    {
        if (vfxPrefab == null)
            return;

        Instantiate(vfxPrefab,pos + new Vector3(0f,0.1f,0f),Quaternion.identity);
    }

    private void CommandToAttack(RaycastHit hit, Characters c) 
    { 
        if (c == null)
            return ;

        Characters target = hit.collider.GetComponent<Characters>();
        Debug.Log("Attack: " + target);

        if(target != null)
            c.ToAttackCharacter(target);
    }
}
