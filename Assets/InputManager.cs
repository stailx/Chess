using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InputManager : NetworkBehaviour
{
    public CameraManager cameraManager;
    public enum Turn { White,Black};
    int[] layerMaskPiece;
    int layerMaskTerrain;
    DrawMoove draw;
    Piece lastPiece ;
    List<Vector2> lastMoove;
    public Turn color;
    public TerrainManager board;
    // Start is called before the first frame update
    void Start()
    {
        cameraManager= FindObjectOfType<CameraManager>();
        draw = FindObjectOfType<DrawMoove>();
        board = FindObjectOfType<TerrainManager>();
        layerMaskPiece = new int[] { LayerMask.GetMask("WhitePiece"),LayerMask.GetMask("BlackPiece")};
        layerMaskTerrain = LayerMask.GetMask("Terrain");
    }
    void changeTurn()
    {
        lastPiece = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(board.GetTurn() == color)
        { 
            RaycastHit hit;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10000, Color.yellow);

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cameraManager.GetCamera().ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray: ray, out hit, 10000f, layerMaskPiece[(int)color]))
                {
                    print("Did Hit");

                    print(hit.transform);
                    lastPiece = hit.transform.GetComponent<Piece>();
                    lastMoove = lastPiece.GetPossibleMoove();
                    draw.Draw(lastMoove);
                }
                else if (lastPiece != null)
                {
                    if (Physics.Raycast(ray: ray, out hit, 10000f, ~layerMaskPiece[(int)color]))
                    {
                        print("Moove Hit");
                        Vector2 pos = hit.transform.position;
                        if (lastMoove.Contains(pos))
                        {
                            lastPiece.Moove(pos);
                            draw.Delete();
                            changeTurn();
                        }

                    }
                }
            }
        }
    }
}
