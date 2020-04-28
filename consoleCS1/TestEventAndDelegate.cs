// Teste com Event e Delegate - 28/04/20
// fonte: https://www.youtube.com/watch?v=jQgwEsJISy0&list=PLTjRvDozrdlz3_FPXwb6lX_HoGXa09Yef&index=11

using System;
using System.Threading;

namespace consoleCS1
{

  public class TestEventAndDelegate
  {
    public static void test()
    {

      var videoEncoder = new VideoEncoder();

      var p1 = new Pessoa() { nome = "Huguinho" };      // subscriber
      var p2 = new Pessoa() { nome = "Pato Donald" };   // subscriber

      videoEncoder.VideoEncoded += p1.OnVideoEncoded;
      videoEncoder.VideoEncoded += p2.OnVideoEncoded;

      videoEncoder.Encode();

    }
  }


  public class VideoEncoder
  {
    // 1- Define a delegate
    // 2- Define an event based on that delegate

    // Forma antiga
    /*
    public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args);
    public event VideoEncodedEventHandler VideoEncoded;
    */


    // Nova forma
    public event EventHandler<VideoEventArgs> VideoEncoded;


    public void Encode()
    {
      Console.WriteLine("Encoding video...");
      Thread.Sleep(3000);
    
      // 3- Raise the event
      OnVideoEncoded();
    }

    protected virtual void OnVideoEncoded()
    {
      if (VideoEncoded != null)
      {
        VideoEncoded(this, new VideoEventArgs() { message = "OnVideoEncoded(): processamento concluído."});
      }
    }
  }


  public class VideoEventArgs : EventArgs
  {
    public string message { get; set; }
  }


  public class Pessoa {

    public string nome;

    public void OnVideoEncoded(object source, VideoEventArgs args)
    {
      Console.WriteLine($"Pessoa: {nome} - ok, fui notificado com a mensagem: {args.message}");
    }

  }

}