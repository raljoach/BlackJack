package Games;

import java.util.HashMap;
import java.util.Map;

public enum Chip {
	Purple(500),
	Black(100),
	Green(25),
	Red(5),
	White(1);
	private final int value;
	Chip(int value) { this.value = value; }
	public int value() { return value; } 
	
	private static final Map<Integer, Chip> intToTypeMap = new HashMap<Integer, Chip>();
	static {
	    for (Chip type : Chip.values()) {
	        intToTypeMap.put(type.value, type);
	    }
	}

	public static Chip fromInt(int i) throws Exception{
	    Chip type = intToTypeMap.get(Integer.valueOf(i));
	    if (type == null) 
	    {
	        throw new Exception("Cannot convert int value into Chip enum value! " + i);
	    }
	    return type;
	}
}
