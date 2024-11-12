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

// report data types

global using ConcessionsReportTuple = (string name, int sold, decimal revenue, int givenAway);
using System.Linq.Expressions;
using Microsoft.VisualBasic;
using System.Numerics;

namespace Shared;

public static class MovieTheater
{
    public static List<ConcessionMenuTuple> ConcessionMenuList = new();
    public static List<ConcessionSaleTuple> ConcessionSaleList = new();


    public static readonly decimal salesTaxRate = 0.06512m;
    public static List<MovieTuple> MovieList = new();// List<(string,int,string,string)>();
    public static Dictionary<int, int> TheaterRoomCapacity = new();
    public static List<ShowingTuple> ScheduleList = new();
    public static List<SoldTicketTuple> SoldTicketList = new();
    public static List<PreferredCustomerTuple> PreferredCustomerList = new();
    public static List<AdvertisementTuple> AdvertisementList = new();
    public static List<ScheduledAdsTuple> ScheduledAdsList = new();

    public static void ReadDataInFromAllFiles()
    {
        MovieList = DataStorage.FileAccess.ReadMovies();
        TheaterRoomCapacity = DataStorage.FileAccess.ReadTheaterRoomData();
        ScheduleList = DataStorage.FileAccess.ReadScheduleData();
        PreferredCustomerList = DataStorage.FileAccess.ReadPreferredCustomerData();
        SoldTicketList = DataStorage.FileAccess.ReadSoldTicketData();
        ConcessionMenuList = DataStorage.FileAccess.ReadConcessionMenuData();
        ConcessionSaleList = DataStorage.FileAccess.ReadConcessionSalesData();
        AdvertisementList = DataStorage.FileAccess.ReadAdvertisementData();
        ScheduledAdsList = DataStorage.FileAccess.ReadAdvertisementScheduleData();
    }
    public static void WriteDataToAllFiles()
    {
        DataStorage.FileAccess.WriteMovies(MovieList);
        DataStorage.FileAccess.WriteTheaterRoomData(TheaterRoomCapacity);
        DataStorage.FileAccess.WriteScheduleData(ScheduleList);
        DataStorage.FileAccess.WritePreferredCustomerData(PreferredCustomerList);
        DataStorage.FileAccess.WriteSoldTicketData(SoldTicketList);
        DataStorage.FileAccess.WriteConcessionMenuData(ConcessionMenuList);
        DataStorage.FileAccess.WriteConcessionSalesData(ConcessionSaleList);
        DataStorage.FileAccess.WriteAdvertisementData(AdvertisementList);
        DataStorage.FileAccess.WriteAdvertisementScheduleData(ScheduledAdsList);
    }
    // Concessions
    public static void PurchaseMenuItem(string customerName, string itemWanted, int quantity, bool preferredCustomerPayWithPoints)
    {
        bool canMakeSale = false;
        decimal price = 0;
        foreach (ConcessionMenuTuple menuitem in ConcessionMenuList)
        {
            if (menuitem.itemName == itemWanted)
            {
                canMakeSale = true;
                price = menuitem.price;
            }
        }
        if (canMakeSale)
        {

            // need to handle preferred customer later
            decimal revenue = price * quantity;
            ConcessionSaleTuple sale = (
                soldDateTime: DateTime.Now,
                itemName: itemWanted,
                quantitySold: quantity,
                revenueCollected: revenue,
                preferredCustomerID: -1
            );
            ConcessionSaleList.Add(sale);
        }
        else
        {
            throw new Exception("cannot make sale, could not find menu item");
        }
    }

