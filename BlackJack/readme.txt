Compile: 
1. Open Mono Command Prompt
2. mcs blackjack.cs -t:library
   mcs blackjacktest.cs -t:library -lib:blackjack.dll -reference:"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\ReferenceAssemblies\v4.0\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll",blackjack.dll -pkg:dotnet
Test:
1. Open Mono Command Prompt
2. csharp
3. LoadAssembly("blackjack")
   LoadAssembly("C:/Users/ralphjo/Documents/CS Scripts/Experiments/BlackJack/blackjack")
   LoadAssembly("C:/Program Files (x86)/Microsoft Visual Studio 12.0/Common7/IDE/ReferenceAssemblies/v4.0/Microsoft.VisualStudio.QualityTools.UnitTestFramework");
   LoadAssembly("C:/Users/ralphjo/Documents/CS Scripts/Experiments/BlackJack/blackjacktest")
4. B b = new B()
   b.M1()
   using BlackJackGameTest;
   BlackJackGameTest.StackTest t = new BlackJackGameTest.StackTest();
   t.PushTest();
   t.PopTest();
