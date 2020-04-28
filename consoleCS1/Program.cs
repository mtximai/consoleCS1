// .NET Core 3.1 - 24/04/20

using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace consoleCS1
{
  class Program
  {
    static void Main(string[] args)
    {

      TestAsync.test();

      //TestEventAndDelegate.test();


    }





    static void bench1()
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();
      double s = 0;


      for (int i = 0; i <1000000000;  i++)
      {
        s += Math.Sqrt(i);
        
        //Console.WriteLine($"i = {i} => {x}");
      }

      var elapsetMs = watch.ElapsedMilliseconds;

      Console.WriteLine($"Tempo consumido: {elapsetMs} ms");
      Console.WriteLine(s);
    }


    
  }
}
