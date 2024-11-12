namespace Test;
using Shared;

public class AdvertisementTests
{
  //RegisterNewAd
  [Fact]
  public void RegisterNewAd()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    MovieTheater.AdvertisementList = new();
    try
    {
      // act
      MovieTheater.RegisterNewAd("Bob's", "Bait Shop on 400N", 30, .5m);
    }
    catch
    {
      // assert
      Assert.Fail("Shouldn't get here");
    }
    Assert.Single(MovieTheater.AdvertisementList);
  }
  [Fact]
  public void RegisterSameAdTwice()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    MovieTheater.AdvertisementList = new();
    try
    {
      // act
      MovieTheater.RegisterNewAd("Bob's", "Bait Shop on 400N", 30, .5m);
      MovieTheater.RegisterNewAd("Bob's", "Bait Shop on 400N", 30, .5m);
    }
    catch
    {
      // assert
      Assert.Single(MovieTheater.AdvertisementList);
      return;
    }
    Assert.Fail("Shouldn't get here");
  }
  //ScheduleAdForMovie
  [Fact]
  public void ScheduleOneAd()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    AdvertisementTuple advertisement = (
      name: "Bob's",
      description: "description",
      lengthInSeconds: 5,
      chargePerPlay: 30
    );
    MovieTheater.AdvertisementList = [advertisement];

    ShowingTuple showing = (
      showingID: 1,
      showingDateTime: DateTime.Today,
      ticketPrice: 5m,
      theaterRoom: 1,
      movieTitle: "Clifford"
    );
    MovieTheater.ScheduleList = [showing];
    MovieTheater.ScheduledAdsList = new();

    // act
    MovieTheater.ScheduleAdForMovie(1, "Bob's");

    // assert
    Assert.Single(MovieTheater.ScheduledAdsList);
  }

  [Fact]
  public void ScheduleMultipleAdsForDifferentShowings()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    AdvertisementTuple advertisement = (
      name: "Bob's",
      description: "description",
      lengthInSeconds: 5,
      chargePerPlay: 30
    );
    MovieTheater.AdvertisementList = [advertisement];

    ShowingTuple showing = (
      showingID: 1,
      showingDateTime: DateTime.Today,
      ticketPrice: 5m,
      theaterRoom: 1,
      movieTitle: "Clifford"
    );
    MovieTheater.ScheduleList = [showing];
    MovieTheater.ScheduledAdsList = new();
    // act
    MovieTheater.ScheduleAdForMovie(1, "Bob's");
    MovieTheater.ScheduleAdForMovie(2, "Bob's");
    // assert
    Assert.Equal(2, MovieTheater.ScheduledAdsList.Count);
  }
  //END FINAL PROJECT 07 TESTS


  //START FINAL PROJECT 08 TESTS
  //AdvertisingReport3_DailyShowingAndAdvertisementLength
  [Fact]
  public void CanGetShowingAndAdvetisementLengthReport()
  {
    // arrange a movie showing
    MovieTheater.ReadDataInFromAllFiles();
    ShowingTuple movieShowing = (
      showingID: 1,
      showingDateTime: DateTime.Today,
      ticketPrice: 5,
      theaterRoom: 1,
      movieTitle: "Clifford"
    );
    MovieTheater.ScheduleList = [movieShowing];

    // arrange an advertisement that can be scheduled
    DateOnly date = DateOnly.FromDateTime(DateTime.Today);
    AdvertisementTuple advertisement = (
      name: "Bill's",
      description: "description",
      lengthInSeconds: 2,
      chargePerPlay: 3
    );
    MovieTheater.AdvertisementList = [advertisement];

    // arrange, schedule the advertisement to run twice for today's showing
    ScheduledAdsTuple schedule1 = (scheduleShowingID: 1, advertisementName: "Bill's");
    ScheduledAdsTuple schedule2 = (scheduleShowingID: 1, advertisementName: "Bill's");
    MovieTheater.ScheduledAdsList = [schedule1, schedule2];

    // act
    var revenueReport = MovieTheater.AdvertisingReport3_DailyShowingAndAdvertisementLength(date);

    // assert
    Assert.Contains("1", revenueReport); // contains showing id 1
    Assert.Contains("2", revenueReport); // 2 ads were played
    Assert.Contains("$6.00", revenueReport); // made $6.00 in showing
    Assert.Contains("0:04", revenueReport); // ran ads for 4 seconds
  }


  //AdvertisingReport4_DailyAdvertisingRevenue
  [Fact]
  public void CanGetDailyAdvertisingRevenueReport()
  {
    // arrange a movie showing
    MovieTheater.ReadDataInFromAllFiles();
    ShowingTuple movieShowing = (
      showingID: 1,
      showingDateTime: DateTime.Today,
      ticketPrice: 5,
      theaterRoom: 1,
      movieTitle: "Clifford"
    );
    MovieTheater.ScheduleList = [movieShowing];

    // arrange an advertisement that can be scheduled
    DateOnly date = DateOnly.FromDateTime(DateTime.Today);
    AdvertisementTuple advertisement = (
      name: "Bill's",
      description: "description",
      lengthInSeconds: 2,
      chargePerPlay: 3
    );
    MovieTheater.AdvertisementList = [advertisement];

    // arrange, schedule the advertisement to run twice for today's showing
    ScheduledAdsTuple schedule1 = (scheduleShowingID: 1, advertisementName: "Bill's");
    ScheduledAdsTuple schedule2 = (scheduleShowingID: 1, advertisementName: "Bill's");
    MovieTheater.ScheduledAdsList = [schedule1, schedule2];

    // act
    var revenueReport = MovieTheater.AdvertisingReport4_DailyAdvertisingRevenue(date);

    // assert
    Assert.Contains("Bill's", revenueReport); // advertisement name is Bill's
    Assert.Contains("2", revenueReport); // 2 showings that day
    Assert.Contains("$6.00", revenueReport); // made $6.00 in showing
  }

  //AdvertisingMonthlyRevenueReport
  [Fact]
  public void TwoShowingsInTheMonth()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    // arrange a movie showing
    ShowingTuple showing1 = (
      showingID: 1,
      showingDateTime: new DateTime(2024, 03, 03),
      ticketPrice: 5,
      theaterRoom: 1,
      movieTitle: "Clifford"
    );
    ShowingTuple showing2 = (
      showingID: 2,
      showingDateTime: new DateTime(2024, 03, 20),
      ticketPrice: 8,
      theaterRoom: 2,
      movieTitle: "Clifford"
    );
    MovieTheater.ScheduleList = [showing1, showing2];

    // arrange an advertisement that can be scheduled
    AdvertisementTuple advertisement = (
      name: "Bill's advertisement",
      description: "description",
      lengthInSeconds: 2,
      chargePerPlay: 3.25m
    );
    MovieTheater.AdvertisementList = [advertisement];

    // arrange, schedule the advertisement to run twice for today's showing
    ScheduledAdsTuple schedule1 = (scheduleShowingID: 1, advertisementName: "Bill's advertisement");
    ScheduledAdsTuple schedule2 = (scheduleShowingID: 2, advertisementName: "Bill's advertisement");
    MovieTheater.ScheduledAdsList = [schedule1, schedule2];

    // act
    DateOnly dateInMonth = new DateOnly(2024, 03, 03);
    string revenueReport = MovieTheater.AdvertisingReport5_MonthlyAdvertisingRevenue(dateInMonth);
    // assert
    Assert.Contains("Bill's advertisement", revenueReport); // advertisement name should be on the report
    Assert.Contains("2", revenueReport); // there were 2 showings
    Assert.Contains("$6.50", revenueReport); // $3.25 per showing with 2 showings is $6.50

  }
  [Fact]
  public void TwoShowingsInTheMonthAndOneNot()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    // arrange a movie showing
    ShowingTuple showing1 = (
      showingID: 1,
      showingDateTime: new DateTime(2024, 03, 03),
      ticketPrice: 5,
      theaterRoom: 1,
      movieTitle: "Clifford"
    );
    ShowingTuple showing2 = (
      showingID: 2,
      showingDateTime: new DateTime(2024, 03, 20),
      ticketPrice: 8,
      theaterRoom: 2,
      movieTitle: "Clifford"
    );
    ShowingTuple showing3_OutOfMonth = (
      showingID: 3,
      showingDateTime: new DateTime(2024, 04, 01),
      ticketPrice: 8,
      theaterRoom: 2,
      movieTitle: "Clifford"
    );
    MovieTheater.ScheduleList = [showing1, showing2, showing3_OutOfMonth];

    // arrange an advertisement that can be scheduled
    AdvertisementTuple advertisement = (
      name: "Bill's advertisement",
      description: "description",
      lengthInSeconds: 2,
      chargePerPlay: 3.25m
    );
    MovieTheater.AdvertisementList = [advertisement];

    // arrange, schedule the advertisement to run twice for today's showing
    ScheduledAdsTuple schedule1 = (scheduleShowingID: 1, advertisementName: "Bill's advertisement");
    ScheduledAdsTuple schedule2 = (scheduleShowingID: 2, advertisementName: "Bill's advertisement");
    ScheduledAdsTuple schedule3 = (scheduleShowingID: 3, advertisementName: "Bill's advertisement");
    MovieTheater.ScheduledAdsList = [schedule1, schedule2, schedule3];

    // act
    DateOnly dateInMonth = new DateOnly(2024, 03, 03);
    string revenueReport = MovieTheater.AdvertisingReport5_MonthlyAdvertisingRevenue(dateInMonth);
    // assert
    Assert.Contains("Bill's advertisement", revenueReport); // advertisement name should be on the report
    Assert.Contains("2", revenueReport); // there were 2 showings
    Assert.Contains("$6.50", revenueReport); // $3.25 per showing with 2 showings is $6.50
  }
}