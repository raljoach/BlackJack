using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;

namespace GamesTest {
[TestClass]
public class StackTest
{
    [TestMethod]
    public void PushTest() {
        Stack<int> a = new Stack<int>();
        a.Push(4);
        Assert.AreEqual(4,a.Pop());
    }

    [TestMethod]
    public void PopTest() {
	    Stack<int> s = new Stack<int>();
	    s.Push(8);
	    s.Push(9);
	    s.Push(6);
        Assert.IsFalse(s.IsEmpty);
	    var v1 = s.Pop();
        Assert.IsFalse(s.IsEmpty);
	    Assert.AreEqual(6,v1);
        Assert.IsFalse(s.IsEmpty);
	    var v2 = s.Pop();
        Assert.IsFalse(s.IsEmpty);
	    Assert.AreEqual(9,v2);
	    var v3 = s.Pop();
	    Assert.AreEqual(8,v3);
        Assert.IsTrue(s.IsEmpty);
    }
}
}