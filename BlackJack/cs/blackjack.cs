//NOTE: See readme.txt for compile instructions
using System;
using System.Collections.Generic;

namespace Games
{
    public enum PlayerResult
    {
        //None,
        Stand,
        Fold,
        Surrender,
        BlackJack,
        BlackJackPure
        //Hit
    }

    public enum PlayerOption
    {
        Hit,
        Surrender,
        DoubleDown,
        Split,
        Stand
    }
    //BlackJack program
    public class BlackJackGame
    {
        private static int maxRetry = 3;
        private static int deckSize = Enum.GetValues(typeof(Suite)).Length * (Card.Ace - 1);

        public static void Main(string[] args)
        { //Main => Test
            Console.WriteLine("Hi, Welcome to BlackJack!");
            Console.WriteLine();

            var game = new BlackJackGame();
            int numPlayers = 0;
            if (AskNumPlayers(game, out numPlayers))
            {
                Table table = null;
                if (AskTable(game, out table))
                {
                    DealerMode mode;
                    if (AskDealerMode(game, out mode))
                    {
                        GameLoop(table, mode, numPlayers);
                    }
                }
            }
        }

        private static bool AskDealerMode(BlackJackGame game, out DealerMode mode)
        {
            mode = DealerMode.StandOn17;
            bool valid = false;
            int retry = 0;
            while (!valid && retry < maxRetry)
            {
                Console.Write("Dealer stand on 17? [Y|N]: ");
                var ans = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrWhiteSpace(ans) || ans == "y")
                {
                    mode = DealerMode.StandOn17;
                    valid = true;
                }
                else if (ans == "n")
                {
                    mode = DealerMode.StandOn18;
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Error: Choose Y or N. '{0}' is invalid!", ans);
                    Console.WriteLine();
                    valid = false;
                    ++retry;
                }
            }

            if (!valid)
            {
                Console.WriteLine("Max tries reached. Exiting game.");
            }
            else
            {
                Console.WriteLine("{0} chosen", mode == DealerMode.StandOn17 ? "Dealer stand on 17" : "Dealer stand on 18");
            }
            Console.WriteLine();
            return valid;
        }

        private static bool AskTable(BlackJackGame game, out Table table)
        {
            table = null;
            bool valid = false;
            int retry = 0;

            while (!valid && retry < maxRetry)
            {
                int[] choices = new int[] { (int)TableBet.Red, (int)TableBet.Green, (int)TableBet.Black };
                string choicesStr = CreateChoicesString(choices);
                Console.Write("Which table (minimum bet) would you like to play at? [{0}]: ", choicesStr);
                var tableAmtStr = Console.ReadLine();
                table = game.CreateTable(tableAmtStr, choices, choicesStr, out valid);
                ++retry;
            }

            if (!valid)
            {
                Console.WriteLine("Max tries reached. Exiting game.");
            }
            else
            {
                Console.WriteLine("${0} Table (Minimum Bet) chosen", table.MinimumBet);
            }
            Console.WriteLine();
            return valid;
        }

        private static bool AskNumPlayers(BlackJackGame game, out int numPlayers)
        {
            numPlayers = -1;
            bool valid = false;
            int retry = 0;

            while (!valid && retry < maxRetry)
            {
                int defVal = 1;
                Console.Write("How many players will be joining? [{0}]: ", defVal);
                var numPlayersStr = Console.ReadLine();
                numPlayers = game.GetNumPlayers(numPlayersStr, defVal, out valid);
                ++retry;
            }

            if (!valid)
            {
                Console.WriteLine("Max tries reached. Exiting game.");
            }
            else
            {
                Console.WriteLine("{0} {1}", numPlayers, numPlayers == 1 ? "Player" : "Players");
            }
            Console.WriteLine();
            return valid;
        }

        public int GetNumPlayers(string numPlayersStr, int defVal, out bool validNum)
        { //GetNumPlayers => Test
            int numPlayers = -1;
            validNum = false;
            numPlayersStr = numPlayersStr.Trim();
            if (string.IsNullOrWhiteSpace(numPlayersStr))
            {
                numPlayers = defVal;
                validNum = true;
            }
            else if (!int.TryParse(numPlayersStr, out numPlayers) || numPlayers < 1)
            {
                Console.WriteLine("Error: Accept default or Enter a number >= 1. '{0}' is invalid!", numPlayersStr);
                Console.WriteLine();
                validNum = false;
            }
            else
            {
                validNum = true;
            }
            return numPlayers;
        }

        public Table CreateTable(string tableBetStr, int[] choices, string choicesStr, out bool valid)
        {
            Table table = null;
            int tableBet = -1;
            valid = false;
            tableBetStr = tableBetStr.Trim();
            if (choices == null || choices.Length == 0) { throw new InvalidOperationException("Choices need to be provided for creating table!"); }
            if (string.IsNullOrWhiteSpace(tableBetStr))
            {
                tableBet = choices[0];
                table = TableCreator.CreateTable((TableBet)tableBet);
                valid = true;
            }
            else if (!int.TryParse(tableBetStr, out tableBet))
            {
                Console.WriteLine("Error: Accept default or Enter one of the choices [{0}]. '{1}' is invalid!", choicesStr, tableBetStr);
                Console.WriteLine();
                valid = false;
            }
            else
            {
                foreach (int val in Enum.GetValues(typeof(TableBet)))
                {
                    if (tableBet == val)
                    {
                        table = TableCreator.CreateTable((TableBet)tableBet);
                        break;
                    }
                }
                if (table != null) { valid = true; }
                else
                {
                    Console.WriteLine(string.Format("Error: Accept default or Enter one the choices [{0}]. '{1}' is not one of the choices listed!",
                        choicesStr, tableBet));
                }
            }
            return table;
        }

