// .NET Core 3.1 - 24/04/20

using System;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace consoleCS1
{
  class Program
  {
    static void Main(string[] args)
    {

      Console.WriteLine($"Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");

      //executarA();

      //executarB();

      //executarBv2();

      //bench1();

      executarIndividual();


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



    static void executarA()
    {

      // Agenda a execução da tarefa no TaskScheduler
      var tarefa = Task.Run(() => Hello("Mickey", 1000));

      Console.WriteLine($"Status antes do Wait(): {tarefa.Status} ...");


      // Aguarda o término da task
      tarefa.Wait();

      Console.WriteLine($"Status após o Wait() - Status: {tarefa.Status} ...");

      // aguarda até a conclusão da task se não for chamado o Wait() antes
      var resultado = tarefa.Result;

      Console.WriteLine($"Resultado: {resultado}");
    }


    static void executarB()
    {
      Task<string> task1 = Task.Run(() => Hello("Huguinho", 2001));
      Task<string> task2 = Task.Run(() => Hello("Zezinho",1002));

      // Bloqueia a thread principal e retornará a pós a conclusão de todas as execuções solicitadas
      Task.WaitAll(task1, task2);

      Console.WriteLine(task1.Result);
      Console.WriteLine(task2.Result);
    }

    static void executarBv2()
    {
      var tarefas = new Task[2];

      tarefas[0] = Task.Run(() => Hello("Huguinho", 2001));
      tarefas[1] = Task.Run(() => Hello("Zezinho",1002));

      Task.WaitAll(tarefas);

      
      var x = ((Task<string>) tarefas[0]).Result;
      var y = ((Task<string>) tarefas[1]).Result;

      Console.WriteLine(x);
      Console.WriteLine(y);
    }


    static void executarIndividual()
    {
      var tarefas = new List<Task<string>>();

      tarefas.Add(Task.Run(() => Hello("Huguinho", 2000)));
      tarefas.Add(Task.Run(() => Hello("Zezinho" , 1000)));
      tarefas.Add(Task.Run(() => Hello("Luizinho",  500)));

      while (tarefas.Count >0)
      {
        var i = Task.WaitAny(tarefas.ToArray());

        Task<string> ts = (Task<string>) tarefas[i];

        Console.WriteLine($"Tarefa {i} => {ts.Result}");

        tarefas.Remove(ts);

      }

      Console.WriteLine("=> Todas as tarefas foram concluídas.");
    }




    // 24/04/20
    static string Hello(string pNome, int pTempoPausa)
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();

      Console.WriteLine($"Hello({pNome},{pTempoPausa}): iniciando...");

      System.Threading.Thread.Sleep(pTempoPausa);

      Console.WriteLine($"Hello({pNome}, {pTempoPausa}): concluído!");

      var elapsetMs = watch.ElapsedMilliseconds;

      return $"Hello({pNome},{pTempoPausa}) - tempo total: {elapsetMs} ms!";
    }


    static string HelloSimple(string pNome, int pTempoPausa)
    {
      Console.WriteLine($"HelloSimple({pNome}): iniciando com pausa de {pTempoPausa} ms!");

      System.Threading.Thread.Sleep(pTempoPausa);

      Console.WriteLine($"HelloSimple({pNome}): concluído: {pTempoPausa}!");

      return $"HelloSimple {pNome} (pausa: {pTempoPausa}) ms!";
    }


    static async Task<string> GetTaskAsync(int timeout)
    {
      Console.WriteLine("Task Thread: " + Thread.CurrentThread.ManagedThreadId);
      await Task.Delay(timeout);
      return timeout.ToString();
    }


    //private static void Watch(Action func(string pNome, int pTIme))
    //{
    //  Console.WriteLine($"Watch(): Starting ...");

    //  var sw = new Stopwatch();
    //  sw.Start();

    //  func();

    //  sw.Stop();

    //  Console.WriteLine($"Watch(): Elapsed {sw.ElapsedMilliseconds} ms");
    //  Console.WriteLine("---------------");
    //}

  }
}
