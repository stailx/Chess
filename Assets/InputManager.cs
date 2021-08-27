using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public enum Turn { White,Black};
    int[] layerMaskPiece;
    int layerMaskTerrain;
    DrawMoove draw;
    Piece lastPiece ;
    List<Vector2> lastMoove;
    Turn turn = Turn.White;
    // Start is called before the first frame update
    void Start()
    {
        draw = FindObjectOfType<DrawMoove>();
        layerMaskPiece = new int[] { LayerMask.GetMask("WhitePiece"),LayerMask.GetMask("BlackPiece")};
        layerMaskTerrain = LayerMask.GetMask("Terrain");
    }
    void changeTurn()
    {
        lastPiece = null;
        if (turn == Turn.White)
            turn = Turn.Black;
        else
            turn = Turn.White;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10000, Color.yellow);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraManager.GetCamera().ScreenPointToRay(Input.mousePosition);
            print("left");

            if (Physics.Raycast(ray: ray, out hit,10000f, layerMaskPiece[(int)turn]))
            {
                print("Did Hit");

                print(hit.transform);
                lastPiece = hit.transform.GetComponent<Piece>();
                lastMoove = lastPiece.GetPossibleMoove();
                draw.Draw(lastMoove);
            }
            else if (lastPiece != null)
            {
                if (Physics.Raycast(ray: ray, out hit, 10000f, ~layerMaskPiece[(int)turn]))
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