        public static string CreateChoicesString(int[] choices)
        { //ChoicesString => Test
            string str = null;
            foreach (var val in choices)
            {
                if (str != null) { str += ", "; }
                str += val;
            }
            return str;
        }

        public static void GameLoop(Table table, DealerMode mode, int numPlayers)
        {  //GameLoop => Test                  
            var cardDeck = CreateDeck();
            var players = CreatePlayers(table, numPlayers, 100 * table.MinimumBet);
            Dealer dealer = new Dealer(table, cardDeck, players);
            dealer.Mode = mode;
            int round = 0;

            
            while (players.Count > 0)
            {
                Console.WriteLine("Round {0}", ++round);
                Console.WriteLine("====================");
                PlayGame(table, dealer, players);

                Console.WriteLine();
                Console.WriteLine("====================");
                Console.WriteLine("End Round{0}", round);
                Console.WriteLine();

                Console.Write("Would you like to continue? [Y|N]: ");
                var key = Console.ReadLine().Trim().ToLower();

                if(key!=string.Empty && key[0]!='y')
                {
                    Console.WriteLine("OK, quitting game...");
                    break;
                }
                Console.WriteLine();                
            }

            Console.WriteLine("Game has ended. Goodbye!");
            //ShowResults(dealer, players);
        }

        public static void ShowResults(Dealer dealer, List<Player> players)
        { //ShowResults => Test
            throw new NotImplementedException("ShowResults not implemented");
        }

        public static void PlayGame(Table table, Dealer dealer, Dictionary<int, Player> players)
        { //PlayGame => Test
            dealer.ShuffleCards();
            MakeBets(table, players);

            dealer.GiveCards(2);
            dealer.PrintHand();

            Dictionary<int, PlayerResult> playerResults = new Dictionary<int, PlayerResult>();

            foreach (var p in players.Values)
            {
                PlayerResult result = p.PlayHand(dealer);
                playerResults.Add(p.Id, result);
                Console.WriteLine("PlayerResult {0}: {1}", p.Id, result);
            }

            var dealerBlackJackPure = false;
            var dealerBlackJack = dealer.HasBlackJack(out dealerBlackJackPure);
            List<int> quit = new List<int>();
            foreach (var p in players.Values)
            {
                var playerResult = playerResults[p.Id];
                //Handle the player has blackjack case,
                if (!PlayerHasBlackJack(table, dealerBlackJackPure, dealerBlackJack, quit, p, playerResult))
                {
                    //then handle Player has no BlackJack
                    if (dealerBlackJack)
                    {
                        Console.WriteLine("Dealer has BlackJack and Player{0} doesn't.",p.Id);
                        PlayerLoses(p,quit);
                    }
                    else if (playerResult == PlayerResult.Surrender)
                    {
                        PlayerSurrenders(p,quit);
                    }
                    else if (playerResult == PlayerResult.Fold)
                    {
                        PlayerLoses(p, quit);
                    }
                    else if (playerResult == PlayerResult.Stand)
                    {
                        var dealerTotal = dealer.GetHandTotal();
                        var playerTotal = p.GetHandTotal();
                        if (dealerTotal == playerTotal)
                        {
                            Console.WriteLine("Dealer has {0}. Player has {1}. This is a tie", dealerTotal, playerTotal);
                        }
                        else if ((dealerTotal > playerTotal || playerTotal>21) && dealerTotal <= 21)
                        {
                            Console.WriteLine("Dealer has {0}. Player has {1}. Player loses", dealerTotal, playerTotal);
                            PlayerLoses(p, quit);
                        }
                        else if(playerTotal<=21)
                        {
                            Console.WriteLine("Dealer has {0}. Player has {1}. Player wins!", dealerTotal, playerTotal);
                            PlayerWins(p);
                        }
                        else
                        {
                            throw new InvalidOperationException("Unhandled condition!");
                        }
                    }
                }
                var cash = p.Cash;
                var chipTotal = p.Chips.GetTotal();
                var betAmt = table.GetBetAmount(p.Id);
                Console.WriteLine("Player{0} has ${0} in chips, ${1} in cash, ${2} remaining on betting table", chipTotal, cash, betAmt);

                /*
                if(!quit.Contains(p.Id))
                {
                    var r1 = randGen.Next(2);
                    if (r1 == 1)
                    {
                        //Remove entire bet...cash out
                        Console.WriteLine("Player{0} chooses to cash out of the game", p.Id);
                        p.CashOut();
                        quit.Add(p.Id);
                    }
                }
                */
            }

            dealer.CollectCards();

            foreach(var id in quit)
            {
                players.Remove(id);
            }
        }

        private static void PlayerSurrenders(Player p, List<int> quit)
        {            
            var table = p.Table;
            int bet = table.GetBetAmount(p.Id);
            var loss = (int)Math.Ceiling(bet * 0.5);
            Console.WriteLine("Player{0} has surrendered. Loses ${1} of ${2} total bet", p.Id, loss, bet);
            table.RemoveBet(p.Id, loss);
        }

