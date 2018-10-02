namespace CQRS.CQRS
{
  public class Person
  {
    private int age; // Nobody changes this!
    EventBroker eventBroker; // Broker must not be in the Scope of person

    public int UniqueId; 

    public Person(EventBroker eventBroker) // TODO: Here we should retrieve all Data from the store
    {
      this.eventBroker = eventBroker;
      eventBroker.Commands += BrokerOnCommands;
      eventBroker.Queries += BrokerOnQueries;
    }

    private void BrokerOnQueries(object sender, Query query)
    {
      var ac = query as AgeQuery;

      if (ac != null && ac.Target == this)
      {
        ac.Result = age;
      }
    }

    private void BrokerOnCommands(object sender, Command command)
    {
      var cac = command as ChangeAgeCommand;
      if (cac != null && cac.Target == this)
      {
        if (cac.Register)
        {
          eventBroker.AllEvents.Add(new ChangeAgeEvent(this, age, cac.Age));
        }
        age = cac.Age;
      }
    }

    public bool canVote => age >= 16; // not Possible
  }
}
