using UnityEngine;

public class HomeEntity : Entity
{
    public override void Action(Frogger other)
    {
        var froggerTrans = other.transform;
        other.Score();
        froggerTrans.position = Transform.position;
        froggerTrans.parent = Transform;
    }
}