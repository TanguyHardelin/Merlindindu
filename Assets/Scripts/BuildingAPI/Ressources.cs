using System.Collections;
using System.Collections.Generic;
using System;


[Serializable]
public class RessourceType
{
    public int food;
    public int wood;
    public int stone;
    public int gold;
    public int silver;
    public int cold;
    public int citizen;

    public RessourceType(RessourceType c)
    {
        food = c.food;
        wood = c.wood;
        stone = c.stone;
        gold = c.gold;
        silver = c.silver;
        cold = c.cold;
        citizen = c.citizen;
    }
    public RessourceType(int gold, int food, int wood, int stone, int silver, int cold, int citizen)
    {
        this.food = food;
        this.gold = gold;
        this.wood = wood;
        this.stone = stone;
        this.silver = silver;
        this.cold = cold;
        this.citizen = citizen;
    }

    public static bool operator >(RessourceType a, RessourceType b)
    {
        if (a.gold <= b.gold && a.food <= b.food && a.wood <= b.wood && a.stone <= b.stone && a.silver <= b.silver && a.cold <= b.cold && a.citizen <= b.citizen)
        {
            return false;
        }
        return true;
    }

    public static bool operator <(RessourceType a, RessourceType b)
    {
        if (a.gold >= b.gold && a.food >= b.food && a.wood >= b.wood && a.stone >= b.stone && a.silver >= b.silver && a.cold >= b.cold && a.citizen >= b.citizen)
        {
            return false;
        }
        return true;
    }

    public static bool operator ==(RessourceType a, RessourceType b)
    {
        if (a.gold != b.gold && a.food != b.food && a.wood != b.wood && a.stone != b.stone && a.silver != b.silver && a.cold != b.cold && a.citizen != b.citizen)
        {
            return false;
        }
        return true;
    }
    public static bool operator !=(RessourceType a, RessourceType b)
    {
        return !(a == b);
    }

    public static RessourceType operator +(RessourceType a, RessourceType b)
    {
        RessourceType tmp = new RessourceType(a);

        tmp.gold += b.gold;
        tmp.food += b.food;
        tmp.wood += b.wood;
        tmp.stone += b.stone;
        tmp.silver += b.silver;
        tmp.cold += b.cold;
        tmp.citizen += b.citizen;

        return tmp;
    }

    public static RessourceType operator *(RessourceType a, RessourceType b)
    {
        RessourceType tmp = new RessourceType(a);

        tmp.gold *= b.gold;
        tmp.food *= b.food;
        tmp.wood *= b.wood;
        tmp.stone *= b.stone;
        tmp.silver *= b.silver;
        tmp.cold *= b.cold;
        tmp.citizen *= b.citizen;

        return tmp;
    }

    public static RessourceType operator /(RessourceType a, RessourceType b)
    {
        RessourceType tmp = new RessourceType(a);

        tmp.gold /= b.gold;
        tmp.food /= b.food;
        tmp.wood /= b.wood;
        tmp.stone /= b.stone;
        tmp.silver /= b.silver;
        tmp.cold /= b.cold;
        tmp.citizen /= b.citizen;

        return tmp;
    }

    public static RessourceType operator -(RessourceType a, RessourceType b)
    {
        RessourceType tmp = new RessourceType(a);

        tmp.gold -= b.gold;
        tmp.food -= b.food;
        tmp.wood -= b.wood;
        tmp.stone -= b.stone;
        tmp.silver -= b.silver;
        tmp.cold -= b.cold;
        tmp.citizen -= b.citizen;

        return tmp;
    }

    public void setToMax(RessourceType a)
    {
        if (wood > a.wood)
        {
            wood = a.wood;
        }
        if (food > a.food)
        {
            food = a.food;
        }
        if (gold > a.gold)
        {
            gold = a.gold;
        }
        if (stone > a.stone)
        {
            stone = a.stone;
        }
        if (silver > a.silver)
        {
            silver = a.silver;
        }
        if (cold > a.cold)
        {
            cold = a.cold;
        }
        if (citizen > a.citizen)
        {
            citizen = a.citizen;
        }
    }

    
}
