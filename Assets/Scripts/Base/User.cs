using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string userName;
    public long energy;
    public long ePc;
    public List<Soldier> soldierList = new List<Soldier>();
    public int bossNum = 0;
    public int enemyNum = 0;
}
