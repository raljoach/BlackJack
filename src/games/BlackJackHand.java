package Games;
import java.util.*;

public class BlackJackHand extends ArrayList<BlackJackCard>
{
    public BlackJackHand(int id)
    {
        this.Id = id;
    }
    public int Id;
    
    public int getTotal()
    {
        int total = 0;
        int numAces = 0;
        for (int i=0; i<this.size(); i++)
        {
        	BlackJackCard card = this.get(i);
            if (card.getValue() != Card.Ace)
            {
                total += card.getActualValue();
            }
            else
            {
                numAces++;
            }
        }

        int diff = 21 - total;
        int[] theAces = new int[numAces];
        int totalAces = numAces;
        while (numAces > 0)
        {
            int current = totalAces - numAces;
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