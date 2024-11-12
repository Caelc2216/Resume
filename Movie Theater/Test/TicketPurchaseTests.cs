namespace Test;
using Shared;

public class TicketPurchaseTests
{
    //Purchase Tickets
    [Fact]
    public void PurchaseTicket_NormalCustomer()
    {
        // arrange
        MovieTheater.ReadDataInFromAllFiles();
        int beforeCount = Shared.MovieTheater.SoldTicketList.Count;
        // act
        Shared.MovieTheater.TicketPurchase(Shared.MovieTheater.ScheduleList[0].showingID, Shared.MovieTheater.ScheduleList[0].ticketPrice);
        // assert
        int afterCount = Shared.MovieTheater.SoldTicketList.Count;
        Assert.Equal(1, (afterCount - beforeCount));//1 more item was added to the List
    }
    [Fact]
    public void PurchaseTicket_PreferredCustomer()
    {
        // arrange
        MovieTheater.ReadDataInFromAllFiles();
        int beforeCount = Shared.MovieTheater.SoldTicketList.Count;
        int beforeTicketPoints = Shared.MovieTheater.PreferredCustomerList[0].ticketPoints;
        // act
        Shared.MovieTheater.TicketPurchase(
               Shared.MovieTheater.ScheduleList[0].showingID,
               Shared.MovieTheater.ScheduleList[0].ticketPrice,
               Shared.MovieTheater.PreferredCustomerList[0].preferredCustomerID);
        // assert
        int afterCount = Shared.MovieTheater.SoldTicketList.Count;
        Assert.Equal(1, (afterCount - beforeCount)); //1 more item was added to the List!
        int afterTicketPoints = Shared.MovieTheater.PreferredCustomerList[0].ticketPoints;
        Assert.Equal(1, (afterTicketPoints - beforeTicketPoints));//TicketPoints went up by 1!
    }

    //SumOfTicketsForShowing
    [Fact]
    public void NoTicketsWereSold()
    {
        // arrange
        MovieTheater.ReadDataInFromAllFiles();
        MovieTheater.SoldTicketList = new();
        // act

        int sold = MovieTheater.SumOfTotalSoldTicketsForShowing(1);

        // assert
        Assert.Equal(0, sold);
    }
    
    [Fact]
    public void OneTicketWasSold()
    {
        // arrange
        MovieTheater.ReadDataInFromAllFiles();
        MovieTheater.SoldTicketList = [(DateTime.Now, 1, 55m, -1)];
        int sold = MovieTheater.SumOfTotalSoldTicketsForShowing(1);
        Assert.Equal(1, sold);
    }

    //HowManySeatsAreAvalibleForShowingID
    [Fact]
    public void AllSeatsLeft()
    {
        // arrange
        MovieTheater.ReadDataInFromAllFiles();
        MovieTheater.SoldTicketList = new();
        MovieTheater.ScheduleList = [(1, DateTime.Now, 5.50m, 4, "Clifford")];
        //act
        int seatsAvaliblie = MovieTheater.HowManySeatsAreAvailableForShowing(1);

        //assert
        Assert.Equal(8888, seatsAvaliblie);
    }

    [Fact]
    public void OneSeatTaken()
    {
        // arrange
        MovieTheater.ReadDataInFromAllFiles();
        MovieTheater.SoldTicketList = new();
        MovieTheater.ScheduleList = [(1, DateTime.Now, 5.50m, 4, "Clifford")];
        MovieTheater.TicketPurchase(1, 5.50m);
        // act
        int seatsAvaliblie = MovieTheater.HowManySeatsAreAvailableForShowing(1);
        //assert
        Assert.Equal(8887, seatsAvaliblie);
    }

    //GetDailyScheduleBasic
    [Fact]
    public void GetDailySchedule_BasicWithoutAnyShowings()
    {
        // arrange
        MovieTheater.ReadDataInFromAllFiles();
        MovieTheater.ScheduleList = new();
        // act
        var dailySchedule = MovieTheater.GetDailySchedule_Basic(DateOnly.Parse("1/1/2001"));
        // assert
        Assert.Empty(dailySchedule);
    }

    [Fact]
    public void GetDailySchedule_BasicWithOneShowing()
    {
        // arrange
        MovieTheater.ReadDataInFromAllFiles();
        MovieTheater.ScheduleList = new();
        DateTime date = new DateTime(2024, 4, 10);
        MovieTheater.ScheduleList.Add((1, date, 5, 1, "Clifford"));
        // act
        var dailySchedule = MovieTheater.GetDailySchedule_Basic(DateOnly.FromDateTime(date));
        // assert
        Assert.Single(dailySchedule);
    }

    [Fact]
    public void GetDailySchedule_OnlyShowsShowingsForThatDay()
    {
        // arrange
        MovieTheater.ReadDataInFromAllFiles();
        MovieTheater.ScheduleList = new();
        DateTime date = new DateTime(2024, 4, 10);
        DateTime otherDate = new DateTime(2024, 4, 11);
        // act
        MovieTheater.ScheduleList.Add((1, date, 5, 1, "Clifford"));
        MovieTheater.ScheduleList.Add((1, otherDate, 5, 1, "Clifford"));
        var dailySchedule = MovieTheater.GetDailySchedule_Basic(DateOnly.FromDateTime(date));
        // assert
        Assert.Single(dailySchedule);
    }
    [Fact]
public void DailyTicketRevenueReportHasOneTicket()
{
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    DateTime date = new DateTime(2024, 4, 10);
    ShowingTuple showing = (
        showingID: 1,
        showingDateTime: date,
        ticketPrice: 5m,
        theaterRoom: 1,
        movieTitle: "Clifford"
    );
    MovieTheater.ScheduleList = [showing];
    MovieTheater.SoldTicketList = [
        (
            soldDateTime: new DateTime(2024, 4, 09),
            showingID: 1,
            revenueCharged: 5m,
            preferredCustomerNum: -1
        )
    ];

    // act
    var ticketReport = MovieTheater.TicketReport5_TicketSalesRevenue(DateOnly.FromDateTime(date));

    // assert
    Assert.Contains("$5.00", ticketReport); // $15.00 from the two sales
    Assert.Contains("Clifford", ticketReport); // movie name is in the report
    Assert.Contains("0", ticketReport); // 0 tickets given away to preferred customers
    Assert.Contains("4/10/2024", ticketReport); // showtime is present in the report
}
[Fact]
public void DailyTicketRevenueReportHasTwoTickets()
{
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    DateTime date = new DateTime(2024, 4, 10);
    ShowingTuple showing = (
        showingID: 1,
        showingDateTime: date,
        ticketPrice: 5m,
        theaterRoom: 1,
        movieTitle: "Clifford"
    );
    MovieTheater.ScheduleList = [showing];

    MovieTheater.SoldTicketList = [
        (
            soldDateTime: new DateTime(2024, 4, 09),
            showingID: 1,
            revenueCharged: 5m,
            preferredCustomerNum: -1
        ),
        (
            soldDateTime: new DateTime(2024, 4, 08),
            showingID: 1,
            revenueCharged: 5m,
            preferredCustomerNum: -1
        ),
    ];

    // act
    var ticketReport = MovieTheater.TicketReport5_TicketSalesRevenue(DateOnly.FromDateTime(date));

    // assert
    Assert.Contains("$10.00", ticketReport); // $15.00 from the two sales
}
}