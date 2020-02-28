using System;
using Rhythm.Models;
using System.Linq;

namespace Rhythm
{
  class Program
  {
    static void Main(string[] args)
    {
      var isRunning = true;
      var tracker = new RhythmTracker();
      
      while (isRunning)
      {
        Console.Clear();
        Console.WriteLine("Welcome to Rhythm's Gonna Get You!");
        Console.WriteLine("***THE PREMIERE BAND DATABASE***");
        tracker.PopulateDbBand();
        tracker.PopulateDbAlbum();
        tracker.PopulateDbSong();
        Console.WriteLine("(A)DD BAND, (P)RODUCE ALBUM, (L)ET GO, (R)ESIGN, (V)IEW, or (Q)UIT?");
        var main = Console.ReadLine().ToUpper();
          while (main != "A" && main != "P" && main != "L" && main != "R" && main != "V" && main != "Q")
              {
              }
          if (main == "A")
          {
            Console.Clear();
            tracker.AddNewBandUI();
          }
          else if (main == "P")
          {
            Console.Clear();
            tracker.AddAlbumUI(albumId);
          }
          else if (main == "L")
          {
            Console.Clear();
            tracker.LetBandGo();
          }
          else if (main == "R")
          {
            Console.Clear();
            tracker.ResignBand();
          }
          else if (main == "V")
          {
            Console.Clear();
            Console.WriteLine("(S)IGNED, (U)NSIGNED, (A)LL ALBUMS, (M)AIN, or (Q)?");
            var view = Console.ReadLine().ToUpper();
              while (view != "S" && view != "U" && view != "A" && view != "M" && view != "Q")
              {
              }
              if (view == "S")
              {
                tracker.ViewSignedBands();
              }
              else if (view == "U")
              {
                tracker.ViewUnsignedBands();
              }
              else if (view == "A")
              {
                tracker.ViewAllAlbums();
              }
              else if (view == "M")
              {
              }
          }
          if (main == "Q")
          {

          }
      }
    }
  }
}
