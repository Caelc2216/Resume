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
using Shared;
using DataStorage;
using System.Diagnostics;

internal class Program
{
  private static void Main()
  {
    Shared.MovieTheater.ReadDataInFromAllFiles();
    MainMenu();
    Shared.MovieTheater.WriteDataToAllFiles();

  }
  public static void MainMenu()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("***Main Menu***");
      string menu = "1-Ticket Window\n" +
                      "2-Concession Stand\n" +
                      "3-Advertisement Controls\n" +
                      "4-Scheduling Controls\n" +
                      "5-Theaterwide Controls\n" +
                      "6-Save and Exit\n" +
                      "What would you like to do? ";
      int choice = getIntWillLoop(menu, 1, 6);
      if (choice == 1)//Ticket Window
      {
        TicketWindowMenu();
      }
      else if (choice == 2)//Concession Stand
      {
        ConcessionMenu();
      }
      else if (choice == 3)//Advertisment Controls
      {
        AdvertisementMenu();
      }
      else if (choice == 4)//Scheduling Controls
      {
        SchedulingControlsMenu();
      }
      else if (choice == 5)//Theaterwide Controls
      {
        // TheaterWideMenu();
      }
      else if (choice == 6)// Save and Exit
      {
        return;
      }
    }
  }
  public static void ConcessionMenu()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("***Concessions Menu***");
      string menu = "1-View Menu Items\n" +
                      "2-Purchase a Concession\n" +
                      "3-All Sales Report\n" +
                      "4-Reveneue Per Day Report\n" +
                      "5-Item Sold for a Given Day\n" +
                      "6-Return to Main Menu\n" +
                      "What would you like to do? ";
      int choice = getIntWillLoop(menu, 1, 6);
      if (choice == 1)//Print Menu
      {
        Console.Clear();
        Console.WriteLine("****Concessions Menu****");
        Console.WriteLine($"{"NAME",-30} {"DESCRIPTION",-30} {"PRICE",-10:C2}");
        foreach (var mi in MovieTheater.ConcessionMenuList)
        {
          Console.WriteLine($"{mi.itemName,-30} {mi.itemDescription,-30} {mi.price,-10:C2}");
        }
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey(true);
      }
      if (choice == 2)//Purchase Concession Item
      {
        Console.Clear();
        Console.WriteLine("***Purchase Concessions***");
        // display items and walk user through purchase
        Console.WriteLine("****Concessions Menu****");
        Console.WriteLine($"{"NAME",-27} {"PRICE",-10:C2}");
        for (int i = 0; i < MovieTheater.ConcessionMenuList.Count; i++)
        {
          ConcessionMenuTuple mi = MovieTheater.ConcessionMenuList[i];
          Console.WriteLine($"{i + 1,3}. {mi.itemName,-30} {mi.price,-10:C2}");
        }
        int userConcessionNumber = getIntWillLoop("Enter the number of the concession you would like to buy", 1, MovieTheater.ConcessionMenuList.Count + 1);

        ConcessionMenuTuple menuItem = MovieTheater.ConcessionMenuList[userConcessionNumber - 1];
        Console.WriteLine("What is your name?");
        string customerName = Console.ReadLine();

        int quantity = getIntWillLoop("How many would you like to buy?", 1, int.MaxValue);

        MovieTheater.PurchaseMenuItem(customerName, menuItem.itemName, quantity, false);

        Console.WriteLine();
        Console.WriteLine();


        PressKeyToContinue("\nTransaction Complete.\nPress any key to continue.");
      }
      if (choice == 3)//Receipts from All Sales
      {
        Console.Clear();
        Console.WriteLine(MovieTheater.ConcessionReport3_AllReceipts());
        PressKeyToContinue("Hit any key to move on");
      }
      if (choice == 4)//Revenue Totals For All Days
      {
        Console.Clear();
        Console.WriteLine(MovieTheater.ConcessionReport4_RevenueTotalsForAllDays());
        PressKeyToContinue("Hit any key to move on");
      }
      if (choice == 5)//Display Item Revenue For A Given Day
      {
        Console.Clear();
        // Have the user input a date
        List<DateOnly> datesOfSales = new();
        foreach (ConcessionSaleTuple sale in MovieTheater.ConcessionSaleList)
        {
          if (!datesOfSales.Contains(DateOnly.FromDateTime(sale.soldDateTime)))
          {
            datesOfSales.Add(DateOnly.FromDateTime(sale.soldDateTime));
          }
        }
        for (int i = 0; i < datesOfSales.Count; i++)
        {
          Console.WriteLine($"{i + 1}. {datesOfSales[i]}");
        }
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Type the number of the day you want to use");
        string inputDate = Console.ReadLine();

        Console.WriteLine(MovieTheater.ConcessionReport5_ItemTotalsPerDay(datesOfSales[int.Parse(inputDate) - 1]));
        PressKeyToContinue("Hit any key to move on");
      }
      if (choice == 6)//return to main menu
      {

        return;
      }
    }
  }

  public static DateOnly getDateOnlyWillLoop(string prompt)
  {
    // todo
    return new DateOnly();
  }

  public static int getIntWillLoop(string prompt, int min, int max)
  {
    while (true)
    {
      Console.WriteLine(prompt);
      int number;
      if (int.TryParse(Console.ReadLine(), out number))
      {
        if (number >= min && number <= max)
        {
          return number;
        }
      }
      Console.Write("Invalid.  Please enter valid number: ");
    }
  }

  public static decimal getDecimalWillLoop(string prompt, decimal min, decimal max)
  {
    while (true)
    {
      Console.WriteLine(prompt);
      decimal numberDecimal;
      if (decimal.TryParse(Console.ReadLine(), out numberDecimal))
      {
        if (numberDecimal >= min && numberDecimal <= max)
        {
          return numberDecimal;
        }
      }
      Console.WriteLine("Invalid. Please enter a valid number:");
    }

  }

  public static bool GetBoolWillLoop(string prompt)
  {
    while (true)
    {
      Console.WriteLine(prompt);
      string input = Console.ReadLine().ToUpper();
      if (input == "YES" || input == "Y" || input.ToLower() == "true" || input.ToLower() == "t") return true;
      else if (input == "NO" || input == "N" || input.ToLower() == "false" || input.ToLower() == "f") return false;
      Console.Write("Invalid.  Please enter a valid True/False/Yes/No answer");
    }
  }


  public static void PressKeyToContinue(string prompt)
  {
    while (Console.KeyAvailable)
    {
      Console.ReadKey(true);
    }
    Console.WriteLine(prompt);
    Console.ReadKey(true);
  }

  public static void SchedulingControlsMenu()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("***Scheduling Controls***");
      string menu = "1-New Movie\n" +
                      "2-Create ShowingID\n" +
                      "3-View Registered Movies\n" +
                      "4-View Showtimes for Movie\n" +
                      "5-Return to Main Menu\n" +
                      "What would you like to do? ";
      int choice = getIntWillLoop(menu, 1, 5);
      if (choice == 1) //New Movie
      {
        Console.Clear();
        Console.WriteLine("***Register New Movie***");
        Console.WriteLine("What movie do you want to add?");
        string movieInput = Console.ReadLine();
        Console.WriteLine("What is the runtime of the movie?");
        string runtime = Console.ReadLine();
        Console.WriteLine("What do you want to say to advertise the message?");
        string advertiseMessage = Console.ReadLine();
        Console.WriteLine("Who are the lead actors in the movie?");
        string leadActors = Console.ReadLine();
        try
        {
          MovieTheater.NewMovieRegistration(movieInput, int.Parse(runtime), advertiseMessage, leadActors);
        }
        catch
        {
          Console.WriteLine("Movie was not registered");
        }
        // get data from user and call MovieTheater.NewMovieRegistration
        // the advertisingMesssage can be any string input by a user
        // leads refers to the lead actors of the film
        // if MovieTheater.NewMovieRegistration throws an exception, let the user know the movie was not registered and continue
      }
      if (choice == 2) //Create ShowingID
      {
        Console.Clear();
        Console.WriteLine("***Register New Showing ID***");
        Console.WriteLine("Enter the showing ID");
        string showingID = Console.ReadLine();
        Console.WriteLine("Enter the date and time of the showing using the format month, day, year, hour(in 24hour):minutes:seconds");
        DateTime showingdatetime = DateTime.Parse(Console.ReadLine());
        decimal ticketPrice = getDecimalWillLoop("Enter the ticket price", 0, int.MaxValue);
        int theaterRoom = getValidTheaterRoomWillLoop("Choose a theater");
        Console.WriteLine("Enter the movie title");
        string movieTitle = Console.ReadLine();
        try
        {
          MovieTheater.CreateShowingID(ID: int.Parse(showingID), date: showingdatetime, ticketPrice: ticketPrice, theaterRoom: theaterRoom, movieTitle: movieTitle);
        }
        catch
        {
          Console.WriteLine("Something went wrong");
          SchedulingControlsMenu();
        }
        // get a new showing ID from the user
        // have the user input the date and time of the showing
        // use getDecimalWillLoop to have the user input a ticket price
        // have the user select a theater room (implement getValidTheaterRoomWillLoop)
        // use MovieTheater.CreateShowingID to create the showing
        //    - if MovieTheater.CreateShowingID throws an exception, let the user know something went wrong and keep going.
      }
      // get data from user and call MovieTheater.NewMovieRegistration
      // the advertisingMesssage can be any string input by a user
      // leads refers to the lead actors of the film
      // if MovieTheater.NewMovieRegistration throws an exception, let the user know the movie was not registered and continue
      if (choice == 3)
      {
        Console.Clear();
        Console.WriteLine("***Movie Report***");
        foreach (MovieTuple movie in MovieTheater.MovieList)
        {
          Console.WriteLine($"\tTITLE: {movie.title,20}\tRUNLENGTH: {movie.runLengthMinutes} \nADVERTISING MESSAGE: {movie.advertisingMesssage}");
          Console.WriteLine($"LEADS: {movie.leads}");
          Console.WriteLine("\n");
        }
        PressKeyToContinue("\nPress any key to continue");
      }
      if (choice == 4)
      {
        Console.Clear();
        string movieTitle = getValidMovieTitleWillLoop("Which movie would you like to see showtimes for? ");
        Console.Clear();
        string output = MovieTheater.MovieShowingReport(movieTitle);
        Console.WriteLine(output);
        PressKeyToContinue("\nPress any key to continue");
      }
      if (choice == 5)//return to main menu
      {
        return;
      }
    }
  }

  public static int getValidTheaterRoomWillLoop(string prompt)
  {
    Console.Clear();
    List<int> theaterRooms = new();
    foreach (KeyValuePair<int, int> theaterRoom in MovieTheater.TheaterRoomCapacity)
    {
      theaterRooms.Add(theaterRoom.Key);
    }
    for (int i = 0; i < theaterRooms.Count; i++)
    {
      Console.WriteLine($"{i + 1}. {theaterRooms[i]}");
    }
    Console.WriteLine();
    Console.WriteLine();
    while (true)
    {

      Console.WriteLine("Type the number of the theater you want to use");
      string input = Console.ReadLine();
      if (int.Parse(input) <= theaterRooms.Count && int.Parse(input) > 0)
      {
        return int.Parse(input);

      }
      Console.WriteLine("Invalid input. Try again");
    }
  }
  // show the user a list of theater rooms
  // allow them to input a number to select one
  // return the ID of the theater room they selected

  public static string getValidMovieTitleWillLoop(string prompt)
  {
    Console.WriteLine(prompt);
    for (int i = 0; i < MovieTheater.MovieList.Count; i++)
    {
      Console.WriteLine($"{i + 1,4}: {MovieTheater.MovieList[i].title}");
    }
    int userChoice = getIntWillLoop("Select a Movie", 1, MovieTheater.MovieList.Count);
    return MovieTheater.MovieList[userChoice - 1].title;
  }

  public static ShowingTuple getValidShowingWillLoop(string prompt, string movieTitle)
  {
    Console.WriteLine(prompt);

    List<ShowingTuple> showingsForMovie = new();
    foreach (ShowingTuple showing in MovieTheater.ScheduleList)
    {
      if (showing.movieTitle == movieTitle)
      {
        showingsForMovie.Add(showing);
      }
    }
    for (int i = 0; i < showingsForMovie.Count; i++)
    {
      Console.WriteLine($"{i + 1,4}: {showingsForMovie[i].showingDateTime}");
    }
    int choice = getIntWillLoop("Select a time", 1, showingsForMovie.Count);
    return showingsForMovie[choice - 1];
  }

  public static string getValidMovieTitleWithShowingWillLoop(string prompt)
  {
    Console.WriteLine(prompt);
    List<string> movieTitleWithShowings = new();
    foreach (ShowingTuple showing in MovieTheater.ScheduleList)
    {
      if (!movieTitleWithShowings.Contains(showing.movieTitle))
      {
        movieTitleWithShowings.Add(showing.movieTitle);
      }
    }
    for (int i = 0; i < movieTitleWithShowings.Count; i++)
    {
      Console.WriteLine($"{i + 1,4}: {movieTitleWithShowings[i]}");
    }
    int userChoice = getIntWillLoop("Select a Movie", 1, movieTitleWithShowings.Count);
    return movieTitleWithShowings[userChoice - 1];
  }
  public static void TicketWindowMenu()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("***Ticket Window Menu***");
      string menu = "1-Purchase Ticket\n" +
                      "2-Seat Availability\n" +
                      "3-Daily Movies & Showtime Report\n" +
                      "4-Preferred Customer Registration\n" +
                      "5-Daily Ticket Sales Revenue Report\n" +
                      "6-Preferred Customer Report\n" +
                      "7-Return to Main Menu\n" +
                      "What would you like to do? ";
      int choice = getIntWillLoop(menu, 1, 7);
      if (choice == 1) //Purchase Ticket
      {
        PurchaseTicket();
      }
      else if (choice == 2) //Seat Avalability
      {
        Console.Clear();
        Console.WriteLine("****Seat Avalibility****");


        string movieTitle = getValidMovieTitleWillLoop("Pick a Movie");
        ShowingTuple showing = getValidShowingWillLoop("Pick a showing", movieTitle);
        int ticketsAvailable = MovieTheater.HowManySeatsAreAvailableForShowing(showing.showingID);
        Console.WriteLine($"There are {ticketsAvailable} tickets available for {movieTitle}");
        PressKeyToContinue("Press any key to continue.");
      }
      else if (choice == 3) //Daily Movies & Showtime Report
      {
        Console.Clear();
        Console.WriteLine("****Daily Movies & Showtime Report****");
        DateOnly dateChosen = getDateWithShowtimes();
        List<ShowingTuple> showingsForDayChosen = new();
        foreach (ShowingTuple showing in MovieTheater.ScheduleList)
        {
          if (DateOnly.FromDateTime(showing.showingDateTime) == dateChosen)
          {
            showingsForDayChosen.Add(showing);
          }
        }
        for (int i = 0; i < showingsForDayChosen.Count; i++)
        {
          Console.WriteLine($"{i + 1}. {showingsForDayChosen[i].showingDateTime}    {showingsForDayChosen[i].movieTitle}");
        }
        int Pick = getIntWillLoop("Choose a Showtime for a Movie", 1, showingsForDayChosen.Count) - 1;
        ShowingTuple showingChosen = showingsForDayChosen[Pick];
        string report = $"***Date Chosen {DateOnly.FromDateTime(showingsForDayChosen[Pick].showingDateTime)}***\n";
        report += $"{"ID",5}{"Movie Title",30}{"Movie Runtime",20}{"Lead Actors",30}\n";
        foreach (MovieTuple movie in MovieTheater.MovieList)
        {
          if (showingsForDayChosen[Pick].movieTitle == movie.title)
          {
            report += $"{showingsForDayChosen[Pick].showingID,5}{showingsForDayChosen[Pick].movieTitle,30}{movie.runLengthMinutes,20}{movie.leads,30}";

          }
        }
        Console.Clear();
        Console.WriteLine(report);
        PressKeyToContinue("Press any key to continue");

        // have user select day that has showtimes
        // display list of showings (with movie names) to user, have them select one
        // display movie details and lead actors to user
      }
      else if (choice == 4)//Preferred Customer Regestration
      {
        int ID = GetPreferredCustomerID("Enter ID");
        Console.WriteLine("Enter Name");
        string name = Console.ReadLine();
        Console.WriteLine("Enter email");
        string email = Console.ReadLine();
        MovieTheater.PreferredCustomerRegistration(ID: ID, name: name, email: email);


        // todo when we implement preferred customer
      }
      else if (choice == 5)//Daily Ticket Sales Revenue Report
      {
        List<DateOnly> validDates = new();
        foreach (ShowingTuple showing in MovieTheater.ScheduleList)
        {
          if (!validDates.Contains(DateOnly.FromDateTime(showing.showingDateTime)))
          {
            validDates.Add(DateOnly.FromDateTime(showing.showingDateTime));
          }
        }
        Console.Clear();
        for (int i = 0; i < validDates.Count; i++)
        {
          Console.WriteLine($"{i + 1}. {validDates[i]}");
        }
        int dateChosen = getIntWillLoop("Choose the day you want to see", 1, validDates.Count);
        Console.Clear();
        Console.WriteLine($"Report for {validDates[dateChosen - 1]}");
        MovieTheater.TicketReport5_TicketSalesRevenue(validDates[dateChosen - 1]);
        PressKeyToContinue("Press any key to continue");
        // have user select day that has showtimes
        // display all showtimes for user selected day
        //    - include time, movie title, sum of revenue collected
      }
      else if (choice == 7)//return to main menu
      {
        return;
      }
      else if (choice == 6) //display Preferred Customer Data
      {
        Console.Clear();
        MovieTheater.DisplayPreferredCustomerData();
        PressKeyToContinue("Press any key to continue");
        return;

      }
    }
  }

  public static int GetPreferredCustomerID(string prompt)
  {
    Console.WriteLine(prompt);
    try
    {
      int preferredCustomerID = int.Parse(Console.ReadLine());
      foreach (PreferredCustomerTuple customerTuple in MovieTheater.PreferredCustomerList)
      {
        if (customerTuple.preferredCustomerID == preferredCustomerID)
        {
          throw new Exception("ID already exists. Try agaiin");

        }
      }
      return preferredCustomerID;
    }
    catch
    {
      Console.WriteLine("Something went wrong. Try Again");
    }
    return GetPreferredCustomerID(prompt);
  }
  public static void PurchaseTicket()
  {
    // display list of movies that have showtimes, have user select one
    string movieTitle = getValidMovieTitleWithShowingWillLoop("Select a movie");
    // display a list of showings for that movie, have user select one
    ShowingTuple showing = getValidShowingWillLoop("Pick a Showing", movieTitle);
    // tell user how many tickets are available, have them input their desired ticket quantity
    int ticketsAvailable = MovieTheater.HowManySeatsAreAvailableForShowing(showing.showingID);
    Console.WriteLine($"There are {ticketsAvailable} tickets available for {movieTitle}");
    string prompt = "How many tickets would you like to buy?";
    int ticketCount = getIntWillLoop(prompt, 1, ticketsAvailable);
    //    - if user requests too many tickets, inform them there are not enough seats available and return without recording purchase
    decimal cost = ticketCount * showing.ticketPrice;
    Console.WriteLine("What is your name?");
    string customerName = Console.ReadLine();
    decimal totalCost = cost;
    if (MovieTheater.CheckIfPreferredCustomer(customerName))
    {
      for (int i = 0; i < MovieTheater.PreferredCustomerList.Count; i++)
      {
        if (MovieTheater.PreferredCustomerList[i].name == customerName)
        {
          MovieTheater.TicketPurchase(showing.showingID, cost, preferredID: MovieTheater.PreferredCustomerList[i].preferredCustomerID);
          Console.WriteLine($"That will cost {totalCost - showing.ticketPrice:$0.00}");
        }
      }
    }


    else
    {

      MovieTheater.TicketPurchase(showing.showingID, cost);
    }
    PressKeyToContinue("\nTransaction Recorded, press any key to continue");
    // inform user of cost of that many tickets
    // ask user for their name
    // inform the user that the transaction is complete
  }

  public static void AdvertisementMenu()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("***Advertisement Menu***");
      string menu = "1-Register NEW advertisement\n" +
                      "2-Schedule Advertisements\n" +
                      "3-Daily Showing & Advertisement Length Report\n" +
                      "4-Daily Advertisement Revenue Report\n" +
                      "5-Monthly Advertising Revenue Report\n" +
                      "6-Return to Main Menu\n" +
                      "What would you like to do? ";
      int choice = getIntWillLoop(menu, 1, 6);
      if (choice == 1)//Register NEW advertisement
      {
        Console.Clear();
        Console.WriteLine("***Register New Ad***");
        // get name, description, length in seconds (int), and charge per play (decimal) from the user
        // register an advertisement, if not successful, inform the user and continue
        PressKeyToContinue("Ad Registered.\nPress any key to continue.");
      }
      else if (choice == 2)//Schedule Advertisements
      {
        Console.Clear();
        Console.WriteLine("***Schedule Advertisement***");

        //Print all available movies
        List<string> scheduledMovies = MovieTheater.GetScheduledMovies();
        // Get user choice for movie

        // Print all scheduled showings for selected movie, have user select one

        // Display all advertisements to user, have them select one

        // Schedule add for showing, if not successful, inform the user and continue

        PressKeyToContinue("Advertisement Scheduled.\nPress any key to continue.");
      }
      else if (choice == 3)//Daily showing & advertisement length report
      {
        Console.Clear();
        Console.WriteLine("***Daily Scheduled Ad Report***");
        // show list of days that have showings, have user select one
        // give each showing that day a row
        //      - display the number of advertisement before the movie, the total revenue from advertisements, and the total length of advertisements
        PressKeyToContinue("Press any key to continue.");
      }
      else if (choice == 4)//Daily Advertising Revenue Report
      {
        Console.Clear();
        Console.WriteLine("***Daily Advertising Revenue Report***");
        // show list of days that have showings, have user select one
        // give each advertisement that played that day a row
        //     - display the advertisement name, number of times it was showed that day, and the sum revenue for that advertisement on that day
        PressKeyToContinue("Press any key to continue.");
      }
      else if (choice == 5)// Monthly Advertising Revenue Report
      {
        Console.Clear();
        Console.WriteLine("***Monthly Advertising Revenue Report***");

        // very similar to option 4, but showing count and revenue should be for all showings in that month, not just that day
        // show list of days that have showings, have user select one
        // show report for all days with the same month as the selected day
        // display the advertisement name, number of times it was showed that month, and the sum revenue for that advertisement on that month
        PressKeyToContinue("Press any key to continue.");
      }
      else //return to main menu
      {
        return;
      }
    }
  }

  public static DateOnly getDateWithShowtimes()
  {
    List<DateOnly> showingDates = new();
    foreach (ShowingTuple showing in MovieTheater.ScheduleList)
    {
      if (!showingDates.Contains(DateOnly.FromDateTime(showing.showingDateTime)))
      {
        showingDates.Add(DateOnly.FromDateTime(showing.showingDateTime));
      }
    }
    for (int i = 0; i < showingDates.Count; i++)
    {
      Console.WriteLine($"{i + 1}. {showingDates[i]}");
    }
    int choice = getIntWillLoop("Pick a Date to use", 1, showingDates.Count);
    return showingDates[choice - 1];
  }
}