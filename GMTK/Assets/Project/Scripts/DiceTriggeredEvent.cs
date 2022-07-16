using Coimbra.Services.Events;
using UnityEngine;

public partial struct DiceTriggeredEvent : IEvent
{
    public readonly GameObject Hit;

    public DiceTriggeredEvent(GameObject hit)
    {
        Hit = hit;
    }
}