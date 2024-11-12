global using MovieTuple = (string title, int runLengthMinutes, string advertisingMesssage, string leads);
global using ShowingTuple = (int showingID, System.DateTime showingDateTime, decimal ticketPrice, int theaterRoom, string movieTitle);
global using DailyShowingTuple = (string MovieTitle, System.DateTime showTime);

global using PreferredCustomerTuple = (int preferredCustomerID, string name, string email, int ticketPoints, int concessionPoints);
global using SoldTicketTuple = (System.DateTime soldDateTime, int showingID, decimal revenueCharged, int preferredCustomerNum);

//concessions
global using ConcessionMenuTuple = (string itemName, string itemDescription, decimal price);
global using ConcessionSaleTuple = (System.DateTime soldDateTime, string itemName, int quantitySold, decimal revenueCollected, int preferredCustomerID);

// advertisements
global using AdvertisementTuple = (string name, string description, int lengthInSeconds, decimal chargePerPlay);
global using ScheduledAdsTuple = (int scheduleShowingID, string advertisementName);

using System.Globalization;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;
namespace DataStorage;

public class FileAccess
{

  public static List<ConcessionMenuTuple> ReadConcessionMenuData()
  {
    string filePath = GetBasePath() + "ConcessionMenuData.txt";
    List<ConcessionMenuTuple> menuList = new();

    foreach (var line in File.ReadAllLines(filePath))
    {
      var x = line.Split(";");
      ConcessionMenuTuple item = (
        itemName: x[0],
        itemDescription: x[1],
        price: decimal.Parse(x[2], NumberStyles.Currency)
      );
      menuList.Add(item);
    }
    return menuList;
  }
  public static List<ConcessionSaleTuple> ReadConcessionSalesData()
  {
    string filePath = GetBasePath() + "ConcessionSalesData.txt";
    List<ConcessionSaleTuple> saleList = new();
    // TODO 
    //(System.DateTime soldDateTime, string itemName, int quantitySold, decimal revenueCollected, int preferredCustomerID);
    string [] lines = File.ReadAllLines(filePath);
    foreach (var line in lines)
    {
      string [] itemsOfSale = line.Split(";");
      ConcessionSaleTuple sale = (soldDateTime: DateTime.Parse(itemsOfSale[0]), 
      itemName: itemsOfSale[1], 
      quantitySold: int.Parse(itemsOfSale[2]),
      revenueCollected: decimal.Parse(itemsOfSale[3], NumberStyles.Currency),
      preferredCustomerID: int.Parse(itemsOfSale[4]));
      saleList.Add(sale);
    }
    
    return saleList;
  }

  public static void WriteConcessionMenuData(List<ConcessionMenuTuple> menuList)
  {
    string filePath = GetBasePath() + "ConcessionMenuData.txt";
    List<string> fileLines = new List<string>();
    foreach (var x in menuList)
    {
      string menuLineForfile =
        x.itemName + ";" +
        x.itemDescription + ";" +
        x.price.ToString("C2", CultureInfo.CurrentCulture); // properly handle the '$'

      fileLines.Add(menuLineForfile);
    }
    File.WriteAllLines(filePath, fileLines);
  }

  public static void WriteConcessionSalesData(List<ConcessionSaleTuple> soldConcessions)
  {
    string filePath = GetBasePath() + "ConcessionSalesData.txt";
    List<string> lines = new();
    foreach (ConcessionSaleTuple sale in soldConcessions)
    {
      string line =$"{sale.soldDateTime};{sale.itemName};{sale.quantitySold};{sale.revenueCollected};{sale.preferredCustomerID}";
      lines.Add(line);
    }
    File.WriteAllLines(filePath, lines);
  }


  public static List<MovieTuple> ReadMovies()
  {
    string filePath = GetBasePath() + "MovieData.txt";
    List<MovieTuple> movies = new();
    // (string title, int runLengthMinutes, string advertisingMesssage, string leads)
   foreach (var line in File.ReadAllLines(filePath))
    {
      var x = line.Split(";");
      MovieTuple item = (
        title: x[0],
        runLengthMinutes: int.Parse(x[1]),
        advertisingMesssage: x[2],
        leads: x[3]);
      movies.Add(item);
    }
    return movies;
  }

