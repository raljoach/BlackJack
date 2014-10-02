package Games;
import java.util.*;

public abstract class Table
{
    private Map<Integer, List<ChipStack>> tableBets = new HashMap<Integer, List<ChipStack>>();

    public Table(TableBet minBet, PayOut payOut)
    { //Table => Test
        this.MinimumBet = minBet.value();
        this.PayOut = payOut;
    }

    public PayOut PayOut;
    public int MinimumBet;

    public void addBet(int playerId, ChipStack bet)
    { // AddBet => Test
        List<ChipStack> bets;
        if (!tableBets.containsKey(playerId))
        {
            bets = new ArrayList<ChipStack>();
            tableBets.put(playerId, bets);
        }
        else
        {
            bets = tableBets.get(playerId);
        }
        bets.add(bet);
    }

    public void addChips(int id, ChipStack newChips)
    { //AddChips => Test
        List<ChipStack> list = tableBets.get(id);
        int i = list.size();
        if (i == 0)
        {
            list.add(newChips);
        }
        else
        {
            ChipStack lastStack = list.get(i - 1);
            for (Chip chip:newChips)
            {
                lastStack.push(chip);
            }
        }
    }

    public int getBetAmount(int id)
    { //GetBetAmount => Test(Done)
        int total = 0;
        if (tableBets.containsKey(id))
        {
            for (ChipStack stack : tableBets.get(id))
            {
                total += stack.getTotal();
            }
        }
        return total;
    }

    public ChipStack removeBet(int id)
    {
        ChipStack result = null;
        for (ChipStack s : tableBets.get(id))
        {
            if(result==null)
            {
                result = s;
            }
            else
            {
                while(s.size()>0)
                {
                    result.push(s.pop());
                }
            }
        }
        tableBets.remove(id);
        return result;
    }

    public ChipStack removeBet(int id, int amount) throws Exception
    {
        int total = 0;
        int rem = amount;
        ChipStack result = null;
        for(ChipStack s : tableBets.get(id))
        {               
            int taken = s.getTotal();
            total += taken;
            if (rem < taken)
            {
                taken = rem;                    
            }
            ChipStack thisStack = s.removeAmount(taken);
            if(result==null)
            {
                result = thisStack;
            }
            else
            {
                while(thisStack.size()>0)
                {
                    result.push(thisStack.pop());
                }
            }
            rem = rem - taken;

            if(rem==0)
            {
                break;
            }
            else if(rem<0)
            {
                throw new Exception(
                    String.format("Error: Player%1$s doesn't have enough money betted to recoup. Player has %2$s, amount to be taken away %3$s",id,total,amount));
            }
        }
        return result;
    }
}
