package Games;

import java.util.*;

public enum TableBet {
	None(0),
	Red(5),
	Green(25),
	Black(100);
	private final int value;
	TableBet(int value) { this.value = value; }
	public int value() { return value; } 
	
	private static final Map<Integer, TableBet> intToTypeMap = new HashMap<Integer, TableBet>();
	static {
	    for (TableBet type : TableBet.values()) {
	        intToTypeMap.put(type.value, type);
	    }
	}

	public static TableBet fromInt(int i) throws Exception{
	    TableBet type = intToTypeMap.get(Integer.valueOf(i));
	    if (type == null) 
	    {
	        throw new Exception("Cannot convert int value into TableBet enum value! " + i);
	    }
	    return type;
	}
}
