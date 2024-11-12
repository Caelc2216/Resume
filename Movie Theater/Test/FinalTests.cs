using Shared;

namespace Test;
public class FinalTests
{
  public void SetupScenario()
  {
    MovieTheater.MovieList = [
      (title: "Dune: Part Two (2024)", runLengthMinutes: 166, advertisingMesssage: "Paul Atreides unites with Chani and the Fremen while seeking revenge against the conspirators who destroyed his family.", leads: "Timothee Chalamet, Zendaya, Rebecca Ferguson"),
      (title: "Leo", runLengthMinutes: 112, advertisingMesssage: "Adam Sandler is a lizard named Leo in this coming-of-age musical comedy about the last year of elementary school as seen through the eyes of a class pet.", leads: "Adam Sandler, Bill Burr"),
      (title: "The Lion King (1994)", runLengthMinutes: 95, advertisingMesssage: "Disney's The Lion King is about a young lion named Simba, who is the crown prince of an African Savanna. When his father dies in an accident staged by his uncle, Simba is made to feel responsible for his father's death and must overcome his fear of taking responsibility as the rightful heir to the throne.", leads: "Jonathan Taylor Thomas, Matthew Broderick"),
    ];
    MovieTheater.TheaterRoomCapacity = new() {
      {2, 500},
      {13, 350},
      {1, 370},
    };
    MovieTheater.ScheduleList = [
      (showingID: 8, showingDateTime: new DateTime(2024, 04, 03, 13, 30, 0), ticketPrice: 5.25m, theaterRoom: 2, movieTitle: "Leo" ),
      (showingID: 4, showingDateTime: new DateTime(2024, 04, 03, 15, 00, 0), ticketPrice: 5.25m, theaterRoom: 13, movieTitle: "The Lion King (1994)" ),
      (showingID: 9, showingDateTime: new DateTime(2024, 04, 02, 12, 00, 0), ticketPrice: 7.13m, theaterRoom: 1, movieTitle: "Dune: Part Two (2024)" ),
      (showingID: 10, showingDateTime: new DateTime(2024, 04, 05, 13, 30, 0), ticketPrice: 6.25m, theaterRoom: 2, movieTitle: "The Lion King (1994)" ),
      (showingID: 11, showingDateTime: new DateTime(2024, 04, 06, 15, 00, 0), ticketPrice: 4.35m, theaterRoom: 13, movieTitle: "Leo" ),
      (showingID: 12, showingDateTime: new DateTime(2024, 04, 07, 12, 00, 0), ticketPrice: 7.13m, theaterRoom: 1, movieTitle: "Dune: Part Two (2024)" ),
      (showingID: 1, showingDateTime: new DateTime(2024, 04, 03, 15, 30, 0), ticketPrice: 5.25m, theaterRoom: 2, movieTitle: "Leo" ),
      (showingID: 2, showingDateTime: new DateTime(2024, 04, 03, 17, 00, 0), ticketPrice: 5.25m, theaterRoom: 13, movieTitle: "The Lion King (1994)" ),
      (showingID: 3, showingDateTime: new DateTime(2024, 04, 02, 13, 00, 0), ticketPrice: 7.13m, theaterRoom: 1, movieTitle: "Dune: Part Two (2024)" ),
      (showingID: 5, showingDateTime: new DateTime(2024, 04, 05, 18, 30, 0), ticketPrice: 6.25m, theaterRoom: 2, movieTitle: "The Lion King (1994)" ),
      (showingID: 20, showingDateTime: new DateTime(2024, 04, 06, 20, 00, 0), ticketPrice: 4.35m, theaterRoom: 13, movieTitle: "Leo" ),
      (showingID: 19, showingDateTime: new DateTime(2024, 04, 07, 15, 00, 0), ticketPrice: 7.13m, theaterRoom: 1, movieTitle: "Dune: Part Two (2024)" ),
      (showingID: 21, showingDateTime: new DateTime(2024, 04, 08, 16, 00, 0), ticketPrice: 8.00m, theaterRoom: 2, movieTitle: "Dune: Part Two (2024)"),
      (showingID: 22, showingDateTime: new DateTime(2024, 04, 09, 19, 30, 0), ticketPrice: 5.50m, theaterRoom: 1, movieTitle: "Leo"),
      (showingID: 23, showingDateTime: new DateTime(2024, 04, 10, 14, 00, 0), ticketPrice: 5.75m, theaterRoom: 13, movieTitle: "The Lion King (1994)"),
      (showingID: 24, showingDateTime: new DateTime(2024, 04, 11, 15, 30, 0), ticketPrice: 6.50m, theaterRoom: 2, movieTitle: "Dune: Part Two (2024)"),
      (showingID: 25, showingDateTime: new DateTime(2024, 04, 12, 18, 00, 0), ticketPrice: 4.00m, theaterRoom: 1, movieTitle: "Dune: Part Two (2024)"),
      (showingID: 26, showingDateTime: new DateTime(2024, 04, 13, 13, 00, 0), ticketPrice: 6.00m, theaterRoom: 13, movieTitle: "Leo"),
      (showingID: 27, showingDateTime: new DateTime(2024, 04, 03, 15, 30, 0), ticketPrice: 7.00m, theaterRoom: 2, movieTitle: "The Lion King (1994)"),
      (showingID: 28, showingDateTime: new DateTime(2024, 04, 05, 17, 00, 0), ticketPrice: 5.50m, theaterRoom: 1, movieTitle: "Dune: Part Two (2024)"),
      (showingID: 29, showingDateTime: new DateTime(2024, 04, 06, 12, 30, 0), ticketPrice: 5.00m, theaterRoom: 13, movieTitle: "Leo"),
      (showingID: 30, showingDateTime: new DateTime(2024, 04, 07, 19, 00, 0), ticketPrice: 7.50m, theaterRoom: 2, movieTitle: "The Lion King (1994)"),
      (showingID: 31, showingDateTime: new DateTime(2024, 04, 08, 14, 00, 0), ticketPrice: 8.25m, theaterRoom: 1, movieTitle: "Dune: Part Two (2024)"),
      (showingID: 32, showingDateTime: new DateTime(2024, 04, 09, 13, 30, 0), ticketPrice: 4.75m, theaterRoom: 13, movieTitle: "Leo"),
      (showingID: 33, showingDateTime: new DateTime(2024, 04, 05, 16, 00, 0), ticketPrice: 6.25m, theaterRoom: 2, movieTitle: "The Lion King (1994)"),
      (showingID: 34, showingDateTime: new DateTime(2024, 04, 01, 18, 30, 0), ticketPrice: 7.75m, theaterRoom: 1, movieTitle: "Dune: Part Two (2024)"),
      (showingID: 35, showingDateTime: new DateTime(2024, 04, 02, 15, 00, 0), ticketPrice: 5.25m, theaterRoom: 13, movieTitle: "Leo")
    ];

    MovieTheater.SoldTicketList = [
      (soldDateTime: new DateTime(2024, 02, 01, 10, 00, 0), showingID: 8, revenueCharged: 5.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 25, 14, 30, 0), showingID: 4, revenueCharged: 5.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 11, 00, 0), showingID: 9, revenueCharged: 7.13m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 04, 12, 30, 0), showingID: 10, revenueCharged: 6.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 05, 13, 45, 0), showingID: 11, revenueCharged: 4.35m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 06, 11, 20, 0), showingID: 12, revenueCharged: 7.13m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 02, 15, 50, 0), showingID: 1, revenueCharged: 5.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 03, 16, 15, 0), showingID: 2, revenueCharged: 5.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 12, 00, 0), showingID: 3, revenueCharged: 7.13m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 10, 00, 0), showingID: 8, revenueCharged: 5.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 25, 14, 30, 0), showingID: 4, revenueCharged: 5.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 11, 00, 0), showingID: 9, revenueCharged: 7.13m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 12, 30, 0), showingID: 10, revenueCharged: 6.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 13, 45, 0), showingID: 11, revenueCharged: 4.35m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 11, 20, 0), showingID: 12, revenueCharged: 7.13m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 15, 50, 0), showingID: 1, revenueCharged: 5.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 16, 15, 0), showingID: 2, revenueCharged: 5.25m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 01, 12, 00, 0), showingID: 3, revenueCharged: 7.13m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 05, 19, 30, 0), showingID: 20, revenueCharged: 4.35m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 06, 14, 00, 0), showingID: 19, revenueCharged: 7.13m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 07, 15, 30, 0), showingID: 21, revenueCharged: 8.00m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 08, 18, 45, 0), showingID: 22, revenueCharged: 5.50m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 09, 13, 15, 0), showingID: 23, revenueCharged: 5.75m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 10, 14, 30, 0), showingID: 24, revenueCharged: 6.50m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 11, 17, 00, 0), showingID: 25, revenueCharged: 4.00m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 12, 12, 30, 0), showingID: 26, revenueCharged: 6.00m, preferredCustomerNum: -1),
      (soldDateTime: new DateTime(2024, 02, 03, 14, 30, 0), showingID: 27, revenueCharged: 7.00m, preferredCustomerNum: -1),

    ];


    MovieTheater.ConcessionMenuList = [
      (itemName: "Large Popcorn", itemDescription: "Large bucket of popcorn, one free refill", price: 8.90m),
      (itemName: "Medium Popcorn", itemDescription: "Medium bucket of popcorn", price: 5.01m),
      (itemName: "Small Popcorn", itemDescription: "Small bucket of popcorn", price: 3.99m),
    ];

    MovieTheater.ConcessionSaleList = [
      (soldDateTime: new DateTime(2024, 2, 10, 14, 30, 0), itemName: "Large Popcorn", quantitySold: 2, revenueCollected: 17.8m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 2, 10, 15, 30, 0), itemName: "Large Popcorn", quantitySold: 3, revenueCollected: 26.7m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 2, 10, 18, 30, 0), itemName: "Medium Popcorn", quantitySold: 3, revenueCollected: 15.03m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 2, 10, 18, 32, 0), itemName: "Small Popcorn", quantitySold: 5, revenueCollected: 19.95m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 3, 10, 14, 30, 0), itemName: "Large Popcorn", quantitySold: 2, revenueCollected: 17.8m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 3, 10, 15, 30, 0), itemName: "Large Popcorn", quantitySold: 3, revenueCollected: 26.7m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 3, 10, 18, 30, 0), itemName: "Medium Popcorn", quantitySold: 3, revenueCollected: 15.03m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 3, 10, 18, 32, 0), itemName: "Small Popcorn", quantitySold: 5, revenueCollected: 19.95m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 2, 11, 14, 30, 0), itemName: "Large Popcorn", quantitySold: 2, revenueCollected: 17.8m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 2, 11, 15, 30, 0), itemName: "Large Popcorn", quantitySold: 3, revenueCollected: 26.7m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 2, 11, 18, 30, 0), itemName: "Medium Popcorn", quantitySold: 3, revenueCollected: 15.03m, preferredCustomerID: -1),
      (soldDateTime: new DateTime(2024, 2, 11, 18, 32, 0), itemName: "Small Popcorn", quantitySold: 5, revenueCollected: 19.95m, preferredCustomerID: -1),
    ];
  }

  [Fact]
  public void CanRegisterNewMovie()
  {
    SetupScenario();
    MovieTheater.NewMovieRegistration("new movie title", 50, "advertising message", "lead actor values");

    Assert.Equal(4, MovieTheater.MovieList.Count);
    Assert.Equal("new movie title", MovieTheater.MovieList.Last().title);
    Assert.Equal(50, MovieTheater.MovieList.Last().runLengthMinutes);
    Assert.Equal("advertising message", MovieTheater.MovieList.Last().advertisingMesssage);
    Assert.Equal("lead actor values", MovieTheater.MovieList.Last().leads);
  }

  [Fact]
  public void CannotRegisterDuplicateMovie()
  {
    SetupScenario();
    try
    {
      MovieTheater.NewMovieRegistration("Dune: Part Two (2024)", 30, "description", "actovalues");
    }
    catch
    {
      // should throw an exception to prevent duplicate movies
      Assert.True(true); //will always pass if we get here
      return;
    }
    Assert.Fail("Shouldn't get here");
  }

  [Fact]
  public void CanScheduleNewMovie()
  {
    SetupScenario();

    MovieTheater.CreateShowingID(99, new DateTime(2024, 2, 11, 18, 32, 0), 5.23m, 13, "Leo");

    Assert.Equal(99, MovieTheater.ScheduleList.Last().showingID);
    Assert.Equal(new DateTime(2024, 2, 11, 18, 32, 0), MovieTheater.ScheduleList.Last().showingDateTime);
    Assert.Equal(5.23m, MovieTheater.ScheduleList.Last().ticketPrice);
    Assert.Equal(13, MovieTheater.ScheduleList.Last().theaterRoom);
    Assert.Equal("Leo", MovieTheater.ScheduleList.Last().movieTitle);
  }

  [Fact]
  public void CannotScheduleNewMovie_WhenMovieTitleNotRegistered()
  {
    SetupScenario();
    try
    {
      MovieTheater.CreateShowingID(99, new DateTime(2024, 2, 11, 18, 32, 0), 5.23m, 13, "title that does not exist");
    }
    catch
    {
      // should throw an exception to prevent movies that dont exist from being scheduled
      Assert.True(true); //will always pass if we get here
      return;
    }
    Assert.Fail("Shouldn't get here");
  }
  [Fact]
  public void CannotScheduleNewMovie_UsingDuplicateShowingID()
  {
    SetupScenario();
    try
    {
      MovieTheater.CreateShowingID(1, new DateTime(2024, 2, 11, 18, 32, 0), 5.23m, 13, "Leo");
    }
    catch
    {
      // should throw an exception to prevent showings from having the same ID
      Assert.True(true); //will always pass if we get here
      return;
    }
    Assert.Fail("Shouldn't get here");
  }

  [Fact]
  public void CanCountTicketsSoldForShowing()
  {
    SetupScenario();
    int seatsSold = MovieTheater.SumOfTotalSoldTicketsForShowing(1);
    Assert.Equal(2, seatsSold);
  }

  [Fact]
  public void CanGetAvailableSeatsForShowing()
  {
    SetupScenario();
    int seatsAvailable = MovieTheater.HowManySeatsAreAvailableForShowing(10);
    Assert.Equal(498, seatsAvailable);
  }

  [Fact]
  public void PurchasingTicket_UsesAvailableSeat()
  {
    SetupScenario();
    MovieTheater.TicketPurchase(10, 5.25m);
    int seatsAvailable = MovieTheater.HowManySeatsAreAvailableForShowing(10);
    Assert.Equal(497, seatsAvailable);
  }

  [Fact]
  public void CustomerCanPurchaseTicket()
  {
    SetupScenario();
    MovieTheater.TicketPurchase(1, 5.25m);

    SoldTicketTuple lastSale = MovieTheater.SoldTicketList.Last();
    Assert.Equal(5.25m, lastSale.revenueCharged);
    Assert.Equal(-1, lastSale.preferredCustomerNum);
    Assert.Equal(1, lastSale.showingID);
  }

  [Fact]
  public void CustomerCannotPurchaseTicketIfNoShowing()
  {
    SetupScenario();
    try
    {
      MovieTheater.TicketPurchase(-999, 500.25m);
    }
    catch
    {
      // should throw an exception to prevent sales for invalid showings
      Assert.True(true); //will always pass if we get here
      return;
    }
    Assert.Fail("Shouldn't get here");
  }

  [Fact]
  public void GetDailySchedule()
  {
    SetupScenario();
    var dailySchedule = MovieTheater.GetDailySchedule_Basic(new DateOnly(2024, 04, 03));
    Assert.Equal(5, dailySchedule.Count);
  }

  [Fact]
  public void TicketRevenueReport()
  {
    SetupScenario();
    var ticketReport = MovieTheater.TicketReport5_TicketSalesRevenue(new DateOnly(2024, 04, 03));

    Assert.Contains("$10.50", ticketReport); // for leo showing
    Assert.Contains("$7.00", ticketReport); // for Lion King showing
    Assert.Contains("The Lion King (1994)", ticketReport);
    Assert.Contains("Leo", ticketReport);
    Assert.Contains("4/3/2024 1:30:00", ticketReport);
    Assert.Contains("4/3/2024 3:00:00", ticketReport);
    Assert.Contains("4/3/2024 3:30:00", ticketReport);
    Assert.Contains("4/3/2024 5:00:00", ticketReport);
    Assert.Contains("4/3/2024 3:30:00", ticketReport);
  }



  // concession tests

  [Fact]
  public void CanPurchaseConcession()
  {
    SetupScenario();
    MovieTheater.PurchaseMenuItem("Bob", "Large Popcorn", 5, false);

    Assert.Equal("Large Popcorn", MovieTheater.ConcessionSaleList.Last().itemName);
    Assert.Equal(5, MovieTheater.ConcessionSaleList.Last().quantitySold);
    Assert.Equal(44.5m, MovieTheater.ConcessionSaleList.Last().revenueCollected);
    Assert.Equal(-1, MovieTheater.ConcessionSaleList.Last().preferredCustomerID);
  }

  [Fact]
  public void CannotPurchaseConcessions_ThatDoNotExist()
  {
    SetupScenario();
    try
    {
      MovieTheater.PurchaseMenuItem("Bob", "Not a Large Popcorn", 5, false);
    }
    catch
    {
      // should throw an exception to sales to concessions that do not exist
      Assert.True(true);
      return;
    }
    Assert.Fail("Shouldn't get here");
  }

  [Fact]
  public void CanGetAllReciepts()
  {
    SetupScenario();
    string dailyReport = MovieTheater.ConcessionReport3_AllReceipts();

    // two data items that should be in report
    Assert.Contains("Large Popcorn", dailyReport); 
    Assert.Contains("$26.70", dailyReport); 
  }

  [Fact]
  public void CanGetRevenueTotalsForAllDays()
  {
    SetupScenario();
    string dailyReport = MovieTheater.ConcessionReport4_RevenueTotalsForAllDays();

    // days that should be in report
    Assert.Contains("2/10/2024", dailyReport); 
    Assert.Contains("3/10/2024", dailyReport); 
    Assert.Contains("2/11/2024", dailyReport); 

    // amount made on each day
    Assert.Contains("$79.48", dailyReport); 
  }
  
  [Fact]
  public void CanGetItemTotalsPerDay()
  {
    SetupScenario();
    string dailyReport = MovieTheater.ConcessionReport5_ItemTotalsPerDay(new DateOnly(2024, 2, 10));

    // items and totals
    Assert.Contains("$44.50", dailyReport); 
    Assert.Contains("$15.03", dailyReport); 
    Assert.Contains("$19.95", dailyReport);
    Assert.Contains("Large Popcorn", dailyReport); 
    Assert.Contains("Medium Popcorn", dailyReport); 
    Assert.Contains("Small Popcorn", dailyReport);
  }
  
  [Fact]
  public void ReportOnDayWithNoSales_ReportsNoMoney()
  {
    SetupScenario();
    string dailyReport = MovieTheater.ConcessionReport5_ItemTotalsPerDay(new DateOnly(2024, 5, 5));

    Assert.DoesNotContain("$", dailyReport);
  }
}