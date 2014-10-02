package GamesTest;

import static org.junit.Assert.*;

import java.util.Random;

import org.junit.Assert;
import org.junit.Test;

import Games.Card;
import Games.Suite;

public class CardTest {

	private Random randGen = new Random();
	
	@Test
	public void constructorTest() throws Exception
    {				
        Suite[] suites = Suite.values();          
        int r1 = randGen.nextInt(14) + 1;
        Suite s = suites[randGen.nextInt(suites.length)];
        Card c = new Card(s, r1);
        Assert.assertEquals(s, c.getSuite());
        Assert.assertEquals(r1, c.getValue());

        boolean exThrown = false;
        try
        {
            c = new Card(s, randGen.nextInt(Integer.MAX_VALUE)+15);
        }
        catch(Exception ex)
        {
            exThrown = true;
        }

        Assert.assertTrue(exThrown);
    }

}