    public static string ConcessionReport3_AllReceipts()
    {
        string output = $"{"Item Name",20}{"Sale Date",25}{"Quantity",10}{"Revenue",13}\n";
        foreach (ConcessionSaleTuple sale in ConcessionSaleList)
        {
            output += $"{sale.itemName,20}{sale.soldDateTime,25}{sale.quantitySold,10}{sale.revenueCollected,13:$0.00}\n";
        }
        return output;
    }
    public static string ConcessionReport4_RevenueTotalsForAllDays()
    {
        string output = $"{"Date",20}{"Price",20}\n";

        Dictionary<DateOnly, decimal> pricePerDay = new();
        foreach (ConcessionSaleTuple sale in ConcessionSaleList)
        {
            DateOnly date = DateOnly.FromDateTime(sale.soldDateTime);
            if (pricePerDay.ContainsKey(date))
            {
                pricePerDay[date] = pricePerDay[date] + sale.revenueCollected;
            }
            else
            {
                pricePerDay[date] = sale.revenueCollected;
            }

        }
        foreach (KeyValuePair<DateOnly, decimal> pair in pricePerDay)
        {
            output += $"{pair.Key,20}{pair.Value,20: $0.00}\n";
        }
        return output;
    }
    public static string ConcessionReport5_ItemTotalsPerDay(DateOnly givenDay)
    {
        string output = $"Date Selected: {givenDay}\n{"Item",20}{"Quantity",15}{"Revenue",13}\n";

        Dictionary<string, int> quantityOfItem = new();
        Dictionary<string, decimal> revenueOfItem = new();

        foreach (ConcessionSaleTuple sale in ConcessionSaleList)
        {
            if (DateOnly.FromDateTime(sale.soldDateTime) == givenDay)
            {
                if (!quantityOfItem.ContainsKey(sale.itemName))
                {
                    quantityOfItem[sale.itemName] = sale.quantitySold;
                }
                else
                {
                    quantityOfItem[sale.itemName] = quantityOfItem[sale.itemName] + sale.quantitySold;
                }
                if (!revenueOfItem.ContainsKey(sale.itemName))
                {
                    revenueOfItem[sale.itemName] = sale.revenueCollected;
                }
                else
                {
                    revenueOfItem[sale.itemName] = revenueOfItem[sale.itemName] + sale.revenueCollected;
                }
            }
        }


        foreach (var item in quantityOfItem)
        {
            if (revenueOfItem.ContainsKey(item.Key))
            {
                output += $"{item.Key,20}{item.Value,15}{revenueOfItem[item.Key],13:$0.00}\n";
            }
        }

        return output;
    }


    //Scheduling
    public static void CreateShowingID(int ID, DateTime date, decimal ticketPrice, int theaterRoom, string movieTitle)
    {
        // (int showingID, System.DateTime showingDateTime, decimal ticketPrice, int theaterRoom, string movieTitle
        ShowingTuple newShowing =
        (showingID: ID,
        showingDateTime: date,
        ticketPrice: ticketPrice,
        theaterRoom: theaterRoom,
        movieTitle: movieTitle
        );
        bool hasSameID = false;
        CheckIfMovieExists(movieTitle);
        foreach (ShowingTuple item in ScheduleList)
        {
            if (item.showingID == newShowing.showingID)
            {
                throw new ArgumentException();
            }
        }
        ScheduleList.Add(newShowing);
    }
    public static void CheckIfMovieExists(string movieTitle)
    {
        List<string> movieTitles = new();
        foreach (MovieTuple movie in MovieList)
        {
            movieTitles.Add(movie.title);
        }
        if (!movieTitles.Contains(movieTitle))
        {
            throw new Exception("Movie doesn't exist");
        }
    }
    public static void NewMovieRegistration(string title, int runLengthMinutes, string advertisingMesssage, string leads)
    {
        MovieTuple newMovie = new MovieTuple(title, runLengthMinutes, advertisingMesssage, leads);

        foreach (MovieTuple movie in MovieList)
        {

            if (movie.title == newMovie.title)
            {
                throw new Exception("Can't add duplicate movie");
            }
        }
        MovieList.Add(newMovie);
    }



    //Advertisements
    public static string AdvertisingReport3_DailyShowingAndAdvertisementLength(DateOnly date)
    //Gets showing IDs for given day
    // 	Produce a report, given a certain date, that shows the following data per showingID
    // 	- # of commercials total
    // - Sum of Minutes/Seconds 
    // - $ earned from Advertisements for that showing
    {
        return "";
    }

    public static string AdvertisingReport4_DailyAdvertisingRevenue(DateOnly date)
    {
        // 	Produce a report, given a certain day, that shows the following data summations:
        // 	- AdvertisementTitle, # of Showings, Total$Revenue
        return "";
    }

