package Games;

public class BlackJackCard extends Card {
	public BlackJackCard(Suite suite, int val) throws Exception {
		super(suite, val);
	}

	public int getActualValue() {		
		int thisVal = super.getValue();
		if (thisVal == Card.Ace) {
			return 11;
		}
		if (thisVal >= Card.Jack && thisVal <= Card.King) {
			return 10;
		}
		return this.val;
	}
}