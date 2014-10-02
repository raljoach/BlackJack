package Games;

public class TableCreator {
	public static Table createTable(TableBet tableBet) throws Exception
    { //CreateTable => Test
        PayOut payOut = PayOut.ThreeTwo;
        switch (tableBet)
        {
            case Red: return new RedTable(payOut);
            case Green: return new GreenTable(payOut);
            case Black: return new BlackTable(payOut);
            default: throw new Exception(String.format("Error: Don't know what table to create for %1$s table", tableBet));
        }
    }
}