    public static string AdvertisingReport5_MonthlyAdvertisingRevenue(DateOnly date)
    {
        // 	Produce a report, given a certain month, that shows the following data summations for each commercial shown: (each commercial should have its own line, and only be listed once)
        // 	- Title & Description
        // 	- Total # of showings
        // 	- Total $ of revenue

        // [Advertisement Title]          [Showings]     [Revenue]
        // DrWisdomTooth-17              4              $88.88
        // DrWisdomTooth-16              4              $80.00
        // Food Pantry #2                6              $0.00 
        // Bob's Fishing                 1              $0.50 
        return "";
    }

    public static void ScheduleAdForMovie(int showingID, string name)
    {
        ScheduledAdsTuple scheduledAd = (
            showingID: showingID,
            name: name
        );
        foreach (ScheduledAdsTuple ad in ScheduledAdsList)
        {
            if (ScheduledAdsList.Contains(scheduledAd))
            {
                throw new Exception("Add already exsits for this showing");
            }
        }
        ScheduledAdsList.Add(scheduledAd);
        // Schedule Advertisements for a ShowingID (schedule time & movie)
    }

    public static void RegisterNewAd(string name, string description, int lengthInSeconds, decimal chargePerPlay)
    {
        AdvertisementTuple ad = (
            name: name,
            description: description,
            lengthInSeconds: lengthInSeconds,
            chargePerPlay: chargePerPlay
        );
        foreach (AdvertisementTuple advertisement in AdvertisementList)
        {
            if (AdvertisementList.Contains(ad))
            {
                throw new Exception();
            }
        }
        AdvertisementList.Add(ad);
        // Register NEW advertisement and their rates
    }



    //Tickets

