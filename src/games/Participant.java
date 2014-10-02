package Games;
import java.util.*;

public abstract class Participant {
    protected List<BlackJackHand> multipleHands = new ArrayList<BlackJackHand>();

    public Participant()
    {
        this.multipleHands.add(new BlackJackHand(0));
    }

    public void addCard(BlackJackCard card)
    {
        BlackJackHand hand = this.multipleHands.get(multipleHands.size() - 1);
        hand.add(card);
    }

    public void addCard(int handNum, BlackJackCard card)
    {
        this.multipleHands.get(handNum).add(card);
    }

    public List<BlackJackCard> getCards()
    {
        List<BlackJackCard> cards = new ArrayList<BlackJackCard>();
        for (int i=0; i<this.multipleHands.size(); i++)
        {
        	BlackJackHand hand = multipleHands.get(i);
            cards.addAll(hand);
        }

        this.multipleHands.clear();
        this.multipleHands.add(new BlackJackHand(0));

        return cards;
    }

    public boolean bust()
    {
        int total = this.getBestHand().getTotal();
        return (total > 21);
    }

    public BlackJackType hasBlackJack()
    {
    	BlackJackType type = BlackJackType.None;
        BlackJackHand theHand = this.getBestHand();
        int total = theHand.getTotal();
        if ( total == 21)
        {
            if (theHand.size() == 2)
            {
                type = BlackJackType.Pure;
            }
            else
            {
                type = BlackJackType.Natural;
            }
        }
        return type;
    }

    //public List<Object> /*int*/ getHandTotal()
    //{
        //BlackJackHand theHand;
    //    return getHandTotal(/*out theHand*/);
    //}

    public BlackJackHand getBestHand()
    {    	
    	BlackJackHand theHand = null;
        int bestHand = 22;
        for (int i = 0; i < this.multipleHands.size(); i++)
        {
            BlackJackHand thisHand = multipleHands.get(i);
            int amt = thisHand.getTotal();
            if (bestHand > 21 || (amt > bestHand && amt <= 21) || (amt == 21 && theHand.getTotal() == 21))
            {
                //bestHand = amt;
                theHand = thisHand;
            }
        }
        
        return theHand;
    }

    public void printHand()
    {
        for (int i = 0; i < this.multipleHands.size(); i++)
        {
            BlackJackHand hand = multipleHands.get(i);
            System.out.println(String.format("Hand%1$s:", i + 1));
            for(int j=0; j<hand.size(); j++)
            {
            	BlackJackCard card = hand.get(j);
                String strVal = Integer.toString(card.getValue());
                if (card.getValue() == Card.Jack)
                {
                    strVal = "Jack";
                }
                else if (card.getValue() == Card.Queen)
                {
                    strVal = "Queen";
                }
                else if (card.getValue() == Card.King)
                {
                    strVal = "King";
                }
                else if (card.getValue() == Card.Ace)
                {
                    strVal = "Ace";
                }
                System.out.println(String.format("(%1$s,%2$s,%3$s)", card.getSuite(), strVal, card.getActualValue()));
            }
            System.out.println("Total: " + hand.getTotal());
            System.out.println();
        }
    }
}