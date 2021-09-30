using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string userName;
    public long energy;
    public long ePc;
    public int dia;
    public List<Soldier> soldierList = new List<Soldier>();
    public List<StatUp> statUpList = new List<StatUp>();
    public int bossNum = 0;
    public int enemyNum = 0;
}
