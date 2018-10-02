using CQRS.CQRS;
using System;

namespace CQRS
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CQRSLogic();

      Console.ReadLine();
    }

    public static void ClassicLogic()
    {
      var p = new Classic.Person(); // Working with object reference
      p.Age = 23; // No audits, no tracking, no roleback 
      Console.WriteLine("Age: " + p.Age);
    }

    public static void CQRSLogic()
    {
      var eb = new EventBroker();
      var p = new CQRS.Person(eb);
      int age;

      eb.Command(new ChangeAgeCommand(p, 123));

      foreach (var e in eb.AllEvents)
      {
        Console.WriteLine(e);
      }

      age = eb.Query<int>(new AgeQuery { Target = p });
      Console.WriteLine("Age: " + age);

      Console.WriteLine("Reverting");
      eb.UndoLast(); 

      foreach (var e in eb.AllEvents)
      {
        Console.WriteLine(e);
      }

      age = eb.Query<int>(new AgeQuery { Target = p });
      Console.WriteLine("Age: " + age);
    }
  }
}
