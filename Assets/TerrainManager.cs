using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using System;

public class TerrainManager : MonoBehaviour
{
    public GameObject WhiteCell;
    public GameObject BlackCell;
    public PieceInformation[] Pieces;
    public Transform PieceHolder;
    public Dictionary<Vector2, Piece> Board=new Dictionary<Vector2, Piece>();

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0;y < 8; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    Instantiate<GameObject>(WhiteCell, new Vector3(x, y, 0), Quaternion.identity, this.transform);
                }
                else
                {
                    Instantiate<GameObject>(BlackCell, new Vector3(x, y, 0), Quaternion.identity, this.transform);
                }
            }
        }
        foreach (PieceInformation piece in Pieces)
        {
            foreach (Vector2 pos in piece.list)
            {
                GameObject g = Instantiate<GameObject>(piece.prefab, pos, Quaternion.FromToRotation(new Vector3(0, 0, 1), new Vector3(0, 1, 0)), PieceHolder);
                g.transform.localPosition = pos;
                Board.Add(pos, g.GetComponent<Piece>());

            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Piece GetPieceAt(Vector2 pos)
    {
        if (Board.ContainsKey(pos))
        {
            return Board[pos];
        }
        else return null;
    }
    public void Moove(Piece piece, Vector2 pos)
    {
        print(Board);
        Vector3 p = piece.transform.position;
        Board.Remove(new Vector2(p.x,p.y));
        if (Board.ContainsKey(pos))
        {
            Destroy(Board[pos].gameObject);
            Board.Remove(pos);
        }
        Board.Add(pos, piece);
        print(Board);

    }
}
[Serializable]
public struct PieceInformation
{
    public String Name;
    public GameObject prefab;
    public List<Vector2> list;
}