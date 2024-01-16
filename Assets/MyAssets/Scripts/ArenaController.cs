using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void Update()
    {
        Ray point = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(point, out hit, 200))
        {
            if (hit.collider.gameObject.tag == "tile")
            {
                var hitTile = hit.collider.gameObject;
                var hitTileRend = hitTile.GetComponent<Renderer>();
                hitTileRend.material.SetColor("_Color", Color.yellow);
            }

        }
    }
}
