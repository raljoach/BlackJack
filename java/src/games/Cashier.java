package Games;

public class Cashier {
    public static ChipStack exchange(double cash)
    { //Exchange => Test
        return ChipStack.getChips(cash);
    }
    
    public static ChipStack exchange(double cash, int denom) throws Exception
    { //Exchange => Test
        return ChipStack.getChips(cash, denom);
    }
}