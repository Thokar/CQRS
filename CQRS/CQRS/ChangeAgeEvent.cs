namespace CQRS.CQRS
{
  public class ChangeAgeEvent : Event
  {
    public Person Target;
    public int OldValue, NewValue;

    public ChangeAgeEvent(Person target, int oldValue, int newValue)
    {
      this.Target = target;
      this.OldValue = oldValue;
      this.NewValue = newValue;
    }

    public override string ToString()
    {
      return $" Age changed from { OldValue } to { NewValue }"; //base.ToString();
    }
  }
}
