using Coimbra.Services.Events;

public partial struct DiceResultEvent : IEvent
{
    public int Result;

    public DiceResultEvent(int result)
    {
        Result = result;
    }
}