    public static string TicketReport5_TicketSalesRevenue(DateOnly date)
    {
        Dictionary<int, DateTime> showTime = new();
        Dictionary<int, string> movieTitle = new();
        Dictionary<int, decimal> revenueCollected = new();
        Dictionary<int, int> givenAway = new();
        foreach (ShowingTuple showing in ScheduleList)
        {
            if (DateOnly.FromDateTime(showing.showingDateTime) == date)
            {
                showTime.Add(showing.showingID, showing.showingDateTime);
                movieTitle.Add(showing.showingID, showing.movieTitle);
                revenueCollected.Add(showing.showingID, showing.ticketPrice);
                givenAway.Add(showing.showingID, 0);
                foreach (SoldTicketTuple ticket in SoldTicketList)
                {
                    if (givenAway.ContainsKey(showing.showingID))
                    {
                        givenAway[showing.showingID] = TicketsGivenToPreferredCustomerForShowing(showing.showingID);
                    }
                }
            }

        }
        string report = $"{"Show Time",20}{"Movie Title",25}{"Tickets Sold",20}{"Revenue Collected",30}{"Given Away",15}\n";
        foreach (KeyValuePair<int, DateTime> item in showTime)
        {
            report += $"{showTime[item.Key],20}{movieTitle[item.Key],25}{SumOfTotalSoldTicketsForShowing(item.Key),20}{SumOfTotalSoldTicketsForShowing(item.Key) * revenueCollected[item.Key],30: $0.00}{givenAway[item.Key],15}\n";
        }

        Console.WriteLine(report);
        //      Produce a report, for a given date, that shows the following data summations for each showingID:
        //  	- ShowTime,  MovieTitle, NumberTicketsSold, Sum$Collected, CountGivenAwayFreeToPreferredCustomers
        //  	(NumberTicketsSold should include the #s of free tickets.  Example 3 tickets paid for + 1 free ticket = 4 NumberTicketsSold)

        // example:
        // Show Time                Movie Title                   Revenue Collected             Given Away                    
        // 4/11/2024 6:35:00 PM     Dune: Part Two (2024)         $19,999.98                    0 
        return report;
    }
    public static void PreferredCustomerRegistration(int ID, string name, string email)
    {
        // (int preferredCustomerID, string name, string email, int ticketPoints, int concessionPoints)
        PreferredCustomerTuple newPreferredCustomer = (
            ID: ID,
            name: name,
            email: email,
            ticketPoints: 0,
            concessionPoints: 0
        );
        PreferredCustomerList.Add(newPreferredCustomer);
        //-T5 - (Preferred Customer Registration)
        //    A Customer can register as a preferred customer.
        //    PreferredCustomerTuple = (int preferredCustomerID, string name, string email, int ticketPoints, int concessionPoints);

    }
    public static List<DailyShowingTuple> GetDailySchedule_Basic(System.DateOnly requestedDate)
    {
        List<DailyShowingTuple> showingsForRequestedDate = new();
        foreach (ShowingTuple showing in ScheduleList)
        {
            if (requestedDate == DateOnly.FromDateTime(showing.showingDateTime))
            {
                DailyShowingTuple dailyShowing = (
                    MovieTitle: showing.movieTitle,
                    showTime: showing.showingDateTime
                );
                showingsForRequestedDate.Add(dailyShowing);
            }
        }

        return showingsForRequestedDate;
    }
    public static int HowManySeatsAreAvailableForShowing(int showingID)
    {
        foreach (ShowingTuple showing in ScheduleList)
        {
            if (showingID == showing.showingID)
            {
                int theaterRoomCapacity = TheaterRoomCapacity[showing.theaterRoom];
                int ticketsSold = SumOfTotalSoldTicketsForShowing(showingID);
                return theaterRoomCapacity - ticketsSold;
            }


        }
        //REQ T2 - (Seat Availability)
        //Customers(providing a schedule) can check ticket availability(is a show sold out already? How many seats are left?)

        return -1;
    }
    public static int SumOfTotalSoldTicketsForShowing(int showingID)
    {
        int ticketsSold = 0;
        foreach (ShowingTuple showing in ScheduleList)
        {
            
        foreach (SoldTicketTuple ticket in SoldTicketList)
        {
            if (ticket.showingID == showingID&& showing.showingID == showingID)
            {
                ticketsSold += Convert.ToInt32(ticket.revenueCharged/showing.ticketPrice);
            }
        }
        }
        return ticketsSold;
    }
    public static int TicketsGivenToPreferredCustomerForShowing(int showingID)
    {
        int ticketsGivenAway = 0;
       

        return ticketsGivenAway;
    }
    public static void TicketPurchase(int showingID, decimal revenueAmt)
    {
        SoldTicketTuple ticket = (
            soldDateTime: DateTime.Now,
            showingID: showingID,
            revenueCharged: revenueAmt,
            preferredCustomerNum: -1
        );
        List<int> showingId = new();
        foreach (ShowingTuple showing in ScheduleList)
        {
            showingId.Add(showing.showingID);
        }
        if (!showingId.Contains(showingID))
        {
            throw new Exception("Showing ID doesn't exist");
        }
        SoldTicketList.Add(ticket);
    }
    public static void TicketPurchase(int showingID, decimal revenueAmt, int preferredID)
    {

        SoldTicketTuple ticket = (
            soldDateTime: DateTime.Now,
            showingID: showingID,
            revenueCharged: revenueAmt,
            preferredCustomerNum: preferredID
        );
        List<int> showingId = new();
        bool usedPoints = false;
        PreferredCustomerTuple toUpdate = (0, "filler", "filler", 0, 0);
        decimal ticketPrice = 0;
        decimal charged = revenueAmt;
        foreach (ShowingTuple showing in ScheduleList)
        {
            foreach (PreferredCustomerTuple customer in MovieTheater.PreferredCustomerList)
            {
                if (customer.preferredCustomerID == preferredID && customer.ticketPoints >= 5)
                {
                    revenueAmt = revenueAmt - showing.ticketPrice;
                    toUpdate = customer;
                    usedPoints=true;

                }
                else if (customer.preferredCustomerID == preferredID && customer.ticketPoints < 5)
                {
                    toUpdate = customer;
                    usedPoints = false;                    
                }
                    ticketPrice = showing.ticketPrice;

            }
            showingId.Add(showing.showingID);
            if (!showingId.Contains(showingID))
            {
                throw new Exception("Showing ID doesn't exist");
            }
        }
        UpdatePreferredCustomer(ticketNumber: (int)(charged / ticketPrice), preferredCustomer: toUpdate, usedPoints);
        SoldTicketList.Add(ticket);
        // Customers can buy a ticket
        // preferred customers get special treatment
    }

