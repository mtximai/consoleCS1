// 28/04/20
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

      var p1 = new Pessoa() { nome = "Huguinho" };
      var p2 = new Pessoa() { nome = "Pato Donald" };

      videoEncoder.VideoEncoded += p1.OnVideoEncoded;
      videoEncoder.VideoEncoded += p2.OnVideoEncoded;

      videoEncoder.Encode();

    }
  }


  public class VideoEncoder
  {
    // 1- Define a delegate
    // 2- Define an event based on that delegate
    // 3- Raise the event

    public delegate void VideoEncodedEventHandler(object source, EventArgs args);

    // Vai receber os ponteiros dos Subscribers
    public event VideoEncodedEventHandler VideoEncoded;

    public void Encode()
    {
      Console.WriteLine("Encoding video...");
      Thread.Sleep(3000);

      OnVideoEncoded();
    }

    protected virtual void OnVideoEncoded()
    {
      if (VideoEncoded != null)
      {
        VideoEncoded(this, EventArgs.Empty);
      }
    }
  }


  public class Pessoa {

    public string nome;

    public void OnVideoEncoded(object source, EventArgs args)
    {
      Console.WriteLine($"Pessoa: {nome} - ok, fui notificado.");
    }


  }

}