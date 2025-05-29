using System.Collections.Generic;
using UnityEngine;

public class RightClick : MonoBehaviour
{
    private Camera cam;
    public LayerMask layerMask;

    public static RightClick instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
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

    private void CommandToWalk(RaycastHit hit , List<Characters> heroes)
    {
        foreach (Characters h in heroes)
        {
            if (h != null)
            {
                h.WalkToPosition(hit.point);
            }

        }
        CreateVFX(hit.point,VFXManager.instance.DoubleRingMarker);
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
                    CommandToWalk(hit,PartyManager.instance.SelectChars);
                    break;
                case "Enemy":
                    CommandToAttack(hit,PartyManager.instance.SelectChars);
                    break;
                case "NPC":
                    CommandTalkToNPC(hit, PartyManager.instance.SelectChars);
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

    private void CommandToAttack(RaycastHit hit, List<Characters> heroes)
    { 
        Characters target = hit.collider.GetComponent<Characters>();
        Debug.Log("Attack: " + target);

        foreach (Characters h in heroes)
        {
            h.ToAttackCharacter(target);
        }
    }

    private void CommandTalkToNPC(RaycastHit hit, List<Characters> heroes)
    {
        Characters npc = hit.collider.GetComponent<Characters>();
        Debug.Log("Talk to NPC: " + npc);

        if (heroes.Count <= 0)
            return;

        heroes[0].ToTalkToNPC(npc);
    }
}
