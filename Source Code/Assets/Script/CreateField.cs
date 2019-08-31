using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateField : MonoBehaviour {
    public static CreateField Field;
    public GameObject house;
    public GameObject[] minionPre;
    public List<MinionBehave>[] minion;
    public HouseBehave[,] list;
    public int[,] fl;

    public static MinionBehave createMinion(int side, Vector3 loc)
    {
        if (side < 1 || side > 2) return null;
        GameObject tmp = (GameObject)Instantiate(Field.minionPre[side - 1]);
        tmp.transform.position = loc;
        tmp.GetComponent<MinionBehave>().created();
        Field.minion[side - 1].Add(tmp.GetComponent<MinionBehave>());
        return tmp.GetComponent<MinionBehave>();
    }

    void Start () {
        if (Field != null)
        {
            GameObject a = Field.gameObject;
            Field = null;
			Debug.Log ("Destroy");
            Destroy(a);
        }
        Field = this;
        Get.Field = this;
        Create();
    }
    //RESEEEETTTT
    //
    /// <summary>
    /// 
    /// RESET
    /// 
    /// </summary>
    public static void setToNull()
    {
        GridMove.stop();
        IndicatorBehave.minion = null;
        //Field = null;
        // Get.Field = null;
    }

    public void restart()
    {
        setToNull();
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                list[i, j].updateFl(0);
        foreach (MinionBehave m in minion[0]) Destroy(m);
        minion[0].Clear();
        foreach (MinionBehave m in minion[1]) Destroy(m);
        minion[1].Clear();
        GamePhase.current = new MinionPlacePhase();
    }

    void Create()
    {
        GameObject tmp;
        list = new HouseBehave[5, 5];
        fl = new int[5, 5];
        minion = new List<MinionBehave>[2];
        minion[0] = new List<MinionBehave>();
        minion[1] = new List<MinionBehave>();
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
            {
                tmp = (GameObject)Instantiate(house);
                tmp.transform.position = new Vector3(i - 2, 0, j - 2);
                tmp.name = "House " + i + j;
                tmp.transform.SetParent(transform);
                list[i, j] = tmp.GetComponent<HouseBehave>();
            }
    }

    public static void Receive(GameObject a, int st)
    {
        CreateField tmp = Field.GetComponent<CreateField>();
        Vector3 loc = a.transform.position;
        int i = (int) loc.x + 2, j = (int)loc.z + 2;
        if (tmp.list[i, j].gameObject == a)
        {
            tmp.fl[i, j] = st;
            return;
        }
        for (i = 0; i < 5; i++)
            for (j = 0; j < 5; j++)
                if (tmp.list[i, j].gameObject == a)
                {
                    tmp.fl[i, j] = st;
                    return;
                }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SPACE");
            GamePhase.nextPhase();
        }
    }
}

public class Get
{
    public static CreateField Field;
    public static MinionBehave getMinion(Vector3 a)
    {
        Vector3 b;
        a.y = 0;
        foreach (MinionBehave m in Field.minion[0])
        {
            b = m.transform.position;
            b.y = 0;
            if (b == a) return m;
        }
        foreach (MinionBehave m in Field.minion[1])
        {
            b = m.transform.position;
            b.y = 0;
            if (b == a) return m;
        }
        return null;
    }

    public static HouseBehave getHouse(Vector3 a)
    {
        if (VecMathf.outSide(a)) return null;
        HouseBehave h = Field.list[(int)a.x + 2, (int)a.z + 2];
        return h;
    }
}