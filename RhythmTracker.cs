using System;
using Rhythm.Models;
using System.Linq;

namespace Rhythm
{
    public class RhythmTracker
    {
        public static DatabaseContext db { get; set; } = new DatabaseContext();
        // public void IntValidation(string bandId)
        // {
        //     var bandId = Console.ReadLine().ToLower();
        //     var test = 0;
        //     var testing = true;
        //     while (testing)
        //     {

        //     }


        // }
        public void BandValidation(string bandName)
        {
            var validation = db.Bands.Any(band => band.Name == bandName);
            while (validation)
            {
                Console.WriteLine("That band doesn't exist. Please enter a signed band.");
                bandName = Console.ReadLine();
            }
        }
        public void PopulateDbBand()
        {
            if (!db.Bands.Any())
            {
                var band = new Band()
                {
                    Name = "Andrew Bird",
                    CountryOfOrigin = "USA",
                    NumberOfMembers = "1",
                    Website = "www.andrewbird.net",
                    Style = "American Indie Rock/Folk",
                    IsSigned = true,
                    PersonOfContact = "Andrea Troolin",
                    ContactPhoneNumber = "1-733-342-2420",
                };
            db.Bands.Add(band);
            db.SaveChanges();
            }
        }
        public void PopulateDbAlbum()
        {
            if (!db.Albums.Any())
            {
                var album = new Album()
                {
                    Title = "Weather Systems",
                    IsExplicit = false,
                    ReleaseDate = new DateTime(2003, 4, 1),
                    BandId = 1
                };
            db.Albums.Add(album);
            db.SaveChanges();
            }
        }
        public void PopulateDbSong()
        {
            if (!db.Songs.Any())
            {
                var song = new Song()
                {
                    Title = "Weather Systems",
                    Lyrics = "Hold still a while / Don't spill the wine / I can see it all from here / I can see / I can see / Weather systems of the world",
                    Length = "6:32",
                    Genre = "Alternative/Indie",
                    AlbumId = 1
                };
                db.Songs.Add(song);
                db.SaveChanges();
            }
        }

