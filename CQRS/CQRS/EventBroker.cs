using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS.CQRS
{
  public class EventBroker // be singleton
  {
    // 1. i have all Events that happend.
    public IList<Event> AllEvents = new List<Event>();

    // 2. Commands => Send Command to everybody!
    public event EventHandler<Command> Commands;

    // 3. Query
    public event EventHandler<Query> Queries;


    public void Command(Command c) // What to do? f.e. change Age
    {
      Commands?.Invoke(this, c);
    }


    public T Query<T>(Query q)
    {
      Queries?.Invoke(this, q);
      return (T)q.Result;
    }

    public void UndoLast()
    {
      var e = AllEvents.LastOrDefault();

      var ac = e as ChangeAgeEvent;

      if (ac != null)
      {
        Command(new ChangeAgeCommand(ac.Target, ac.OldValue) { Register = false });
        AllEvents.Remove(e);
      }
    }
  }
}
