package Games;

import java.io.*;
import java.util.*;

public class Player extends Participant {
    private Random randGen = new Random();

    public Player(int id, int cashTotal, Table table)
    { //Player => Test
        this.Id = id;
        this.Chips = Cashier.exchange(cashTotal);
        this.Cash = 0;
        this.Table = table;
    }
    
    public Player(int id, ChipStack chips, Table table)
    { //Player => Test
        this.Id = id;
        this.Chips = chips;
        this.Cash = 0;
        this.Table = table;
    }

    public int Id;

    public ChipStack Chips;

    public int Cash;

    public Table Table;

    public void placeBet() throws Exception
    {
        int currentBet = Table.getBetAmount(this.Id);
        boolean betted = false;
        if(currentBet==0)
        {
            this.placeMinimumBet();
            betted = true;
        }
        else if(currentBet<this.Table.MinimumBet)
        {
            this.placeRandomBet();
            betted = true;
        }

        System.out.println(String.format("Player%1$s has $%2$s in chips, $%3$s in cash remaining", this.Id, this.Chips.getTotal(), this.Cash));
        if(!betted)
        {
            System.out.println(String.format("...and has $%1$s currently on the betting table",currentBet));
        }        
    }

    public void placeMinimumBet() throws Exception
    { //PlaceBet => Test
        int chipTotal = this.Chips.getTotal();
        if (chipTotal >= this.Table.MinimumBet)
        {
            ChipStack stack = this.Chips.removeAmount(this.Table.MinimumBet);
            System.out.println(String.format("Player %1$s bets minimum: $%2$s", this.Id, stack.getTotal()));
            this.Table.addBet(this.Id, stack);  
        }
        else if (this.Cash >= this.Table.MinimumBet)
        {
            this.Cash -= this.Table.MinimumBet;
            System.out.println(String.format("Player %1$s bets minimum: $%2$s", this.Id, this.Table.MinimumBet));
            this.Table.addBet(this.Id, ChipStack.getChips(this.Table.MinimumBet));
        }
        else
        {
            throw new Exception(String.format("Player %1$s COULD NOT PlACE MINIMUM BET: $%2$s. Cash $%3$s, Chips ${3}", this.Id, this.Table.MinimumBet, this.Cash, chipTotal));
        }
    }

    public void placeRandomBet() throws Exception
    {
        this.removeEntireBet();            
        int chipTotal = this.Chips.getTotal();
        int total = chipTotal + this.Cash;
        if (total > this.Table.MinimumBet)
        {
            int newBet = 0;

            while (newBet < this.Table.MinimumBet)
            {
                int num = randGen.nextInt(50) + 1;
                double percent = num / 100.0;
                newBet = (int)Math.ceil(percent * total);
            }

            System.out.println(String.format("Player %1$s random bet: $%2$s", this.Id, newBet));
            if(newBet<=chipTotal)
            {
                ChipStack newBetStack = this.Chips.removeAmount(newBet);
                this.Table.addBet(this.Id, newBetStack);
            }
            else
            {
                int rem = newBet - chipTotal;      
                ChipStack stack = new ChipStack();
                while(this.Chips.size()>0)
                {
                    stack.push(this.Chips.pop());
                }
                this.Cash -= rem;
                ChipStack more = ChipStack.getChips(rem);
                while(more.size()>0)
                {
                    stack.push(more.pop());
                }
                this.Table.addBet(this.Id, stack);                                                           
            }
        }
        else
        {
            throw new Exception(String.format("Player %1$s COULD NOT PlACE RANDOM BET: $%2$s. Cash $%3$s, Chips ${3}", this.Id, this.Table.MinimumBet, this.Cash, chipTotal));
        }
    }

