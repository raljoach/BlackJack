//NOTE: See readme.txt for compile instructions
using System;
using System.Collections.Generic;

namespace Games {

public class A { //BlackJack program
  public A(){
    var b = new B();
  }
  
  public void M1() {  //Main => Test
  }
}

public class B { //Dealer
  public B(){
    //var c = new C();
  }
  
  public void M1() { //Shuffle => Test
  }
}

public class Stack<T> { //Stack
  //Implement
  private List<T> collection = new List<T>();

  public void Push(T val){ //Push => Test
     collection.Add(val);
  }
  
  public T Pop(){ //Pop => Test

     if(!this.IsEmpty) {
         var i = collection.Count - 1;
         var item = collection[i];
         collection.RemoveAt(i);
         return item;
     }
     return default(T);
  }

  public bool IsEmpty
  {
      get { return collection.Count == 0; }
  }
  
  public void M3(){ //Peek => Test
  }

  
}
}