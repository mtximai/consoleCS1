using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace consoleCS1
{
  class TestAsync
  {
    public static void test()
    {

      //Console.WriteLine("Thread ID: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

      /* Executa 1 tarefa */
      //executarA();

      /* Executa 2 tarefas */
      //executarB();

      // Executa uma lista de tarefas
      //executarBv2();

      //bench1();

      // Executa uma lista de tarefa e monitora o término individualmente
      //executarIndividual();

      var x = myFuncAsync();

      Console.WriteLine("vai aguardar...");
      var y = x.Result;

      Console.WriteLine($"terminou: {y}");


    }



    static async Task<int> myFuncAsync()
    {
      Console.WriteLine("myFuncAsync(): pausado por 2s ...");

      await Task.Run(() => Thread.Sleep(2000));

      Console.WriteLine("myFuncAsync(): concluído.");

      return 1;
    }


    // Executa 1 tarefa
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
      Console.WriteLine();
    }


    // Executa 2 tarefas
    static void executarB()
    {
      Task<string> task1 = Task.Run(() => Hello("Huguinho", 2001));
      Task<string> task2 = Task.Run(() => Hello("Zezinho", 1002));

      // Bloqueia a thread principal e retornará a pós a conclusão de todas as execuções solicitadas
      Task.WaitAll(task1, task2);

      Console.WriteLine(task1.Result);
      Console.WriteLine(task2.Result);
    }

    // Executa uma lista de tarefas
    static void executarBv2()
    {
      var tarefas = new Task[2];

      tarefas[0] = Task.Run(() => Hello("Huguinho", 2001));
      tarefas[1] = Task.Run(() => Hello("Zezinho", 1002));

      Task.WaitAll(tarefas);


      var x = ((Task<string>)tarefas[0]).Result;
      var y = ((Task<string>)tarefas[1]).Result;

      Console.WriteLine(x);
      Console.WriteLine(y);
    }


    // Executa uma lista de tarefas e monitora o término de cada uma delas
    static void executarIndividual()
    {
      var tarefas = new List<Task<string>>();

      tarefas.Add(Task.Run(() => Hello("Huguinho", 2000)));
      tarefas.Add(Task.Run(() => Hello("Zezinho", 1000)));
      tarefas.Add(Task.Run(() => Hello("Luizinho", 500)));

      while (tarefas.Count > 0)
      {
        var i = Task.WaitAny(tarefas.ToArray());

        Task<string> ts = (Task<string>)tarefas[i];

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
