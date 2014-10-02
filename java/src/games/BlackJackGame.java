package Games;

import java.util.*;
import java.io.*;

public class BlackJackGame {
	private static int maxRetry = 3;
    private static int deckSize = 52;
    private static int numPlayers;
    private static Table table;
    private static DealerMode mode;
    
    /*
	public static void main(String[] args) throws Exception{
        System.out.println("Hi, Welcome to BlackJack!");
        System.out.println();

        BlackJackGame game = new BlackJackGame();
        if (askNumPlayers(game))
        {
            if (askTable(game))
            {
                if (askDealerMode(game))
                {
                    gameLoop(table, mode, numPlayers);
                }
            }
        }
    }
    */
    
    public static void main(String[] args) throws Exception{
        System.out.println("Hi, Welcome to BlackJack!");
        System.out.println();

        numPlayers = 1;
        table = TableCreator.createTable(TableBet.Red);
        mode = DealerMode.StandOn17;
        gameLoop2(table, mode, numPlayers);              
    }

    private static boolean askDealerMode() throws Exception
    {
        mode = DealerMode.StandOn17;
        boolean valid = false;
        int retry = 0;
        while (!valid && retry < maxRetry)
        {
            System.out.print("Dealer stand on 17? [Y|N]: ");
            int key = System.in.read();           

            if(key!=' ' && key!='y' && key!='Y')
            {
                mode = DealerMode.StandOn17;
                valid = true;
            }
            else if (key == 'n' || key=='N')
            {
                mode = DealerMode.StandOn18;
                valid = true;
            }
            else
            {
                System.out.println(String.format("Error: Choose Y or N. '%1$s' is invalid!", key));
                System.out.println();
                valid = false;
                ++retry;
            }
        }

        if (!valid)
        {
            System.out.println("Max tries reached. Exiting game.");
        }
        else
        {
            System.out.println(String.format("%1$s chosen", mode == DealerMode.StandOn17 ? "Dealer stand on 17" : "Dealer stand on 18"));
        }
        System.out.println();
        return valid;
    }

    private static boolean askTable(BlackJackGame game) throws Exception
    {
        table = null;
        boolean valid = false;
        int retry = 0;

        while (!valid && retry < maxRetry)
        {
            TableBet[] choices = new TableBet[] { TableBet.Red, TableBet.Green, TableBet.Black };
            String choicesStr = createChoicesString(choices);
            System.out.print(String.format("Which table (minimum bet) would you like to play at? [%1$s]: ", choicesStr));
            java.io.DataInput in = new java.io.DataInputStream(System.in);
            String tableAmtStr = in.readLine();
            table = game.createTable(tableAmtStr, choices, choicesStr);
            valid = table!=null;
            ++retry;
        }

        if (!valid)
        {
            System.out.println("Max tries reached. Exiting game.");
        }
        else
        {
            System.out.println(String.format("$%1$s Table (Minimum Bet) chosen", table.MinimumBet));
        }
        System.out.println();
        return valid;
    }

    private static boolean askNumPlayers(BlackJackGame game) throws Exception
    {
        numPlayers = -1;
        boolean valid = false;
        int retry = 0;

        while (!valid && retry < maxRetry)
        {
            int defVal = 1;
            System.out.print(String.format("How many players will be joining? [%1$s]: ", defVal));
            java.io.DataInput in = new java.io.DataInputStream(System.in);
            String numPlayersStr = in.readLine();
            numPlayers = game.getNumPlayers(numPlayersStr, defVal);
            valid = numPlayers > 0;
            ++retry;
        }

        if (!valid)
        {
            System.out.println("Max tries reached. Exiting game.");
        }
        else
        {
            System.out.println(String.format("%1$s %2$s", numPlayers, numPlayers == 1 ? "Player" : "Players"));
        }
        System.out.println();
        return valid;
    }

