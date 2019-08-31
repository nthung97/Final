using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VecMathf
{
    public static Vector3 error = Vector3.down * 4;
    public static Vector3 destination(Vector3 end)
    {
        if (Mathf.Abs(end.x) > 2) return error;
        if (Mathf.Abs(end.z) > 2) return error;
        Vector3 res = end;
        int x = (int)end.x + 2;
        int y = (int)end.z + 2;
        res.y = CreateField.Field.fl[x,y] * 0.5f;
        return res;
    }
    public static int compHeight(Vector3 a, Vector3 b)
    {
        if (a.y == b.y) return 0;
        if (a.y - b.y > 0) return 1;
        return -1;
    }
    public static bool Near(Vector3 a, Vector3 b)
    {
        return (Mathf.Abs(a.x - b.x) < 2 && Mathf.Abs(a.z - b.z) < 2);
    }
    public static bool atPerimeter(Vector3 a)
    {
        return (Mathf.Abs(a.x) == 2 || Mathf.Abs(a.z) == 2) ;
    }
    public static bool outSide(Vector3 a)
    {
        return (Mathf.Abs(a.x) > 2 || Mathf.Abs(a.z) > 2);
    }
}

public abstract class GamePhase {
    //public static List
    // Use this for initialization
	protected int side, phaseId;
    protected static bool allowedNext = false;
    static float lastClick;

    public abstract String getString();
    public abstract String getDString();
    public static GamePhase current = new MinionPlacePhase();
    public void click(Vector3 loc, int mSide)
    {
        if (Time.time - lastClick < 1) return;
        lastClick = Time.time;
        clickAt(loc, mSide);
    }
    public abstract void clickAt(Vector3 loc, int mSide);
    public abstract GamePhase createNext();
    public static void nextPhase() {
        if (!allowedNext) return;
        allowedNext = false;
        current = current.createNext();
        IndicatorBehave.updateColor(current.side);
		PhaseText.UpText();
		Debug.Log ("Phase ID" + current.phaseId);
		GodUI.GodUIOne.updateStatus ((current.side != 2), current.phaseId);
		GodUI.GodUITwo.updateStatus ((current.side != 1), current.phaseId);
        //DescriptionText.UpText();
    }
}

public class MinionPlacePhase : GamePhase
{
    public override String getString()
    {
        return "Minion Place Phase";
    }
    public override String getDString()
    {
        return "Place 2 minions each side. Click for yellow minions \n and right click for red minions.";
    }

    public MinionPlacePhase()
    {
		phaseId = 0;
        side = 0;
    }
    public override void clickAt(Vector3 loc, int mSide)
    {
        MinionBehave a = Get.getMinion(loc);
        if (a == null)
        {
            if (CreateField.Field.minion[mSide - 1].Count < 2)
                a = CreateField.createMinion(mSide, loc);
            if (CreateField.Field.minion[0].Count == 2)
                if (CreateField.Field.minion[1].Count == 2)
                    allowedNext = true;
            return;
        }
        if (a.side == mSide)
        {
            //Check if minion has same side. Delete.
            a.removed();
            allowedNext = false;
        }
    }
    public override GamePhase createNext()
    {
        return new MoveMinionPhase(1);
    }
}

public class MoveMinionPhase : GamePhase
{ //Error Constructor
    public override String getString()
    {
        return "Move Minion Phase";
    }
    public override String getDString()
    {
        String color;
        if (side == 1) color = "Yellow";
        else color = "Red";
        return "Chose a " + color + " minion and place it in 1 of 8 adjacent grid.";
    }
    MinionBehave m;
    Vector3 location;
    bool up;
    public MoveMinionPhase(int mSide)
    {
        side = mSide;
		phaseId = 1;
        m = null;
    }
    public override void clickAt(Vector3 loc, int mSide)
    {
        if (up == false)
        {
            allowedNext = false;
            if (m != null)
                m.transform.position = location;
            m = Get.getMinion(loc);
            if (m == null) return;
            if (m.side != side) m = null;
            else
            {
                //Pick Minion
                Debug.Log("Pick Minion Up");
                location = m.transform.position;
                loc.y = 4;
                m.move(loc);
                IndicatorBehave.minion = m.transform;
                up = true;
            }
        }
        else
        {
            up = false;
            MinionBehave a = Get.getMinion(loc);
            IndicatorBehave.minion = null;
            if (a != null) loc = location;

            allowedNext = true;
            loc = MinionBehave.destination(loc);
            if (!VecMathf.Near(loc, location) | loc.y < -2 | loc.y > location.y + 0.5f) //Errors
            {
                loc = location;
                allowedNext = false;
            }
            m.move(loc);
            if (loc.y == 1.5f) //3rd floor
                CameraController.setMiddle(loc);
        }
    }
    public override GamePhase createNext()
    {
        return new AddBlockPhase(side, m.transform.position);
    }
}

public class AddBlockPhase : GamePhase
{
    public override String getString()
    {
        return "Add Block Phase";
    }
    public override String getDString()
    {
        return "Place a house block";
    }
    Vector3 location;
    HouseBehave h;
    public AddBlockPhase(int s, Vector3 loc)
    {
		phaseId = 2;
        side = s;
        location = loc;
    }
    public override void clickAt(Vector3 loc, int mSide)
    {
        if (VecMathf.Near(loc, location))
        {
            if (Get.getMinion(loc) != null) return;
            HouseBehave cur = Get.getHouse(loc);
            if (cur.state == 4) return; //Dorm
            allowedNext = true;
            if (h != null) h.decFl();
            h = cur;
            cur.addFl();
        }
    }
    public override GamePhase createNext()
    {
        return new MoveMinionPhase(3 - side);
    }
}

public class EndGame : GamePhase
{
    public override String getString()
    {
        return "GameOver";
    }
    public override String getDString()
    {
        return "GameOver";
    }
    public override void clickAt(Vector3 loc, int mSide)
    {
    }

    public override GamePhase createNext()
    {
        throw null;
    }
}