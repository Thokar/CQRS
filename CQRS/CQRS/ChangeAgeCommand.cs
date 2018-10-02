using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CQRS
{
  public class ChangeAgeCommand : Command
  {
    public Person Target; // TODO: we dont know how to serialize this, better use uniqueId
    public int uniqueId;
    public int Age;

    public ChangeAgeCommand(Person person, int age)
    {
      this.Target = person;
      this.Age = age;
    }
  }
}
