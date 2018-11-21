using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Ressources : MonoBehaviour
{
    public RessourceType ressources;

    public bool isSuperiorThan(RessourceType other)
    {
        if (other.bois > ressources.bois) return false;
        if (other.pierre > ressources.pierre) return false;
        if (other.or > ressources.or) return false;
        if (other.fer > ressources.fer) return false;
        if (other.charbon > ressources.charbon) return false;

        return true;
    }

    public bool isInferiorThan(RessourceType other)
    {
        return !isSuperiorThan(other);
    }
}

[Serializable]
public struct RessourceType
{
    public int bois;
    public int pierre;
    public int or;
    public int fer;
    public int charbon;
}