        private static bool PlayerHasBlackJack(Table table, bool dealerBlackJackPure, bool dealerBlackJack, List<int> quit, Player p, PlayerResult playerResult)
        {
            bool playerBlackJackPure = playerResult == PlayerResult.BlackJackPure;
            var playerHasBlackJack = playerResult == PlayerResult.BlackJack || playerBlackJackPure;
            if (playerHasBlackJack)
            {
                if (!dealerBlackJack || (playerBlackJackPure && !dealerBlackJackPure))
                { //Dealer has no BlackJack OR dealer's has natural whereas player has pure black jack => PlayerWins
                    PlayerWins(p);

                }
                else if (dealerBlackJackPure && !playerBlackJackPure)
                { //Dealer has BlackJack AND Dealer's BlackJack is pure(Player's BlackJack pure/unpure) OR Player's BlackJack is not pure(Dealer's BlackJack is pure/unpure)
                    //Dealer has pure BlackJack, whereas Player has Natural black Jack => Player loses
                    PlayerLoses(p, quit);
                }
                else if (dealerBlackJackPure && playerBlackJackPure)
                {
                    //Dealer has pure BlackJack and Player has pure BlackJack => Tie
                    Console.WriteLine("Dealer has pure BlackJack and Player has pure BlackJack. This is a tie.");
                }
                else /*if(!dealerBlackJackPure && !playerBlackJackPure)*/
                {
                    //Dealer has Natural black jack AND Player has Natural Black Jack => Tie
                    Console.WriteLine("Dealer has natural BlackJack and Player has natural BlackJack. This is a tie.");
                }
            }
            return false;
        }

        public static void PlayerLoses(Player p, List<int> quit)
        {
            var total = -1;
            p.Table.RemoveBet(p.Id, out total);
            Console.WriteLine("Player{0} loses ${1}", p.Id, total);
            if(p.Chips.Count==0)
            {
                //player ran out of chips, so gone from table
                Console.WriteLine("Player{0} ran out of chips, quits game.", p.Id, total);
                quit.Add(p.Id);
            }
        }

        public static void PlayerWins(Player p)
        { //PlayerWins => Test
            var table = p.Table;
            var playerBet = table.GetBetAmount(p.Id);
            var paid = (int)Math.Ceiling(((table.PayOut == PayOut.ThreeTwo) ? 0.5 : 0.2) * playerBet);
            var playerWinTotal = playerBet + paid;
            var newChips = Cashier.Exchange(paid);
            table.AddChips(p.Id, newChips);

            Console.WriteLine("Player{0} Wins! Player betted ${1}, now has ${2}", p.Id, playerBet, playerWinTotal);            
        }

        public static void MakeBets(Table table, Dictionary<int, Player> players)
        { //MakeBets => Test
            foreach (var player in players.Values)
            {                
                player.PlaceBet();
            }
            Console.WriteLine();
        }
        public static Dictionary<int,Player> CreatePlayers(Table table, int playerCount, int cashTotal)
        { //CreatePlayers => Test(Done)
            Dictionary<int,Player> players = new Dictionary<int,Player>();
            for (var i = 0; i < playerCount; i++)
            {
                Player player = new Player(i, cashTotal, table);
                players.Add(i,player);
                Console.WriteLine("Player{0} has ${1} in cash", i, cashTotal);
            }
            return players;
        }

        public static BlackJackCard[] CreateDeck(int numDecks = 1)
        { //CreateDeck => Test(Done)            
            if (numDecks < 1)
            {
                throw new InvalidOperationException("Number of decks needs to be >= 1");
            }

            BlackJackCard[] baseDeck = CreateBaseDeck();

            if (numDecks == 1)
            {
                return baseDeck;
            }
            else
            {
                BlackJackCard[] fullDeck = new BlackJackCard[numDecks * deckSize];
                for (var k = 0; k < numDecks; k++)
                {
                    baseDeck.CopyTo(fullDeck, k * deckSize);
                }
                return fullDeck;
            }
        }

