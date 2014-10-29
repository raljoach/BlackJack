package Games;

public class Card {
	protected Suite suite;
	protected int val;

	public Card(Suite suite, int val) throws Exception { // Card => Test (Done)
		this.setSuite(suite);
		this.setValue(val);
	}

	public static int Ace = 14;
	public static int King = 13; // King => Test(Done)
	public static int Queen = 12; // Queen => Test(Done)
	public static int Jack = 11; // Jack => Test(Done)

	public Suite getSuite() {
		return this.suite;
	}

	public void setSuite(Suite suite) {
		this.suite = suite;
	}

	public int getValue() { // Value => Test (Done)
		return val;
	}

	public void setValue(int newVal) throws Exception {
		if (newVal < 1 || newVal > 14) {
			throw new Exception(String.format("Card value must be between 1-14. Invalid value %1$s",
					newVal));
		}
		this.val = newVal;
	}

	
	public String getFaceValue() throws Exception { // GetFaceValue => Test (Done)
		if (this.val < 11) {
			return Integer.toString(this.val);
		} else if (this.val == 11) {
			return "Jack";
		} else if (this.val == 12) {
			return "Queen";
		} else if (this.val == 13) {
			return "King";
		} else if (this.val == 14) {
			return "Ace";
		}
		throw new Exception("Cannot determine value of card!");
	}
}