  public static List<PreferredCustomerTuple> ReadPreferredCustomerData()
  {
    string filePath = GetBasePath() + "PreferredCustomerData.txt";
    List<PreferredCustomerTuple> customers = new();
    // (int preferredCustomerID, string name, string email, int ticketPoints, int concessionPoints)
    foreach (var line in File.ReadAllLines(filePath))
    {
      var split = line.Split(';');
      PreferredCustomerTuple toAdd = (preferredCustomerID: int.Parse(split[0]),
      name: split[1],
      email: split[2],
      ticketPoints: int.Parse(split[3]),
      concessionPoints: int.Parse(split[4])
      );
      customers.Add(toAdd);
    }
    return customers;
  }
  public static Dictionary<int, int> ReadTheaterRoomData()
  {
    string filePath = GetBasePath() + "TheaterRoomData.txt";
    Dictionary<int, int> x = new();
    foreach (var line in File.ReadAllLines(filePath))
    {
      var split = line.Split(";");
      x.Add(int.Parse(split[0]), int.Parse(split[1]));
    }
    return x;
  }
  public static List<ShowingTuple> ReadScheduleData()
  {
    string filePath = GetBasePath() + "ScheduleData.txt";
    List<ShowingTuple> schedule = new();
    // (int showingID, System.DateTime showingDateTime, decimal ticketPrice, int theaterRoom, string movieTitle)
    foreach (var line in File.ReadAllLines(filePath))
    {
      string[] x = Convert.ToString(line).Split(";");
      ShowingTuple showingTuple = (
        showingID: int.Parse(x[0]),
        showingDateTime: DateTime.Parse(x[1]),
        ticketPrice: decimal.Parse(x[2], NumberStyles.Currency),
        theaterRoom: int.Parse(x[3]),
        movieTitle: x[4]
      );
      schedule.Add(showingTuple);
    }
    return schedule;
  }
  public static List<SoldTicketTuple> ReadSoldTicketData()
  {
    string filePath = GetBasePath() + "SoldTicketData.txt";
    List<SoldTicketTuple> ticketList = new();
    foreach (var line in File.ReadAllLines(filePath))
    {
      string [] partsOfLine = line.Split(";");
      SoldTicketTuple ticket = (
        soldDateTime: DateTime.Parse(partsOfLine[0]),
        showingID: int.Parse(partsOfLine[1]),
        revenueCharged: Decimal.Parse(partsOfLine[2], NumberStyles.Currency),
        preferredCustomerNum: int.Parse(partsOfLine[3])
      );
      ticketList.Add(ticket);
    }
    // TODO 
    return ticketList;
  }
  public static List<AdvertisementTuple> ReadAdvertisementData()
  {
    string filePath = GetBasePath() + "AdvertisementData.txt";
    List<AdvertisementTuple> adList = new();
    // (string name, string description, int lengthInSeconds, decimal chargePerPlay)
    foreach (var line in File.ReadAllLines(filePath))
    {
      string [] partsOfLine = line.Split(";");
      AdvertisementTuple advertisement = (
        name: partsOfLine[0],
        description: partsOfLine[1],
        lengthInSeconds: int.Parse(partsOfLine[2]),
        chargePerPlay: decimal.Parse(partsOfLine[3], NumberStyles.Currency)
      );
      adList.Add(advertisement);
    }
    return adList;
  }
  public static List<ScheduledAdsTuple> ReadAdvertisementScheduleData()
  {
    string filePath = GetBasePath() + "AdvertisementScheduleData.txt";
    List<ScheduledAdsTuple> adList = new();
    // (int scheduleShowingID, string advertisementName)
    foreach (var line in File.ReadAllLines(filePath))
    {
      string [] partsofline = line.Split(";");
      ScheduledAdsTuple ad = (
        scheduleShowingID: int.Parse(partsofline[0]),
        advertisementName: partsofline[1]
      );
      adList.Add(ad);
    } 
    return adList;
  }