    // Returns true, if done playing hand
    public PlayerResult playHand(Dealer dealer) throws Exception
    {
        this.printHand();
        boolean pure = false;
        if (bust())
        {
            System.out.println(String.format("Player %1$s busted!", this.Id));
            return PlayerResult.Fold;
        }

        BlackJackType blackJack = hasBlackJack();
        if (blackJack!=BlackJackType.None)
        {
            if (blackJack==BlackJackType.Pure)
            {
                System.out.println(String.format("Player %1$s has pure BlackJack!", this.Id));
                return PlayerResult.BlackJackPure;
            }
            else
            {
                System.out.println(String.format("Player %1$s has natural BlackJack!", this.Id));
                return PlayerResult.BlackJack;
            }
        }

        List<PlayerOption> options = new ArrayList<PlayerOption>();
        options.add(PlayerOption.Stand);
        //int splitHand = -1;
        if (canSplit())
        {
            //System.out.println("Player%1$s can Split", this.Id);
            options.add(PlayerOption.Split);
        }

        if (canDoubleDown())
        {
            //System.out.println("Player%1$s can DoubleDown", this.Id);
            options.add(PlayerOption.DoubleDown);
        }

        List<BlackJackHand> hitHands = canHit();
        if (hitHands!=null && hitHands.size()>0)
        {
            //System.out.println("Player%1$s can Hit", this.Id);
            options.add(PlayerOption.Hit);
        }

        if (canSurrender())
        {
            //System.out.println("Player%1$s can Surrender", this.Id);
            options.add(PlayerOption.Surrender);
        }

        if (options.size() > 0)
        {
            for (PlayerOption thisOption : options)
            {
                System.out.println(String.format("Player%1$s can %2$s", this.Id, thisOption));
            }
            System.out.println();
            int i = randGen.nextInt(options.size());
            PlayerOption op = options.get(i);
            System.out.println(String.format("Player%1$s chooses %2$s option", this.Id, op));
            switch (op)
            {
                case DoubleDown:
                    return handleDoubleDown(dealer);
                case Hit:
                    return handleHit(dealer, hitHands);
                case Split:
                    return handleSplit(dealer);
                case Surrender:
                    System.out.println(String.format("Player %1$s will surrender!", this.Id));
                    return PlayerResult.Surrender;
                case Stand:
                    System.out.println(String.format("Player %1$s will stand!", this.Id));
                    return PlayerResult.Stand;
                default:
                    throw new Exception(String.format("PlayerOption not handled: %1$s", op));

            }
        }
        System.out.println(String.format("Player %1$s has no options and will resort to Folding!", this.Id));
        return PlayerResult.Fold;
    }
    
    public PlayerResult playHand2(Dealer dealer) throws Exception
    {
        this.printHand();
        boolean pure = false;
        if (bust())
        {
            System.out.println(String.format("Player %1$s busted!", this.Id));
            return PlayerResult.Fold;
        }

        BlackJackType blackJack = hasBlackJack();
        if (blackJack!=BlackJackType.None)
        {
            if (blackJack==BlackJackType.Pure)
            {
                System.out.println(String.format("Player %1$s has pure BlackJack!", this.Id));
                return PlayerResult.BlackJackPure;
            }
            else
            {
                System.out.println(String.format("Player %1$s has natural BlackJack!", this.Id));
                return PlayerResult.BlackJack;
            }
        }

        List<PlayerOption> options = new ArrayList<PlayerOption>();
        options.add(PlayerOption.Stand);
        //int splitHand = -1;
        if (canSplit())
        {
            //System.out.println("Player%1$s can Split", this.Id);
            options.add(PlayerOption.Split);
        }

        if (canDoubleDown())
        {
            //System.out.println("Player%1$s can DoubleDown", this.Id);
            options.add(PlayerOption.DoubleDown);
        }

        List<BlackJackHand> hitHands = canHit();
        if (hitHands!=null && hitHands.size()>0)
        {
            //System.out.println("Player%1$s can Hit", this.Id);
            options.add(PlayerOption.Hit);
        }

        if (canSurrender())
        {
            //System.out.println("Player%1$s can Surrender", this.Id);
            options.add(PlayerOption.Surrender);
        }

        if (options.size() > 0)
        {
        	int i=1;
            for (PlayerOption thisOption : options)
            {
                System.out.println(String.format(i + ") Player%1$s can %2$s", this.Id, thisOption));
                ++i;
            }
            System.out.println();
            /*
            int i = randGen.nextInt(options.size());
            PlayerOption op = options.get(i);
            System.out.println(String.format("Player%1$s chooses %2$s option", this.Id, op));
            */
            
            boolean valid = false;
            int j = 0;
            while(!valid)
            {
	            System.out.println("Choose option [1-" + options.size() +"]: ");
	            java.io.DataInput in = new java.io.DataInputStream(System.in);
	            String optStr = in.readLine().trim();
	            try
	            {
	            	j = Integer.parseInt(optStr);
	            	if(j<1 || j>options.size())
	            	{
	            		System.out.println("Invalid option chosen: '" + optStr + "'");
	            		System.out.println("Choose option [1-" + options.size() +"]: ");
	            	}
	            	else
	            	{
	            		valid = true;
	            	}
	            	
	            }
	            catch(Exception ex)
	            {
	            	System.out.println("Invalid option chosen: '" + optStr + "'");
            		System.out.println("Choose option [1-" + options.size() +"]: ");
	            }	            
            }
            
            PlayerOption op = options.get(j-1);
            System.out.println(String.format("Player%1$s chooses %2$s option", this.Id, op));
            
            switch (op)
            {
                case DoubleDown:
                    return handleDoubleDown2(dealer);
                case Hit:
                    return handleHit2(dealer, hitHands);
                case Split:
                    return handleSplit2(dealer);
                case Surrender:
                    System.out.println(String.format("Player %1$s will surrender!", this.Id));
                    return PlayerResult.Surrender;
                case Stand:
                    System.out.println(String.format("Player %1$s will stand!", this.Id));
                    return PlayerResult.Stand;
                default:
                    throw new Exception(String.format("PlayerOption not handled: %1$s", op));

            }
        }
        System.out.println(String.format("Player %1$s has no options and will resort to Folding!", this.Id));
        return PlayerResult.Fold;
    }

