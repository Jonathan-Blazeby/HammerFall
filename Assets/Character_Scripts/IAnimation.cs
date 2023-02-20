using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationProperties
{
    Horizontal, Vertical
}

public interface IAnimation
{
    public bool IsWeaponActive();
}
