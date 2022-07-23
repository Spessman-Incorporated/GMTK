using Coimbra.Services.Events;
using UnityEngine;

public partial struct DiceCollidedEvent : IEvent
{
    public readonly GameObject Hit;

    public DiceCollidedEvent(GameObject hit)
    {
        Hit = hit;
    }
}