    private PlayerResult handleHit(Dealer dealer, List<BlackJackHand> hitHands) throws Exception
    {
        System.out.println(String.format("Player %1$s will hit!", this.Id));
        int i = randGen.nextInt(hitHands.size());
        //Hit
        this.addCard(hitHands.get(i).Id, dealer.getCard());            
        System.out.println("...and will play again!");
        return this.playHand(dealer);            
    }
    
    private PlayerResult handleHit2(Dealer dealer, List<BlackJackHand> hitHands) throws Exception
    {
        System.out.println(String.format("Player %1$s will hit!", this.Id));
        int i = randGen.nextInt(hitHands.size());
        //Hit
        this.addCard(hitHands.get(i).Id, dealer.getCard());            
        System.out.println("...and will play again!");
        return this.playHand2(dealer);            
    }

    private PlayerResult handleDoubleDown(Dealer dealer) throws Exception
    {
        System.out.println(String.format("Player %1$s will double down!", this.Id));
        //Double down
        int currentBet = this.Table.getBetAmount(this.Id);
        int currentCash = this.Chips.getTotal();
        int amt = currentBet;
        if (currentCash < currentBet)
        {
            amt = currentCash;
        }
        int newBet = 0;
        double percent = 0.0;
        while (newBet == 0)
        {
            percent = (double)(randGen.nextInt(100)+1) / 100.0;
            newBet = (int)Math.ceil(percent * amt);
        }
        System.out.println(String.format("... places new bet $%1$s (%2$s of total)!", newBet, percent * 100));
        ChipStack chipAmt = this.Chips.removeAmount(newBet);
        this.Table.addBet(this.Id, chipAmt);
        System.out.println(String.format("... new total bet $%1$s!", this.Table.getBetAmount(this.Id)));
        this.addCard(dealer.getCard());
        return this.playHand(dealer);
    }
    
    private PlayerResult handleDoubleDown2(Dealer dealer) throws Exception
    {
        System.out.println(String.format("Player %1$s will double down!", this.Id));
        //Double down
        int currentBet = this.Table.getBetAmount(this.Id);
        int chipTotal = this.Chips.getTotal();
        int amt = currentBet;
        if (chipTotal < currentBet)
        {
            amt = chipTotal;
        }

        boolean valid = false;
        double num = 0;
        while(!valid)
        {
        	System.out.println("You have $" + amt + " available, what percentage of this amount do you want to bet? [1-100]: ");
        	DataInput in = new DataInputStream(System.in);
            String numStr = in.readLine().trim();
            try
            {
            	num = Double.parseDouble(numStr);
            	if(num<1 || num>100)
            	{
            		System.out.println("Invalid choice: '" + numStr + "'");
            		System.out.println("Choose a percent value [1-100]: ");
            	}
            	else
            	{
            		valid = true;
            	}            	
            }
            catch(Exception ex)
            {
            	System.out.println("Invalid option chosen: '" + numStr + "'");
        		System.out.println("Choose a percent value [1-100]: ");
            }	            
        }
        double percent = num/100.0;
        int newBet = (int)Math.ceil(percent*amt);
        
        System.out.println(String.format("You have chosen to place an additional bet $%1$s (%2$s% of total)!", newBet, percent * 100));
        ChipStack chipAmt = this.Chips.removeAmount(newBet);
        this.Table.addBet(this.Id, chipAmt);
        System.out.println(String.format("New total bet $%1$s!", this.Table.getBetAmount(this.Id)));
        this.addCard(dealer.getCard());
        return this.playHand2(dealer);
    }

