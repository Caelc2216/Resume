namespace Test;
using Shared;

public class Final_PreferredCustomerTests
{
  public void SetupScenario()
  {
    MovieTheater.ReadDataInFromAllFiles();
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

    MovieTheater.SoldTicketList = [];

    MovieTheater.ConcessionMenuList = [
      (itemName: "Large Popcorn", itemDescription: "Large bucket of popcorn, one free refill", price: 8.90m),
      (itemName: "Medium Popcorn", itemDescription: "Medium bucket of popcorn", price: 5.01m),
      (itemName: "Small Popcorn", itemDescription: "Small bucket of popcorn", price: 3.99m),
    ];

    MovieTheater.ConcessionSaleList = [];
  }

  [Fact]
  public void CanRegisterPreferredCustomer()
  {
    SetupScenario();

    MovieTheater.PreferredCustomerRegistration(800, "Test User", "Test@test.test");

    PreferredCustomerTuple customer = MovieTheater.PreferredCustomerList.Last();

    Assert.Equal("Test User", customer.name);
    Assert.Equal("Test@test.test", customer.email);
    Assert.Equal(800, customer.preferredCustomerID);
    Assert.Equal(0, customer.ticketPoints);
    Assert.Equal(0, customer.concessionPoints);
  }

  [Fact]
  public void PreferredCustomerIdsMustBeUnique()
  {
    SetupScenario();
    try
    {
      MovieTheater.PreferredCustomerRegistration(800, "Test User", "Test@test.test");
      MovieTheater.PreferredCustomerRegistration(800, "Test User", "Test@test.test");
    }
    catch
    {
      Assert.True(true);
      return;
    }
    Assert.Fail("should throw exception when same preferred customer id used");
  }
  
  [Fact]
  public void WhenPreferredCustomerPurchasesTicketWithPoints_DoesNotShowUpInRevenue()
  {
    SetupScenario();

    MovieTheater.PreferredCustomerList = [(
      preferredCustomerID: 800, 
      name: "Test User", 
      email: "Test@test.test",
      ticketPoints: 1000,
      concessionPoints: 1000
    )];
    MovieTheater.TicketPurchase(22, 0, 800);

    string report = MovieTheater.TicketReport5_TicketSalesRevenue(new DateOnly(2024, 04, 09));

    Assert.Contains("1", report); // one ticket given away
    Assert.Contains("$0.00", report); // no revenue made
  }
  
  [Fact]
  public void WhenPreferredCustomerPurchasesConcessionWithPoints_DoesNotShowUpInRecieptsReport()
  {
    SetupScenario();

    MovieTheater.PreferredCustomerList = [(
      preferredCustomerID: 800, 
      name: "Test User", 
      email: "Test@test.test",
      ticketPoints: 1000,
      concessionPoints: 1000
    )];

    MovieTheater.PurchaseMenuItem("Test User", "Large Popcorn", 1, true);

    string report = MovieTheater.ConcessionReport3_AllReceipts();

    Assert.Contains("$0.00", report); // no money made on sale
  }

  [Fact]
  public void WhenPreferredCustomerPurchasesConcessionWithPoints_DoesNotShowUpInDailyRevenueTotals()
  {
    SetupScenario();

    MovieTheater.PreferredCustomerList = [(
      preferredCustomerID: 800, 
      name: "Test User", 
      email: "Test@test.test",
      ticketPoints: 1000,
      concessionPoints: 1000
    )];

    MovieTheater.PurchaseMenuItem("Test User", "Large Popcorn", 1, true);

    string report = MovieTheater.ConcessionReport4_RevenueTotalsForAllDays();

    Assert.Contains("$0.00", report); // no money made on sale
  }

  [Fact]
  public void WhenPreferredCustomerPurchasesConcessionWithPoints_DoesNotShowUpInItemTotalsPerDay()
  {
    SetupScenario();

    MovieTheater.PreferredCustomerList = [(
      preferredCustomerID: 800, 
      name: "Test User", 
      email: "Test@test.test",
      ticketPoints: 1000,
      concessionPoints: 1000
    )];

    MovieTheater.PurchaseMenuItem("Test User", "Large Popcorn", 1, true);

    string report = MovieTheater.ConcessionReport5_ItemTotalsPerDay(DateOnly.FromDateTime(DateTime.Now));

    Assert.Contains("$0.00", report); // no money made on sale
  }
}