        private static BlackJackCard[] CreateBaseDeck()
        { //CreateBaseDeck => Test
            var suites = Enum.GetValues(typeof(Suite));
            BlackJackCard[] baseDeck = new BlackJackCard[deckSize];
            int j = 0;
            foreach (Suite suit in suites)
            {
                for (var val = 2; val < 15; val++)
                {
                    var card = new BlackJackCard(suit, val);
                    baseDeck[j++] = card;
                }
            }
            return baseDeck;
        }        
    }

    public enum PayOut
    {
        ThreeTwo,
        SixFive
    }

    public enum TableBet
    {
        Red = 5,
        Green = 25,
        Black = 100
    }

    public static class TableCreator
    {
        public static Table CreateTable(TableBet tableBet)
        { //CreateTable => Test
            var payOut = PayOut.ThreeTwo;
            switch (tableBet)
            {
                case TableBet.Red: return new RedTable(payOut);
                case TableBet.Green: return new GreenTable(payOut);
                case TableBet.Black: return new BlackTable(payOut);
                default: throw new InvalidOperationException(string.Format("Error: Don't know what table to create for {0} table", tableBet));
            }
        }
    }

    public abstract class Table
    {
        private Dictionary<int, List<ChipStack>> tableBets = new Dictionary<int, List<ChipStack>>();

        public Table(TableBet minBet, PayOut payOut)
        { //Table => Test
            this.MinimumBet = (int)minBet;
            this.PayOut = payOut;
        }

        public PayOut PayOut { get; private set; }
        public int MinimumBet { get; private set; }

        public void AddBet(int playerId, ChipStack bet)
        { // AddBet => Test
            List<ChipStack> bets;
            if (!tableBets.ContainsKey(playerId))
            {
                bets = new List<ChipStack>();
                tableBets.Add(playerId, bets);
            }
            else
            {
                bets = tableBets[playerId];
            }
            bets.Add(bet);
        }

        public void AddChips(int id, ChipStack newChips)
        { //AddChips => Test
            var list = tableBets[id];
            var i = list.Count;
            if (i == 0)
            {
                list.Add(newChips);
            }
            else
            {
                var lastStack = list[i - 1];
                foreach (var chip in newChips)
                {
                    lastStack.Push(chip);
                }
            }
        }

        public int GetBetAmount(int id)
        { //GetBetAmount => Test(Done)
            var total = 0;
            if (tableBets.ContainsKey(id))
            {
                foreach (var stack in tableBets[id])
                {
                    total += stack.GetTotal();
                }
            }
            return total;
        }

        public ChipStack RemoveBet(int id, out int total)
        {
            total = 0;
            ChipStack result = null;
            foreach (ChipStack s in tableBets[id])
            {
                total += s.GetTotal();
                if(result==null)
                {
                    result = s;
                }
                else
                {
                    while(s.Count>0)
                    {
                        result.Push(s.Pop());
                    }
                }
            }
            tableBets.Remove(id);
            return result;
        }

        public ChipStack RemoveBet(int id, int amount)
        {
            var total = 0;
            var rem = amount;
            ChipStack result = null;
            foreach(var s in tableBets[id])
            {               
                var taken = s.GetTotal();
                total += taken;
                if (rem < taken)
                {
                    taken = rem;                    
                }
                var thisStack = s.RemoveAmount(taken);
                if(result==null)
                {
                    result = thisStack;
                }
                else
                {
                    while(thisStack.Count>0)
                    {
                        result.Push(thisStack.Pop());
                    }
                }
                rem = rem - taken;

                if(rem==0)
                {
                    break;
                }
                else if(rem<0)
                {
                    throw new InvalidOperationException(
                        string.Format("Error: Player{0} doesn't have enough money betted to recoup. Player has {1}, amount to be taken away {2}",id,total,amount));
                }
            }
            return result;
        }
    }

    public class RedTable : Table
    {
        public RedTable(PayOut payOut) : base(TableBet.Red, payOut) { }
    }

    public class GreenTable : Table
    {
        public GreenTable(PayOut payOut) : base(TableBet.Green, payOut) { }
    }

    public class BlackTable : Table
    {
        public BlackTable(PayOut payOut) : base(TableBet.Black, payOut) { }
    }

    public enum DealerMode
    {
        StandOn17,
        StandOn18
    }

    public class BlackJackHand : List<BlackJackCard>
    {
        public BlackJackHand(int id)
        {
            this.Id = id;
        }
        public int Id { get; private set; }
        public int GetTotal()
        {
            int total = 0;
            int numAces = 0;
            foreach (BlackJackCard card in this)
            {
                if (card.Value != Card.Ace)
                {
                    total += card.ActualValue;
                }
                else
                {
                    numAces++;
                }
            }

            var diff = 21 - total;
            int[] theAces = new int[numAces];
            var totalAces = numAces;
            while (numAces > 0)
            {
                var current = totalAces - numAces;
                if (diff < 1)
                {
                    total += 1;
                    theAces[current] = 1;
                    for (int i = current - 1; i >= 0; i--)
                    {
                        if (theAces[i] == 11)
                        {
                            total -= 11;
                            total += 1;
                            theAces[i] = 1;
                            if ((i + 1) <= totalAces)
                            {
                                numAces = i + 1;
                            }
                        }
                    }
                }
                else if (diff < 11)
                {
                    total++;
                    theAces[current] = 1;
                }
                else
                {
                    total += 11;
                    theAces[current] = 11;
                }
                --numAces;
                diff = 21 - total;
            }

            return total;
        }
    }

    public abstract class Participant
    {
        protected List<BlackJackHand> multipleHands = new List<BlackJackHand>();

        public Participant()
        {
            this.multipleHands.Add(new BlackJackHand(0));
        }

        public void AddCard(BlackJackCard card)
        {
            var hand = multipleHands[multipleHands.Count - 1];
            hand.Add(card);
        }

        public void AddCard(int handNum, BlackJackCard card)
        {
            multipleHands[handNum].Add(card);
        }

        public List<BlackJackCard> GetCards()
        {
            var cards = new List<BlackJackCard>();
            foreach (var hand in multipleHands)
            {
                cards.AddRange(hand);
            }

            this.multipleHands.Clear();
            this.multipleHands.Add(new BlackJackHand(0));

            return cards;
        }

        public bool Bust()
        {
            int total = GetHandTotal();

            return (total > 21);
        }

        public bool HasBlackJack(out bool pure)
        {
            /*
            int amt=0;
            int numAces = 0;
            pure = false;
            int aceLoc = -1;
            for (var i = 0; i < hand.Count; i++ )
            {
                var card = hand[i];
                if (card.Value == Card.Ace)
                {
                    numAces++;
                    aceLoc = i;
                }
                amt += card.Value;
            }
            if (hand.Count == 2)
            {
                if (numAces == 1)
                {
                    var nonAce = hand[1 - aceLoc];
                    var tenCard = nonAce.Value == 10;
                    pure = tenCard;
                    return pure;
                }
                else // 2 Aces or 2 non Ace cards
                {
                    return false;
                }
            }
            if(amt>21) { return false; }
            else {
                if(numAces == 0) {
                    return amt==21;
                }
                else {
                    var diff = 21 - amt;                 
                    if(diff>0) {
                        for(int i=0; i<numAces; i++) {
                            var delta = diff - 11;
                            if(delta>=0) {                           
                                amt+=11;
                                diff = delta;
                            }
                            else {
                                delta = diff - 1;                                
                                amt+=1;
                                diff = delta;                                
                            }
                        }  
                        return amt==21;
                    }
                    else { return false; }
                }
            }*/

            pure = false;
            BlackJackHand theHand;
            if (this.GetHandTotal(out theHand) == 21)
            {
                if (theHand.Count == 2)
                {
                    pure = true;
                }
                return true;
            }

            return false;
        }

        public int GetHandTotal()
        {
            BlackJackHand theHand;
            return GetHandTotal(out theHand);
        }

        public int GetHandTotal(out BlackJackHand theHand)
        {
            theHand = null;
            var bestHand = 22;
            foreach (var thisHand in multipleHands)
            {
                var amt = thisHand.GetTotal();
                if (bestHand > 21 || (amt > bestHand && amt <= 21) || (amt == 21 && theHand.GetTotal() == 21))
                {
                    bestHand = amt;
                    theHand = thisHand;
                }
            }
            return bestHand;
        }

        public virtual void PrintHand()
        {
            for (int i = 0; i < this.multipleHands.Count; i++)
            {
                var hand = multipleHands[i];
                Console.WriteLine("Hand{0}:", i + 1);
                foreach (BlackJackCard card in hand)
                {
                    string strVal = card.Value.ToString();
                    if (card.Value == Card.Jack)
                    {
                        strVal = "Jack";
                    }
                    else if (card.Value == Card.Queen)
                    {
                        strVal = "Queen";
                    }
                    else if (card.Value == Card.King)
                    {
                        strVal = "King";
                    }
                    else if (card.Value == Card.Ace)
                    {
                        strVal = "Ace";
                    }
                    Console.WriteLine("({0},{1},{2})", card.Suite, strVal, card.ActualValue);
                }
                Console.WriteLine("Total: " + hand.GetTotal());
                Console.WriteLine();
            }
        }
    }

    public class Dealer : Participant
    {
        private Table table = null;
        private Dictionary<int,Player> players = null;
        private Stack<BlackJackCard> cardStack = new Stack<BlackJackCard>();

        public Dealer(Table table, BlackJackCard[] cardDeck, Dictionary<int,Player> players)
        { //Dealer => Test
            this.table = table;
            this.players = players;
            foreach (var card in cardDeck)
            {
                cardStack.Push(card);
            }
        }
        public DealerMode Mode { get; set; }
        public void ShuffleCards()
        { //Shuffle => Test
            
            var cardDeck = new List<BlackJackCard>();
            while (cardStack.Count > 0)
            {
                cardDeck.Add(cardStack.Pop());
            }
            var deckLength = cardDeck.Count;
            Random randGen = new Random();
            for (var current = 0; current < deckLength - 1; current++)
            {
                var i = randGen.Next(current + 1, deckLength);
                Swap(cardDeck, current, i);
            }
            foreach (var card in cardDeck)
            {
                cardStack.Push(card);
            }

            Console.WriteLine("Dealer has shuffled card deck of {0} cards ({1} decks)", cardStack.Count, deckLength/52);
        }

        public void Swap(List<BlackJackCard> a, int i, int j)
        { //Swap => Test
            var tmp = a[i];
            a[i] = a[j];
            a[j] = tmp;
        }

        public void GiveCards(int numCards)
        { //GiveCards => Test
            for (var i = 0; i < numCards; i++)
            {
                foreach (Player p in players.Values)
                {
                    p.AddCard(GetCard());
                }
                this.AddCard(GetCard());
            }

            var stop = (this.Mode == DealerMode.StandOn17) ? 17 : 18;
            while (this.GetHandTotal() < stop)
            {
                this.AddCard(GetCard());             
            }
        }

        public BlackJackCard GetCard()
        {
            return cardStack.Pop();
        }

        public void CollectCards()
        {
            foreach (var p in players.Values)
            {
                foreach (BlackJackCard card in p.GetCards())
                {
                    cardStack.Push(card);                    
                }
            }

            foreach (BlackJackCard card in this.GetCards())
            {
                cardStack.Push(card);
            }
        }

        public override void PrintHand()
        {
            Console.WriteLine("Dealer:");
            base.PrintHand();
        }        
    }

    public class Player : Participant
    {
        private Random randGen = new Random();

        public Player(int id, int cashTotal, Table table)
        { //Player => Test
            this.Id = id;
            this.Chips = Cashier.Exchange(cashTotal);
            this.Cash = 0;
            this.Table = table;
        }

        public int Id { get; private set; }

        public ChipStack Chips { get; private set; }

        public int Cash { get; private set; }

        public Table Table { get; set; }

        public void PlaceBet()
        {
            var currentBet = Table.GetBetAmount(this.Id);
            bool betted = false;
            if(currentBet==0)
            {
                this.PlaceMinimumBet();
                betted = true;
            }
            else if(currentBet<this.Table.MinimumBet)
            {
                this.PlaceRandomBet();
                betted = true;
            }

            Console.WriteLine("Player{0} has ${1} in chips, ${2} in cash remaining", this.Id, this.Chips.GetTotal(), this.Cash);
            if(!betted)
            {
                Console.WriteLine("...and has ${0} currently on the betting table",currentBet);
            }
            
        }

        public void PlaceMinimumBet()
        { //PlaceBet => Test
            var chipTotal = this.Chips.GetTotal();
            if (chipTotal >= this.Table.MinimumBet)
            {
                ChipStack stack = this.Chips.RemoveAmount(this.Table.MinimumBet);
                Console.WriteLine("Player {0} bets minimum: ${1}", this.Id, stack.GetTotal());
                this.Table.AddBet(this.Id, stack);  
            }
            else if (this.Cash >= this.Table.MinimumBet)
            {
                this.Cash -= this.Table.MinimumBet;
                Console.WriteLine("Player {0} bets minimum: ${1}", this.Id, this.Table.MinimumBet);
                this.Table.AddBet(this.Id, ChipStack.GetChips(this.Table.MinimumBet));
            }
            else
            {
                throw new InvalidOperationException(string.Format("Player {0} COULD NOT PlACE MINIMUM BET: ${1}. Cash ${2}, Chips ${3}", this.Id, this.Table.MinimumBet, this.Cash, chipTotal));
            }
        }

        public void PlaceRandomBet()
        {
            this.RemoveEntireBet();            
            var chipTotal = this.Chips.GetTotal();
            int total = chipTotal + this.Cash;
            if (total > this.Table.MinimumBet)
            {
                var newBet = 0;

                while (newBet < this.Table.MinimumBet)
                {
                    var num = randGen.Next(1, 51);
                    double percent = num / 100.0;
                    newBet = (int)Math.Ceiling(percent * total);
                }

                Console.WriteLine("Player {0} random bet: ${1}", this.Id, newBet);
                if(newBet<=chipTotal)
                {
                    var newBetStack = this.Chips.RemoveAmount(newBet);
                    this.Table.AddBet(this.Id, newBetStack);
                }
                else
                {
                    var rem = newBet - chipTotal;      
                    ChipStack stack = new ChipStack();
                    while(this.Chips.Count>0)
                    {
                        stack.Push(this.Chips.Pop());
                    }
                    this.Cash -= rem;
                    var more = ChipStack.GetChips(rem);
                    while(more.Count>0)
                    {
                        stack.Push(more.Pop());
                    }
                    this.Table.AddBet(this.Id, stack);                                                           
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format("Player {0} COULD NOT PlACE RANDOM BET: ${1}. Cash ${2}, Chips ${3}", this.Id, this.Table.MinimumBet, this.Cash, chipTotal));
            }
        }

        // Returns true, if done playing hand
        public PlayerResult PlayHand(Dealer dealer)
        {
            this.PrintHand();
            var pure = false;
            if (Bust())
            {
                Console.WriteLine("Player {0} busted!", this.Id);
                return PlayerResult.Fold;
            }

            if (HasBlackJack(out pure))
            {
                if (pure)
                {
                    Console.WriteLine("Player {0} has pure BlackJack!", this.Id);
                    return PlayerResult.BlackJackPure;
                }
                else
                {
                    Console.WriteLine("Player {0} has natural BlackJack!", this.Id);
                    return PlayerResult.BlackJack;
                }
            }

            List<PlayerOption> options = new List<PlayerOption>() { PlayerOption.Stand };
            //int splitHand = -1;
            if (HasSplit(/*out splitHand*/))
            {
                //Console.WriteLine("Player{0} can Split", this.Id);
                options.Add(PlayerOption.Split);
            }

            if (CanDoubleDown())
            {
                //Console.WriteLine("Player{0} can DoubleDown", this.Id);
                options.Add(PlayerOption.DoubleDown);
            }

            var hitHands = new List<BlackJackHand>();
            if (CanHit(ref hitHands))
            {
                //Console.WriteLine("Player{0} can Hit", this.Id);
                options.Add(PlayerOption.Hit);
            }

            if (CanSurrender())
            {
                //Console.WriteLine("Player{0} can Surrender", this.Id);
                options.Add(PlayerOption.Surrender);
            }

            if (options.Count > 0)
            {
                foreach (var thisOption in options)
                {
                    Console.WriteLine("Player{0} can {1}", this.Id, thisOption);
                }
                Console.WriteLine();
                var i = randGen.Next(0, options.Count);
                var op = options[i];
                Console.WriteLine("Player{0} chooses {1} option", this.Id, op);
                switch (op)
                {
                    case PlayerOption.DoubleDown:
                        return HandleDoubleDown(dealer);
                    case PlayerOption.Hit:
                        return HandleHit(dealer, hitHands);
                    case PlayerOption.Split:
                        return HandleSplit(dealer);
                    case PlayerOption.Surrender:
                        Console.WriteLine("Player {0} will surrender!", this.Id);
                        return PlayerResult.Surrender;
                    case PlayerOption.Stand:
                        Console.WriteLine("Player {0} will stand!", this.Id);
                        return PlayerResult.Stand;
                    default:
                        throw new InvalidOperationException(string.Format("PlayerOption not handled: {0}", op));

                }
            }
            Console.WriteLine("Player {0} has no options and will resort to Folding!", this.Id);
            return PlayerResult.Fold;
        }

        private PlayerResult HandleHit(Dealer dealer, List<BlackJackHand> hitHands)
        {
            Console.WriteLine("Player {0} will hit!", this.Id);
            var i = randGen.Next(0, hitHands.Count);
            //Hit
            this.AddCard(hitHands[i].Id, dealer.GetCard());            
            Console.WriteLine("...and will play again!");
            return this.PlayHand(dealer);            
        }

        private PlayerResult HandleDoubleDown(Dealer dealer)
        {
            Console.WriteLine("Player {0} will double down!", this.Id);
            //Double down
            var currentBet = this.Table.GetBetAmount(this.Id);
            var currentCash = this.Chips.GetTotal();
            var amt = currentBet;
            if (currentCash < currentBet)
            {
                amt = currentCash;
            }
            var newBet = 0;
            var percent = 0.0;
            while (newBet == 0)
            {
                percent = randGen.Next(1, 101) / 100.0;
                newBet = (int)Math.Ceiling(percent * amt);
            }
            Console.WriteLine("... places new bet ${0} ({1}% of total)!", newBet, percent * 100);
            var chipAmt = this.Chips.RemoveAmount(newBet);
            this.Table.AddBet(this.Id, chipAmt);
            Console.WriteLine("... new total bet ${0}!", this.Table.GetBetAmount(this.Id));
            this.AddCard(dealer.GetCard());
            return this.PlayHand(dealer);
        }

        private PlayerResult HandleSplit(Dealer dealer)
        {
            BlackJackHand splitHand = this.multipleHands[0];
            Console.WriteLine(
                "Player {0} will split hand ({1},{2})!", 
                this.Id, 
                splitHand[0].GetFaceValue(),
                splitHand[1].GetFaceValue());

            var card = splitHand[1];
            splitHand.RemoveAt(1);
            BlackJackHand newHand = new BlackJackHand(this.multipleHands.Count);
            newHand.Add(card);
            this.multipleHands.Add(newHand);
            Console.WriteLine("...and will play again!");
            return this.PlayHand(dealer);
        }

        public bool CanSurrender()
        {
            return (this.multipleHands.Count == 1 && this.multipleHands[0].Count == 2);
        }

        public bool CanHit(ref List<BlackJackHand> theHands)
        {
            bool result = false;
            if (theHands == null)
            {
                theHands = new List<BlackJackHand>();
            }
            foreach (var hand in this.multipleHands)
            {
                if (hand.GetTotal() < 21)
                {
                    result = true;
                    theHands.Add(hand);
                }
            }
            return result;
        }

        public bool CanDoubleDown()
        {
            if (this.multipleHands[0].Count != 2)
            {
                return false;
            }
            return this.multipleHands.Count == 1 && this.Chips.GetTotal() > 0;
        }

        public bool HasSplit(/*out int splitHand*/)
        {
            //Assumption: Can only split on single hand containing 2 cards
            if (this.multipleHands.Count > 1 || this.multipleHands[0].Count != 2)
            {
                return false;
            }
            return this.multipleHands.Count == 1 && this.multipleHands[0][0].Value == this.multipleHands[0][1].Value;

            /*
            bool result = false;
            foreach(var hand in this.multipleHands)
            {
                if(hand.Count==2 && hand[0].Value == hand[1].Value)
                {
                    result = true;
                    theHand = hand;
                    break;
                }
            }
            return result;*/
        }

        public void RemoveEntireBet()
        {
            if (this.Table.GetBetAmount(this.Id) > 0)
            {
                int amt = -1;
                var betStack = this.Table.RemoveBet(this.Id, out amt);
                while (betStack.Count > 0)
                {
                    this.Chips.Push(betStack.Pop());
                }
            }
        }

        public void CashOut()
        {
            this.RemoveEntireBet();
            this.Cash += this.Chips.GetTotal();
            this.Chips.Clear();
        }                

        public override void PrintHand()
        {
            Console.WriteLine("Player {0}:", this.Id);
            base.PrintHand();
        }
    }

    public static class Cashier
    {
        public static ChipStack Exchange(double cash)
        { //Exchange => Test
            return ChipStack.GetChips(cash);
        }
    }

    public enum Chip
    {
        Purple = 500,
        Black = 100,
        Green = 25,
        Red = 5,
        White = 1
    }

    public class ChipStack : Stack<Chip>
    {
        private int cashAmount = 0;

        new public void Push(Chip newChip)
        { //Push => Test(Done)
            cashAmount += (int)newChip;
            if (this.Count == 0)
            {
                base.Push(newChip);
            }
            else
            {
                var secondStack = new ChipStack();
                while (this.Count > 0 && this.Peek() < newChip)
                {
                    secondStack.Push(this.Pop());
                }
                base.Push(newChip);
                while (secondStack.Count > 0)
                {
                    this.Push(secondStack.Pop());
                }
            }
        }

        new public Chip Pop()
        { //Pop => Test
            var val = base.Pop();
            cashAmount -= (int)val;
            return val;
        }

        //This will return the combo of chips that are present within current chip stack
        public ChipStack RemoveAmount(double total)
        { //RemoveAmount => Test(Done)

            var amt = total;
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Error: No chips available!");
            }
            var myStack = this;
            var secondStack = new Stack<Chip>();
            var chips = new ChipStack();
            Dictionary<Chip, int> chipCount = new Dictionary<Chip, int>();
            var largestAmt = -1;

            while (myStack.Count > 0)
            {
                var current = myStack.Pop();
                secondStack.Push(current);
                if (!chipCount.ContainsKey(current))
                {
                    chipCount.Add(current, 1);
                }
                else
                {
                    chipCount[current]++;
                }
            }

            if (amt > 0)
            {
                var list = new List<Chip>();
                list.AddRange((Chip[])Enum.GetValues(typeof(Chip)));
                list.Sort();

                for (int j = list.Count - 1; j >= 0; j--)
                {
                    var chip = list[j];
                    int denom = (int)chip;

                    var numChips = Math.Floor(amt / denom);

                    if (denom >= total && chipCount.ContainsKey(chip) && chipCount[chip] > 0)
                    {
                        largestAmt = denom;
                    }

                    if (numChips > 0)
                    {
                        if (!chipCount.ContainsKey(chip) || chipCount[chip] == 0)
                        {
                            continue;
                        }
                        else
                        {

                            if (chipCount[chip] >= numChips)
                            {
                                for (var i = 0; i < numChips; i++)
                                {
                                    while (secondStack.Count > 0 && secondStack.Peek() != chip)
                                    {
                                        myStack.Push(secondStack.Pop());
                                    }
                                    if (secondStack.Count == 0)
                                    {
                                        throw new InvalidOperationException("Something went wrong..There should have been chips available on the stack!");
                                    }
                                    chips.Push(secondStack.Pop());
                                }
                                amt = amt % denom;

                                if (amt == 0)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                while (numChips > 0 && numChips > chipCount[chip])
                                {
                                    --numChips;
                                }

                                for (var i = 0; i < numChips; i++)
                                {
                                    while (secondStack.Count > 0 && secondStack.Peek() != chip)
                                    {
                                        myStack.Push(secondStack.Pop());
                                    }
                                    if (secondStack.Count == 0)
                                    {
                                        throw new InvalidOperationException("Something went wrong..There should have been chips available on the stack!");
                                    }
                                    chips.Push(secondStack.Pop());
                                }

                                amt = amt - (numChips * (int)chip);

                                if (amt == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            while (secondStack.Count > 0)
            {
                myStack.Push(secondStack.Pop());
            }

            var diff = total - chips.GetTotal();
            if (diff != 0)
            {
                //We don't have enough 
                //and we don't have any unused chips
                if (myStack.Count == 0)
                {
                    //So we return the chips to the stack
                    while (chips.Count > 0)
                    {
                        myStack.Push(chips.Pop());
                    }

                    //If we have a single denomination
                    //that's bigger than the entire total
                    if (largestAmt > 0)
                    {
                        //then find that denomination
                        while (myStack.Count > 0 && (int)myStack.Peek() >= largestAmt)
                        {
                            var chip = myStack.Pop();
                            if ((int)chip == largestAmt)
                            {
                                //return it in chips
                                chips.Push(chip);

                                //and make change
                                var change = (int)chip - amt;
                                foreach (var newChip in ChipStack.GetChips(change))
                                {
                                    myStack.Push(newChip);
                                }
                                break;
                            }
                            else
                            {
                                secondStack.Push(chip);
                            }
                        }

                        while (secondStack.Count > 0)
                        {
                            myStack.Push(secondStack.Pop());
                        }
                    }
                }
                else
                {
                    if (largestAmt < 0)
                    {
                        var addChips = this.RemoveAmount(diff);
                        while (addChips.Count > 0)
                        {
                            chips.Push(addChips.Pop());
                        }
                    }
                    else
                    {
                        //If we have a single denomination
                        //that's bigger than the entire total

                        //then find that denomination
                        while (myStack.Count > 0 && (int)myStack.Peek() >= largestAmt)
                        {
                            var chip = myStack.Pop();
                            if ((int)chip == largestAmt)
                            {
                                var change = (int)chip - amt;
                                var returned = (int)chip - change;
                                var extra = ChipStack.GetChips(returned);
                                if (extra.GetTotal() == returned)
                                {
                                    foreach (var thisChip in extra)
                                    {
                                        //return whatever is needed in chips
                                        chips.Push(thisChip);
                                    }

                                    foreach (var newChip in ChipStack.GetChips(change))
                                    {
                                        //and make change
                                        myStack.Push(newChip);
                                    }
                                }
                                break;
                            }
                            else
                            {
                                secondStack.Push(chip);
                            }
                        }

                        while (secondStack.Count > 0)
                        {
                            myStack.Push(secondStack.Pop());
                        }
                    }
                }
            }
            return chips;
        }

        public static ChipStack GetChips(double amt)
        { //GetChips => Test(Done)
            var chips = new ChipStack();
            if (amt > 0)
            {
                var list = new List<Chip>();
                list.AddRange((Chip[])Enum.GetValues(typeof(Chip)));
                list.Sort();
                for (int j = list.Count - 1; j >= 0; j--)
                {
                    var chip = list[j];
                    int denom = (int)chip;
                    var numChips = Math.Floor(amt / denom);
                    for (var i = 0; i < numChips; i++)
                    {
                        chips.Push(chip);
                    }
                    amt = amt % denom;

                    if (amt == 0)
                    {
                        break;
                    }
                }
            }
            return chips;
        }

        public int GetTotal()
        { //GetTotal => Test
            return cashAmount;
        }
    }

    public enum Suite
    {
        Hearts,
        Spades,
        Clubs,
        Diamonds
    }

    public class BlackJackCard : Card
    {
        public BlackJackCard(Suite suite, int val) : base(suite, val) { } //BlackCard => Test(Done)
        public int ActualValue
        { //ActualValue => Test
            get
            {
                var thisVal = base.Value;
                if (thisVal == Card.Ace)
                {
                    return 11;
                }
                if (thisVal >= Card.Jack && thisVal <= Card.King)
                {
                    return 10;
                }
                return this.val;
            }
        }
    }

    public class Card
    {
        protected int val;
        public Card(Suite suite, int val)
        { //Card  => Test (Done)
            this.Suite = suite;
            this.val = val; //There's a quirk when BlackJackCard.Value is set, val becomes equal to 0
            this.Value = val;
        }

        public static int Ace { get { return 14; } } //Ace => Test(Done)
        public static int King { get { return 13; } } //King => Test(Done)
        public static int Queen { get { return 12; } } //Queen => Test(Done)
        public static int Jack { get { return 11; } } //Jack => Test(Done)

        public Suite Suite
        { //Suite => Test (Done)
            get;
            set;
        }
        public virtual int Value
        { //Value => Test (Done)
            get { return val; }
            set
            {
                var newVal = value;
                if (newVal < 1 || newVal > 14)
                {
                    throw new InvalidOperationException(string.Format("Card value must be between 1-14. Invalid value {0}", newVal));
                }
                this.val = newVal;
            }
        }
        public string GetFaceValue()
        { //GetFaceValue => Test (Done)
            if (this.Value < 11)
            {
                return this.Value.ToString();
            }
            else if (this.Value == 11)
            {
                return "Jack";
            }
            else if (this.Value == 12)
            {
                return "Queen";
            }
            else if (this.Value == 13)
            {
                return "King";
            }
            else if (this.Value == 14)
            {
                return "Ace";
            }
            throw new InvalidOperationException("Cannot determine value of card!");
        }
    }

    /*
    public class Stack<T>
    {
        private List<T> collection = new List<T>();

        public void Push(T val) { //Push => Test (Done)
            collection.Add(val);
        }

        public T Pop() { //Pop => Test (Done)

            if (!this.IsEmpty)
            {
                var i = collection.Count - 1;
                var item = collection[i];
                collection.RemoveAt(i);
                return item;
            }
            return default(T);
        }

        public bool IsEmpty { //IsEmpty => Test (Done)
            get { return collection.Count == 0; }
        }

        public T Peek() { //Peek => Test (Done)
            var i = collection.Count - 1;
            return collection[i];
        }

        public List<T> PopAll()
        {
            var list = new List<T>();
            while (this.Count > 0)
            {
                list.Add(this.Pop());
            }
            return list;
        }
    }*/
}