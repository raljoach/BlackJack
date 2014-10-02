package Games;
import java.util.*;

public class ChipStack extends Stack<Chip> {

    private int cashAmount = 0;

    public Chip push(Chip newChip)
    { //Push => Test(Done)
        cashAmount += newChip.value();
        if (this.size() == 0)
        {
            super.push(newChip);
        }
        else
        {
            ChipStack secondStack = new ChipStack();
            while (this.size() > 0 && this.peek().value() < newChip.value())
            {
                secondStack.push(this.pop());
            }
            super.push(newChip);
            while (secondStack.size() > 0)
            {
                this.push(secondStack.pop());
            }
        }
        return newChip;
    }

    public Chip pop()
    { //Pop => Test
        Chip val = super.pop();
        cashAmount -= val.value();
        return val;
    }

    //This will return the combo of chips that are present within current chip stack
    public ChipStack removeAmount(double total) throws Exception
    { //RemoveAmount => Test(Done)

        double amt = total;
        if (this.size() == 0)
        {
            throw new Exception("Error: No chips available!");
        }
        ChipStack myStack = this;
        Stack<Chip> secondStack = new Stack<Chip>();
        ChipStack chips = new ChipStack();
        
        Map<Chip, Integer> chipCount = new HashMap<Chip, Integer>();
        int largestAmt = -1;

        while (myStack.size() > 0)
        {
            Chip current = myStack.pop();
            secondStack.push(current);
            if (!chipCount.containsKey(current))
            {
                chipCount.put(current, 1);
            }
            else
            {
                chipCount.put(current,chipCount.get(current)+1);
            }
        }

        if (amt > 0)
        {
            List<Chip> list = Arrays.asList(Chip.values());
            Collections.sort(list);

            for (int j = list.size() - 1; j >= 0; j--)
            {
                Chip chip = list.get(j);
                int denom = chip.value();

                int numChips = (int)Math.floor(amt / denom);

                if (denom >= total && chipCount.containsKey(chip) && chipCount.get(chip) > 0)
                {
                    largestAmt = denom;
                }

                if (numChips > 0)
                {
                    if (!chipCount.containsKey(chip) || chipCount.get(chip) == 0)
                    {
                        continue;
                    }
                    else
                    {

                        if (chipCount.get(chip) >= numChips)
                        {
                            for (int i = 0; i < numChips; i++)
                            {
                                while (secondStack.size() > 0 && secondStack.peek() != chip)
                                {
                                    myStack.push(secondStack.pop());
                                }
                                if (secondStack.size() == 0)
                                {
                                    throw new Exception("Something went wrong..There should have been chips available on the stack!");
                                }
                                chips.push(secondStack.pop());
                            }
                            amt = amt % denom;

                            if (amt == 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            while (numChips > 0 && numChips > chipCount.get(chip))
                            {
                                --numChips;
                            }

                            for (int i = 0; i < numChips; i++)
                            {
                                while (secondStack.size() > 0 && secondStack.peek() != chip)
                                {
                                    myStack.push(secondStack.pop());
                                }
                                if (secondStack.size() == 0)
                                {
                                    throw new Exception("Something went wrong..There should have been chips available on the stack!");
                                }
                                chips.push(secondStack.pop());
                            }

                            amt = amt - (numChips * chip.value());

                            if (amt == 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        while (secondStack.size() > 0)
        {
            myStack.push(secondStack.pop());
        }

        double diff = total - chips.getTotal();
        if (diff != 0)
        {
            //We don't have enough 
            //and we don't have any unused chips
            if (myStack.size() == 0)
            {
                //So we return the chips to the stack
                while (chips.size() > 0)
                {
                    myStack.push(chips.pop());
                }

                //If we have a single denomination
                //that's bigger than the entire total
                if (largestAmt > 0)
                {
                    //then find that denomination
                    while (myStack.size() > 0 && myStack.peek().value() >= largestAmt)
                    {
                        Chip chip = myStack.pop();
                        if (chip.value() == largestAmt)
                        {
                            //return it in chips
                            chips.push(chip);

                            //and make change
                            int change = chip.value() - (int)amt;
                            ChipStack s = ChipStack.getChips(change);
                            while (s.size()>0)
                            {
                                myStack.push(s.pop());
                            }
                            break;
                        }
                        else
                        {
                            secondStack.push(chip);
                        }
                    }

                    while (secondStack.size() > 0)
                    {
                        myStack.push(secondStack.pop());
                    }
                }
            }
            else
            {
                if (largestAmt < 0)
                {
                    ChipStack addChips = this.removeAmount(diff);
                    while (addChips.size() > 0)
                    {
                        chips.push(addChips.pop());
                    }
                }
                else
                {
                    //If we have a single denomination
                    //that's bigger than the entire total

                    //then find that denomination
                    while (myStack.size() > 0 && myStack.peek().value() >= largestAmt)
                    {
                        Chip chip = myStack.pop();
                        if (chip.value() == largestAmt)
                        {
                            int change = chip.value() - (int)amt;
                            int returned = chip.value() - change;
                            ChipStack extra = ChipStack.getChips(returned);
                            if (extra.getTotal() == returned)
                            {
                               
                                	while(extra.size()>0)
                                	{
	                                	Chip thisChip = extra.pop();
	                                    //return whatever is needed in chips
	                                    chips.push(thisChip);
                                	}
                                
                                	ChipStack s = ChipStack.getChips(change);
                                while (s.size() > 0 )
                                {
                                    //and make change
                                    myStack.push(s.pop());
                                }
                            }
                            break;
                        }
                        else
                        {
                            secondStack.push(chip);
                        }
                    }

                    while (secondStack.size() > 0)
                    {
                        myStack.push(secondStack.pop());
                    }
                }
            }
        }
        return chips;
    }

    public static ChipStack getChips(double amt)
    { //GetChips => Test(Done)
        ChipStack chips = new ChipStack();
        if (amt > 0)
        {
        	List<Chip> list = Arrays.asList(Chip.values());
            Collections.sort(list);
            for (int j = list.size() - 1; j >= 0; j--)
            {
                Chip chip = list.get(j);
                int denom = chip.value();
                int numChips = (int)Math.floor(amt / denom);
                for (int i = 0; i < numChips; i++)
                {
                    chips.push(chip);
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
    
    public static ChipStack getChips(double amt, int denom) throws Exception
    { //GetChips => Test(Done)
        ChipStack chips = new ChipStack();
        if (amt > 0)
        {
        	List<Chip> list = Arrays.asList(Chip.values());
            Collections.sort(list);
            /*for (int j = list.size() - 1; j >= 0; j--)
            {
                Chip chip = list.get(j);
                int denom = chip.value();*/
                Chip chip = Chip.fromInt(denom);
                int numChips = (int)Math.floor(amt / denom);
                for (int i = 0; i < numChips; i++)
                {
                    chips.push(chip);
                }
                
                /*
                amt = amt % denom;

                if (amt == 0)
                {
                    //break;
                }*/
           // }
        }
        return chips;
    }


    public int getTotal()
    { //GetTotal => Test
        return cashAmount;
    }
}