    public static decimal GetSalesTotal(int numberOfTickets, decimal ticketPrice)
    {
        // Requirements:
        // 1) expected to return the correct amount
        // 2) must include sales tax
        // 3) must be rounded to the nearest penny  (up , down as appropriate)

        return -1m;
    }
    public static decimal GetAdvertisementRevenue(int watchers, decimal ratePerWatcher)
    {
        // Requirements:
        // 1) expected to return the correct amount
        // 2) must NOT include sales tax
        // 3) must be rounded to the nearest penny  (up , down as appropriate)

        return -1m;
    }
    public static decimal GetTicketRevenueNoSalesTax(int numberOfTickets, decimal ticketPrice)
    {
        // Requirements:
        // 1) expected to return the correct amount
        // 2) must NOT include sales tax
        // 3) must be rounded to the nearest penny  (up , down as appropriate)

        return -1m;
    }
    public static decimal GetTotalRevenuePerShowing(int numberOfTickets, decimal ticketPrice, decimal ratePerWatcher)
    {
        // Requirements:
        // 1) expected to return the correct amount
        // 2) Per Showing:
        //      Advertisement Revenue (no sales tax)   +  TicketRevenue (no sales tax)
        // 3) must be rounded to the nearest penny  (up , down as appropriate)

        return -1m;
    }
    public static bool DoesMovieExist(string movieTitle, DateTime date, out ShowingTuple movie)
    {
        movie = new();
        return false;
    }

    public static bool CheckIfPreferredCustomer(string userName)
    {
        foreach (PreferredCustomerTuple customer in PreferredCustomerList)
        {
            if (customer.name == userName)
            {
                return true;
            }
        }
        return false;
    }

    public static PreferredCustomerTuple GetPreferredCustomer(string userName)
    {
        return new();
    }

    public static ShowingTuple? GetMovieSchedule(int movieID, DateTime time)
    {
        return null;
    }

    public static List<string> GetScheduledMovies()
    {
        return [];
    }

    public static Dictionary<int, DateTime> AvailableShowingDatesByShowingId(string movieTitle)
    {
        return new();
    }

    public static void UpdatePreferredCustomer(int ticketNumber, PreferredCustomerTuple? preferredCustomer, bool usePoints)
    {
        for (int i = 0; i < PreferredCustomerList.Count; i++)
        {
            if (preferredCustomer == PreferredCustomerList[i])
            {
                if (usePoints == true)
                {
                    PreferredCustomerList[i] = (preferredCustomerID: PreferredCustomerList[i].preferredCustomerID,
                    name: PreferredCustomerList[i].name,
                    email: PreferredCustomerList[i].email,
                    ticketPoints: PreferredCustomerList[i].ticketPoints - 5,
                    concessionPoints: PreferredCustomerList[i].concessionPoints);
                }
                if (usePoints == false)
                {
                    PreferredCustomerList[i] = (preferredCustomerID: PreferredCustomerList[i].preferredCustomerID,
                    name: PreferredCustomerList[i].name,
                    email: PreferredCustomerList[i].email,
                    ticketPoints: PreferredCustomerList[i].ticketPoints + (1 * ticketNumber),
                    concessionPoints: PreferredCustomerList[i].concessionPoints);
                }

            }
        }
    }

    public static void DisplayPreferredCustomerData()
    {
        string report = $"{"Customer Name",15}{"Customer ID",15}{"Email",20}{"Ticket Pts",15}{"Concession Pts",20}\n";
        foreach (PreferredCustomerTuple customer in PreferredCustomerList)
        {
            report += $"{customer.name,15}{customer.preferredCustomerID,15}{customer.email,20}{customer.ticketPoints,15}{customer.concessionPoints,20}\n";
        }
        Console.WriteLine(report);

    }

    public static string MovieShowingReport(string titleofMovie)
    {
           string output = $"***Showing Report for{titleofMovie}***\n{"Showing ID", 10}{"Showtime", 25}{"Theater Room", 15}\n";
        bool hasshowing = false;
        foreach (ShowingTuple showing in MovieTheater.ScheduleList)
        {
          if (showing.movieTitle == titleofMovie)
          {
            output += $"{showing.showingID, 10}{showing.showingDateTime, 25}{showing.theaterRoom, 10}\n";
            hasshowing = true;
          }
        }
        if (hasshowing == false)
        {
          output += "\nNo Showings Scheduled";
        }
        return output;
    }
}