    private PlayerResult handleSplit(Dealer dealer) throws Exception
    {
        BlackJackHand splitHand = this.multipleHands.get(0);
        System.out.println(String.format(
            "Player %1$s will split hand (%2$s,%3$s)!", 
            this.Id, 
            splitHand.get(0).getFaceValue(),
            splitHand.get(1).getFaceValue()));

        BlackJackCard card = splitHand.get(1);
        splitHand.remove(1);
        BlackJackHand newHand = new BlackJackHand(this.multipleHands.size());
        newHand.add(card);
        this.multipleHands.add(newHand);
        System.out.println("...and will play again!");
        return this.playHand(dealer);
    }
    
    private PlayerResult handleSplit2(Dealer dealer) throws Exception
    {
        BlackJackHand splitHand = this.multipleHands.get(0);
        System.out.println(String.format(
            "Player %1$s will split hand (%2$s,%3$s)!", 
            this.Id, 
            splitHand.get(0).getFaceValue(),
            splitHand.get(1).getFaceValue()));
        
        int currentBet = this.Table.getBetAmount(this.Id);        
        ChipStack chipAmt = this.Chips.removeAmount(currentBet);
        this.Table.addChips(this.Id, chipAmt);
        
        System.out.println(String.format(
                "Player %1$s has bet an additional %2$s", 
                this.Id, 
                currentBet));

        BlackJackCard card = splitHand.get(1);
        splitHand.remove(1);
        BlackJackHand newHand = new BlackJackHand(this.multipleHands.size());
        newHand.add(card);
        this.multipleHands.add(newHand);
        System.out.println("...and will play again!");
        return this.playHand2(dealer);
    }

    public boolean canSurrender()
    {
        return (this.multipleHands.size() == 1 && this.multipleHands.get(0).size() == 2);
    }

    public List<BlackJackHand> canHit()
    {
        List<BlackJackHand> theHands=null;
        for (BlackJackHand hand : this.multipleHands)
        {
            if (hand.getTotal() < 21)
            {
            	if(theHands == null)
            	{
            		theHands = new ArrayList<BlackJackHand>();
            	}
                theHands.add(hand);
            }
        }
        return theHands;
    }

    public boolean canDoubleDown()
    {
        if (this.multipleHands.get(0).size() != 2)
        {
            return false;
        }
        return this.multipleHands.size() == 1 && this.Chips.getTotal() > 0;
    }

    public boolean canSplit(/*out int splitHand*/)
    {
        //Assumption: Can only split on single hand containing 2 cards
        if (this.multipleHands.size() > 1 || this.multipleHands.get(0).size() != 2)
        {
            return false;
        }
        else
        {
        	int currentBet = this.Table.getBetAmount(this.Id);
            int chipTotal = this.Chips.getTotal();
            if (chipTotal < currentBet)
            {
                return false;
            }
        }
        return this.multipleHands.size() == 1 && this.multipleHands.get(0).get(0).getValue() == this.multipleHands.get(0).get(1).getValue();
    }

    public void removeEntireBet()
    {
        if (this.Table.getBetAmount(this.Id) > 0)
        {
            ChipStack betStack = this.Table.removeBet(this.Id);
            while (betStack.size() > 0)
            {
                this.Chips.push(betStack.pop());
            }
        }
    }

    public void cashOut()
    {
        this.removeEntireBet();
        this.Cash += this.Chips.getTotal();
        this.Chips.clear();
    }                

    public void printHand()
    {
        System.out.println(String.format("Player %1$s:", this.Id));
        super.printHand();
    }
}
