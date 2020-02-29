using System;
using Rhythm.Models;
using System.Linq;
using ConsoleTools;

namespace Rhythm
{
  class Program
  {
  public class MenuConfig
  {
    public ConsoleColor SelectedItemBackgroundColor = Console.ForegroundColor;
    public ConsoleColor SelectedItemForegroundColor = Console.BackgroundColor;
    public ConsoleColor ItemBackgroundColor = Console.BackgroundColor;
    public ConsoleColor ItemForegroundColor = Console.ForegroundColor;
    public Action WriteHeaderAction = () => Console.WriteLine("Pick an option:");
    public Action<MenuItem> WriteItemAction = item => Console.Write("[{0}] {1}", item.Index, item.Name);
    public string Selector = ">> ";
    public string FilterPrompt = "Filter: ";
    public bool ClearConsole = true;
    public bool EnableFilter = false;
    public string ArgsPreselectedItemsKey = "--menu-select=";
    public char ArgsPreselectedItemsValueSeparator = '.';
    public bool EnableWriteTitle = true;
    public string Title = "My menu";
    public Action<string> WriteTitleAction = title => Console.WriteLine(title);
    public bool EnableBreadcrumb = false;
    }
      // OLD MENU UI()
    // {
    //   var isRunning = true;
    //   var tracker = new RhythmTracker();
      
    //   while (isRunning)
    //   {
    //     Console.Clear();
    //     Console.WriteLine("Welcome to Rhythm's Gonna Get You!");
    //     Console.WriteLine("***THE PREMIERE BAND DATABASE***");
    //     tracker.PopulateDbBand();
    //     tracker.PopulateDbAlbum();
    //     tracker.PopulateDbSong();
    //     Console.WriteLine("(A)DD BAND, (P)RODUCE ALBUM, (L)ET GO, (R)ESIGN, (V)IEW, or (Q)UIT?");
    //     var main = Console.ReadLine().ToUpper();
    //       while (main != "A" && main != "P" && main != "L" && main != "R" && main != "V" && main != "Q" && main == null)
    //           {
    //             Console.WriteLine("(A)DD BAND, (P)RODUCE ALBUM, (L)ET GO, (R)ESIGN, (V)IEW, or (Q)UIT?");
    //           }
    //       if (main == "A")
    //       {
    //         Console.Clear();
    //         tracker.AddNewBandUI();
    //       }
    //       else if (main == "P")
    //       {
    //         Console.Clear();
    //         tracker.AddAlbum();
    //       }
    //       else if (main == "L")
    //       {
    //         Console.Clear();
    //         tracker.LetBandGo();
    //       }
    //       else if (main == "R")
    //       {
    //         Console.Clear();
    //         tracker.ResignBand();
    //       }
    //       else if (main == "V")
    //       {
    //         Console.Clear();
    //         Console.WriteLine("(S)IGNED, (U)NSIGNED, (B)ANDS ALBUMS, ALBUMS BY (R)ELEASE DATE, S(O)NGS IN AN ALBUM, (M)AIN, or (Q)UIT?");
    //         var view = Console.ReadLine().ToUpper();
    //           while (view != "S" && view != "U"  && view != "B"  && view != "R"  && view != "O" && view != "M" && view != "Q" && view == null)
    //           {
    //             Console.WriteLine("(S)IGNED, (U)NSIGNED, (B)ANDS ALBUMS, ALBUMS BY (R)ELEASE DATE, S(O)NGS IN AN ALBUM, (M)AIN, or (Q)UIT?");
    //           }
    //           if (view == "S")
    //           {
    //             Console.Clear();
    //             tracker.ViewSignedBands();
    //           }
    //           else if (view == "U")
    //           {
    //             Console.Clear();
    //             tracker.ViewUnsignedBands();
    //           }
    //           else if (view == "B")
    //           {
    //             Console.Clear();
    //             tracker.ViewBandAlbums();
    //           }
    //           else if (view == "R")
    //           {
    //             Console.Clear();
    //             tracker.ViewAlbumReleaseDate();
    //           }
    //           else if (view == "O")
    //           {
    //             Console.Clear();
    //             tracker.ViewAlbumSongs();
    //           }
    //           else if (view == "M")
    //           {
    //           }
    //       }
    //       if (main == "Q")
    //       {
    //         isRunning = false;
    //       }
    //   }
    // }
    static void Main(string[] args)
    {
    var tracker = new RhythmTracker();
    var subMenu = new ConsoleMenu(args, level: 1)
        .Add("View All Bands", () => tracker.ViewAllBands())
        .Add("View Signed Bands", () => tracker.ViewSignedBands())
        .Add("View Unsigned Bands", () => tracker.ViewUnsignedBands())
        .Add("View Band's Albums", () => tracker.ViewBandAlbums())
        .Add("View Albums by Release Date", () => tracker.ViewAlbumReleaseDate())
        .Add("View Songs in an Album", () => tracker.ViewAlbumSongs())
        .Add("Return to Main Menu", ConsoleMenu.Close)
		.Configure(config =>
        {
          config.Selector = "--> ";
          config.EnableFilter = true;
          config.Title = "Submenu";
          config.EnableBreadcrumb = true;
          config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });
        
      var menu = new ConsoleMenu(args, level: 0)
        .Add("View", subMenu.Show)
        .Add("Add", () => tracker.AddNewBandUI())
        .Add("Produce", () => tracker.AddAlbum())
        .Add("Let Go", () => tracker.LetBandGo())
        .Add("Resign", () => tracker.ResignBand())
        .Add("Exit", () => Environment.Exit(0))
        .Configure(config =>
        {
          config.Selector = "--> ";
          config.EnableFilter = true;
          config.Title = "***RHYTHM RECORDS**";
          config.EnableWriteTitle = true;
          config.EnableBreadcrumb = true;
        });

      menu.Show();
    }
  }
}