    public int getNumPlayers(String numPlayersStr, int defVal)
    { //GetNumPlayers => Test
        int numPlayers = -1;
       
        numPlayersStr = numPlayersStr.trim();
        if (numPlayersStr.isEmpty())
        {
            numPlayers = defVal;           
        }
        else
        {
	        boolean valid = true;
	        try
	        {
	        	numPlayers = Integer.parseInt(numPlayersStr);
	        }
	        catch(Exception ex)
	        {
	        	valid = false;
	        }
	        
	        if (!valid || numPlayers < 1)
	        {
	            System.out.println(String.format("Error: Accept default or Enter a number >= 1. '%1$s' is invalid!", numPlayersStr));
	            System.out.println();
	        }
        }
        return numPlayers;
    }

    public Table createTable(String tableBetStr, TableBet[] choices, String choicesStr) throws Exception
    {
        Table table = null;
        TableBet tableBet = TableBet.None;
        tableBetStr = tableBetStr.trim();
        if (choices == null || choices.length == 0) { throw new Exception("Choices need to be provided for creating table!"); }
        if (tableBetStr.isEmpty())
        {
            tableBet = choices[0];
            table = TableCreator.createTable(tableBet);            
        }
        else
        {
	        boolean valid = true;
	        try
	        {
	        	int amt = Integer.parseInt(tableBetStr);
	        	tableBet = TableBet.fromInt(amt);	        	
	        }
	        catch(Exception ex)
	        {
	        	valid = false;
	        }
	        
	        if (!valid)
	        {
	            System.out.println(String.format("Error: Accept default or Enter one of the choices [%1$s]. '%2$s' is invalid!", choicesStr, tableBetStr));
	            System.out.println();
	        }
	        else
	        {
	            for (TableBet val : TableBet.values())
	            {
	                if (tableBet == val)
	                {
	                    table = TableCreator.createTable(tableBet);
	                    break;
	                }
	            }
	            if (table != null) { valid = true; }
	            else
	            {
	                System.out.println(String.format("Error: Accept default or Enter one the choices [%1$s]. '%2$s' is not one of the choices listed!",
	                    choicesStr, tableBet));
	            }
	        }
        }
        return table;
    }

    public static String createChoicesString(TableBet[] choices)
    { //ChoicesString => Test
        String str = "";
        for (TableBet val : choices)
        {
            if (str != "") { str += ", "; }
            str += val.value();
        }
        return str;
    }

    public static void gameLoop(Table table, DealerMode mode, int numPlayers) throws Exception
    {  //GameLoop => Test                  
        List<BlackJackCard> cardDeck = createDeck(1);
        HashMap<Integer,Player> players = createPlayers(table, numPlayers, 100 * table.MinimumBet);
        Dealer dealer = new Dealer(table, cardDeck, players);
        dealer.Mode = mode;
        int round = 0;
        
        while (players.size() > 0)
        {
            System.out.println(String.format("Round %1$s", ++round));
            System.out.println("====================");
            playGame(table, dealer, players);

            System.out.println();
            System.out.println("====================");
            System.out.println(String.format("End Round%1$s", round));
            System.out.println();

            System.out.print("Would you like to continue? [Y|N]: ");
            int key = System.in.read();           

            if(key!=' ' && key!='y' && key!='Y')
            {
                System.out.println("OK, quitting game...");
                break;
            }
            System.out.println();                
        }

        System.out.println("Game has ended. Goodbye!");
    }
    