        public void AddNewBandToDb(string bandName, string countryOfOrigin, string numberOfMembers, string website, string style, string personOfContact, string contactPhoneNumber)
        {
            var newBand = new Band()
            {
            Name = bandName,
            CountryOfOrigin = countryOfOrigin,
            NumberOfMembers = numberOfMembers,
            Website = website,
            Style = style,
            IsSigned = true,
            PersonOfContact = personOfContact,
            ContactPhoneNumber = contactPhoneNumber
            };
            db.Bands.Add(newBand);
            db.SaveChanges();
        }
        public void AddNewBandUI()
        {
            Console.WriteLine("Name?");
            var bandName = Console.ReadLine();
            BandValidation(bandName);
            Console.WriteLine("Country?");
            var countryOfOrigin = Console.ReadLine();
            Console.WriteLine("Members?");
            var numberOfMembers = Console.ReadLine();
            Console.WriteLine("Website?");
            var website = Console.ReadLine();
            Console.WriteLine("Style?");
            var style = Console.ReadLine();
            Console.WriteLine("Contact?");
            var personOfContact = Console.ReadLine();
            Console.WriteLine("Number?");
            var contactPhoneNumber = Console.ReadLine();
            AddNewBandToDb(bandName, countryOfOrigin, numberOfMembers, website, style, personOfContact, contactPhoneNumber);
        }
        public void AddAlbum()
        {
            ViewAllBands();
            Console.WriteLine("Band you'd like to add an album to? Pick ID #.");
            // pick the id of the band user would like to add album to
            var bandId = int.Parse(Console.ReadLine());
            // need validation for ints
            // add an album title
            Console.WriteLine("Album Title?");
            // enter the title of the album
            var title = Console.ReadLine();
            Console.WriteLine("Explicit? (Y)ES or (N)O>");
            // choose whether the album has explicit lyrics or not
            var isExplicit = false;
            var explicitCheck = Console.ReadLine().ToUpper();
                if (explicitCheck != "Y" && explicitCheck != "N")
                {
                    Console.WriteLine("That's not a valid answer.\n\n (Y)ES or (N)O?");
                }
                if (explicitCheck == "Y")
                {
                    isExplicit = true;
                }
                else if (explicitCheck == "N")
                {
                    isExplicit = false;
                }
            Console.WriteLine("Release Date? Enter in yyyy, mm, dd format.");
            // enter the release date of the album
            var releaseDate = Console.ReadLine();
            var parsedDate = DateTime.Parse(releaseDate);
            // add the album to the database according to bandId
            var newAlbum = new Album()
                {
                BandId = bandId,
                Title = title,
                IsExplicit = isExplicit,
                ReleaseDate = parsedDate
                };
                db.Albums.Add(newAlbum);
                db.SaveChanges();
            // passes title of band picked to
            var albumId = newAlbum.Id;
            AddSongUI(albumId);
        }
        public void AddSongUI(int albumId)
        {
            Console.WriteLine("Please add a song to the album.");
            // add song
            Console.WriteLine("Song Title?");
            // type in title of song
            var songTitle = Console.ReadLine();
            Console.WriteLine("Lyrics?");
            // type in a few lyrics of song
            var lyrics = Console.ReadLine();
            Console.WriteLine("Length?");
            // type in the length of song
            var length = Console.ReadLine();
            Console.WriteLine("Genre?");
            // type in the genre of song
            var genre = Console.ReadLine();
            // method to return the album Id
            AddSongToDb(albumId, songTitle, lyrics, length, genre);
            Console.WriteLine("Would you like to add another song?\n\n (Y)ES or (N)O?");
            var addAnother = Console.ReadLine().ToUpper();
            var addSongs = true;
            while (addSongs)
            {
                if (addAnother != "Y" && addAnother != "N")
                {
                    Console.WriteLine("Not a valid input. Please choose either (Y)ES or (N)O.");
                    addAnother = Console.ReadLine().ToUpper();
                }
                // while loop to keep adding songs
                if (addAnother == "Y")
                {
                    // add a song to album using AddSong()
                        AddSongUI(albumId);
                }
                else if (addAnother == "N")
                {
                    addSongs = false;
                }
            }
        }
        public void AddSongToDb(int albumId, string songTitle, string lyrics, string length, string genre)
        {
            // add song
            var newSong = new Song()
            {
            AlbumId = albumId,
            Title = songTitle,
            Lyrics = lyrics,
            Length = length,
            Genre = genre
            };
            db.Songs.Add(newSong);
            db.SaveChanges();
        }
        public void LetBandGo()
        {
            // set IsSigned to false
            ViewAllBands();
            Console.WriteLine("What band would you like to let go? Please choose band ID #.");
            var letGo = int.Parse(Console.ReadLine());
            var unSign = db.Bands.FirstOrDefault(band => band.Id == letGo);
            if ( unSign == null)
            {
                Console.WriteLine("Not a valid option. Pick valid band ID #.");
                letGo = int.Parse(Console.ReadLine());
                unSign = db.Bands.FirstOrDefault(band => band.Id == letGo);
            }
            if (unSign != null)
            {
                unSign.IsSigned = false;
                db.SaveChanges();
            }
        }
        public void ResignBand()
        {
            // set IsSigned to true
            ViewAllBands();
            Console.WriteLine("What band would you like to sign? Please choose band ID #.");
            var signUp = int.Parse(Console.ReadLine());
            var signed = db.Bands.FirstOrDefault(band => band.Id == signUp);
            if (signed == null)
            {
                Console.WriteLine("Not a valid option. Pick valid band ID #.");
                signUp = int.Parse(Console.ReadLine());
                signed = db.Bands.FirstOrDefault(band => band.Id == signUp);
            }
            if (signed != null)
            {
                signed.IsSigned = true;
                db.SaveChanges();
            }
        }
        public void ViewSignedBands()
        {
            // view all bands where IsSigned is true
            var signedBands = db.Bands.Where(band => band.IsSigned == true);
            foreach (var signedBand in signedBands)
            {
                Console.WriteLine($"({signedBand.Id}): {signedBand.Name}");
            }
            Console.WriteLine("Press Enter to return MAIN MENU.");
            Console.ReadKey();
        }
        public void ViewUnsignedBands()
        {
            // view all bands where IsSigned is false
            var unSignedBands = db.Bands.Where(band => band.IsSigned == false);
            foreach (var unsignedBand in unSignedBands)
            {
                Console.WriteLine($"({unsignedBand.Id}): {unsignedBand.Name}");
            }
                Console.WriteLine("Press Enter to return MAIN MENU.");
                Console.ReadKey();
        }
        public void ViewAllBands()
        {
            var allBands = db.Bands.OrderBy(band => band.Name);
            foreach (var band in allBands)
            {
                Console.WriteLine($"({band.Id}): {band.Name}");
            }
        }
         public void ViewBandAlbums()
        {
            // view all albums of all bands
            ViewAllBands();
            Console.WriteLine("Choose band ID # to view all Albums.");
            var bandId = int.Parse(Console.ReadLine());
            var bandAlbums = db.Albums.Where(album => album.BandId == bandId);
            foreach (var album in bandAlbums)
            {
                Console.WriteLine($"{album.Title}");
            }
            Console.WriteLine("Press Enter to return MAIN MENU.");
            Console.ReadKey();
        }
        public void ViewAllAlbums()
        {
            // order all albums by ReleaseDate
        }
          public void ViewAlbumReleaseDate()
        {
            // order all albums by ReleaseDate
        }
        public void ViewAlbumSongs()
        {
            // view the songs of a specific album of a specific band
        }
    }
}