  public static void WriteMovies(List<MovieTuple> movies)
  {
    string filePath = GetBasePath() + "MovieData.txt";
    List<string> lines = new();
    // (string title, int runLengthMinutes, string advertisingMesssage, string leads)
    foreach (MovieTuple movie in movies)
    {
      string line =$"{movie.title};{movie.runLengthMinutes};{movie.advertisingMesssage};{movie.leads}";
      lines.Add(line);
    }
    File.WriteAllLines(filePath, lines);
  }
  public static void WritePreferredCustomerData(List<PreferredCustomerTuple> customers)
  {
    string filePath = GetBasePath() + "PreferredCustomerData.txt";
    List<string> lines = new();
    // (int preferredCustomerID, string name, string email, int ticketPoints, int concessionPoints)
    foreach (PreferredCustomerTuple preferredCustomer in customers)
    {
      string line = $"{preferredCustomer.preferredCustomerID};{preferredCustomer.name};{preferredCustomer.email};{preferredCustomer.ticketPoints};{preferredCustomer.concessionPoints}";
      lines.Add(line);
    }
    File.WriteAllLines(filePath, lines);
  }
  public static void WriteTheaterRoomData(Dictionary<int, int> rooms)
  {
    string filePath = GetBasePath() + "TheaterRoomData.txt";
    List<string> lines = new();
    foreach (KeyValuePair<int, int> item in rooms)
    {
      string line = $"{item.Key};{item.Value}";
      lines.Add(line);
    }
    File.WriteAllLines(filePath, lines);
  }
  public static void WriteScheduleData(List<ShowingTuple> schedule)
  {
    string filePath = GetBasePath() + "ScheduleData.txt";
    List<string> lines = new();
     // (int showingID, System.DateTime showingDateTime, decimal ticketPrice, int theaterRoom, string movieTitle)
    foreach (ShowingTuple showing in schedule)
    {
      string line = $"{showing.showingID};{showing.showingDateTime};{showing.ticketPrice};{showing.theaterRoom};{showing.movieTitle}";
      lines.Add(line);
    }
    File.WriteAllLines(filePath, lines);
  }
  public static void WriteSoldTicketData(List<SoldTicketTuple> soldTickets)
  {
    List<string> ticketsSold = new();
    string filePath = GetBasePath() + "SoldTicketData.txt";
    foreach (SoldTicketTuple ticket in soldTickets)
    {
      string line =$"{ticket.soldDateTime};{ticket.showingID};{ticket.revenueCharged: $0.00};{ticket.preferredCustomerNum}";
      ticketsSold.Add(line);
    }
    File.WriteAllLines(filePath, ticketsSold);
  }

  public static void WriteAdvertisementData(List<AdvertisementTuple> advertisements)
  {
    List<string> ads = new();
    string filePath = GetBasePath() + "AdvertisementData.txt";
    // (string name, string description, int lengthInSeconds, decimal chargePerPlay)
    foreach (AdvertisementTuple ad in advertisements)
    {
      string line = $"{ad.name};{ad.description};{ad.lengthInSeconds};{ad.chargePerPlay:$0.00}";
      ads.Add(line);    
    } 
    File.WriteAllLines(filePath, ads);
  }
  public static void WriteAdvertisementScheduleData(List<ScheduledAdsTuple> scheduleAds)
  {
    List<string> adsScheduled = new();
    string filePath = GetBasePath() + "AdvertisementScheduleData.txt";
    // (int scheduleShowingID, string advertisementName) 
    foreach (ScheduledAdsTuple ads in scheduleAds)
    {
      string line = $"{ads.scheduleShowingID};{ads.advertisementName}";
      adsScheduled.Add(line);    
    }
    File.WriteAllLines(filePath, adsScheduled);
  }

  // do not change, makes datafiles discoverable between tests and user interface
  public static string GetBasePath()
  {
    if (Directory.GetCurrentDirectory().Contains("UserInterface"))
      return "../";
    if (Directory.GetCurrentDirectory().Contains("Test"))
      return "../../../../";

    return "./";
  }
}