    public static void gameLoop2(Table table, DealerMode mode, int numPlayers) throws Exception
    {  //GameLoop => Test                  
        List<BlackJackCard> cardDeck = createDeck(1);
        HashMap<Integer,Player> players = createPlayers2(table, numPlayers, 100 * table.MinimumBet, table.MinimumBet);
        Dealer dealer = new Dealer(table, cardDeck, players);
        dealer.Mode = mode;
        int round = 0;
        
        while (players.size() > 0)
        {
            System.out.println(String.format("Round %1$s", ++round));
            System.out.println("====================");
            playGame2(table, dealer, players);

            System.out.println();
            System.out.println("====================");
            System.out.println(String.format("End Round%1$s", round));
            System.out.println();

            System.out.print("Would you like to continue? [Y|N]: ");
            DataInput in = new DataInputStream(System.in);
            String key = in.readLine().trim();        

            if(!key.isEmpty() && key!="y" && key!="Y")
            {
                System.out.println("OK, quitting game...");
                break;
            }
            System.out.println();                
        }

        System.out.println("Game has ended. Goodbye!");
    }
    
    public static void playGame(Table table, Dealer dealer, HashMap<Integer, Player> players) throws Exception
    { //PlayGame => Test
        dealer.shuffleCards();
        makeBets(table, players);

        dealer.giveCards(2);
        dealer.printHand();

        Map<Integer, PlayerResult> playerResults = new HashMap<Integer, PlayerResult>();

        for (Player p : players.values())
        {
            PlayerResult result = p.playHand(dealer);
            playerResults.put(p.Id, result);
            System.out.println(String.format("PlayerResult %1$s: %2$s", p.Id, result));
        }
        
        BlackJackType dealerBlackJack = dealer.hasBlackJack();
        List<Integer> quit = new ArrayList<Integer>();
        for (Player p : players.values())
        {
            PlayerResult playerResult = playerResults.get(p.Id);
            //Handle the player has blackjack case,
            if (!playerHasBlackJack(table, quit, dealerBlackJack, p, playerResult))
            {
                //then handle Player has no BlackJack
                if (dealerBlackJack!=BlackJackType.None)
                {
                    System.out.println(String.format("Dealer has BlackJack and Player%1$s doesn't.",p.Id));
                    playerLoses(p,quit);
                }
                else if (playerResult == PlayerResult.Surrender)
                {
                    playerSurrenders(p,quit);
                }
                else if (playerResult == PlayerResult.Fold)
                {
                    playerLoses(p, quit);
                }
                else if (playerResult == PlayerResult.Stand)
                {
                    int dealerTotal = dealer.getBestHand().getTotal();
                    int playerTotal = p.getBestHand().getTotal();
                    if (dealerTotal == playerTotal)
                    {
                        System.out.println(String.format("Dealer has %1$s. Player has %2$s. This is a tie", dealerTotal, playerTotal));
                    }
                    else if ((dealerTotal > playerTotal || playerTotal>21) && dealerTotal <= 21)
                    {
                        System.out.println(String.format("Dealer has %1$s. Player has %2$s. Player loses", dealerTotal, playerTotal));
                        playerLoses(p, quit);
                    }
                    else if(playerTotal<=21)
                    {
                        System.out.println(String.format("Dealer has %1$s. Player has %2$s. Player wins!", dealerTotal, playerTotal));
                        playerWins(p);
                    }
                    else
                    {
                        throw new Exception("Unhandled condition!");
                    }
                }
            }
            int cash = p.Cash;
            int chipTotal = p.Chips.getTotal();
            int betAmt = table.getBetAmount(p.Id);
            System.out.println(String.format("Player%1$s has $%2$s in chips, $%3$s in cash, $%4$s remaining on betting table", p.Id, chipTotal, cash, betAmt));
        }

        dealer.collectCards();

        for(int id : quit)
        {
            players.remove(id);
        }
    }
    
