package Games;
import java.util.*;

public class Dealer extends Participant{
    private Table table = null;
    private Map<Integer,Player> players = null;
    private Stack<BlackJackCard> cardStack = new Stack<BlackJackCard>();

    public Dealer(Table table, List<BlackJackCard> cardDeck, HashMap<Integer,Player> players)
    { //Dealer => Test
        this.table = table;
        this.players = players;
        for (BlackJackCard card : cardDeck)
        {
            cardStack.push(card);
        }
    }
    public DealerMode Mode;
    
    public void shuffleCards()
    { //Shuffle => Test
        
        List<BlackJackCard> cardDeck = new ArrayList<BlackJackCard>();
        while (cardStack.size() > 0)
        {
            cardDeck.add(cardStack.pop());
        }
        int deckLength = cardDeck.size();
        Random randGen = new Random();
        for (int current = 0; current < deckLength - 1; current++)
        {
            int i = current + randGen.nextInt(deckLength-current-1) + 1;
            swap(cardDeck, current, i);
        }
        for (BlackJackCard card : cardDeck)
        {
            cardStack.push(card);
        }

        System.out.println(String.format("Dealer has shuffled card deck of %1$s cards (%2$s decks)", cardStack.size(), deckLength/52));
    }

    public void swap(List<BlackJackCard> a, int i, int j)
    { //Swap => Test
        BlackJackCard tmp = a.get(i);
        a.set(i,a.get(j));
        a.set(j,tmp);
    }

    public void giveCards(int numCards)
    { //GiveCards => Test
        for (int i = 0; i < numCards; i++)
        {
            for (Player p : players.values())
            {
                p.addCard(getCard());
            }
            this.addCard(getCard());
        }

        int stop = (this.Mode == DealerMode.StandOn17) ? 17 : 18;
        while (this.getBestHand().getTotal() < stop)
        {
            this.addCard(getCard());             
        }
    }

    public BlackJackCard getCard()
    {
        return cardStack.pop();
    }

    public void collectCards()
    {
        for (Player p : players.values())
        {
            for (BlackJackCard card : p.getCards())
            {
                cardStack.push(card);                    
            }
        }

        for (BlackJackCard card : this.getCards())
        {
            cardStack.push(card);
        }
    }

    public void printHand()
    {
        System.out.println("Dealer:");
        super.printHand();
    }        
}