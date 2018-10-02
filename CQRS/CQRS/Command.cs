using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.CQRS
{
  public class Command : EventArgs
  {
    public bool Register = true;
  }
}