    public static void playGame2(Table table, Dealer dealer, HashMap<Integer, Player> players) throws Exception
    { //PlayGame => Test
        dealer.shuffleCards();
        makeBets(table, players);

        dealer.giveCards(2);
        dealer.printHand();

        Map<Integer, PlayerResult> playerResults = new HashMap<Integer, PlayerResult>();

        for (Player p : players.values())
        {
            PlayerResult result = p.playHand2(dealer);
            playerResults.put(p.Id, result);
            System.out.println(String.format("PlayerResult %1$s: %2$s", p.Id, result));
        }
        
        BlackJackType dealerBlackJack = dealer.hasBlackJack();
        List<Integer> quit = new ArrayList<Integer>();
        for (Player p : players.values())
        {
            PlayerResult playerResult = playerResults.get(p.Id);
            //Handle the player has blackjack case,
            if (!playerHasBlackJack(table, quit, dealerBlackJack, p, playerResult))
            {
                //then handle Player has no BlackJack
                if (dealerBlackJack!=BlackJackType.None)
                {
                    System.out.println(String.format("Dealer has BlackJack and Player%1$s doesn't.",p.Id));
                    playerLoses(p,quit);
                }
                else if (playerResult == PlayerResult.Surrender)
                {
                    playerSurrenders(p,quit);
                }
                else if (playerResult == PlayerResult.Fold)
                {
                    playerLoses(p, quit);
                }
                else if (playerResult == PlayerResult.Stand)
                {
                    int dealerTotal = dealer.getBestHand().getTotal();
                    int playerTotal = p.getBestHand().getTotal();
                    if (dealerTotal == playerTotal)
                    {
                        System.out.println(String.format("Dealer has %1$s. Player has %2$s. This is a tie", dealerTotal, playerTotal));
                    }
                    else if ((dealerTotal > playerTotal || playerTotal>21) && dealerTotal <= 21)
                    {
                        System.out.println(String.format("Dealer has %1$s. Player has %2$s. Player loses", dealerTotal, playerTotal));
                        playerLoses(p, quit);
                    }
                    else if(playerTotal<=21)
                    {
                        System.out.println(String.format("Dealer has %1$s. Player has %2$s. Player wins!", dealerTotal, playerTotal));
                        playerWins(p);
                    }
                    else
                    {
                        throw new Exception("Unhandled condition!");
                    }
                }
            }
            int cash = p.Cash;
            int chipTotal = p.Chips.getTotal();
            int betAmt = table.getBetAmount(p.Id);
            System.out.println(String.format("Player%1$s has $%2$s in chips, $%3$s in cash, $%4$s remaining on betting table", p.Id, chipTotal, cash, betAmt));
        }

        dealer.collectCards();

        for(int id : quit)
        {
            players.remove(id);
        }
    }

    private static void playerSurrenders(Player p, List<Integer> quit) throws Exception
    {            
        Table table = p.Table;
        int bet = table.getBetAmount(p.Id);
        int loss = (int)Math.ceil(bet * 0.5);
        System.out.println(String.format("Player%1$s has surrendered. Loses $%2$s of $%3$s total bet", p.Id, loss, bet));
        table.removeBet(p.Id, loss);
    }

    private static boolean playerHasBlackJack(Table table, List<Integer> quit, BlackJackType dealerBlackJack, Player p, PlayerResult playerResult)
    {
        boolean playerBlackJackPure = playerResult == PlayerResult.BlackJackPure;
        boolean playerHasBlackJack = playerResult == PlayerResult.BlackJack || playerBlackJackPure;
        boolean dealerBlackJackPure = dealerBlackJack == BlackJackType.Pure;
 
        if (playerHasBlackJack)
        {
            if ( dealerBlackJack == BlackJackType.None || (playerBlackJackPure && dealerBlackJack == BlackJackType.Natural))
            { //Dealer has no BlackJack OR dealer's has natural whereas player has pure black jack => PlayerWins
                playerWins(p);

            }
            else if (dealerBlackJack == BlackJackType.Pure && !playerBlackJackPure)
            { //Dealer has BlackJack AND Dealer's BlackJack is pure(Player's BlackJack pure/unpure) OR Player's BlackJack is not pure(Dealer's BlackJack is pure/unpure)
                //Dealer has pure BlackJack, whereas Player has Natural black Jack => Player loses
                playerLoses(p, quit);
            }
            else if (dealerBlackJackPure && playerBlackJackPure)
            {
                //Dealer has pure BlackJack and Player has pure BlackJack => Tie
                System.out.println("Dealer has pure BlackJack and Player has pure BlackJack. This is a tie.");
            }
            else /*if(!dealerBlackJackPure && !playerBlackJackPure)*/
            {
                //Dealer has Natural black jack AND Player has Natural Black Jack => Tie
                System.out.println("Dealer has natural BlackJack and Player has natural BlackJack. This is a tie.");
            }
        }
        return false;
    }

