using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMoove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    List<GameObject> tab=new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



    }
    public void Draw(List<Vector2> list)
    {
        Delete();
        foreach (Vector2 ele in list)
        {
            GameObject g = Instantiate<GameObject>(prefab, this.transform);
            tab.Add(g);
            Vector3 ele3 = new Vector3(ele.x, ele.y, -0.51f);
            g.transform.position = ele3;
        }
    }
    public void Delete()
    {
        for (int i = tab.Count - 1; i >= 0; i--)
        {
            Destroy(tab[i]);
        }
    }
}