    public static void playerLoses(Player p, List<Integer> quit)
    {
        int total = p.Table.removeBet(p.Id).getTotal();
        System.out.println(String.format("Player%1$s loses $%2$s", p.Id, total));
        if(p.Chips.size()==0)
        {
            //player ran out of chips, so gone from table
            System.out.println(String.format("Player%1$s ran out of chips, quits game.", p.Id, total));
            quit.add(p.Id);
        }
    }

    public static void playerWins(Player p)
    { //PlayerWins => Test
        Table table = p.Table;
        int playerBet = table.getBetAmount(p.Id);
        int paid = (int)Math.ceil(((table.PayOut == PayOut.ThreeTwo) ? 0.5 : 0.2) * playerBet);
        int playerWinTotal = playerBet + paid;
        ChipStack newChips = Cashier.exchange(paid);
        table.addChips(p.Id, newChips);

        System.out.println(String.format("Player%1$s Wins! Player betted $%2$s, now has $%3$s", p.Id, playerBet, playerWinTotal));            
    }

    public static void makeBets(Table table, HashMap<Integer, Player> players) throws Exception
    { //MakeBets => Test
        for (Player player:players.values())
        {                
            player.placeBet();
        }
        System.out.println();
    }
    public static HashMap<Integer,Player> createPlayers(Table table, int playerCount, int cashTotal)
    { //CreatePlayers => Test(Done)
        HashMap<Integer,Player> players = new HashMap<Integer,Player>();
        for (int i = 0; i < playerCount; i++)
        {
            Player player = new Player(i, cashTotal, table);
            players.put(i,player);
            System.out.println(String.format("Player%1$s has $%2$s in cash", i, cashTotal));
        }
        return players;
    }
    
    public static HashMap<Integer,Player> createPlayers2(Table table, int playerCount, int cashTotal, int denom) throws Exception
    { //CreatePlayers => Test(Done)
        HashMap<Integer,Player> players = new HashMap<Integer,Player>();
        for (int i = 0; i < playerCount; i++)
        {
        	ChipStack chips = Cashier.exchange(cashTotal,denom);
            Player player = new Player(i, chips, table);
            players.put(i,player);
            System.out.println(String.format("Player%1$s has $%2$s in chips (%3$s chips)", i, cashTotal, player.Chips.size()));
        }
        return players;
    }

    public static List<BlackJackCard> createDeck(int numDecks) throws Exception
    { //CreateDeck => Test(Done)            
        if (numDecks < 1)
        {
            throw new Exception("Number of decks needs to be >= 1");
        }

        List<BlackJackCard> baseDeck = createBaseDeck();

        if (numDecks == 1)
        {
            return baseDeck;
        }
        else
        {
            List<BlackJackCard> fullDeck = new ArrayList<BlackJackCard>();
            for (int k = 0; k < numDecks; k++)
            {
            	fullDeck.addAll(baseDeck);
            }
            return fullDeck;
        }
    }

    private static List<BlackJackCard> createBaseDeck() throws Exception
    { //CreateBaseDeck => Test
        List<BlackJackCard> baseDeck = new ArrayList<BlackJackCard>();        
        for (Suite suit : Suite.values())
        {
            for (int val = 2; val < 15; val++)
            {
                BlackJackCard card = new BlackJackCard(suit, val);
                baseDeck.add(card);
            }
        }
        return baseDeck;
    }        
}
